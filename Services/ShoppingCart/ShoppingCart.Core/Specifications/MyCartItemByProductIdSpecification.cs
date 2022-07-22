using Ardalis.Specification;
using ShoppingCart.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Core.Specifications
{
    public class MyCartItemByProductIdSpecification : Specification<CartItem>
    {
        public MyCartItemByProductIdSpecification(int ownerId, int productId)
        {
            Query
                .Where(e => e.ProductId == productId && e.Cart.OwnerId == ownerId)
                .Include(e => e.Cart);
        }
    }
}
