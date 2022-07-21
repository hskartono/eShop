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
    public class StoreConfiguration : IEntityTypeConfiguration<Store>
    {
        public void Configure(EntityTypeBuilder<Store> builder)
        {
            //var navigation = builder.Metadata.FindNavigation(nameof(ProductCategory));
            //navigation.SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.HasKey(e => e.Id);

            builder.HasMany(e => e.StoreProducts)
                .WithOne(p => p.Store)
                .HasForeignKey(p => p.StoreId);
        }
    }
}
