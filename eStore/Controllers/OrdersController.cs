using BusinessObject.DTOs;
using DataAccess.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace eStore.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _service;

    public OrdersController(IOrderService service)
    {
        _service = service;
    }

    [HttpGet]
    public ActionResult<IEnumerable<OrderDto>> Get() => Ok(_service.GetOrders());

    [HttpGet("{id}")]
    public ActionResult<OrderDto> Get(int id)
    {
        var dto = _service.GetOrder(id);
        return dto == null ? NotFound() : Ok(dto);
    }

    [HttpGet("member/{memberId}")]
    public ActionResult<IEnumerable<OrderDto>> GetByMember(int memberId) => Ok(_service.GetOrdersForMember(memberId));

    [HttpPost]
    public IActionResult Post(OrderDto dto)
    {
        return _service.CreateOrder(dto) ? CreatedAtAction(nameof(Get), new { id = dto.OrderId }, dto) : BadRequest();
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, OrderDto dto)
    {
        if (id != dto.OrderId) return BadRequest();
        return _service.UpdateOrder(dto) ? NoContent() : BadRequest();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        return _service.DeleteOrder(id) ? NoContent() : BadRequest();
    }
}
