using Catalog.Core.Entities;
using Catalog.Core.Interfaces;
using Catalog.Core.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Core.Services
{
    public class ProductService : IProductService
    {
        private readonly IRepository<StoreProduct> _storeProductRepository;

        public ProductService(IRepository<StoreProduct> storeProductRepository)
        {
            _storeProductRepository = storeProductRepository;
        }

        public async Task<Product?> GetProductDetail(int productid, int storeid)
        {
            var specification = new ProductByIdAndStoreSpecification(productid, storeid);
            var productStores = await _storeProductRepository.ListAsync(specification);
            if(productStores == null || productStores.Count == 0)
            {
                return null;
            }

            return productStores.First()?.Product;
        }

        public async Task<IEnumerable<StoreProduct>> SerchProductWithCategory(string searchQuery, int? categoryId)
        {
            var specification = new ProductListQueryAndCatalogSpecification(searchQuery, categoryId);
            var result = await _storeProductRepository.ListAsync(specification);
            if(result == null)
            {
                return new List<StoreProduct>();
            }

            return result;
        }
    }
}
