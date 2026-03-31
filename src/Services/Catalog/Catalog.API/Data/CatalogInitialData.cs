using Marten.Schema;

namespace Catalog.API.Data
{
    public class CatalogInitialData : IInitialData
    {
        public async Task Populate(IDocumentStore store, CancellationToken cancellation)
        {
            using var session = store.LightweightSession();

            if (await session.Query<Product>().AnyAsync())
                return;

            session.Store<Product>(GetPreConfiguredProducts);

            await session.SaveChangesAsync();
        }
    public static IEnumerable<Product> GetPreConfiguredProducts =>
        new List<Product>
    {
            new Product
            {
                Name = "IPhone X",
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec sed odio dui. ",
                ImageFile = "product-1.png",
                Price = 950.00M,
                Category = new List<string> { "Smart Phone", "Electronics" }
            },
            new Product
            {
                Name = "Samsung 10",
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec sed odio dui.",
                ImageFile = "product-2.png",
                Price = 840.00M,
                Category = new List<string> { "Smart Phone", "Electronics" }
            },
            new Product
            {
                Name = "Huawei Plus",
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec sed odio dui.",
                ImageFile = "product-3.png",
                Price = 650.00M,
                Category = new List<string> { "Smart Phone", "Electronics" }
            },
            new Product
            {
                Name = "Xiaomi Mi 9",
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec sed odio dui.",
                ImageFile = "product-4.png",
                Price = 470.00M,
                Category = new List<string> { "Smart Phone", "Electronics" }
            },
            new Product
            {
                Name = "HTC U11+ Plus",
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec sed odio dui.",
                ImageFile = "product-5.png",
                Price = 380.00M,
                Category = new List<string> { "Smart Phone", "Electronics" }
            },
            new Product
            {
                Name = "LG G7 ThinQ",
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec sed odio dui.",
                ImageFile = "product-6.png",
                Price = 240.00M,
                Category = new List<string> { "Smart Phone", "Electronics" }
            }
    };

    } 
}
