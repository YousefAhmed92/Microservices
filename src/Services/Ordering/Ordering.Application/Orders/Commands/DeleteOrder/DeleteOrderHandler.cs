namespace Ordering.Application.Orders.Commands.DeleteOrder
{
    public class DeleteOrderHandler(IApplicationDbContext context)
        : ICommandHandler<DeleteOrderCommand, DeleteOrderResult>
    {
        public async Task<DeleteOrderResult> Handle(DeleteOrderCommand command, CancellationToken cancellationToken)
        {
            var orderId = OrderId.Of(command.OrderID);

            var order = await context.orders.
                FindAsync(new object[] { orderId }, cancellationToken);

            if (order is null)
            {
                throw new OrderNotFoundException(command.OrderID);
            }

            context.orders.Remove(order);

            await context.SaveChangesAsync(cancellationToken);

            return new DeleteOrderResult(true);
        }
    }
}
