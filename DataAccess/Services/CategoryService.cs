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
            _repository.GetAll().Select(ToDto);

        public CategoryDto? GetCategory(int id)
        {
            var category = _repository.GetById(id);
            return category == null ? null : ToDto(category);
        }

        public void CreateCategory(CategoryDto dto)
        {
            var category = new BusinessObject.Category
            {
                CategoryName = dto.CategoryName
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
            _repository.Update(category);
            _repository.SaveChanges();
        }

        public void DeleteCategory(int id)
        {
            var category = _repository.GetById(id);
            if (category == null) return;
            _repository.Delete(category);
            _repository.SaveChanges();
        }

        private static CategoryDto ToDto(BusinessObject.Category c) => new()
        {
            CategoryId = c.CategoryId,
            CategoryName = c.CategoryName
        };
    }
}
