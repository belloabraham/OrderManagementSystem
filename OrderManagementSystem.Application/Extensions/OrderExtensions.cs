using OrderManagementSystem.Domain.Entities;

namespace OrderManagementSystem.Application.Extensions;

/// <summary>
/// Extension methods for applying business logic to Order entities.
/// </summary>
public static class OrderExtensions
{
    public static decimal CalculateDiscount(this Order order, decimal rate)
    {
        if (order == null) throw new ArgumentNullException(nameof(order));
        return order.TotalAmount * rate;
    }
}