using System.Threading.Channels;
using AutoMapper;
using OrderManagementSystem.Application.Extensions;
using OrderManagementSystem.Domain.Entities;
using OrderManagementSystem.Domain.Enums;
using OrderManagementSystem.Domain.Interfaces;
using OrderManagementSystem.Domain.Requests;
using OrderManagementSystem.Domain.Responses;

namespace OrderManagementSystem.Application.Services;

public class OrderService(IOrderRepository orderRepository, IMapper mapper, Channel<DiscountRequest> discountChannel)
    : IOrderService
{
    public async Task<List<OrderResponse>> GetOrdersAsync(int? pageNumber, int? pageSize)
    {
        var orders = await orderRepository.GetOrderAsync(pageNumber, pageSize);
        return mapper.Map<List<OrderResponse>>(orders);
    }

    public async Task<Order> AddAsync(OrderCreateRequest orderCreateRequest)
    {
        var order = await orderRepository.AddAsync(orderCreateRequest.ToOrders());
        await WriteToDiscountChannelAsync(new DiscountRequest()
        {
            CustomerId = orderCreateRequest.CustomerId,
            OrderId = order.OrderId,
            CustomerSegment = orderCreateRequest.CustomerSegment
        });
        return order;
    }

    private async Task WriteToDiscountChannelAsync(DiscountRequest discountRequest)
    {
        await discountChannel.Writer.WriteAsync(discountRequest);
    }

    public async Task<OrderResponse?> GetByIdAsync(Guid id)
    {
        var order = await orderRepository.GetByIdAsync(id);
        return order != null ? mapper.Map<OrderResponse>(order) : null;
    }
    
    public async Task<int> UpdateOrderStatusAsync(Guid orderId, StatusId statusId)
    {
        return await orderRepository.UpdateOrderStatusAsync(orderId, statusId);
    }
    
}