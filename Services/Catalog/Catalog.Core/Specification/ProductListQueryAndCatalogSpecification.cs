using Ardalis.Specification;
using Catalog.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Core.Specification
{
    public class ProductListQueryAndCatalogSpecification : Specification<StoreProduct>
    {
        public ProductListQueryAndCatalogSpecification(string query, int? categoryId = null)
        {
            Query
                .Where(e => e.Product.Name.Contains(query));

            if (categoryId.HasValue)
                Query.Where(e => e.Product.ProductCategoryId == categoryId.Value);

            Query.Include(e => e.Product).Include(e => e.Store);
        }
    }
}
