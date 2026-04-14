using Ordering.Application.Orders.Queries.GetOrderByCustomer;

namespace Ordering.API.Endpoints
{

    public record GetOrderByCustomerResponse(IEnumerable<OrderDto> Orders);

    public class GetOrdersByCustomer : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/orders/customers/{customerId}", async (Guid customerId, ISender sender) =>
            {
                var result =  await sender.Send(new GetOrderByCustomerQuery(customerId));

                var response = result.Adapt<GetOrderByCustomerResponse>();

                return Results.Ok(response);
            });
        }
    }
}
