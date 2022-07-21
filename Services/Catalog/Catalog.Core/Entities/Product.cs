namespace Catalog.Core.Entities
{
    public class Product : BaseEntity
    {
        public Product()
        {
            ProductCategoryId = null;
            ProductCategory = null;
        }

        public Product(string name, string package, string image, string description, double price, int? productCategoryId)
        {
            Name = name;
            Package = package;
            Image = image;
            Description = description;
            Price = price;
            ProductCategoryId = productCategoryId;
            ProductCategory = null;
        }

        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Package { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double Price { get; set; }

        public int? ProductCategoryId { get; set; }

        public ProductCategory? ProductCategory { get; set; }
    }
}