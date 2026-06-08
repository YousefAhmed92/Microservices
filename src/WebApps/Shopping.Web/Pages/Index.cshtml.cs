using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shopping.Web.Models.Catalog;

namespace Shopping.Web.Pages
{
    public class IndexModel
        (ICatalogService catalogService, IBasketService basketService, ILogger<IndexModel> logger)
        : PageModel
    {
        public IEnumerable<ProductModel> ProductList { get; set; } = new List<ProductModel>();

        public async Task<IActionResult> OnGet()
        {
            logger.LogInformation("index page product list");

            var result = await catalogService.GetProducts();

            ProductList = result.Products;

            return Page();
        }
    }
}
