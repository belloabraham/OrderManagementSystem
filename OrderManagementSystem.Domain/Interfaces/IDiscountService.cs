using OrderManagementSystem.Domain.Request;

namespace OrderManagementSystem.Domain.Interfaces;

public interface IDiscountService
{
    Task SetDiscount(DiscountRequest request);
}
