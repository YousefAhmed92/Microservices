namespace Catalog.API.Exceptions
{
    public class ProductNotFoundException : Exception
    {
        public ProductNotFoundException() : base("product is not found")
        {
        }
    }
}
