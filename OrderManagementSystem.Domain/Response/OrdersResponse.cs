namespace OrderManagementSystem.Domain.Response;

public class OrdersResponse
{
    public Guid OrderId { get; set; }

    public string OrderNumber { get; set; } = string.Empty;
    public Guid CustomerId { get; set; }
    public int StatusId { get; set; }

    public DateTime OrderDate { get; set; }
    public DateTime LastModifiedDate { get; set; }
    public DateTime? StatusChangeDate { get; set; }

    public decimal Subtotal { get; set; }
    public decimal DiscountAmount { get; set; } = 0.00m;
    public Guid? AppliedPromotionId { get; set; }
    public decimal TotalAmount { get; set; }

    public Guid? ShippingAddressId { get; set; }
    public Guid? BillingAddressId { get; set; }
    public DateTime? EstimatedDeliveryDate { get; set; }
    public DateTime? ActualDeliveryDate { get; set; }
    public DateTime? ShippedDate { get; set; }
    public int? FulfillmentTime { get; set; }

    public string? Note { get; set; }
}