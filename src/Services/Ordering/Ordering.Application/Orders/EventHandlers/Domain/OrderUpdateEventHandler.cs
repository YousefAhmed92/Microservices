namespace Ordering.Application.Orders.EventHandlers.Domain
{
    public class OrderUpdateEventHandler(ILogger<OrderUpdateEventHandler> logger)
        : INotificationHandler<OrderUpdatedEvent>
    {
        public Task Handle(OrderUpdatedEvent notification, CancellationToken cancellationToken)
        {
            logger.LogInformation("Order with id {OrderId} updated", notification.GetType().Name);

            return Task.CompletedTask;
        }
    }
}
