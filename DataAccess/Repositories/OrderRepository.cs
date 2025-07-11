using BusinessObject;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;

        public OrderRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Order> GetAll() =>
            _context.Orders.AsNoTracking().ToList();

        public Order? GetById(int id) =>
            _context.Orders.Find(id);

        public void Add(Order order) => _context.Orders.Add(order);

        public void Update(Order order) => _context.Orders.Update(order);

        public void Delete(Order order) => _context.Orders.Remove(order);

        public bool HasDetails(int orderId) =>
            _context.OrderDetails.AsNoTracking().Any(od => od.OrderId == orderId);

        public bool MemberExists(int memberId) =>
            _context.Members.AsNoTracking().Any(m => m.MemberId == memberId);

        public void SaveChanges() => _context.SaveChanges();
    }
}
