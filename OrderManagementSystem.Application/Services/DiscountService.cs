using System.Data;
using OrderManagementSystem.Application.Extensions;
using OrderManagementSystem.Domain.Constant;
using OrderManagementSystem.Domain.Interfaces;
using OrderManagementSystem.Domain.Requests;

namespace OrderManagementSystem.Application.Services;

/// <summary>
/// Service responsible for applying discount logic to orders.
/// A discount is applied if the customer is new or has spent above a defined VIP threshold.
/// </summary>
public class DiscountService(IOrderRepository orderRepository) : IDiscountService
{
    /// <summary>
    /// Applies a standard discount to an order if the customer is either new or qualifies as a VIP.
    /// </summary>
    /// <param name="request">The discount request containing order and customer information.</param>
    /// <param name="vipMinimumSpendThreshold">The minimum total amount spent by a customer to qualify for VIP status.</param>
    /// <param name="standardDiscountRate">The discount rate to be applied (e.g., 0.10 for 10%).</param>
    public async Task SetDiscount(DiscountRequest request, decimal vipMinimumSpendThreshold,
        decimal standardDiscountRate)
    {
        var isNewCustomer = request.CustomerSegment.Equals(CustomerSegment.New, StringComparison.OrdinalIgnoreCase);
        var orders = await orderRepository.GetByCustomerIdAsync(request.CustomerId);
        var isVipCustomer = orders.Sum(order => order.TotalAmount) > vipMinimumSpendThreshold;

        if (isNewCustomer || isVipCustomer)
        {
            var order = await orderRepository.GetByIdAsync(request.OrderId);
            if (order == null) throw new InvalidOperationException("Order not found.");

            order.DiscountAmount = order.CalculateDiscount(standardDiscountRate);
            await orderRepository.UpdateAsync(order);
        }
    }
}