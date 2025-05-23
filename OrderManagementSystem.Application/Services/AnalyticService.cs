using AutoMapper;
using OrderManagementSystem.Domain.Interfaces;
using OrderManagementSystem.Domain.Responses;

namespace OrderManagementSystem.Application.Services;

public class AnalyticService(IOrderRepository orderRepository, IMapper mapper) : IAnalyticService
{
    
    public async Task<List<AnalyticResponse>>  GetAnalyticsAsync(int? pageNumber, int? pageSize)
    {
        var orders = await orderRepository.GetOrderAsync(pageNumber, pageSize);
        return mapper.Map<List<AnalyticResponse>>(orders);
    }
}