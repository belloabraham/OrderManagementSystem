using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace OrderManagementSystem.Test;
using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using OrderManagementSystem.Domain.Requests;
using Xunit;

public class OrderApiIntegrationTests: IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public OrderApiIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task CreateOrder_Should_Return_201()
    {
        var request = new OrderCreateRequest
        {
            CustomerId = Guid.NewGuid(),
            CustomerSegment = "New",
            Subtotal = 1000,
            TotalAmount = 1000,
            OrderItems =
            [
                new OrderItemRequest { ProductId = Guid.NewGuid(), Quantity = 1, UnitPrice = 1000 }
            ]
        };

        var response = await _client.PostAsJsonAsync("/api/orders", request);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }
}
