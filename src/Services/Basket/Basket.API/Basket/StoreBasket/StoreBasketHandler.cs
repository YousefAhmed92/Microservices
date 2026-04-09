using Discount.gRPC;

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

    public class StoreBasketCommandHandler 
        (IBasketRepository repository, DiscountProtoService.DiscountProtoServiceClient discount)
        : ICommandHandler<StoreBasketCommand, StoreBasketResult>
    {
        public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
        {
            await DeductDiscount(command.ShoppingCart, cancellationToken);

            await repository.StoreBasket(command.ShoppingCart, cancellationToken);

            return new StoreBasketResult(command.ShoppingCart.UserName);
        }

        private async Task DeductDiscount(ShoppingCart cart, CancellationToken cancellationToken)
        {
            foreach (var item in cart.Items)
            {
                var copun = await discount.GetDiscountAsync(new GetDiscountRequest { ProductName = item.ProductName }, cancellationToken: cancellationToken);
                item.Price -= copun.Amount;
            }
        }
    }
}
