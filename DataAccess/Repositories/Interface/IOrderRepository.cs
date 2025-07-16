using BusinessObject.Models;

namespace DataAccess.Repositories.Interface
{
    public interface IOrderRepository
    {
        IEnumerable<Order> GetAll();
        IEnumerable<Order> GetPaged(int page, int pageSize);
        Order? GetById(int id);
        void Add(Order order);
        void Update(Order order);
        void Delete(Order order);
        bool HasDetails(int orderId);
        bool MemberExists(int memberId);
        IEnumerable<Order> GetByMemberId(int memberId);
        IEnumerable<Order> GetByMemberIdPaged(int memberId, int page, int pageSize);
        IEnumerable<BusinessObject.DTOs.SalesReportDto> GetSalesReport(DateTime startDate, DateTime endDate);
        void SaveChanges();
    }
}
