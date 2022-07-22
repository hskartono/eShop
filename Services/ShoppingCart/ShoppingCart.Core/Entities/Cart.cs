using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Core.Entities
{
    public class Cart : BaseEntity
    {
        public int Id { get; set; }
        public int StoreId { get; set; }
        public int OwnerId { get; set; }

        public double Total => _items.Sum(e => e.ProductSubtotal);
        private readonly List<CartItem> _items = new List<CartItem>();
        public IReadOnlyList<CartItem> Items => _items.AsReadOnly();

        public void AddItem(Product product, int quantity = 1)
        {
            if(!Items.Any(i=>i.ProductId == product.Id))
            {
                _items.Add(new CartItem(product));
                return;
            }

            var existingItem = Items.FirstOrDefault(i => i.ProductId == product.Id);
            existingItem?.AddQuantity(quantity);
        }

        public void RemoveEmptyItems()
        {
            _items.RemoveAll(i => i.ProductQty <= 0);
        }
    }
}
