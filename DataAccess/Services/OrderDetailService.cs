using BusinessObject;
using BusinessObject.DTOs;
using DataAccess.Repositories;

namespace DataAccess.Services
{
    public class OrderDetailService : IOrderDetailService
    {
        private readonly IOrderDetailRepository _repository;
        public OrderDetailService(IOrderDetailRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<OrderDetailDto> GetDetails(int orderId) =>
            _repository.GetByOrder(orderId).Select(ToDto);

        public bool CreateDetail(OrderDetailDto dto)
        {
            if (!_repository.OrderExists(dto.OrderId) || !_repository.ProductExists(dto.ProductId))
                return false;
            if (dto.Quantity < 1 || dto.Discount < 0 || dto.Discount > 100)
                return false;
            var detail = new OrderDetail
            {
                OrderId = dto.OrderId,
                ProductId = dto.ProductId,
                UnitPrice = _repository.GetProductPrice(dto.ProductId) ?? dto.UnitPrice,
                Quantity = dto.Quantity,
                Discount = dto.Discount / 100
            };
            _repository.Add(detail);
            _repository.SaveChanges();
            return true;
        }

        public bool UpdateDetail(OrderDetailDto dto)
        {
            var detail = _repository.Get(dto.OrderId, dto.ProductId);
            if (detail == null) return false;
            if (dto.Quantity < 1 || dto.Discount < 0 || dto.Discount > 100)
                return false;
            detail.Quantity = dto.Quantity;
            detail.Discount = dto.Discount / 100;
            _repository.Update(detail);
            _repository.SaveChanges();
            return true;
        }

        public bool DeleteDetail(int orderId, int productId)
        {
            var detail = _repository.Get(orderId, productId);
            if (detail == null) return false;
            _repository.Delete(detail);
            _repository.SaveChanges();
            return true;
        }

        private OrderDetailDto ToDto(OrderDetail detail) => new()
        {
            OrderId = detail.OrderId,
            ProductId = detail.ProductId,
            ProductName = detail.Product?.ProductName ?? _repository.GetProductName(detail.ProductId) ?? string.Empty,
            UnitPrice = detail.UnitPrice,
            Quantity = detail.Quantity,
            Discount = detail.Discount * 100
        };
    }
}
