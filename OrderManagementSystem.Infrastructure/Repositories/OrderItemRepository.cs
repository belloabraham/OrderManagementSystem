using Microsoft.EntityFrameworkCore;
using OrderManagementSystem.Domain.Entities;
using OrderManagementSystem.Domain.Interfaces;

namespace OrderManagementSystem.Infrastructure.Repositories;

public class OrderItemRepository(ApplicationDbContext context) : IOrderItemsRepository
{
    
    public async Task<List<OrderItem>> GetAllAsync()
    {
        return await context.OrdersItems
            .ToListAsync();
    }
    
    public async Task<OrderItem?> GetByIdAsync(Guid id)
    {
        return await context.OrdersItems
            .FirstOrDefaultAsync(o => o.OrderItemId == id);
    }
    
    public async Task<List<OrderItem>> GetByOrderIdAsync(Guid orderId)
    {
        return await context.OrdersItems
            .Where(o => o.OrderId == orderId)
            .ToListAsync();
    }
    
    
    public async Task<List<OrderItem>> GetByProductIdAsync(Guid productId)
    {
        return await context.OrdersItems
            .Where(o => o.ProductId == productId)
            .ToListAsync();
    }

}