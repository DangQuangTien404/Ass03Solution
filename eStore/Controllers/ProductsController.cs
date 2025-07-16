using BusinessObject.DTOs;
using DataAccess.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace eStore.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _service;

    public ProductsController(IProductService service)
    {
        _service = service;
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
        return CreatedAtAction(nameof(Get), new { id = dto.ProductId }, dto);
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, ProductDto dto)
    {
        if (id != dto.ProductId) return BadRequest();
        _service.UpdateProduct(dto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        _service.DeleteProduct(id);
        return NoContent();
    }
}
