using BusinessObject.DTOs;
using DataAccess.Services.Interface;
using eStore.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Mvc;

namespace eStore.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _service;
    private readonly IHubContext<CategoryHub>? _hub;

    public CategoriesController(ICategoryService service, IHubContext<CategoryHub>? hub = null)
    {
        _service = service;
        _hub = hub;
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
        _hub?.Clients.All.SendAsync("CategoryCreated", dto);
        return CreatedAtAction(nameof(Get), new { id = dto.CategoryId }, dto);
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, CategoryDto dto)
    {
        if (id != dto.CategoryId) return BadRequest();
        var existing = _service.GetCategory(id);
        if (existing == null) return NotFound();
        _service.UpdateCategory(dto);
        _hub?.Clients.All.SendAsync("CategoryUpdated", dto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        if (!_service.DeleteCategory(id)) return BadRequest();
        _hub?.Clients.All.SendAsync("CategoryDeleted", id);
        return NoContent();
    }

}

