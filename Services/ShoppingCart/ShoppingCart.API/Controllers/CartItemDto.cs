using ShoppingCart.Core.Entities;

namespace ShoppingCart.API.Controllers
{
    public class CartItemDto
    {
        public CartItemDto()
        {

        }

        public CartItemDto(CartItem item)
        {
            Id = item.Id;
            ProductId = item.ProductId;
            ProductName = item.ProductName;
            ProductPrice = item.ProductPrice;
            ProductQty = item.ProductQty;
            ProductSubtotal = item.ProductSubtotal;
        }

        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public double ProductPrice { get; set; }
        public int ProductQty { get; set; }
        public double ProductSubtotal { get; set; }

    }
}
