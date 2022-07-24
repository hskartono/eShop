namespace Catalog.API.Controllers
{
    public class StoreProductDto
    {
        public int Id { get; set; }
        public string StoreName { get; set; } = string.Empty;
        public List<ProductDto> Products { get; set; }
        public string HostName { get; set; } = string.Empty;

    }
}
