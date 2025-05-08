using System;
using System.Collections.Generic;

#nullable disable

namespace Retail.Infrastructure.Models
{
    public partial class ProductCategory
    {
        public ProductCategory()
        {
            ProductCatalogues = new HashSet<ProductCatalogue>();
            ProductModels = new HashSet<ProductModel>();
            SalesTargets = new HashSet<SalesTarget>();
        }

        public int ProductCategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? ParentCategoryId { get; set; }
        public bool? IsActive { get; set; }
        public int? CategoryLevel { get; set; }

        public virtual ICollection<ProductCatalogue> ProductCatalogues { get; set; }
        public virtual ICollection<ProductModel> ProductModels { get; set; }
        public virtual ICollection<SalesTarget> SalesTargets { get; set; }
    }
}
