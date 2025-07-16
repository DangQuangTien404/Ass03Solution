using BusinessObject.DTOs;
using DataAccess.Services.Interface;
using eStore.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Mvc;

namespace eStore.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _service;
    private readonly IHubContext<OrderHub>? _hub;

    public OrdersController(IOrderService service, IHubContext<OrderHub>? hub = null)
    {
        _service = service;
        _hub = hub;
    }

    [HttpGet]
    public ActionResult<IEnumerable<OrderDto>> GetPaged([FromQuery] int page = 1) =>
        Ok(_service.GetOrders(page));

    [HttpGet("{id}")]
    public ActionResult<OrderDto> Get(int id)
    {
        var dto = _service.GetOrder(id);
        return dto == null ? NotFound() : Ok(dto);
    }

    [HttpGet("member/{memberId}")]
    public ActionResult<IEnumerable<OrderDto>> GetByMember(int memberId, [FromQuery] int page = 1) =>
        Ok(_service.GetOrdersForMember(memberId, page));

    [HttpPost]
    public IActionResult Post(OrderDto dto)
    {
        if (!_service.CreateOrder(dto)) return BadRequest();
        _hub?.Clients.All.SendAsync("OrderCreated", dto);
        return CreatedAtAction(nameof(Get), new { id = dto.OrderId }, dto);
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, OrderDto dto)
    {
        if (id != dto.OrderId) return BadRequest();
        if (!_service.UpdateOrder(dto)) return BadRequest();
        _hub?.Clients.All.SendAsync("OrderUpdated", dto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        if (!_service.DeleteOrder(id)) return BadRequest();
        _hub?.Clients.All.SendAsync("OrderDeleted", id);
        return NoContent();
    }

}

