using BusinessObject.DTOs;

namespace DataAccess.Services
{
    public interface ICategoryService
    {
        IEnumerable<CategoryDto> GetCategories();
        CategoryDto? GetCategory(int id);
        void CreateCategory(CategoryDto dto);
        void UpdateCategory(CategoryDto dto);
        void DeleteCategory(int id);
    }
}
