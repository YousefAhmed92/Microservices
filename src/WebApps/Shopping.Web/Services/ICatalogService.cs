using Shopping.Web.Models.Catalog;

namespace Shopping.Web.Services
{
    public interface ICatalogService
    {
        [Get("/catalog-service/products?PageNumber={PageNumber}&PageSize={PageSize}")]
        Task<GetProductResponse> GetProducts(int? PageNumber = 1, int? PageSize = 10);

        [Get("/catalog-service/products/{id}")]
        Task<GetProductByIdResponse> GetProduct(Guid id);

        [Get("/catalog-service/products/category/{category}")]
        Task<GetProductByCategoryResponse> GetProductByCategory(string category);
    }
}
