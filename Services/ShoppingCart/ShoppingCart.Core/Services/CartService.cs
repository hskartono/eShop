using Microsoft.Extensions.Options;
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
        private readonly ICatalogApiClient _catalogApiClient;
        private readonly int ownerId = 1;

        public CartService(IRepository<CartItem> cartItemRepository, 
            IRepository<Cart> cartRepository,
            ICatalogApiClient catalogApiClient)
        {
            _cartItemRepository = cartItemRepository;
            _cartRepository = cartRepository;
            _catalogApiClient = catalogApiClient;
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
            var cart = await GetById(id);
            if (cart == null) return;

            if (cart.Items.Count > 0)
            {
                await _cartItemRepository.DeleteRangeAsync(cart.Items);
            }

            await _cartRepository.DeleteAsync(cart);
            await _cartRepository.SaveChangesAsync();
        }

        public async Task<Cart?> CreateCart(int productId, int storeId)
        {
            // load data productnya dulu
            var product = await GetProduct(productId, storeId);
            if (product == null) throw new Exception("Product not found");

            // cek apakah user sudah memiliki cart, jika belum langsung create
            var myCartFilter = new CartByOwnerIdSpecification(ownerId);
            if(!await _cartRepository.AnyAsync(myCartFilter))
            {
                return await CreateNewCart(product, storeId);
            }

            // cek apakah cart dengan store yang sama, jika berbeda maka add
            var myCartByStoreFilter = new CartByStoreIdSpecification(storeId, ownerId);
            var myCart = await _cartRepository.GetBySpecAsync(myCartByStoreFilter);
            if(myCart == null)
            {
                return await CreateNewCart(product, storeId);
            }

            // cek apakah item-nya ada
            if(myCart.Items.Any(e=>e.ProductId == productId))
            {
                var cartItem = myCart.Items.Where(e => e.ProductId == productId).FirstOrDefault();
                if (cartItem != null)
                {
                    cartItem.AddQuantity(1);
                    await _cartItemRepository.UpdateAsync(cartItem);
                    await _cartItemRepository.SaveChangesAsync();
                }

                return myCart;
            }

            // item belum ada, add ke dalam keranjang
            myCart.AddItem(product);
            await _cartRepository.UpdateAsync(myCart);
            await _cartRepository.SaveChangesAsync();
            return myCart;
        }

        private async Task<Cart> CreateNewCart(Product product, int storeId)
        {
            var cart = new Cart()
            {
                OwnerId = ownerId,
                StoreId = storeId
            };
            cart.AddItem(product);

            await _cartRepository.AddAsync(cart);
            await _cartRepository.SaveChangesAsync();

            return cart;
        }

        private async Task<Product?> GetProduct(int id, int storeid)
        {
            var product = await _catalogApiClient.GetProduct(id, storeid);

            //var product = new Product() { Id = id, Name = "test", Description = "test desc", Package = "test package", Image = "test image", Price = 123 };

            return product;
        }

        public async Task<Cart?> GetById(int id)
        {
            var filter = new CartByIdWithItemSpecification(id);
            var carts = await _cartRepository.ListAsync(filter);
            if (carts == null || carts.Count == 0) return null;
            return carts.FirstOrDefault();
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

            if (cartItem.ProductQty == 0)
            {
                await _cartItemRepository.DeleteAsync(cartItem);
            } else
            {
                await _cartItemRepository.UpdateAsync(cartItem);
            }
            
            await _cartItemRepository.SaveChangesAsync();
        }
    }
}
