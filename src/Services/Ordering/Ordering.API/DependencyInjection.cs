namespace Ordering.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services)
        {
            return services;
        }

        public static WebApplicationBuilder AddApiServices(this WebApplicationBuilder app)
        {
            return app;
        }
    }
}
