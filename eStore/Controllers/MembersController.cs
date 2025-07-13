using BusinessObject.DTOs;
using DataAccess.Services;
using Microsoft.AspNetCore.Mvc;

namespace eStore.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MembersController : ControllerBase
{
    private readonly IMemberService _service;

    public MembersController(IMemberService service)
    {
        _service = service;
    }

    [HttpGet]
    public ActionResult<IEnumerable<MemberDto>> Get() => Ok(_service.GetMembers());

    [HttpGet("{id}")]
    public ActionResult<MemberDto> Get(int id)
    {
        var dto = _service.GetMember(id);
        return dto == null ? NotFound() : Ok(dto);
    }

    [HttpPost]
    public IActionResult Post(MemberDto dto)
    {
        _service.CreateMember(dto);
        return CreatedAtAction(nameof(Get), new { id = dto.MemberId }, dto);
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, MemberDto dto)
    {
        if (id != dto.MemberId) return BadRequest();
        _service.UpdateMember(dto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        return _service.DeleteMember(id) ? NoContent() : BadRequest();
    }
}
