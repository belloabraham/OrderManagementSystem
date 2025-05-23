namespace OrderManagementSystem.Domain.Request;

public class OrderStatusUpdateRequest
{
    public Guid OrderId { get; set; }
    public int StatusId { get; set; }
}