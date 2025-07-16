using BusinessObject.DTOs;
using DataAccess.Services.Interface;
using eStore.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Mvc;

namespace eStore.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MembersController : ControllerBase
{
    private readonly IMemberService _service;
    private readonly IHubContext<MemberHub>? _hub;

    public MembersController(IMemberService service, IHubContext<MemberHub>? hub = null)
    {
        _service = service;
        _hub = hub;
    }

    [HttpGet]
    public ActionResult<IEnumerable<MemberDto>> GetPaged([FromQuery] int page = 1) =>
        Ok(_service.GetMembers(page));

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
        _hub?.Clients.All.SendAsync("MemberCreated", dto);
        return CreatedAtAction(nameof(Get), new { id = dto.MemberId }, dto);
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, MemberDto dto)
    {
        if (id != dto.MemberId) return BadRequest();
        var existing = _service.GetMember(id);
        if (existing == null) return NotFound();
        _service.UpdateMember(dto);
        _hub?.Clients.All.SendAsync("MemberUpdated", dto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        if (!_service.DeleteMember(id)) return BadRequest();
        _hub?.Clients.All.SendAsync("MemberDeleted", id);
        return NoContent();
    }

}

