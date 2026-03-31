namespace Basket.API.Basket.StoreBasket
{
    public record StoreBasketRequest(ShoppingCart ShoppingCart);

    public record StoreBasketResponse(string UserName);

    public class StoreBasketEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/basket", async (StoreBasketRequest request, ISender sender) =>
            {
                // 1. adapt the incoming request to a command
                var command = request.Adapt<StoreBasketCommand>();

                // 2. sending [passing] the command object to mediator for handling and getting the result
                var result = await sender.Send(command);

                // 3. adapt the result object to response object
                var response = result.Adapt<StoreBasketResponse>();

                return Results.Created($"basket/{response.UserName}", response);
            });
        }
    }
}
