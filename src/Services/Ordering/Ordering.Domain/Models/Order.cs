using Ordering.Domain.Abstractions;
using Ordering.Domain.Enums;
using Ordering.Domain.ValueObjects;

namespace Ordering.Domain.Models
{
    public class Order : Entity<Guid>
    {
        private readonly List<OrderItem> _orderItems = new();

        public IReadOnlyList<OrderItem> OrderItems => _orderItems.AsReadOnly();

        public Guid CustomerId { get; private set; } = default!;

        public string OrderName { get; set; } = default!;

        public decimal TotalPrice
        {
            get => OrderItems.Sum(x => x.Quantity * x.Price);
            private set { } 
        }

        public Payment Payment { get; set; } = default!;

        public string ShippingAddress { get; set; } = default!;

        public string BillingAddress { get; set; } = default!;

        public OrderStatus Status { get; set; } = OrderStatus.Pending;
    }
}
