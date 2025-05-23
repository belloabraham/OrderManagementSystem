using FluentValidation;
using OrderManagementSystem.Domain.Entities;
using OrderManagementSystem.Domain.Enums;
using OrderManagementSystem.Domain.Requests;

namespace OrderManagementSystem.Application.Validators;

public class OrderStatusUpdateRequestValidator : AbstractValidator<OrderStatusUpdateRequest>
{
    public OrderStatusUpdateRequestValidator()
    {
        RuleFor(x => x.StatusId)
            .IsInEnum()
            .WithMessage($"Invalid status. Must be one of: {string.Join("-> ", Enum.GetNames(typeof(StatusId)))}");
    }
}