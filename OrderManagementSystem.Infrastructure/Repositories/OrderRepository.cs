using System.Collections.Immutable;
using System.Net;
using Microsoft.EntityFrameworkCore;
using OrderManagementSystem.Domain.Entities;
using OrderManagementSystem.Domain.Enums;
using OrderManagementSystem.Domain.Exceptions;
using OrderManagementSystem.Domain.Interfaces;

namespace OrderManagementSystem.Infrastructure.Repositories;

public class OrderRepository(ApplicationDbContext context) : IOrderRepository
{
    private async Task<List<Order>> GetAllAsync()
    {
        return await context.Orders
            .Include(o => o.Status)
            .ToListAsync();
    }

    public async Task<List<Order>> GetOrderAsync(int? pageNumber, int? pageSize)
    {
        if (pageNumber is null || pageSize is null)
        {
            return await GetAllAsync();
        }

        if (pageNumber <= 0 || pageSize <= 0)
        {
            throw new ProblemException(
                errorMessage: $"{nameof(pageNumber)} and {nameof(pageNumber)} must be greater that or equals 1",
                title: "Bad Request", statusCode: (int)HttpStatusCode.BadRequest);
        }

        var query = context.Orders.Include(o => o.Status).AsQueryable();
        var orders = await query
            .OrderByDescending(o => o.OrderDate)
            .Skip((pageNumber!.Value - 1) * pageSize.Value)
            .Take(pageSize.Value)
            .ToListAsync();

        return orders;
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

    public async Task<int> UpdateAsync(Order order)
    {
        context.Orders.Update(order);
      return await context.SaveChangesAsync();
    }

    public async Task<int> UpdateOrderStatusAsync(Guid orderId, StatusId statusId)
    {
        var order = await GetByIdAsync(orderId);
        if (order is null)
            throw new ProblemException(errorMessage: $"Order does not exist for {orderId}", title: "Not Found",
                statusCode: (int)HttpStatusCode.NotFound);

        var statusStepIsGreaterThanOne = ((int)statusId - (int)order.StatusId) > 1;
        var isNotAValidStatusId = !Enum.IsDefined(typeof(OrderStatus), statusId);

        if (statusStepIsGreaterThanOne || isNotAValidStatusId)
            throw new ProblemException(
                errorMessage:
                $"Order status update must follow the order of  {string.Join("-> ", Enum.GetNames(typeof(StatusId)))}",
                title: "Bad Request", statusCode: (int)HttpStatusCode.BadRequest);

        order.StatusId = (int)statusId;
        var updatedDate = DateTime.UtcNow;
        order.LastModifiedDate = updatedDate;
        order.StatusChangeDate = updatedDate;
        switch (statusId)
        {
            case StatusId.Shipped:
                order.ShippedDate = updatedDate;
                break;
            case StatusId.Delivered:
                order.ActualDeliveryDate = updatedDate;
                order.FulfillmentTime = (int)(updatedDate - order.OrderDate).TotalMinutes;
                break;
        }

        return await UpdateAsync(order);
    }
}