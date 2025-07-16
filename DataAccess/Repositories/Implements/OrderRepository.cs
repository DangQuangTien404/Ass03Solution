using BusinessObject;
using DataAccess.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.Implements
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

        public IEnumerable<Order> GetPaged(int page, int pageSize) =>
            _context.Orders.AsNoTracking()
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

        public Order? GetById(int id) =>
            _context.Orders.Find(id);

        public void Add(Order order) => _context.Orders.Add(order);

        public void Update(Order order) => _context.Orders.Update(order);

        public void Delete(Order order) => _context.Orders.Remove(order);

        public bool HasDetails(int orderId) =>
            _context.OrderDetails.AsNoTracking().Any(od => od.OrderId == orderId);

        public bool MemberExists(int memberId) =>
            _context.Members.AsNoTracking().Any(m => m.MemberId == memberId);

        public IEnumerable<Order> GetByMemberId(int memberId) =>
            _context.Orders
                .AsNoTracking()
                .Where(o => o.MemberId == memberId)
                .ToList();

        public IEnumerable<Order> GetByMemberIdPaged(int memberId, int page, int pageSize) =>
            _context.Orders
                .AsNoTracking()
                .Where(o => o.MemberId == memberId)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

        public IEnumerable<BusinessObject.DTOs.SalesReportDto> GetSalesReport(DateTime startDate, DateTime endDate) =>
            _context.Orders
                .AsNoTracking()
                .Include(o => o.OrderDetails)
                .Where(o => o.OrderDate >= startDate && o.OrderDate <= endDate)
                .Select(o => new BusinessObject.DTOs.SalesReportDto
                {
                    OrderId = o.OrderId,
                    MemberId = o.MemberId,
                    OrderDate = o.OrderDate,
                    Total = o.OrderDetails.Sum(d => d.UnitPrice * d.Quantity * (1 - (decimal)d.Discount))
                })
                .OrderByDescending(r => r.Total)
                .ToList();

        public void SaveChanges() => _context.SaveChanges();
    }
}
