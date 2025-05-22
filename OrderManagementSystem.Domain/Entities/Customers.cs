using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderManagementSystem.Domain.Entities;

public class Customers
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid CustomerId { get; set; }

    public string SegmentName { get; set; } = string.Empty;
    public string? Description { get; set; }

    public ICollection<Orders> Orders { get; set; } = new List<Orders>();
}