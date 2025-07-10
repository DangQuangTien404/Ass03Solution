using BusinessObject.DTOs;

namespace DataAccess.Services
{
    public interface ICategoryService
    {
        IEnumerable<CategoryDto> GetCategories();
    }
}
