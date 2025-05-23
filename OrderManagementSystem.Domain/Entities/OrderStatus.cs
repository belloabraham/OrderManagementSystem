using System.ComponentModel.DataAnnotations;

namespace OrderManagementSystem.Domain.Entities;

public class OrderStatus
{
    [Key]
    public int StatusId { get; set; }

    public string StatusName { get; set; } = string.Empty;
    public string? Description { get; set; }
}