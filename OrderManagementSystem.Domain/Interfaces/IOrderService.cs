using OrderManagementSystem.Domain.Entities;
using OrderManagementSystem.Domain.Request;
using OrderManagementSystem.Domain.Response;

namespace OrderManagementSystem.Domain.Interfaces;

public interface IOrderService
{
    Task<List<OrderResponse>>  GetOrdersAsync(int? pageNumber, int? pageSize);
    Task<Order> AddAsync(OrderCreateRequest orderCreateRequest);
    Task<OrderResponse?> GetByIdAsync(Guid id);
    Task<int> UpdateOrderStatusAsync(Guid orderId, int statusId);
}
