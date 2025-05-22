using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderManagementSystem.Domain.Entities;

public class OrderStatuses
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int StatusId { get; set; }

    public string StatusName { get; set; } = string.Empty;
    public string? Description { get; set; }

    public ICollection<Orders> Orders { get; set; } = new List<Orders>();
}