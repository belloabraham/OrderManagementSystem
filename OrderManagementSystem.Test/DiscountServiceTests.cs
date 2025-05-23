namespace OrderManagementSystem.Test;

// Unit Tests for Discount Logic
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using OrderManagementSystem.Application.Services;
using OrderManagementSystem.Domain.Entities;
using OrderManagementSystem.Domain.Interfaces;
using OrderManagementSystem.Domain.Requests;
using Xunit;

public class DiscountServiceTests
{
    private readonly Mock<IOrderRepository> _orderRepoMock = new();

    [Fact]
    public async Task Should_Apply_10Percent_Discount_For_NewCustomer()
    {
        // Arrange
        var orderId = Guid.NewGuid();
        var customerId = Guid.NewGuid();
        var order = new Order { OrderId = orderId, CustomerId = customerId, TotalAmount = 1000m };

        _orderRepoMock.Setup(r => r.GetByIdAsync(orderId)).ReturnsAsync(order);
        _orderRepoMock.Setup(r => r.GetByCustomerIdAsync(customerId)).ReturnsAsync(new List<Order> { });

        var service = new DiscountService(_orderRepoMock.Object);
        var request = new DiscountRequest
        {
            OrderId = orderId,
            CustomerId = customerId,
            CustomerSegment = "New"
        };

        // Act
        await service.SetDiscount(request, vipMinimumSpendThreshold: 10000m, standardDiscountRate: 0.10m);

        // Assert
        Assert.Equal(100m, order.DiscountAmount); // 10% of 1000
    }

    [Fact]
    public async Task Should_Apply_10Percent_Discount_For_VIPCustomer()
    {
        // Arrange
        var orderId = Guid.NewGuid();
        var customerId = Guid.NewGuid();
        var pastOrders = new List<Order>
        {
            new() { TotalAmount = 5000 },
            new() { TotalAmount = 6000 }
        };
        var order = new Order { OrderId = orderId, CustomerId = customerId, TotalAmount = 2000m };

        _orderRepoMock.Setup(r => r.GetByIdAsync(orderId)).ReturnsAsync(order);
        _orderRepoMock.Setup(r => r.GetByCustomerIdAsync(customerId)).ReturnsAsync(pastOrders);

        var service = new DiscountService(_orderRepoMock.Object);
        var request = new DiscountRequest
        {
            OrderId = orderId,
            CustomerId = customerId,
            CustomerSegment = "Existing"
        };

        // Act
        await service.SetDiscount(request, vipMinimumSpendThreshold: 10000m, standardDiscountRate: 0.10m);

        // Assert
        Assert.Equal(200m, order.DiscountAmount); // 10% of 2000
    }
}