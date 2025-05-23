using Microsoft.EntityFrameworkCore;
using OrderManagementSystem.Domain.Entities;

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
}