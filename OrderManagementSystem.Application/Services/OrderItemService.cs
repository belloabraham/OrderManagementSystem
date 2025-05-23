using AutoMapper;
using OrderManagementSystem.Domain.Interfaces;
using OrderManagementSystem.Domain.Response;

namespace OrderManagementSystem.Application.Services;


public class OrderItemService(IOrderItemsRepository orderItemsRepository,  IMapper mapper) : IOrderItemsService
{
    public async Task<List<OrderItemResponse>> GetAllAsync()
    {
        var orderItems = await orderItemsRepository.GetAllAsync();
        return mapper.Map<List<OrderItemResponse>>(orderItems);
    }
    
    public async Task<OrderItemResponse?> GetByIdAsync(Guid id)
    {
        var orderItem = await orderItemsRepository.GetByIdAsync(id);
        return orderItem != null ? mapper.Map<OrderItemResponse>(orderItem) : null;
    }
    
    public async Task<List<OrderItemResponse>> GetByOrderIdAsync(Guid orderId)
    {
        var orderItems = await orderItemsRepository.GetByOrderIdAsync(orderId);
        return mapper.Map<List<OrderItemResponse>>(orderItems);
    }
   
}