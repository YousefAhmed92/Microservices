using Discount.gRPC.Models;
using Microsoft.EntityFrameworkCore;

namespace Discount.gRPC.Data
{
    public class DiscountDbContext : DbContext
    {
        public DiscountDbContext(DbContextOptions<DiscountDbContext> context) : base(context)
        {
            
        }

        public DbSet<Coupon> Coupons { get; set; }
    }
}
