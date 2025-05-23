namespace OrderManagementSystem.Domain.Responses;

public class AnalyticResponse
{
    public Guid CustomerId { get; set; }
    public string Status { get; set; } =  string.Empty;
    
    public int OrderItemsCount { get; set; }
    
    public DateTime OrderDate { get; set; }

    public decimal DiscountAmount { get; set; } = 0.00m;
    public Guid? AppliedPromotionId { get; set; }
    public decimal TotalAmount { get; set; }

    public DateTime? EstimatedDeliveryDate { get; set; }
    public DateTime? ActualDeliveryDate { get; set; }
    public int? FulfillmentTime { get; set; }
    
}