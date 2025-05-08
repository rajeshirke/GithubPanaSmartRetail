using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Retail.Infrastructure.RequestModels
{
    public class AttendanceRequest
    {
        public long AttendanceId { get; set; }
        public long PersonId { get; set; }
        public DateTime AttendanceDate { get; set; }
        public DateTime? CheckInDate { get; set; }
        public DateTime? CheckoutDate { get; set; }
        public int? LocationId { get; set; }
        public decimal? PersonLocationLongitude { get; set; }
        public decimal? PersonLocationLatitude { get; set; }
        public bool? IsOffDay { get; set; }
        public decimal? ProximityRange { get; set; }
        public int? OnlineOffLineAttendanceStatus { get; set; }
    }
}
