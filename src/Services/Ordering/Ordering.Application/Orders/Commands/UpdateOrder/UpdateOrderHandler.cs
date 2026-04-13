namespace Ordering.Application.Orders.Commands.UpdateOrder
{
    public class UpdateOrderHandler(IApplicationDbContext context)
        : ICommandHandler<UpdateOrderCommand, UpdateOrderResult>
    {
        public async Task<UpdateOrderResult> Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
        {
            var orderId = OrderId.Of(command.Order.Id);
            
            var order = await context.orders
                .FindAsync([orderId], cancellationToken);

            if (order is null)
            {
                throw new OrderNotFoundException(command.Order.Id);
            }

            UpdateOrderValues(order, command.Order);

            context.orders.Update(order);

            await context.SaveChangesAsync(cancellationToken);

            return new UpdateOrderResult(true);
        }

        private void UpdateOrderValues(Order order, OrderDto orderDto)
        {
            var updatedShippingAddress = Address.Of(
                orderDto.ShippingAddress.FirstName,
                orderDto.ShippingAddress.LastName,
                orderDto.ShippingAddress.EmailAddress,
                orderDto.ShippingAddress.AddressLine,
                orderDto.ShippingAddress.Country,
                orderDto.ShippingAddress.State,
                orderDto.ShippingAddress.ZipCode
            );

            var updatedBillingAddress = Address.Of(
                orderDto.BillingAddress.FirstName,
                orderDto.BillingAddress.LastName,
                orderDto.BillingAddress.EmailAddress,
                orderDto.BillingAddress.AddressLine,
                orderDto.BillingAddress.Country,
                orderDto.BillingAddress.State,
                orderDto.BillingAddress.ZipCode
            );

            var UpdatedPayment = Payment.Of(
                orderDto.Payment.CardName,
                orderDto.Payment.CardNumber,
                orderDto.Payment.Expiration,
                orderDto.Payment.Cvv,
                orderDto.Payment.PaymentMethod
            );

            order.Update(
                orderName: OrderName.Of(orderDto.OrderName),
                shippingaddress: updatedShippingAddress,
                billingaddress: updatedBillingAddress,
                payment: UpdatedPayment,
                orderDto.Status
            );
        }
    }
}
