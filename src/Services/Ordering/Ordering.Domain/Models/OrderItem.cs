using Ordering.Domain.Abstractions;

namespace Ordering.Domain.Models
{
    public class OrderItem : Entity<Guid>
    {
        public Guid OrderId { get; private set; } = default!;

        public Guid ProductId { get; private set; } = default!;

        public int Quantity { get; private set; }

        public decimal Price { get; private set; }

        public OrderItem(Guid productId, int quantity, decimal unitPrice)
        {
            ProductId = productId;
            Quantity = quantity;
            Price = unitPrice;
        }
    }
}