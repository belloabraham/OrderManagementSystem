using OrderManagementSystem.Domain.Entities;
using OrderManagementSystem.Domain.Enums;

namespace OrderManagementSystem.Domain.Requests;

public class OrderStatusUpdateRequest
{
    public StatusId StatusId { get; set; }
}