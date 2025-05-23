namespace OrderManagementSystem.Domain.Request;

public class OrderItemsRequest
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}