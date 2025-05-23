using AutoMapper;
using OrderManagementSystem.Domain.Interfaces;
using OrderManagementSystem.Domain.Response;

namespace OrderManagementSystem.Application.Services;

public class AnalyticService(IOrderRepository orderRepository, IMapper mapper) : IAnalyticService
{
    
    public async Task<List<AnalyticResponse>>  GetAllAsync()
    {
        var orders = await orderRepository.GetAllAsync();
        return mapper.Map<List<AnalyticResponse>>(orders);
    }
}