using Ordering.Domain.Abstractions;
using Ordering.Domain.Enums;
using Ordering.Domain.Events;
using Ordering.Domain.ValueObjects;

namespace Ordering.Domain.Models
{
    public class Order : Aggregate<OrderId>
    {
        private readonly List<OrderItem> _orderItems = new();

        public IReadOnlyList<OrderItem> OrderItems => _orderItems.AsReadOnly();

        public CustomerId CustomerId { get; private set; } = default!;

        public OrderName OrderName { get; set; } = default!;

        public decimal TotalPrice
        {
            get => OrderItems.Sum(x => x.Quantity * x.Price);
            private set { } 
        }

        public Payment Payment { get; set; } = default!;

        public Address ShippingAddress { get; set; } = default!;

        public Address BillingAddress { get; set; } = default!;

        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        public static Order Create(OrderId id, CustomerId customerId, OrderName orderName, Address shippingAddress, Address billingAddress, Payment payment)
        {
            var order = new Order
            {
                Id = id,
                CustomerId = customerId,
                OrderName = orderName,
                ShippingAddress = shippingAddress,
                BillingAddress = billingAddress,
                Payment = payment,
                Status = OrderStatus.Pending
            };

            order.AddDomainEvent(new OrderCreatedEvent(order));

            return order;
        }

        public void Update(OrderName orderName, Address shippingaddress, Address billingaddress, Payment payment, OrderStatus status)
        {
            OrderName = orderName;
            ShippingAddress = shippingaddress;
            BillingAddress = billingaddress;
            Payment = payment;
            Status = status;

            AddDomainEvent(new OrderUpdatedEvent(this));
        }

        public void AddOrderItem(ProductId productId, int quantity, decimal price)
        {
            var orderItem = new OrderItem(Id, productId, quantity, price);

            _orderItems.Add(orderItem);
        }

        public void RemoveOrderItem(ProductId productId)
        {
            var orderItem = _orderItems.FirstOrDefault(x => x.ProductId == productId);

            if (orderItem is not null)
            {
                _orderItems.Remove(orderItem);
            }
        }
    }
}
