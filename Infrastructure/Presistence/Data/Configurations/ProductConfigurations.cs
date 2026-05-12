namespace Presistence.Data.Configurations
{
    internal class ProductConfigurations : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasOne(p => p.ProductBrand).WithMany().HasForeignKey(p => p.BrandId); // relationship 1-M with ProductBrand

            builder.HasOne(p=>p.ProductType).WithMany().HasForeignKey(p=>p.TypeId); // relationship 1-M with ProductType

            builder.Property(p => p.Price).HasColumnType("decimal(15,2)"); // Make Price Properity from type decimal (15,2)
        }
    }
}
