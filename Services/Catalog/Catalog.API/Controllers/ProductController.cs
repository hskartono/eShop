using Catalog.Core.Entities;
using Catalog.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("{productid}/{storeid}")]
        public async Task<IActionResult> GetByIdAndStore(int productid, int storeid)
        {
            var item = await _productService.GetProductDetail(productid, storeid);
            if (item is null)
                return NotFound();

            var hostname = Request.Host.Host;
            var port = Request.Host.Port;

            var result = new ProductDto()
            {
                Id = item.Id,
                Name = item.Name,
                Package = item.Package,
                Image = item.Image,
                Description = item.Description,
                Price = item.Price,
                HostName = $"{hostname}:{port}"
            };

            return Ok(result);
        }

        [HttpGet()]
        public async Task<IActionResult> SearchByNameOrCategory()
        {
            string q = string.Empty;
            int? categoryId = null;

            if (Request.Query.ContainsKey("q"))
            {
                q = Request.Query["q"];
            }

            if (Request.Query.ContainsKey("categoryId"))
            {
                int cateid = 0;
                if(int.TryParse(Request.Query["categoryId"], out cateid))
                {
                    categoryId = cateid;
                }
            }

            var hostname = System.Net.Dns.GetHostName();
            var productStores = await _productService.SerchProductWithCategory(q, categoryId);

            // kelompokkan per store
            var dict = new Dictionary<int, List<StoreProduct>>();
            var dicParent = new Dictionary<int, StoreProduct>();
            foreach(var item in productStores)
            {
                if (!dict.ContainsKey(item.StoreId.Value))
                {
                    dict.Add(item.StoreId.Value, new List<StoreProduct>());
                    dicParent.Add(item.StoreId.Value, item);
                }

                dict[item.StoreId.Value].Add(item);
            }

            var result = new List<StoreProductDto>();

            // masukkan ke dalam list
            foreach (var id in dicParent.Keys)
            {
                var storeProduct = dicParent[id];
                var item = new StoreProductDto()
                {
                    Id = id,
                    StoreName = (storeProduct.Store == null) ? "Unknown" : storeProduct.Store.Name,
                    Products = new List<ProductDto>(),
                    HostName = hostname
                };

                foreach(var productStore in dict[id])
                {
                    var product = productStore.Product;
                    item.Products.Add(new ProductDto()
                    {
                        Id = product.Id,
                        Name = product.Name,
                        Package = product.Package,
                        Description = product.Description,
                        Image = product.Image,
                        Price = product.Price
                    });
                }
                result.Add(item);
            }

            return Ok(result);
        }
    }
}
