namespace Ordering.Application.Orders.Commands.UpdateOrder
{
    public record UpdateOrderCommand(OrderDto Order)
         : ICommand<UpdateOrderResult>;

    public record UpdateOrderResult(bool IsSuccess);

    public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
    {
        public UpdateOrderCommandValidator()
        {
            RuleFor(x => x.Order.CustomerId)
                .NotNull()
                .WithMessage("Customer cannot be null.");

            RuleFor(x => x.Order.OrderName)
                .NotEmpty()
                .WithMessage("Order name cannot be empty.");

            RuleFor(x => x.Order.Id)
                .NotNull()
                .WithMessage("Order ID cannot be null.");
        }
    }
}
