﻿using Ardalis.Specification;
using ShoppingCart.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Core.Specifications
{
    public class CartByOwnerIdSpecification : Specification<Cart>
    {
        public CartByOwnerIdSpecification(int id)
        {
            Query
                .Where(e => e.OwnerId == id)
                .Include(e => e.Items);
        }
    }
}
