
namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductCommand(string Name, string Description, decimal Price, string ImageFile, List<string> Category)
        : ICommand<CreateProductResult>;

    public record createProductResult(Guid Id);

    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
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

    public class CreateProductCommandHandler
        (IDocumentSession session, ILogger<CreateProductCommandHandler> logger)
        : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            logger.LogInformation("create product command handler");

            var product = new Product
            {
                Name = command.Name,
                Category = command.Category,
                Description = command.Description,
                Price = command.Price,
                ImageFile = command.ImageFile
            };

            session.Store(product);

            await session.SaveChangesAsync(cancellationToken);

            return new CreateProductResult(product.Id);
        }
    }
}
