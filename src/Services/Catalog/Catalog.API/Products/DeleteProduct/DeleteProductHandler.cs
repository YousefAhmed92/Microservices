
namespace Catalog.API.Products.DeleteProduct
{
    public record DeleteProductCommand(Guid Id): ICommand<DeleteProductResult>;

    public record DeleteProductResult(bool IsSuccess);

    public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
    {
        public DeleteProductCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Id can not be null");
        }
    }

    public class DeleteProductCommandHandler
        (IDocumentSession session, ILogger<DeleteProductCommandHandler> logger)
        : ICommandHandler<DeleteProductCommand, DeleteProductResult>
    {
        public async Task<DeleteProductResult> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling DeleteProductCommand for Id: {Id}", request.Id);

            session.Delete<Product>(request.Id);

            await session.SaveChangesAsync(cancellationToken);

            logger.LogInformation("Deleted Product with Id: {Id}", request.Id);

            return new DeleteProductResult(true);
        }
    }
}
