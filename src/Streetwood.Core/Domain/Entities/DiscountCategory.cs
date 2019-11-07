﻿using Streetwood.Core.Domain.Abstract;

namespace Streetwood.Core.Domain.Entities
{
    // JoinTable
    public class DiscountCategory : Entity
    {
        public virtual ProductCategory ProductCategory { get; protected set; }

        public virtual ProductCategoryDiscount ProductCategoryDiscount { get; protected set; }

        public void SetProductCategory(ProductCategory category)
            => ProductCategory = category;

        public void SetDiscount(ProductCategoryDiscount discount)
            => ProductCategoryDiscount = discount;

        public DiscountCategory(ProductCategory productCategory, ProductCategoryDiscount productCategoryDiscount)
        {
            SetProductCategory(productCategory);
            SetDiscount(productCategoryDiscount);
        }

        protected DiscountCategory()
        {
        }
    }
}
