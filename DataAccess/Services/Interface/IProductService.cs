using BusinessObject.DTOs;

namespace DataAccess.Services.Interface
{
    public interface IProductService
    {
        IEnumerable<ProductDto> GetProducts();
        IEnumerable<ProductDto> GetProducts(int page, int pageSize);
        ProductDto? GetProduct(int id);
        void CreateProduct(ProductDto dto);
        void UpdateProduct(ProductDto dto);
        void DeleteProduct(int id);
    }
}
