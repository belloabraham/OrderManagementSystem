using OrderManagementSystem.Domain.Entities;

namespace OrderManagementSystem.Domain.Interfaces;

public interface IOrderItemsRepository
{
    Task<List<OrderItem>> GetAllAsync();
    Task<OrderItem?> GetByIdAsync(Guid id);
    Task<List<OrderItem>> GetByOrderIdAsync(Guid orderId);
    Task<List<OrderItem>> GetByProductIdAsync(Guid productId);
}