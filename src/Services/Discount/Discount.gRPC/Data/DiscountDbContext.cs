using Discount.gRPC.Models;
using Microsoft.EntityFrameworkCore;

namespace Discount.gRPC.Data
{
    public class DiscountDbContext : DbContext
    {
        public DiscountDbContext(DbContextOptions<DiscountDbContext> context) : base(context)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Coupon>().HasData(
                new Coupon { Id = 1, Amount = 3, Description = "First Description", ProductName = "Iphone" },
                new Coupon { Id = 2, Amount = 35, Description = "Second Description", ProductName = "LapTop" }
                );
        }

        public DbSet<Coupon> Coupons { get; set; }
    }
}
