using BusinessObject.DTOs;
using DataAccess.Services.Interface;
using eStore.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Mvc;

namespace eStore.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _service;
    private readonly IHubContext<ProductHub>? _hub;

    public ProductsController(IProductService service, IHubContext<ProductHub>? hub = null)
    {
        _service = service;
        _hub = hub;
    }

    [HttpGet]
    public ActionResult<IEnumerable<ProductDto>> GetPaged([FromQuery] int page = 1) =>
        Ok(_service.GetProducts(page));

    [HttpGet("{id}")]
    public ActionResult<ProductDto> Get(int id)
    {
        var dto = _service.GetProduct(id);
        return dto == null ? NotFound() : Ok(dto);
    }

    [HttpPost]
    public IActionResult Post(ProductDto dto)
    {
        _service.CreateProduct(dto);
        _hub?.Clients.All.SendAsync("ProductCreated", dto);
        return CreatedAtAction(nameof(Get), new { id = dto.ProductId }, dto);
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, ProductDto dto)
    {
        if (id != dto.ProductId) return BadRequest();
        var existing = _service.GetProduct(id);
        if (existing == null) return NotFound();
        _service.UpdateProduct(dto);
        _hub?.Clients.All.SendAsync("ProductUpdated", dto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        if (_service.GetProduct(id) == null) return NotFound();
        _service.DeleteProduct(id);
        _hub?.Clients.All.SendAsync("ProductDeleted", id);
        return NoContent();
    }

}

