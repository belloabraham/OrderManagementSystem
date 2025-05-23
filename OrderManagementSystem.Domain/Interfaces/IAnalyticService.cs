using OrderManagementSystem.Domain.Responses;

namespace OrderManagementSystem.Domain.Interfaces;

public interface IAnalyticService
{
    Task<List<AnalyticResponse>>  GetAnalyticsAsync(int? pageNumber, int? pageSize);
}