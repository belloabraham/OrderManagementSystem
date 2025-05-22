using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using OrderManagementSystem.Domain.Interfaces;
using OrderManagementSystem.Infrastructure;
using OrderManagementSystem.Infrastructure.Repositories;
using OrderManagementSystem.Infrastructure.Settings;

namespace OrderManagementSystem.API;

public static class ServiceRegistration
{
    //TODO
    public static void RegisterServices(this IServiceCollection services)
    {
        //TODO

        services.AddScoped<IOrderRepository, OrdersRepository>();
        services.AddScoped<IOrderItemsRepository, OrderItemsRepository>();


        // Register DB context
        services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            var config = sp.GetRequiredService<IOptions<ConnectionStrings>>().Value;
            options.UseSqlServer(config.DefaultConnection);
        });

    }
}