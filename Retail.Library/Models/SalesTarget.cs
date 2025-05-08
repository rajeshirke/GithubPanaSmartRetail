using System;
using System.Collections.Generic;

#nullable disable

namespace Retail.Infrastructure.Models
{
    public partial class SalesTarget
    {
        public long SalesTargetId { get; set; }
        public long PesronId { get; set; }
        public int TargetValue { get; set; }
        public int? AcheivedValue { get; set; }
        public int? LocationId { get; set; }
        public DateTime? TargetMonthYear { get; set; }
        public long TargetAssignedByPersonId { get; set; }
        public DateTime? TargetAssignedDate { get; set; }
        public int? ProductCategoryId { get; set; }
        public int? ProductSubCategoryId { get; set; }
        public int? TargetYear { get; set; }
        public int? TargetMonth { get; set; }

        public virtual Location Location { get; set; }
        public virtual Person Pesron { get; set; }
        public virtual ProductCategory ProductCategory { get; set; }
        public virtual Person TargetAssignedByPerson { get; set; }
    }
}
