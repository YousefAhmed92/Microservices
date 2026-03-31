namespace Catalog.API.Products.UpdateProduct
{
    public record UpdateProductCommand(Guid Id, string Name, string Description, string ImageFile, decimal Price, List<string> Category)
        : ICommand<UpdateProductResult>;

    public record UpdateProductResult(bool IsSuccess);

    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Id can not be null");

            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(100)
                .WithMessage("name can not be null");

            RuleFor(x => x.Description)
                .NotEmpty()
                .MaximumLength(500)
                .WithMessage("Description can not be null");

            RuleFor(x => x.Price)
                .GreaterThan(0)
                .WithMessage("price can not be null");
           
            RuleFor(x => x.ImageFile)
                .MaximumLength(200)
                .WithMessage("Description can not be null");

            RuleFor(x => x.Category)
                .NotEmpty()
                .WithMessage("Description can not be null");
        }
    }
    public class UpdateProductCommandHandler
        (IDocumentSession session, ILogger<UpdateProductCommandHandler> logger)
        : ICommandHandler<UpdateProductCommand, UpdateProductResult>
    {
        public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling UpdateProductCommand for Id: {Id}", command.Id);

            var product = await session.LoadAsync<Product>(command.Id, cancellationToken);

            if (product is null)
            {
                throw new ProductNotFoundException();
            }

            product.Name = command.Name;
            product.Description = command.Description;
            product.Price = command.Price;
            product.ImageFile = command.ImageFile;
            product.Category = command.Category;

            session.Update(product);

            await session.SaveChangesAsync();

            return new UpdateProductResult(true);
        }
    }
}
