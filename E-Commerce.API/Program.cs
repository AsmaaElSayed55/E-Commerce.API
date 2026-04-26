using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Presistence.Data;

namespace E_Commerce.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // 1. REGISTER SERVICES (Must be before builder.Build())
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            // FIX: Move this registration UP here!
            builder.Services.AddScoped<IDataSeeding, DataSeeding>();

            // ---------------------------------------------------------
            var app = builder.Build(); // The container is now locked
            // ---------------------------------------------------------

            // 2. EXECUTE SEEDING (Must be after builder.Build())
            // Use a scope to resolve scoped services
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var seeder = services.GetRequiredService<IDataSeeding>();
                    // This triggers Migrate() inside your class to create the DB
                    seeder.SeedData();
                }
                catch (Exception ex)
                {
                    // Log the error if seeding fails (check your Console window!)
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
            }

            // 3. CONFIGURE MIDDLEWARE
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.MapControllers();
            app.Run();
        }
    }
}


// Module ==> Entities
// Product Module
// 3 entities => [Product, ProductType, ProductBrand]
// Product => int Id, string Name, string Description,  decimal Price, string PictureUrl
// ProductBrand => int Id, string Name 

// Relationships ==> 1-M Product , productType || 1-M Product, productBrand 

// DomainModels , Domain ==> Core folder








