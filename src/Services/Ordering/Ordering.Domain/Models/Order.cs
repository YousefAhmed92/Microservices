using Ordering.Domain.Abstractions;
using Ordering.Domain.Enums;
using Ordering.Domain.ValueObjects;

namespace Ordering.Domain.Models
{
    public class Order : Entity<OrderId>
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
    }
}
