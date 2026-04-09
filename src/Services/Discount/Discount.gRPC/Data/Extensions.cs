using Microsoft.EntityFrameworkCore;

namespace Discount.gRPC.Data
{
    public static class Extensions
    {
        public static async Task<IApplicationBuilder> AutoMigrate(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();

            using var context = scope.ServiceProvider.GetRequiredService<DiscountDbContext>();

            await context.Database.MigrateAsync();

            return app;
        }
    }
}
