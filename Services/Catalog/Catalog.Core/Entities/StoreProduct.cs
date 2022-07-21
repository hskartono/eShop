using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Core.Entities
{
    public class StoreProduct : BaseEntity
    {
        public int Id { get; set; }
        public int? StoreId { get; set; }
        public Store? Store { get; set; }
        public int? ProductId { get; set; }
        public Product? Product { get; set; }

    }
}
