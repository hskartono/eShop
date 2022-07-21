using ShoppingCart.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Core.Interfaces
{
    public interface ICartService
    {
        Task<Cart?> CreateCart(int productId, int storeId);
        Task<Cart?> GetById(int id);
        Task AddQty(int id);
        Task SubQty(int id);
        Task Checkout(int id);
    }
}
