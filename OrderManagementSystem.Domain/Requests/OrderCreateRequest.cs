namespace OrderManagementSystem.Domain.Requests;

public class OrderCreateRequest
{
    public Guid CustomerId { get; set; }
    
    public string? CustomerSegment { get; set; } = string.Empty;
    
    public decimal Subtotal { get; set; }
    public Guid? AppliedPromotionId { get; set; }
    public decimal TotalAmount { get; set; }
    public Guid? ShippingAddressId { get; set; }
    public Guid? BillingAddressId { get; set; }
    public DateTime? EstimatedDeliveryDate { get; set; }
    public DateTime? ActualDeliveryDate { get; set; }
    public DateTime? ShippedDate { get; set; }
    public int? FulfillmentTime { get; set; }

    public string? Note { get; set; }

    public ICollection<OrderItemRequest> OrderItems { get; set; } = new List<OrderItemRequest>();
}