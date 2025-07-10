using BusinessObject.DTOs;
using DataAccess.Repositories;

namespace DataAccess.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;

        public CategoryService(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<CategoryDto> GetCategories() =>
            _repository.GetAll().Select(c => new CategoryDto
            {
                CategoryId = c.CategoryId,
                CategoryName = c.CategoryName
            });
    }
}
