using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Presistence.Data;
using Presistence.Repositories;

namespace E_Commerce.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            builder.Services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            // FIX: Move this registration UP here!
            builder.Services.AddScoped<IDataSeeding, DataSeeding>();

            var app = builder.Build(); 
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var seeder = services.GetRequiredService<IDataSeeding>();
                    // This triggers Migrate() inside your class to create the DB
                    await seeder.SeedDataAsync();
                }
                catch (Exception ex)
                {
                    // Log the error if seeding fails (check your Console window!)
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
            }
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            // 3. CONFIGURE MIDDLEWARE
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.MapOpenApi();
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








