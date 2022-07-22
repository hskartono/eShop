using ShoppingCart.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Core.Interfaces
{
    public interface ICatalogApiClient
    {
        Task<Product?> GetProduct(int id, int storeId);
    }
}
