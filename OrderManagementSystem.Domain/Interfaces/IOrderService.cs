using OrderManagementSystem.Domain.Entities;
using OrderManagementSystem.Domain.Enums;
using OrderManagementSystem.Domain.Requests;
using OrderManagementSystem.Domain.Responses;

namespace OrderManagementSystem.Domain.Interfaces;

public interface IOrderService
{
    Task<List<OrderResponse>>  GetOrdersAsync(int? pageNumber, int? pageSize);
    Task<Order> AddAsync(OrderCreateRequest orderCreateRequest);
    Task<OrderResponse?> GetByIdAsync(Guid id);
    Task<int> UpdateOrderStatusAsync(Guid orderId, StatusId statusId);
}
