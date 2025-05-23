using OrderManagementSystem.Domain.Entities;
using OrderManagementSystem.Domain.Enums;
using OrderManagementSystem.Domain.Requests;

namespace OrderManagementSystem.Application.Extensions;

public static class OrdersRequestExtension
{
    public static Order ToOrders(this OrderCreateRequest createRequest)
    {
        var orderId = Guid.NewGuid();
        var createdDate = DateTime.UtcNow;
        var year = createdDate.Year;
        var random = new Random().Next(0, 10000); 
        var orderNumber = $"ORD-{year}{random:D4}";
        
        return new Order
        {
            OrderId = orderId,
            OrderNumber = orderNumber,
            CustomerId = createRequest.CustomerId,
            StatusId = (int)StatusId.Pending,
            OrderDate = createdDate,
            LastModifiedDate = createdDate,
            StatusChangeDate = createdDate,
            Subtotal = createRequest.Subtotal,
            DiscountAmount = 0,
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