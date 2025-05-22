using Microsoft.EntityFrameworkCore;
using OrderManagementSystem.Domain.Entities;
using OrderManagementSystem.Domain.Interfaces;

namespace OrderManagementSystem.Infrastructure.Repositories;

public class OrderItemsRepository(ApplicationDbContext context) : IOrderItemsRepository
{
    
    public async Task<List<OrderItems>> GetAllAsync()
    {
        return await context.OrdersItems
            .ToListAsync();
    }
    
    public async Task<OrderItems?> GetByIdAsync(Guid id)
    {
        return await context.OrdersItems
            .FirstOrDefaultAsync(o => o.OrderItemId == id);
    }
    
    public async Task<List<OrderItems>> GetByOrderIdAsync(Guid orderId)
    {
        return await context.OrdersItems
            .Where(o => o.OrderId == orderId)
            .ToListAsync();
    }
    
    
    public async Task<OrderItems?> GetByProductIdAsync(Guid productId)
    {
        return await context.OrdersItems
            .FirstOrDefaultAsync(o => o.ProductId == productId);
    }

}