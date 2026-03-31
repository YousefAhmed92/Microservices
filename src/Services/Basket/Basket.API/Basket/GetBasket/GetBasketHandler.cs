
namespace Basket.API.Basket.GetBasket
{

    public record GetBasketQuery(string UserName): IQuery<GetBasketResult>;

    public record GetBasketResult(ShoppingCart ShoppingCart);

    public class GetBasketHandler : IQueryHandler<GetBasketQuery, GetBasketResult>
    {
        public async Task<GetBasketResult> Handle(GetBasketQuery request, CancellationToken cancellationToken)
        {
            //TODO: Get the shopping cart from the database Repository pattern

            return new GetBasketResult(new ShoppingCart("Yousef"));
        }
    }
}
