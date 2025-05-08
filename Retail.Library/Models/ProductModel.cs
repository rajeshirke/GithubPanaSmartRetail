using System;
using System.Collections.Generic;

#nullable disable

namespace Retail.Infrastructure.Models
{
    public partial class ProductModel
    {
        public ProductModel()
        {
            ProductFiles = new HashSet<ProductFile>();
            SalesTargetEntries = new HashSet<SalesTargetEntry>();
        }

        public long ProductModelId { get; set; }
        public int? ProductCategoryId { get; set; }
        public int? ProductSubCategoryId { get; set; }
        public string ProductModelNumber { get; set; }
        public int? RegionId { get; set; }
        public string RegionName { get; set; }
        public int? CountryId { get; set; }
        public string CountryName { get; set; }
        public int? DivisionId { get; set; }
        public bool? IsActive { get; set; }
        public string Category { get; set; }
        public string CategoryLevel2 { get; set; }
        public string CategoryLevel3 { get; set; }
        public string CategoryLevel4 { get; set; }

        public virtual ProductCategory ProductCategory { get; set; }
        public virtual ICollection<ProductFile> ProductFiles { get; set; }
        public virtual ICollection<SalesTargetEntry> SalesTargetEntries { get; set; }
    }
}
