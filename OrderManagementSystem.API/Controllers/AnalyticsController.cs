using Microsoft.AspNetCore.Mvc;
using OrderManagementSystem.Domain.Interfaces;
using OrderManagementSystem.Domain.Responses;

namespace OrderManagementSystem.API.Controllers;

[Route("api/[controller]")]
public class AnalyticsController(IAnalyticService analyticService):ControllerBase
{
    // GET: api/analytics?pageNumber=1&pageSize=20
    [HttpGet]
    public async Task<ActionResult<List<AnalyticResponse>>> GetAnalytics([FromQuery] int? pageNumber, [FromQuery] int? pageSize)
    {
        var analytics = await analyticService.GetAnalyticsAsync(pageNumber, pageSize);
        return Ok(analytics);
    }

}