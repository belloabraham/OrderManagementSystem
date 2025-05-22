using OrderManagementSystem.Domain.Entities;

namespace OrderManagementSystem.Domain.Interfaces;

public interface IOrderRepository
{
    Task<List<Orders>> GetAllAsync();
    Task<Orders?> GetByIdAsync(Guid id);
    Task<List<Orders>>  GetByCustomerIdAsync(Guid customerId);
    Task<Orders> AddAsync(Orders order);
    Task UpdateAsync(Orders order);
}