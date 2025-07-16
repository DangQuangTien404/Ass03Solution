using BusinessObject;
using DataAccess.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.Implements
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        private readonly AppDbContext _context;
        public OrderDetailRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<OrderDetail> GetByOrder(int orderId) =>
            _context.OrderDetails.Include(od => od.Product)
                .AsNoTracking()
                .Where(od => od.OrderId == orderId)
                .ToList();

        public IEnumerable<OrderDetail> GetByOrderPaged(int orderId, int page, int pageSize) =>
            _context.OrderDetails.Include(od => od.Product)
                .AsNoTracking()
                .Where(od => od.OrderId == orderId)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

        public OrderDetail? Get(int orderId, int productId) =>
            _context.OrderDetails.Find(orderId, productId);

        public void Add(OrderDetail detail) => _context.OrderDetails.Add(detail);

        public void Update(OrderDetail detail) => _context.OrderDetails.Update(detail);

        public void Delete(OrderDetail detail) => _context.OrderDetails.Remove(detail);

        public bool OrderExists(int orderId) =>
            _context.Orders.AsNoTracking().Any(o => o.OrderId == orderId);

        public bool ProductExists(int productId) =>
            _context.Products.AsNoTracking().Any(p => p.ProductId == productId);

        public decimal? GetProductPrice(int productId) =>
            _context.Products.AsNoTracking()
                .Where(p => p.ProductId == productId)
                .Select(p => p.UnitPrice)
                .FirstOrDefault();

        public string? GetProductName(int productId) =>
            _context.Products.AsNoTracking()
                .Where(p => p.ProductId == productId)
                .Select(p => p.ProductName)
                .FirstOrDefault();

        public void SaveChanges() => _context.SaveChanges();
    }
}
