using System.Threading.Channels;
using OrderManagementSystem.Domain.Interfaces;
using OrderManagementSystem.Domain.Request;

namespace OrderManagementSystem.API;

public class DiscountBackgroundService(
    Channel<DiscountRequest> discountChannel,
    IDiscountService discountService,
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
                await discountService.SetDiscount(discountRequest);
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
            }
        }
    }
}