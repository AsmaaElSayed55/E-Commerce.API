
namespace E_Commerce.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
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








