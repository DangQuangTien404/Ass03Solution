using BusinessObject.DTOs;
using DataAccess.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace eStore.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _service;

    public CategoriesController(ICategoryService service)
    {
        _service = service;
    }

    [HttpGet]
    public ActionResult<IEnumerable<CategoryDto>> GetPaged([FromQuery] int page = 1) =>
        Ok(_service.GetCategories(page));

    [HttpGet("{id}")]
    public ActionResult<CategoryDto> Get(int id)
    {
        var dto = _service.GetCategory(id);
        return dto == null ? NotFound() : Ok(dto);
    }

    [HttpPost]
    public IActionResult Post(CategoryDto dto)
    {
        _service.CreateCategory(dto);
        return CreatedAtAction(nameof(Get), new { id = dto.CategoryId }, dto);
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, CategoryDto dto)
    {
        if (id != dto.CategoryId) return BadRequest();
        _service.UpdateCategory(dto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        return _service.DeleteCategory(id) ? NoContent() : BadRequest();
    }
}
