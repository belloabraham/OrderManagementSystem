using OrderManagementSystem.Domain.Entities;

namespace OrderManagementSystem.Domain.Interfaces;

public interface IOrderItemsRepository
{
    Task<List<OrderItems>> GetAllAsync();
    Task<OrderItems?> GetByIdAsync(Guid id);
    Task<List<OrderItems>> GetByOrderIdAsync(Guid orderId);
    Task<OrderItems?> GetByProductIdAsync(Guid productId);
}