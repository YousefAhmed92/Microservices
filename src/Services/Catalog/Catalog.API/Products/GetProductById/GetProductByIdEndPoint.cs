
namespace Catalog.API.Products.GetProductById
{
    public record GetProguctByIdResponse(Product Product);

    public class GetProductByIdEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/Products/{id}", async (Guid id, ISender sender) =>
            {
                var result = await sender.Send(new GetProductByIdQuery(id));

                var response = result.Adapt<GetProguctByIdResponse>();

                return Results.Ok(response);
            });
        }
    }
}
