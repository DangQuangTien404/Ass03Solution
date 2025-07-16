using BusinessObject.DTOs;

namespace DataAccess.Services.Interface
{
    public interface IOrderService
    {
        IEnumerable<OrderDto> GetOrders(int page);
        IEnumerable<OrderDto> GetOrdersForMember(int memberId, int page);
        OrderDto? GetOrder(int id);
        bool CreateOrder(OrderDto dto);
        bool UpdateOrder(OrderDto dto);
        bool DeleteOrder(int id);
        IEnumerable<SalesReportDto> GetSalesReport(DateTime startDate, DateTime endDate);
    }
}
