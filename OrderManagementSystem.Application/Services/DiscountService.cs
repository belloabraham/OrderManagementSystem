using OrderManagementSystem.Domain.Constant;
using OrderManagementSystem.Domain.Interfaces;
using OrderManagementSystem.Domain.Requests;

namespace OrderManagementSystem.Application.Services;

public class DiscountService(IOrderRepository orderRepository) : IDiscountService
{
    public async Task SetDiscount(DiscountRequest request)
    {
        var isNewCustomer = request.CustomerSegment == CustomerSegment.New;
        var orders = await  orderRepository.GetByCustomerIdAsync(request.CustomerId);
        var isVipCustomer = orders.Sum(order => order.TotalAmount) > 10000;

        if (isNewCustomer || isVipCustomer)
        {
            var order = await orderRepository.GetByIdAsync(request.OrderId);
            var updatedDate = DateTime.UtcNow;
            order!.LastModifiedDate = updatedDate;
            order!.DiscountAmount = order.TotalAmount * 0.1m;
        }
    }
}