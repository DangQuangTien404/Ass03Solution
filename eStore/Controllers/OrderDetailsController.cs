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
    public ActionResult<IEnumerable<OrderDetailDto>> Get(int orderId) => Ok(_service.GetDetails(orderId));

    [HttpPost]
    public IActionResult Post(OrderDetailDto dto)
    {
        return _service.CreateDetail(dto) ? Ok() : BadRequest();
    }

    [HttpPut]
    public IActionResult Put(OrderDetailDto dto)
    {
        return _service.UpdateDetail(dto) ? Ok() : BadRequest();
    }

    [HttpDelete("{orderId}/{productId}")]
    public IActionResult Delete(int orderId, int productId)
    {
        return _service.DeleteDetail(orderId, productId) ? Ok() : BadRequest();
    }
}
