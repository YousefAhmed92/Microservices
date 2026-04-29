
namespace Basket.API.Basket.DeleteBasket
{
    //public record DeleteBasketRequest(string UserName);

    public record DeleteBasketResponse(bool IsSuccess);

    public class DeleteBasketEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("basket/{userName}", async (string userName, ISender sender) =>
            {

                // 1. send the command to mediator for handling
                var result = await sender.Send(new DeleteBasketCommand(userName));

                // 2. adapt the result to response object
                var response = result.Adapt<DeleteBasketResponse>();

                // 3. return the response
                return Results.Ok(response);
            });
        }
    }
}
