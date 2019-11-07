﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Streetwood.Core.Constants;

namespace Streetwood.Core.Domain.Entities.Configuration
{
    public class ProductOrderConfiguration : IEntityTypeConfiguration<ProductOrder>
    {
        public void Configure(EntityTypeBuilder<ProductOrder> builder)
        {
            builder.HasKey(s => s.Id);
            builder.Property(s => s.CurrentProductPrice)
                .HasColumnType(ConstantValues.PriceDecimalType);
            builder.Property(s => s.FinalPrice)
                .HasColumnType(ConstantValues.PriceDecimalType);

            builder.HasMany(s => s.ProductOrderCharms)
                .WithOne(s => s.ProductOrder);

            builder.HasOne(s => s.Product)
                .WithMany(s => s.ProductOrders)
                .HasForeignKey("ProductId");

            builder.HasOne(s => s.ProductCategoryDiscount)
                .WithMany(s => s.ProductOrders)
                .HasForeignKey("ProductCategoryDiscountId");

            builder.HasOne(s => s.Order)
                .WithMany(s => s.ProductOrders)
                .HasForeignKey("OrderId");
        }
    }
}
