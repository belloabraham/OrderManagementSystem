using OrderManagementSystem.Domain.Requests;

namespace OrderManagementSystem.Domain.Interfaces;

public interface IDiscountService
{
    Task SetDiscount(DiscountRequest request);
}
