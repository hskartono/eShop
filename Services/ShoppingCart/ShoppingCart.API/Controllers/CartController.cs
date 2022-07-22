using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Core.Interfaces;

namespace ShoppingCart.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet()]
        public async Task<IActionResult> GetCartById()
        {
            var cart = await _cartService.GetMyCart();
            if (cart == null) return NotFound();

            var cartDto = new CartDto();
            cartDto.Id = cart.Id;
            cartDto.OwnerId = cart.OwnerId;
            cartDto.StoreId = cart.StoreId;
            cartDto.Total = cart.Total;
            foreach (var item in cart.Items)
            {
                cartDto.CartItems.Add(new CartItemDto(item));
            }

            return Ok(cartDto);
        }

        [HttpPost("Add/{productid}/{storeid}")]
        public async Task<IActionResult> Add(int productid, int storeid)
        {
            var result = await _cartService.CreateCart(productid, storeid);
            if (result == null) return BadRequest();

            var cartDto = new CartDto()
            {
                Id = result.Id,
                StoreId = result.StoreId,
                OwnerId = result.OwnerId,
                Total = result.Total
            };

            foreach (var item in result.Items)
            {
                cartDto.CartItems.Add(new CartItemDto(item));
            }

            return Ok(cartDto);
        }

        [HttpPost("AddQty/{id}")]
        public async Task<IActionResult> AddQty(int id)
        {
            await _cartService.AddQty(id);
            return Ok();
        }

        [HttpPost("SubQty/{id}")]
        public async Task<IActionResult> SubQty(int id)
        {
            await _cartService.SubQty(id);
            return Ok();
        }

        [HttpGet("Checkout/")]
        public async Task<IActionResult> Checkout()
        {
            await _cartService.Checkout();
            return Ok();
        }
    }
}
