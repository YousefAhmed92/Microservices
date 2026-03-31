
namespace Basket.API.Basket.StoreBasket
{

    public record StoreBasketCommand(ShoppingCart ShoppingCart): ICommand<StoreBasketResult>;

    public record StoreBasketResult(string UserName);

    public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
    {
        public StoreBasketCommandValidator()
        {
            RuleFor(x => x.ShoppingCart)
                .NotNull()
                .WithMessage("ShoppingCart can not be null");

                RuleFor(x => x.ShoppingCart.UserName)
                .NotEmpty()
                .WithMessage("UserName can not be null");
        }
    }

    public class StoreBasketCommandHandler : ICommandHandler<StoreBasketCommand, StoreBasketResult>
    {
        public async Task<StoreBasketResult> Handle(StoreBasketCommand request, CancellationToken cancellationToken)
        {
            ShoppingCart cart = request.ShoppingCart;

            //TODO: USE Marten UPSERT
            //TODO: update cache

            return new StoreBasketResult("Yousef");
        }
    }
}
