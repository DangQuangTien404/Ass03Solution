using BusinessObject.DTOs;
using DataAccess.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace eStore.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderDetailsController : ControllerBase
{
    private readonly IOrderDetailService _service;

    public OrderDetailsController(IOrderDetailService service)
    {
        _service = service;
    }

    [HttpGet("{orderId}")]
    public ActionResult<IEnumerable<OrderDetailDto>> Get(int orderId, [FromQuery] int page = 1) =>
        Ok(_service.GetDetails(orderId, page));

    [HttpPost]
    public IActionResult Post(OrderDetailDto dto)
    {
        if (!_service.CreateDetail(dto)) return BadRequest();
        return Ok();
    }

    [HttpPut]
    public IActionResult Put(OrderDetailDto dto)
    {
        return _service.UpdateDetail(dto) ? Ok() : BadRequest();
    }

    [HttpDelete("{orderId}/{productId}")]
    public IActionResult Delete(int orderId, int productId)
    {
        return _service.DeleteDetail(orderId, productId) ? Ok() : NotFound();
    }

}

