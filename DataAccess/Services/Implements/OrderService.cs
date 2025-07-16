using BusinessObject.DTOs;
using BusinessObject.Models;
using DataAccess.Repositories.Interface;
using DataAccess.Services.Interface;

namespace DataAccess.Services.Implements
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _repository;
        private const int PageSize = 5;

        public OrderService(IOrderRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<OrderDto> GetOrders(int page) =>
            _repository.GetPaged(page, PageSize).Select(ToDto);

        public IEnumerable<OrderDto> GetOrdersForMember(int memberId, int page) =>
            _repository.GetByMemberIdPaged(memberId, page, PageSize).Select(ToDto);

        public OrderDto? GetOrder(int id)
        {
            var order = _repository.GetById(id);
            return order == null ? null : ToDto(order);
        }

        public bool CreateOrder(OrderDto dto)
        {
            if (!_repository.MemberExists(dto.MemberId)) return false;
            var order = new Order
            {
                MemberId = dto.MemberId,
                OrderDate = dto.OrderDate,
                RequiredDate = dto.RequiredDate,
                ShippedDate = dto.ShippedDate,
                Freight = dto.Freight
            };
            _repository.Add(order);
            _repository.SaveChanges();
            dto.OrderId = order.OrderId;
            return true;
        }

        public bool UpdateOrder(OrderDto dto)
        {
            if (!_repository.MemberExists(dto.MemberId)) return false;
            var order = _repository.GetById(dto.OrderId);
            if (order == null) return false;
            order.MemberId = dto.MemberId;
            order.OrderDate = dto.OrderDate;
            order.RequiredDate = dto.RequiredDate;
            order.ShippedDate = dto.ShippedDate;
            order.Freight = dto.Freight;
            _repository.Update(order);
            _repository.SaveChanges();
            return true;
        }

        public bool DeleteOrder(int id)
        {
            if (_repository.HasDetails(id)) return false;
            var order = _repository.GetById(id);
            if (order == null) return false;
            _repository.Delete(order);
            _repository.SaveChanges();
            return true;
        }

        public IEnumerable<SalesReportDto> GetSalesReport(DateTime startDate, DateTime endDate) =>
            _repository.GetSalesReport(startDate, endDate);

        private static OrderDto ToDto(Order order) => new()
        {
            OrderId = order.OrderId,
            MemberId = order.MemberId,
            OrderDate = order.OrderDate,
            RequiredDate = order.RequiredDate,
            ShippedDate = order.ShippedDate,
            Freight = order.Freight
        };
    }
}
