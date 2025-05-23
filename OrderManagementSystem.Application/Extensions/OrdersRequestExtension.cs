using OrderManagementSystem.Domain.Entities;
using OrderManagementSystem.Domain.Request;

namespace OrderManagementSystem.Application.Extensions;

public static class OrdersRequestExtension
{
    public static Order ToOrders(this OrderCreateRequest createRequest)
    {
        var orderId = Guid.NewGuid();
        var year = createRequest.OrderDate.Year;
        var random = new Random().Next(0, 10000); 
        var orderNumber = $"ORD-{year}{random:D4}";
        
        return new Order
        {
            OrderId = orderId,
            OrderNumber = orderNumber,
            CustomerId = createRequest.CustomerId,
            StatusId = 0,
            OrderDate = createRequest.OrderDate,
            LastModifiedDate = createRequest.LastModifiedDate,
            StatusChangeDate = createRequest.StatusChangeDate,
            Subtotal = createRequest.Subtotal,
            DiscountAmount = createRequest.DiscountAmount,
            AppliedPromotionId = createRequest.AppliedPromotionId,
            TotalAmount = createRequest.TotalAmount,
            ShippingAddressId = createRequest.ShippingAddressId,
            BillingAddressId = createRequest.BillingAddressId,
            EstimatedDeliveryDate = createRequest.EstimatedDeliveryDate,
            ActualDeliveryDate = createRequest.ActualDeliveryDate,
            ShippedDate = createRequest.ShippedDate,
            FulfillmentTime = createRequest.FulfillmentTime,
            Note = createRequest.Note,
            OrderItems = createRequest.OrderItems.Select(item => new OrderItem
            {
                OrderItemId = Guid.NewGuid(),
                OrderId = orderId,
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                UnitPrice = item.UnitPrice
            }).ToList()
        };
    }
}