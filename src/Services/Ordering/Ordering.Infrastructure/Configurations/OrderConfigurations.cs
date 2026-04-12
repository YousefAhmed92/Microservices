using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Enums;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;

namespace Ordering.Infrastructure.Configurations
{
    public class OrderConfigurations : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.Id);

            builder.Property(o => o.Id)
                .HasConversion(orderId => orderId.Value,
                dbId => OrderId.Of(dbId));

            builder.HasOne<Customer>()
                .WithMany()
                .HasForeignKey(o => o.CustomerId)
                .IsRequired();

            builder.HasMany(o => o.OrderItems)
                .WithOne()
                .HasForeignKey(oi => oi.OrderId);

            builder.ComplexProperty(o => o.OrderName, builder =>
            {
                builder.Property(p => p.Value)
                .HasColumnName(nameof(Order.OrderName))
                .HasMaxLength(100)
                .IsRequired();
            });

            builder.ComplexProperty(
                o => o.ShippingAddress, builder =>
            {
                builder.Property(a => a.FirstName)
                .HasMaxLength(50)
                .IsRequired();

                builder.Property(a => a.LastName)
                .HasMaxLength(50)
                .IsRequired();

                builder.Property(a => a.EmailAddress)
                .HasMaxLength(100)
                .IsRequired();

                builder.Property(a => a.Country)
                .HasMaxLength(50)
                .IsRequired();

                builder.Property(a => a.State)
                .HasMaxLength(50)
                .IsRequired();

                builder.Property(a => a.ZipCode)
                .HasMaxLength(5)
                .IsRequired();
            });

            builder.ComplexProperty(
            o => o.BillingAddress, builder =>
            {
                builder.Property(a => a.FirstName)
                            .HasMaxLength(50)
                            .IsRequired();

                builder.Property(a => a.LastName)
                            .HasMaxLength(50)
                            .IsRequired();

                builder.Property(a => a.EmailAddress)
                            .HasMaxLength(100)
                            .IsRequired();

                builder.Property(a => a.Country)
                            .HasMaxLength(50)
                            .IsRequired();

                builder.Property(a => a.State)
                            .HasMaxLength(50)
                            .IsRequired();

                builder.Property(a => a.ZipCode)
                            .HasMaxLength(5)
                            .IsRequired();
            });

            builder.ComplexProperty(o => o.Payment, builider =>
            {
                builider.Property(p => p.CardName)
                .HasMaxLength(16);

                builider.Property(p => p.CardNumber)
                .HasMaxLength(16)
                .IsRequired();

                builider.Property(p => p.Expiration)
                .HasMaxLength(10)
                .IsRequired();

                builider.Property(p => p.CVV)
                .HasMaxLength(3)
                .IsRequired();

                builider.Property(p => p.PaymentMethod)
                .IsRequired();
            });

            builder.Property(o => o.Status)
                .HasDefaultValue(OrderStatus.Pending)
                .HasConversion(
                    os => os.ToString(),
                    dbOs => Enum.Parse<OrderStatus>(dbOs));

            builder.Property(o => o.TotalPrice);
        }
    }
}
