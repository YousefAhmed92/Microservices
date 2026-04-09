using BuildingBlocks.Exceptions.Handler;
using Discount.gRPC;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);


// ADD THIS TEMPORARILY
Console.WriteLine("CONNECTION STRING: " + builder.Configuration.GetConnectionString("Database"));

var assembly = typeof(Program).Assembly;

builder.Services.AddCarter();

builder.Services.AddMediatR(configurations =>
{
    configurations.RegisterServicesFromAssembly(assembly);
    configurations.AddOpenBehavior(typeof(ValidationBehavior<,>));
    configurations.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

builder.Services.AddMarten(options =>
{
    options.Connection(builder.Configuration.GetConnectionString("Database")!);
    options.Schema.For<ShoppingCart>().Identity(x => x.UserName);
}).UseLightweightSessions();

builder.Services.AddScoped<IBasketRepository, BasketRepository>();

builder.Services.Decorate<IBasketRepository, CachedBasketRepository>();

builder.Services.AddStackExchangeRedisCache(cache =>
{
    cache.Configuration = builder.Configuration.GetConnectionString("Redis")!;
});

builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(options =>
{
    options.Address = new Uri(builder.Configuration["GrpcSettings:DiscountUrl"]!);
});

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

builder.Services.AddHealthChecks()
    .AddRedis(builder.Configuration.GetConnectionString("Redis")!)
    .AddNpgSql(builder.Configuration.GetConnectionString("Database")!);

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapCarter();

app.UseExceptionHandler(opt => { });

app.UseHealthChecks("/health", 
    new HealthCheckOptions
    {
        ResponseWriter = HealthChecks.UI.Client.UIResponseWriter.WriteHealthCheckUIResponse
    });

app.Run();
