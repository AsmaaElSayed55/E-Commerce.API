
namespace Domain.Entities.ProductModule
{
    public class Product : BaseEntity<int>
    {
        public string Description { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string PictureUrl { get; set; }

        // 1-M relationship with ProductType

        public  ProductType ProductType { get; set; } // Navigation property to ProductType
        public int TypeId { get; set; } // Foreign key for ProductType

        // 1-M relationship with ProductBrand
        public ProductBrand ProductBrand { get; set; } // Navigation property to ProductBrand
        public int BrandId { get; set; } // Foreign key for ProductBrand
    }
}
