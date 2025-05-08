using System;
using System.Collections.Generic;

#nullable disable

namespace Retail.Infrastructure.Models
{
    public partial class ProductModelRecentEntry
    {
        public long ProductModelRecentEntryId { get; set; }
        public long PersonId { get; set; }
        public string ModelNumber { get; set; }
        public DateTime EntryDate { get; set; }
        public int? ProductCategoryId { get; set; }
        public int? ProductCategoryLevel { get; set; }
    }
}
