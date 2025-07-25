using BusinessObject.DTOs;

namespace DataAccess.Services.Interface
{
    public interface ICategoryService
    {
        IEnumerable<CategoryDto> GetCategories(int page);
        CategoryDto? GetCategory(int id);
        void CreateCategory(CategoryDto dto);
        void UpdateCategory(CategoryDto dto);
        bool DeleteCategory(int id);
    }
}
