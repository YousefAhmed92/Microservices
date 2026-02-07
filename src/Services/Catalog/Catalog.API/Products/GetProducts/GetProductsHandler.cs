
namespace Catalog.API.Products.GetProducts
{
    public record GetProguctsQuery() : IQuery<GetProguctsResult>;
    public record GetProguctsResult(IEnumerable<Product> Products);

    public class GetProductsQueryHandler(IQuerySession session, ILogger<GetProductsQueryHandler> logger)
        : IQueryHandler<GetProguctsQuery, GetProguctsResult>
    {
        public async Task<GetProguctsResult> Handle(GetProguctsQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Get Products Handler");

            var products = await session.Query<Product>().ToListAsync(cancellationToken);

            return new GetProguctsResult(products);
        }
    }
}
