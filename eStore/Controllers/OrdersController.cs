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
    public ActionResult<IEnumerable<OrderDto>> Get([FromQuery] int page = 1, [FromQuery] int pageSize = 10) =>
        Ok(_service.GetOrders(page, pageSize));

    [HttpGet("{id}")]
    public ActionResult<OrderDto> Get(int id)
    {
        var dto = _service.GetOrder(id);
        return dto == null ? NotFound() : Ok(dto);
    }

    [HttpGet("member/{memberId}")]
    public ActionResult<IEnumerable<OrderDto>> GetByMember(int memberId, [FromQuery] int page = 1, [FromQuery] int pageSize = 10) =>
        Ok(_service.GetOrdersForMember(memberId, page, pageSize));

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
