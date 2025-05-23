using FluentValidation;
using OrderManagementSystem.Domain.Requests;

namespace OrderManagementSystem.Application.Validators;

public class OrderCreateRequestValidator: AbstractValidator<OrderCreateRequest>
{
    public OrderCreateRequestValidator()
    {
        RuleFor(x => x.CustomerId).NotEmpty().WithMessage("Customer ID is required.");

        RuleFor(x => x.CustomerSegment)
            .NotEmpty().WithMessage("Customer segment is required.")
            .MaximumLength(50).WithMessage("Customer segment must not exceed 50 characters.");

        RuleFor(x => x.Subtotal).GreaterThanOrEqualTo(0).WithMessage("Subtotal cannot be negative.");
        RuleFor(x => x.TotalAmount).GreaterThanOrEqualTo(0).WithMessage("Total amount cannot be negative.");

        RuleForEach(x => x.OrderItems)
            .SetValidator(new OrderItemRequestValidator());

        RuleFor(x => x.OrderItems)
            .NotEmpty().WithMessage("Order must have at least one item.");
    }
}