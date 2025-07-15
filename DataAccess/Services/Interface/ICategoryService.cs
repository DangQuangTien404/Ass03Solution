using BusinessObject.DTOs;

namespace DataAccess.Services.Interface
{
    public interface ICategoryService
    {
        IEnumerable<CategoryDto> GetCategories();
        CategoryDto? GetCategory(int id);
        void CreateCategory(CategoryDto dto);
        void UpdateCategory(CategoryDto dto);
        bool DeleteCategory(int id);
    }
}
