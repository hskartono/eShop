using Ardalis.Specification;
using ShoppingCart.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Core.Specifications
{
    public class CartByStoreIdSpecification : Specification<Cart>
    {
        public CartByStoreIdSpecification(int storeId, int ownerId)
        {
            Query
                .Where(e => e.StoreId == storeId && e.OwnerId == ownerId)
                .Include(e=>e.Items);
        }
    }
}
