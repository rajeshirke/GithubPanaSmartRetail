using System;
using System.Collections.Generic;

#nullable disable

namespace Retail.Infrastructure.Models
{
    public partial class Attendance
    {
        public long AttendanceId { get; set; }
        public long PersonId { get; set; }
        public DateTime? CheckInDate { get; set; }
        public DateTime? CheckoutDate { get; set; }
        public int? LocationId { get; set; }
        public decimal? PersonLocationLongitude { get; set; }
        public decimal? PersonLocationLatitude { get; set; }

        public virtual Location Location { get; set; }
    }
}
