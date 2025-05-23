using OrderManagementSystem.Domain.Response;

namespace OrderManagementSystem.Domain.Interfaces;

public interface IAnalyticService
{
    Task<List<AnalyticResponse>>  GetAllAsync();
}