using Ardalis.Specification;
using ShoppingCart.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Core.Specifications
{
    public class ExistingCartByProductAndStoreSpecification : Specification<CartItem>
    {
        public ExistingCartByProductAndStoreSpecification(int productId)
        {
            Query
                .Where(e => e.ProductId == productId)
                .Include(e => e.Cart);
        }

        public ExistingCartByProductAndStoreSpecification(int productId, int storeId)
        {
            Query
                .Where(e => e.ProductId == productId && e.Cart.StoreId == storeId)
                .Include(e => e.Cart);
        }
    }
}
