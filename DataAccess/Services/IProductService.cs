using BusinessObject.DTOs;

namespace DataAccess.Services
{
    public interface IProductService
    {
        IEnumerable<ProductDto> GetProducts();
        ProductDto? GetProduct(int id);
        void CreateProduct(ProductDto dto);
        void UpdateProduct(ProductDto dto);
        void DeleteProduct(int id);
    }
}
