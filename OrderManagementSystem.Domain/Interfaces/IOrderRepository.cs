using OrderManagementSystem.Domain.Entities;
using OrderManagementSystem.Domain.Enums;

namespace OrderManagementSystem.Domain.Interfaces;

public interface IOrderRepository
{
    Task<List<Order>> GetOrderAsync(int? pageNumber, int? pageSize);
    Task<Order?> GetByIdAsync(Guid id);
    Task<List<Order>>  GetByCustomerIdAsync(Guid customerId);
    Task<Order> AddAsync(Order order);
    Task<int> UpdateAsync(Order order);
    Task<int> UpdateOrderStatusAsync(Guid orderId, StatusId statusId);
}