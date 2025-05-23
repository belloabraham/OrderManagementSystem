using System.Threading.Channels;
using OrderManagementSystem.Domain.Interfaces;
using OrderManagementSystem.Domain.Requests;

namespace OrderManagementSystem.API;

public class DiscountBackgroundService(
    Channel<DiscountRequest> discountChannel,
    IServiceScopeFactory scopeFactory,
    ILogger<DiscountBackgroundService> logger)
    : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (await discountChannel.Reader.WaitToReadAsync(stoppingToken))
        {
            try
            {
                var discountRequest = await discountChannel.Reader.ReadAsync(stoppingToken);
                using var scope = scopeFactory.CreateScope();
                var discountService = scope.ServiceProvider.GetRequiredService<IDiscountService>();
                await discountService.SetDiscount(discountRequest);
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
            }
        }
    }
}