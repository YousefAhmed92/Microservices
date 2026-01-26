namespace Catalog.API.Products.CreateProduct
{

    public record CreateProductRequest(string Name, List<string> Category, string Description, decimal Price, string ImageFile);

    public record CreateProductResult(Guid Id);

    public class CreateProductEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/Products", async (CreateProductRequest request, IMediator sender) =>
            {
                // map our request to command object and send it to mediator for handling
                var command = request.Adapt<CreateProductCommand>();

                // send the command to mediator 
                // and mediator will be trigger our command handler class
                var result =  await sender.Send(command);

                // convert the result to response object
                var response = result.Adapt<CreateProductResult>();

                return Results.Created($"/Products/{response.Id}", response);
            });
        }
    }
}
