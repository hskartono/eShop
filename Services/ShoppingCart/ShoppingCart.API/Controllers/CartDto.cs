﻿namespace ShoppingCart.API.Controllers
{
    public class CartDto
    {
        public int Id { get; set; }
        public int StoreId { get; set; }
        public int OwnerId { get; set; }
        public double Total { get; set; }
        public List<CartItemDto> CartItems { get; set; } = new List<CartItemDto>();

    }
}
