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
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            //var navigation = builder.Metadata.FindNavigation(nameof(Product));
            //navigation.SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.HasKey(e => e.Id);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(256);

            builder
                .HasOne(e => e.ProductCategory)
                .WithMany()
                .HasForeignKey(e => e.ProductCategoryId);
        }
    }
}
