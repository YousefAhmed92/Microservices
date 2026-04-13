using Microsoft.EntityFrameworkCore;
using Ordering.Domain.Models;

namespace Ordering.Application.Data
{
    public interface IApplicationDbContext
    {
        public DbSet<Customer> customers { get; }

        public DbSet<Product> products { get; }

        public DbSet<Order> orders { get; }
        public DbSet<OrderItem> orderItems { get; }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
