using System.Net;
using Microsoft.EntityFrameworkCore;
using OrderManagementSystem.Domain.Entities;
using OrderManagementSystem.Domain.Exceptions;
using OrderManagementSystem.Domain.Interfaces;

namespace OrderManagementSystem.Infrastructure.Repositories;

public class OrderRepository(ApplicationDbContext context) : IOrderRepository
{
    public async Task<List<Order>> GetAllAsync()
    {
        return await context.Orders
            .Include(o => o.Status)
            .ToListAsync();
    }

    public async Task<Order?> GetByIdAsync(Guid id)
    {
        return await context.Orders
            .FirstOrDefaultAsync(o => o.OrderId == id);
    }

    public async Task<List<Order>> GetByCustomerIdAsync(Guid customerId)
    {
        return await context.Orders
            .Where(o => o.CustomerId == customerId)
            .ToListAsync();
    }

    public async Task<Order> AddAsync(Order order)
    {
        context.Orders.Add(order);
        await context.SaveChangesAsync();
        return order;
    }

    public async Task<int> UpdateOrderStatusAsync(Guid orderId, int statusId)
    {
        var order = await context.Orders.FirstOrDefaultAsync(o => o.OrderId == orderId);
        if (order == null)
            throw new ProblemException(errorMessage: $"Order does not exist for {order}", title: "Not Found", statusCode: (int)HttpStatusCode.NotFound);

        var statusStepIsGreaterThanOne = (statusId - order.StatusId) > 1;
        if (statusStepIsGreaterThanOne)
            throw new ProblemException(errorMessage: $"Order status update must follow the order of Pending -> Processing -> Cancelled -> Shipped -> Delivered -> Returned", title: "Bad Request", statusCode: (int)HttpStatusCode.BadRequest);

        order.StatusId = statusId;
        context.Orders.Update(order);
        return await context.SaveChangesAsync();
    }
}