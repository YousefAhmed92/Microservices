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

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapCarter();

app.Run();
