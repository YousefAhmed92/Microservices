using BuildingBlocks.Pagination;

namespace Ordering.Application.Orders.Queries.GetOrders
{
    public class GetOrdersHandler(IApplicationDbContext context)
        : IQueryHandler<GetOrdersQuery, GetOrdersResult>
    {
        public async Task<GetOrdersResult> Handle(GetOrdersQuery query, CancellationToken cancellationToken)
        {
            var pageIndex= query.PaginationRequest.pageIndex;

            var pageSize = query.PaginationRequest.pageSize;

            var totalCount = await context.orders.LongCountAsync(cancellationToken);

            var orders = await context.orders
                .Include(x => x.OrderItems)
                .OrderBy(x => x.OrderName.Value)
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return new GetOrdersResult(
                new PaginationResult<OrderDto>
                (pageIndex, pageSize, totalCount, orders.ToOrderDtoList()));
        }
    }
}
