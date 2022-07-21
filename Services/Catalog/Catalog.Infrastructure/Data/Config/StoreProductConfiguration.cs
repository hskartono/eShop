using Catalog.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Infrastructure.Data.Config
{
    public class StoreProductConfiguration : IEntityTypeConfiguration<StoreProduct>
    {
        public void Configure(EntityTypeBuilder<StoreProduct> builder)
        {
            //var navigation = builder.Metadata.FindNavigation(nameof(ProductCategory));
            //navigation.SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.HasKey(e => e.Id);

            builder.HasOne(d => d.Store)
                .WithMany(p => p.StoreProducts)
                .HasForeignKey(d => d.StoreId);

            builder.HasOne(d => d.Product)
                .WithMany()
                .HasForeignKey(d => d.ProductId);
        }
    }
}
