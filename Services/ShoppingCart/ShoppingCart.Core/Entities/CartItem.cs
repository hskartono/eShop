using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Core.Entities
{
    public class CartItem : BaseEntity
    {
        public CartItem()
        {
        }

        public CartItem(Product product)
        {
            ProductId = product.Id;
            ProductName = product.Name;
            ProductPrice = product.Price;
            ProductQty = 1;
        }

        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public double ProductPrice { get; set; }
        public int ProductQty { get; set; }
        public double ProductSubtotal => ProductPrice * ProductQty;
        public int? CartId { get; set; }
        public Cart? Cart { get; set; }

        public void AddQuantity(int quantity)
        {
            ProductQty += quantity;
        }

        public void SetQuantity(int quantity)
        {
            ProductQty = quantity;
        }
    }
}
