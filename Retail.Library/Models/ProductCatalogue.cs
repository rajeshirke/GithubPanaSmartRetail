using System;
using System.Collections.Generic;

#nullable disable

namespace Retail.Infrastructure.Models
{
    public partial class ProductCatalogue
    {
        public int ProductCatalogueId { get; set; }
        public int ProductCategoryId { get; set; }
        public long FileInfoId { get; set; }
        public string Title { get; set; }

        public virtual FileInfo FileInfo { get; set; }
        public virtual ProductCategory ProductCategory { get; set; }
    }
}
