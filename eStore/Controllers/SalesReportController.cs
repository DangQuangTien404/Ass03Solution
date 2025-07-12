using BusinessObject.DTOs;
using DataAccess.Services;
using Microsoft.AspNetCore.Mvc;

namespace eStore.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SalesReportController : ControllerBase
{
    private readonly IOrderService _service;

    public SalesReportController(IOrderService service)
    {
        _service = service;
    }

    [HttpGet]
    public ActionResult<IEnumerable<SalesReportDto>> Get([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        => Ok(_service.GetSalesReport(startDate, endDate));
}
