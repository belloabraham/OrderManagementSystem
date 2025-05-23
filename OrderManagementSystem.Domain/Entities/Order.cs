using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderManagementSystem.Domain.Entities;

//TODO
public class Order
{
    [Key]
    public Guid OrderId { get; set; }
    public string OrderNumber { get; set; } = string.Empty;
    
    public Guid CustomerId { get; set; }
    public int StatusId { get; set; }

    public DateTime OrderDate { get; set; }
    public DateTime LastModifiedDate { get; set; }
    public DateTime? StatusChangeDate { get; set; }

    // Pricing & Discounts
    public decimal Subtotal { get; set; }
    public decimal DiscountAmount { get; set; } = 0.00m;
    public Guid? AppliedPromotionId { get; set; }
    public decimal TotalAmount { get; set; }

    // Shipping
    public Guid? ShippingAddressId { get; set; }
    public Guid? BillingAddressId { get; set; }
    public DateTime? EstimatedDeliveryDate { get; set; }
    public DateTime? ActualDeliveryDate { get; set; }
    public DateTime? ShippedDate { get; set; }
    public int? FulfillmentTime { get; set; }

    public string? Note { get; set; }

    // Navigation Properties
    [ForeignKey("StatusId")]
    public OrderStatus? Status { get; set; }

    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}