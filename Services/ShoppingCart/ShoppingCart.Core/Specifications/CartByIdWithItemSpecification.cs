using Ardalis.Specification;
using ShoppingCart.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Core.Specifications
{
    public class CartByIdWithItemSpecification : Specification<Cart>
    {
        public CartByIdWithItemSpecification(int id)
        {
            Query
                .Where(e => e.Id == id)
                .Include(e => e.Items);
        }
    }
}
