using OrderManagementSystem.Domain.Response;

namespace OrderManagementSystem.Domain.Interfaces;

public interface IOrderItemsService
{
    Task<List<OrderItemResponse>> GetAllAsync();
    Task<OrderItemResponse?> GetByIdAsync(Guid id);
    Task<List<OrderItemResponse>> GetByOrderIdAsync(Guid orderId);
}