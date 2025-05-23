using Microsoft.AspNetCore.Mvc;
using OrderManagementSystem.Domain.Interfaces;
using OrderManagementSystem.Domain.Responses;

namespace OrderManagementSystem.API.Controllers;

/// <summary>
/// Provides analytical data related to customer orders.
/// </summary>
[Route("api/[controller]")]
public class AnalyticsController(IAnalyticService analyticService):ControllerBase
{
    /// <summary>
    /// Retrieves a paginated list of order analytics.
    /// </summary>
    /// <param name="pageNumber">Optional page number for pagination.</param>
    /// <param name="pageSize">Optional page size for pagination.</param>
    /// <returns>A list of analytic records with insights on order behavior.</returns>
    /// <response code="200">Returns the list of analytics data.</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<AnalyticResponse>))]
    public async Task<ActionResult<List<AnalyticResponse>>> GetAnalytics([FromQuery] int? pageNumber, [FromQuery] int? pageSize)
    {
        var analytics = await analyticService.GetAnalyticsAsync(pageNumber, pageSize);
        return Ok(analytics);
    }
}