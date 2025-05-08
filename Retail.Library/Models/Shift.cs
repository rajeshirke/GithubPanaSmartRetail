using System;
using System.Collections.Generic;

#nullable disable

namespace Retail.Infrastructure.Models
{
    public partial class Shift
    {
        public long ShiftId { get; set; }
        public long PersonId { get; set; }
        public TimeSpan ShiftStartTime { get; set; }
        public TimeSpan ShiftEndTime { get; set; }
        public string OffDays { get; set; }
        public string OffDaysNumbers { get; set; }
        public int? StartDayOfWeek { get; set; }

        public virtual Person Person { get; set; }
    }
}
