using Microsoft.EntityFrameworkCore;
using OrderManagementSystem.Domain.Entities;
using OrderManagementSystem.Domain.Interfaces;

namespace OrderManagementSystem.Infrastructure.Repositories;

public class OrdersRepository(ApplicationDbContext context) : IOrderRepository
{
    public async Task<List<Orders>> GetAllAsync()
    {
        return await context.Orders
            .ToListAsync();
    }
    
    public async Task<Orders?> GetByIdAsync(Guid id)
    {
        return await context.Orders
            .FirstOrDefaultAsync(o => o.OrderId == id);
    }
    
    public async Task<List<Orders>> GetByCustomerIdAsync(Guid customerId)
    {
        return await context.Orders
            .Where(o => o.CustomerId == customerId)
            .ToListAsync();
    }

    public async Task<Orders> AddAsync(Orders order)
    {
        context.Orders.Add(order);
        await context.SaveChangesAsync();
        return order;
    }

    public async Task UpdateAsync(Orders order)
    {
        context.Orders.Update(order);
        await context.SaveChangesAsync();
    }
    
}