using BusinessObject.DTOs;
using DataAccess.Repositories;
using DataAccess.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace DataAccess.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;
        private readonly IHubContext<CategoryHub>? _hub;

        public CategoryService(ICategoryRepository repository, IHubContext<CategoryHub>? hub = null)
        {
            _repository = repository;
            _hub = hub;
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
                CategoryName = dto.CategoryName,
                Description = dto.Description
            };
            _repository.Add(category);
            _repository.SaveChanges();
            dto.CategoryId = category.CategoryId;
            _hub?.Clients.All.SendAsync("CategoryCreated", ToDto(category));
        }

        public void UpdateCategory(CategoryDto dto)
        {
            var category = _repository.GetById(dto.CategoryId);
            if (category == null) return;
            category.CategoryName = dto.CategoryName;
            category.Description = dto.Description;
            _repository.Update(category);
            _repository.SaveChanges();
            _hub?.Clients.All.SendAsync("CategoryUpdated", ToDto(category));
        }

        public bool DeleteCategory(int id)
        {
            if (_repository.IsInUse(id)) return false;
            var category = _repository.GetById(id);
            if (category == null) return false;
            _repository.Delete(category);
            _repository.SaveChanges();
            _hub?.Clients.All.SendAsync("CategoryDeleted", id);
            return true;
        }

        private static CategoryDto ToDto(BusinessObject.Category c) => new()
        {
            CategoryId = c.CategoryId,
            CategoryName = c.CategoryName,
            Description = c.Description
        };
    }
}
