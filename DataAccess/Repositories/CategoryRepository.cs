using BusinessObject;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _context;

        public CategoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Category> GetAll() => _context.Categories.AsNoTracking().ToList();

        public Category? GetById(int id) => _context.Categories.Find(id);

        public void Add(Category category) => _context.Categories.Add(category);

        public void Update(Category category) => _context.Categories.Update(category);

        public void Delete(Category category) => _context.Categories.Remove(category);

        public void SaveChanges() => _context.SaveChanges();
    }
}
