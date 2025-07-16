using BusinessObject.DTOs;

namespace DataAccess.Services.Interface
{
    public interface IOrderDetailService
    {
        IEnumerable<OrderDetailDto> GetDetails(int orderId, int page);
        bool CreateDetail(OrderDetailDto dto);
        bool UpdateDetail(OrderDetailDto dto);
        bool DeleteDetail(int orderId, int productId);
    }
}
