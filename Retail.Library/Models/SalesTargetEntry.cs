using System;
using System.Collections.Generic;

#nullable disable

namespace Retail.Infrastructure.Models
{
    public partial class SalesTargetEntry
    {
        public long SalesTargetEntryId { get; set; }
        public long? ProductModelId { get; set; }
        public string ProductModelNumber { get; set; }
        public DateTime EntryDate { get; set; }
        public int? CurrencyId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public int TargetInOutStatusId { get; set; }
        public long EntryByPersonId { get; set; }
        public int ProductCategoryId { get; set; }

        public virtual CurrencyMaster Currency { get; set; }
        public virtual Person EntryByPerson { get; set; }
        public virtual ProductModel ProductModel { get; set; }
    }
}
