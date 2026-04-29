namespace Shopping.Web.Models.Catalog
{
    public class ProductModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public string ImageFile { get; set; } = string.Empty;

        public List<string> Category { get; set; } = new();
    }

    public record GetProductByIdResponse(ProductModel Product);

    public record GetProductResponse(IEnumerable<ProductModel> Products);

    public record GetProductByCategoryResponse(IEnumerable<ProductModel> Products);
}
