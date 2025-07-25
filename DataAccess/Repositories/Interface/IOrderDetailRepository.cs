using BusinessObject.Models;

namespace DataAccess.Repositories.Interface
{
    public interface IOrderDetailRepository
    {
        IEnumerable<OrderDetail> GetByOrder(int orderId);
        IEnumerable<OrderDetail> GetByOrderPaged(int orderId, int page, int pageSize);
        OrderDetail? Get(int orderId, int productId);
        void Add(OrderDetail detail);
        void Update(OrderDetail detail);
        void Delete(OrderDetail detail);
        bool OrderExists(int orderId);
        bool ProductExists(int productId);
        decimal? GetProductPrice(int productId);
        string? GetProductName(int productId);
        void SaveChanges();
    }
}
