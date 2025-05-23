namespace OrderManagementSystem.Domain.Requests;

public class DiscountRequest
{
    public Guid OrderId { get; set; }
    public Guid CustomerId { get; set; }
    public string? CustomerSegment { get; set; }
}