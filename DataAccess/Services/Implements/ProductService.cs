using BusinessObject.DTOs;
using BusinessObject.Models;
using DataAccess.Repositories.Interface;
using DataAccess.Services.Interface;

namespace DataAccess.Services.Implements
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private const int PageSize = 5;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<ProductDto> GetProducts(int page) =>
            _repository.GetPaged(page, PageSize).Select(ToDto);

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
        }

        public void DeleteProduct(int id)
        {
            var product = _repository.GetById(id);
            if (product == null) return;
            _repository.Delete(product);
            _repository.SaveChanges();
        }

        private static ProductDto ToDto(Product product) => new()
        {
            ProductId = product.ProductId,
            CategoryId = product.CategoryId,
            ProductName = product.ProductName,
            Weight = product.Weight,
            UnitPrice = product.UnitPrice,
            UnitsInStock = product.UnitsInStock ?? 0
        };
    }
}
