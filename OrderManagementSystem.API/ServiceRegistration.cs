using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using OrderManagementSystem.Application.Services;
using OrderManagementSystem.Application.Validators;
using OrderManagementSystem.Domain.Interfaces;
using OrderManagementSystem.Infrastructure;
using OrderManagementSystem.Infrastructure.Repositories;
using OrderManagementSystem.Infrastructure.Settings;

namespace OrderManagementSystem.API;

public static class ServiceRegistration
{
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation();
        services.AddFluentValidationClientsideAdapters();
        services.AddValidatorsFromAssemblyContaining<OrderCreateRequestValidator>();
        
        services.AddHostedService<DiscountBackgroundService>();
        services.AddScoped<IDiscountService, DiscountService>();
        services.AddScoped<IOrderItemsService, OrderItemService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IAnalyticService, AnalyticService>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IOrderItemsRepository, OrderItemRepository>();

        // Register DB context
        services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            var config = sp.GetRequiredService<IOptions<ConnectionStrings>>().Value;
            options.UseSqlServer(config.DefaultConnection);
        });
    }
}