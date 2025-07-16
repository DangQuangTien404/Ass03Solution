using BusinessObject;

namespace DataAccess.Repositories.Interface
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAll();
        IEnumerable<Product> GetPaged(int page, int pageSize);
        Product? GetById(int id);
        void Add(Product product);
        void Update(Product product);
        void Delete(Product product);
        void SaveChanges();
    }
}
