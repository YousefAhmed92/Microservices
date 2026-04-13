namespace Ordering.Application.Orders.Commands.DeleteOrder
{
    public record DeleteOrderCommand(Guid OrderID)
        : ICommand<DeleteOrderResult>;

    public record DeleteOrderResult(bool IsSuccess);

    public class DeleteOrderCommandValidator : AbstractValidator<DeleteOrderCommand>
    {
        public DeleteOrderCommandValidator()
        {
            RuleFor(x => x.OrderID)
                .NotEmpty()
                .WithMessage("Order ID cannot be empty.");
        }
    }
}
