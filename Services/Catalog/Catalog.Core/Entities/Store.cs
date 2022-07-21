using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Core.Entities
{
    public class Store : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        private IList<StoreProduct> _storeProducts = new List<StoreProduct>();

        public IList<StoreProduct> StoreProducts
        {
            get { return _storeProducts; }
            set { _storeProducts = value; }
        }

        public void Add(StoreProduct storeProduct) => _storeProducts.Add(storeProduct);
        public void Add(Product product) => Add(new StoreProduct() { Product = product, ProductId = product.Id, Store = this, StoreId = Id });
        public void Remove(StoreProduct storeProduct) => _storeProducts.Remove(storeProduct);
        public void Clear() => _storeProducts.Clear();
    }
}
