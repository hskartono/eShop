﻿using Catalog.Core.Entities;
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

            var result = new ProductDto()
            {
                Id = item.Id,
                Name = item.Name,
                Package = item.Package,
                Image = item.Image,
                Description = item.Description,
                Price = item.Price
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

            var productStores = await _productService.SerchProductWithCategory(q, categoryId);

            // kelompokkan per store
            var dict = new Dictionary<int, List<StoreProduct>>();
            var dicParent = new Dictionary<int, StoreProduct>();
            foreach(var item in productStores)
            {
                if (!dict.ContainsKey(item.Id))
                {
                    dict.Add(item.Id, new List<StoreProduct>());
                    dicParent.Add(item.Id, item);
                }

                dict[item.Id].Add(item);
            }

            var result = new List<StoreProductDto>();

            // masukkan ke dalam list
            foreach (var id in dicParent.Keys)
            {
                var storeProduct = dicParent[id];
                var item = new StoreProductDto()
                {
                    Id = storeProduct.Id,
                    StoreName = (storeProduct.Store == null) ? "Unknown" : storeProduct.Store.Name,
                    Products = new List<ProductDto>()
                };

                foreach(var productStore in dict[storeProduct.Id])
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
