using Ordering.Application.Extensions;

namespace Ordering.Application.Orders.Queries.GetOrdersByName
{
    public class GetOrderByNameHandler(IApplicationDbContext context)
        : IQueryHandler<GetOrderByNameQuery, GetOrdersByNameResult>
    {
        public async Task<GetOrdersByNameResult> Handle(GetOrderByNameQuery query, CancellationToken cancellationToken)
        {
            var orders = await context.orders
                .Include(x => x.OrderItems)
                .AsNoTracking()
                .Where(x => x.OrderName.Value.Contains(query.name))
                .OrderBy(x => x.OrderName)
                .ToListAsync();


            return new GetOrdersByNameResult(orders.ToOrderDtoList());
        }

    }
}
