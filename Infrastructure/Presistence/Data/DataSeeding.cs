using System.Text.Json;
namespace Presistence.Data
{
    public class DataSeeding (StoreDbContext _dbContext) : IDataSeeding
    {
        public async Task SeedDataAsync()
        {
            try
            {
                var pendingMigrations = await _dbContext.Database.GetPendingMigrationsAsync();
                // Any Pending Migration ==> Apply database
                if ((pendingMigrations).Any())
                {
                    await _dbContext.Database.MigrateAsync();

                }

                if (!_dbContext.ProductBrands.Any())
                {
                    // Seed by order foreign key
                    // 1. ProductType
                    // 2. ProductBrand
                    // 3. Product
                    //  var productBrandData = File.ReadAllText("C:\\D\\Projects\\API Projects\\E-Commerce\\Infrastructure\\Presistence\\Data\\DataSeed\\brands.json"); // Static Path

                    var productBrandData = File.OpenRead("..\\Infrastructure\\Presistence\\Data\\DataSeed\\brands.json"); // Dynamic Path

                    var productBrands = await JsonSerializer.DeserializeAsync<List<ProductBrand>>(productBrandData);

                    if (productBrands is not null && productBrands.Any())
                    {
                        await _dbContext.ProductBrands.AddRangeAsync(productBrands);
                    }

                }

                if (!_dbContext.ProductTypes.Any())
                {

                    var productTypeData = File.OpenRead("..\\Infrastructure\\Presistence\\Data\\DataSeed\\types.json"); // Dynamic Path

                    var productTypes = await JsonSerializer.DeserializeAsync<List<ProductType>>(productTypeData);

                    if (productTypes is not null && productTypes.Any())
                    {
                        await _dbContext.ProductTypes.AddRangeAsync(productTypes);
                    }

                }

                if (!_dbContext.Products.Any())
                {

                    var productData = File.OpenRead("..\\Infrastructure\\Presistence\\Data\\DataSeed\\product.json"); // Dynamic Path

                    var products = await JsonSerializer.DeserializeAsync<List<Product>>(productData);

                    if (products is not null && products.Any())
                    {
                        await _dbContext.Products.AddRangeAsync(products);
                    }

                }

                await _dbContext.SaveChangesAsync();

            }
            catch (Exception ex)
            {

                // Handel Ex
            }
        }
    }
}
