using BusinessObject.DTOs;
using BusinessObject.Models;
using DataAccess.Repositories.Interface;
using DataAccess.Services.Interface;

namespace DataAccess.Services.Implements
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;
        private const int PageSize = 5;

        public CategoryService(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<CategoryDto> GetCategories(int page) =>
            _repository.GetPaged(page, PageSize).Select(ToDto);

        public CategoryDto? GetCategory(int id)
        {
            var category = _repository.GetById(id);
            return category == null ? null : ToDto(category);
        }

        public void CreateCategory(CategoryDto dto)
        {
            var category = new Category
            {
                CategoryName = dto.CategoryName,
                Description = dto.Description
            };
            _repository.Add(category);
            _repository.SaveChanges();
            dto.CategoryId = category.CategoryId;
        }

        public void UpdateCategory(CategoryDto dto)
        {
            var category = _repository.GetById(dto.CategoryId);
            if (category == null) return;
            category.CategoryName = dto.CategoryName;
            category.Description = dto.Description;
            _repository.Update(category);
            _repository.SaveChanges();
        }

        public bool DeleteCategory(int id)
        {
            if (_repository.IsInUse(id)) return false;
            var category = _repository.GetById(id);
            if (category == null) return false;
            _repository.Delete(category);
            _repository.SaveChanges();
            return true;
        }

        private static CategoryDto ToDto(Category c) => new()
        {
            CategoryId = c.CategoryId,
            CategoryName = c.CategoryName,
            Description = c.Description
        };
    }
}
