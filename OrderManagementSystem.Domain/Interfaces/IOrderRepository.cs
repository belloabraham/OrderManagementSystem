using OrderManagementSystem.Domain.Entities;

namespace OrderManagementSystem.Domain.Interfaces;

public interface IOrderRepository
{
    Task<List<Order>> GetAllAsync();
    Task<Order?> GetByIdAsync(Guid id);
    Task<List<Order>>  GetByCustomerIdAsync(Guid customerId);
    Task<Order> AddAsync(Order order);
    Task<int> UpdateOrderStatusAsync(Guid orderId, int statusId);
}