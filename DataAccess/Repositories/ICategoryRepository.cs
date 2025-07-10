using BusinessObject;

namespace DataAccess.Repositories
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetAll();
    }
}
