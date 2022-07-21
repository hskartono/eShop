namespace Catalog.API.Controllers
{
    public class StoreProductDto
    {
        public int Id { get; set; }
        public string StoreName { get; set; }
        public List<ProductDto> Products { get; set; }

    }
}
