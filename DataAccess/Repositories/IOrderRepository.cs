using BusinessObject;

namespace DataAccess.Repositories
{
    public interface IOrderRepository
    {
        IEnumerable<Order> GetAll();
        Order? GetById(int id);
        void Add(Order order);
        void Update(Order order);
        void Delete(Order order);
        bool HasDetails(int orderId);
        bool MemberExists(int memberId);
        IEnumerable<BusinessObject.DTOs.SalesReportDto> GetSalesReport(DateTime startDate, DateTime endDate);
        void SaveChanges();
    }
}
