using Ardalis.Specification;
using Catalog.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Core.Specification
{
    public class ProductByIdAndStoreSpecification: Specification<StoreProduct>, ISingleResultSpecification
    {
        public ProductByIdAndStoreSpecification(int productid, int storeid)
        {
            Query
                .Where(e => e.ProductId == productid && e.StoreId == storeid)
                .Include(e=>e.Product);
        }
    }
}
