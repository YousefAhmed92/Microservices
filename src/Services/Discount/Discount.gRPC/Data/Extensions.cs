using Microsoft.EntityFrameworkCore;

namespace Discount.gRPC.Data
{
    public static class Extensions
    {
        public static IApplicationBuilder AutoMigrate(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();

            using var context = scope.ServiceProvider.GetRequiredService<DiscountDbContext>();

            context.Database.MigrateAsync();

            return app;
        }
    }
}
