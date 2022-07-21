using ShoppingCart.Core.Entities;
using ShoppingCart.Core.Interfaces;
using ShoppingCart.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Core.Services
{
    public class CartService : ICartService
    {
        private readonly IRepository<CartItem> _cartItemRepository;
        private readonly IRepository<Cart> _cartRepository;

        public CartService(IRepository<CartItem> cartItemRepository, IRepository<Cart> cartRepository)
        {
            _cartItemRepository = cartItemRepository;
            _cartRepository = cartRepository;
        }

        public async Task AddQty(int id)
        {
            var specification = new CartItemByIdWithCartSpecification(id);
            var cartItems = await _cartItemRepository.ListAsync(specification);
            if (cartItems == null || cartItems.Count == 0)
                return;

            var cartItem = cartItems.First();
            cartItem.ProductQty++;

            await _cartItemRepository.UpdateAsync(cartItem);
        }

        public async Task Checkout(int id)
        {
            var cart = await _cartRepository.GetByIdAsync(id);
            if (cart == null) return;

            await _cartRepository.DeleteAsync(cart);
            await _cartRepository.SaveChangesAsync();
        }

        public async Task<Cart?> CreateCart(int productId, int storeId)
        {
            // TODO get data product dari service Catalog
            var product = new Product() { Id = productId, Name = "test", Description = "test desc", Package = "test package", Image = "test image", Price = 123 };

            // TODO perlu di cek apakah ada cart yang aktif, jika ada, maka tambahkan saja itemnya
            var cart = new Cart();
            cart.AddItem(product);

            await _cartRepository.AddAsync(cart);
            await _cartRepository.SaveChangesAsync();

            return cart;
        }

        public async Task<Cart?> GetById(int id)
        {
            return await _cartRepository.GetByIdAsync(id);
        }

        public async Task SubQty(int id)
        {
            var specification = new CartItemByIdWithCartSpecification(id);
            var cartItems = await _cartItemRepository.ListAsync(specification);
            if (cartItems == null || cartItems.Count == 0)
                return;

            var cartItem = cartItems.First();
            cartItem.ProductQty--;
            if (cartItem.ProductQty < 0) cartItem.ProductQty = 0;

            await _cartItemRepository.UpdateAsync(cartItem);
            await _cartItemRepository.SaveChangesAsync();
        }
    }
}
