using Ordering.Domain.Abstractions;
using Ordering.Domain.ValueObjects;

namespace Ordering.Domain.Models
{
    public class OrderItem : Entity<OrderItmeId>
    {
        public OrderId OrderId { get; private set; } = default!;

        public ProductId ProductId { get; private set; } = default!;

        public int Quantity { get; private set; }

        public decimal Price { get; private set; }

        private OrderItem()
        {
            
        }
        public OrderItem(OrderId orderId, ProductId productId, int quantity, decimal unitPrice)
        {
            Id = OrderItmeId.Of(Guid.NewGuid());
            OrderId = orderId;
            ProductId = productId;
            Quantity = quantity;
            Price = unitPrice;
        }
    }
}