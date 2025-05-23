using Microsoft.EntityFrameworkCore;
using OrderManagementSystem.Domain.Entities;
using OrderManagementSystem.Domain.Enums;

namespace OrderManagementSystem.Infrastructure;

/// <summary>
/// The application's primary database context, responsible for managing entity sets
/// and facilitating communication between the domain models and the database.
/// </summary>
public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    /// <summary>
    /// Represents the Orders table in the database.
    /// </summary>
    public DbSet<Order> Orders { get; set; }

    /// <summary>
    /// Represents the OrderItems table in the database.
    /// </summary>
    public DbSet<OrderItem> OrdersItems { get; set; }

    /// <summary>
    /// Represents the OrderStatuses table in the database.
    /// </summary>
    public DbSet<OrderStatus> OrderStatuses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<OrderStatus>().HasData(
            new OrderStatus { StatusId = (int)StatusId.Pending, StatusName = nameof(StatusId.Pending), Description = "Order has been placed but not yet processed." },
            new OrderStatus { StatusId = (int)StatusId.Processing, StatusName = nameof(StatusId.Processing), Description = "Order is being processed." },
            new OrderStatus { StatusId = (int)StatusId.Cancelled, StatusName = nameof(StatusId.Cancelled), Description = "Order has been cancelled." },
            new OrderStatus { StatusId = (int)StatusId.Shipped, StatusName = nameof(StatusId.Shipped), Description = "Order has been shipped." },
            new OrderStatus { StatusId = (int)StatusId.Delivered, StatusName = nameof(StatusId.Delivered), Description = "Order has been delivered." },
            new OrderStatus { StatusId = (int)StatusId.Returned, StatusName = nameof(StatusId.Returned), Description = "Order was returned by the customer." }
        );
    }
}