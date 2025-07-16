using BusinessObject.Entities;

namespace DataAccess.Repositories.Interface
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAll();
        Product? GetById(int id);
        void Add(Product product);
        void Update(Product product);
        void Delete(Product product);
        void SaveChanges();
    }
}
