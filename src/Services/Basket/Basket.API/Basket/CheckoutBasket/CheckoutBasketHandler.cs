using Basket.API.Dtos;
using BuildingBlocksMessaging.Events;
using MassTransit;

namespace Basket.API.Basket.CheckoutBasket
{
    public record CheckoutBasketCommand(BastetCheckoutDto BastetCheckoutDto)
    : ICommand<CheckoutBasketResult>;

    public record CheckoutBasketResult(bool IsSuccess);

    public class BasketCheckoutValidator : AbstractValidator<CheckoutBasketCommand>
    {
        public BasketCheckoutValidator()
        {
            RuleFor(x => x.BastetCheckoutDto)
                .NotNull()
                .WithMessage("checkout information is required");

            RuleFor(x => x.BastetCheckoutDto.UserName)
                .NotEmpty()
                .WithMessage("username is required");
        }
    }

    public class CheckoutBasketCommandHandler
        (IBasketRepository basketRepository, IPublishEndpoint publishEndPoint)
        : ICommandHandler<CheckoutBasketCommand, CheckoutBasketResult>
    {
        public async Task<CheckoutBasketResult> Handle(CheckoutBasketCommand command, CancellationToken cancellationToken)
        {
            var basket = await basketRepository.GetBasket(command.BastetCheckoutDto.UserName);

            if (basket is null)
            {
                return new CheckoutBasketResult(false);
            }

            var eventMessage = command.BastetCheckoutDto.Adapt<BasketCheckoutEvent>();

            eventMessage.TotalPrice = basket.TotalPrice;

            await publishEndPoint.Publish(eventMessage, cancellationToken);

            await basketRepository.DeleteBasket(command.BastetCheckoutDto.UserName, cancellationToken);

            return new CheckoutBasketResult(true);
        }
    }
}
