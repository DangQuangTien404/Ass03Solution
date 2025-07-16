using BusinessObject;
using BusinessObject.DTOs;
using DataAccess.Hubs;
using Microsoft.AspNetCore.SignalR;
using DataAccess.Repositories.Interface;
using DataAccess.Services.Interface;

namespace DataAccess.Services.Implements
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly IHubContext<ProductHub>? _hub;
        public ProductService(IProductRepository repository, IHubContext<ProductHub>? hub = null)
        {
            _repository = repository;
            _hub = hub;
        }

        public IEnumerable<ProductDto> GetProducts() =>
            _repository.GetAll().Select(ToDto);

        public IEnumerable<ProductDto> GetProducts(int page, int pageSize) =>
            _repository.GetPaged(page, pageSize).Select(ToDto);

        public ProductDto? GetProduct(int id)
        {
            var product = _repository.GetById(id);
            return product == null ? null : ToDto(product);
        }

        public void CreateProduct(ProductDto dto)
        {
            var product = new Product
            {
                ProductName = dto.ProductName,
                CategoryId = dto.CategoryId,
                Weight = dto.Weight,
                UnitPrice = dto.UnitPrice,
                UnitsInStock = dto.UnitsInStock
            };
            _repository.Add(product);
            _repository.SaveChanges();
            dto.ProductId = product.ProductId;
            _hub?.Clients.All.SendAsync("ProductCreated", ToDto(product));
        }

        public void UpdateProduct(ProductDto dto)
        {
            var product = _repository.GetById(dto.ProductId);
            if (product == null) return;
            product.ProductName = dto.ProductName;
            product.CategoryId = dto.CategoryId;
            product.Weight = dto.Weight;
            product.UnitPrice = dto.UnitPrice;
            product.UnitsInStock = dto.UnitsInStock;
            _repository.Update(product);
            _repository.SaveChanges();
            _hub?.Clients.All.SendAsync("ProductUpdated", ToDto(product));
        }

        public void DeleteProduct(int id)
        {
            var product = _repository.GetById(id);
            if (product == null) return;
            _repository.Delete(product);
            _repository.SaveChanges();
            _hub?.Clients.All.SendAsync("ProductDeleted", id);
        }

        private static ProductDto ToDto(Product product) => new()
        {
            ProductId = product.ProductId,
            CategoryId = product.CategoryId,
            ProductName = product.ProductName,
            Weight = product.Weight,
            UnitPrice = product.UnitPrice,
            UnitsInStock = product.UnitsInStock
        };
    }
}
