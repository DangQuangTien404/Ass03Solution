using BusinessObject.Models;

namespace DataAccess.Repositories.Interface
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetAll();
        IEnumerable<Category> GetPaged(int page, int pageSize);
        Category? GetById(int id);
        void Add(Category category);
        void Update(Category category);
        void Delete(Category category);
        bool IsInUse(int categoryId);
        void SaveChanges();
    }
}
