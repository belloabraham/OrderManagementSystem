using Microsoft.EntityFrameworkCore;
using OrderManagementSystem.Domain.Entities;

namespace OrderManagementSystem.Infrastructure;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Orders> Orders { get; set; }
    public DbSet<OrderItems> OrdersItems { get; set; }
    public DbSet<Customers> Customers { get; set; }
    public DbSet<OrderStatuses> OrderStatuses { get; set; }
}