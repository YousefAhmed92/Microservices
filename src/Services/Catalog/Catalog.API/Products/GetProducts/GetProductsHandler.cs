
using Marten.Pagination;

namespace Catalog.API.Products.GetProducts
{
    public record GetProguctsQuery(int? PageNumber = 1, int? PageSize = 10) : IQuery<GetProguctsResult>;
    public record GetProguctsResult(IEnumerable<Product> Products);

    public class GetProductsQueryHandler(IQuerySession session)
        : IQueryHandler<GetProguctsQuery, GetProguctsResult>
    {
        public async Task<GetProguctsResult> Handle(GetProguctsQuery request, CancellationToken cancellationToken)
        {
            var products = await session.Query<Product>()
                .ToPagedListAsync(request.PageNumber ?? 1, request.PageSize ?? 10, cancellationToken);

            return new GetProguctsResult(products);
        }
    }
}
