using FluentValidation;
using OrderManagementSystem.Domain.Requests;

namespace OrderManagementSystem.Application.Validators;

public class OrderItemRequestValidator: AbstractValidator<OrderItemRequest>
{
    public OrderItemRequestValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty().WithMessage("Product ID is required.");

        RuleFor(x => x.Quantity)
            .GreaterThan(0).WithMessage("Quantity must be greater than zero.");

        RuleFor(x => x.UnitPrice)
            .GreaterThanOrEqualTo(0).WithMessage("Unit price must be zero or positive.");
    }
}