using BusinessObject.DTOs;

namespace DataAccess.Services.Interface
{
    public interface IOrderService
    {
        IEnumerable<OrderDto> GetOrders();
        IEnumerable<OrderDto> GetOrders(int page, int pageSize);
        OrderDto? GetOrder(int id);
        bool CreateOrder(OrderDto dto);
        bool UpdateOrder(OrderDto dto);
        bool DeleteOrder(int id);
        IEnumerable<OrderDto> GetOrdersForMember(int memberId);
        IEnumerable<OrderDto> GetOrdersForMember(int memberId, int page, int pageSize);
        IEnumerable<SalesReportDto> GetSalesReport(DateTime startDate, DateTime endDate);
    }
}
