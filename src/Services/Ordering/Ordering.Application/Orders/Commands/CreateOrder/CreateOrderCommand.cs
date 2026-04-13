using BuildingBlocks.CQRS;
using FluentValidation;
using Ordering.Application.Dtos;

namespace Ordering.Application.Orders.Commands.CreateOrder
{
    public record CreateOrderCommand(OrderDto Order) 
        : ICommand<CreateOrderCommandResult>;

    public record CreateOrderCommandResult(Guid OrderId);

    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(x => x.Order.CustomerId)
                .NotNull()
                .WithMessage("Customer cannot be null.");

                RuleFor(x => x.Order.OrderName)
                .NotEmpty()
                .WithMessage("Order name cannot be empty.");

            RuleFor(x => x.Order.OrderItems)
                .NotNull()
                .WithMessage("Order items cannot be null.");
        }
    }
}
