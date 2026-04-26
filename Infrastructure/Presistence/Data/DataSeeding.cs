using Domain.Contracts;
using System.Text.Json;
namespace Presistence.Data
{
    public class DataSeeding (StoreDbContext _dbContext) : IDataSeeding
    {
        public void SeedData()
        {
            try
            {
                // Any Pending Migration ==> Apply database
                if (_dbContext.Database.GetPendingMigrations().Any())
                {
                    _dbContext.Database.Migrate();

                }

                if (!_dbContext.ProductBrands.Any())
                {
                    // Seed by order foreign key
                    // 1. ProductType
                    // 2. ProductBrand
                    // 3. Product
                    //  var productBrandData = File.ReadAllText("C:\\D\\Projects\\API Projects\\E-Commerce\\Infrastructure\\Presistence\\Data\\DataSeed\\brands.json"); // Static Path

                    var productBrandData = File.ReadAllText("..\\Infrastructure\\Presistence\\Data\\DataSeed\\brands.json"); // Dynamic Path

                    var productBrands = JsonSerializer.Deserialize<List<ProductBrand>>(productBrandData);

                    if (productBrands is not null && productBrands.Any())
                    {
                        _dbContext.ProductBrands.AddRange(productBrands);
                    }

                }

                if (!_dbContext.ProductTypes.Any())
                {

                    var productTypeData = File.ReadAllText("..\\Infrastructure\\Presistence\\Data\\DataSeed\\types.json"); // Dynamic Path

                    var productTypes = JsonSerializer.Deserialize<List<ProductType>>(productTypeData);

                    if (productTypes is not null && productTypes.Any())
                    {
                        _dbContext.ProductTypes.AddRange(productTypes);
                    }

                }

                if (!_dbContext.Products.Any())
                {

                    var productData = File.ReadAllText("..\\Infrastructure\\Presistence\\Data\\DataSeed\\product.json"); // Dynamic Path

                    var products = JsonSerializer.Deserialize<List<Product>>(productData);

                    if (products is not null && products.Any())
                    {
                        _dbContext.Products.AddRange(products);
                    }

                }

                _dbContext.SaveChanges();

            }
            catch (Exception ex)
            {

                // Handel Ex
            }
        }
    }
}
