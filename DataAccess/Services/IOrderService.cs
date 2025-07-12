using BusinessObject.DTOs;

namespace DataAccess.Services
{
    public interface IOrderService
    {
        IEnumerable<OrderDto> GetOrders();
        OrderDto? GetOrder(int id);
        bool CreateOrder(OrderDto dto);
        bool UpdateOrder(OrderDto dto);
        bool DeleteOrder(int id);
        IEnumerable<SalesReportDto> GetSalesReport(DateTime startDate, DateTime endDate);
    }
}
