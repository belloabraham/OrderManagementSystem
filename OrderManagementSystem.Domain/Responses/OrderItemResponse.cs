namespace OrderManagementSystem.Domain.Responses;

public class OrderItemResponse
{
    public Guid OrderItemId { get; set; }

    public Guid OrderId { get; set; }
    public Guid ProductId { get; set; }

    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    
    public decimal LineItemTotal => Quantity * UnitPrice;
}