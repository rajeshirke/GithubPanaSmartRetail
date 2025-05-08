using System;
using System.Collections.Generic;

#nullable disable

namespace Retail.Infrastructure.Models
{
    public partial class AttendanceView
    {
        public long PersonId { get; set; }
        public long AttendanceId { get; set; }
        public DateTime AttendanceDate { get; set; }
        public DateTime? CheckInDate { get; set; }
        public DateTime? CheckoutDate { get; set; }
        public int? LocationId { get; set; }
        public bool? IsOffDay { get; set; }
        public decimal? PersonLocationLongitude { get; set; }
        public decimal? PersonLocationLatitude { get; set; }
        public decimal? ProximityRange { get; set; }
        public string LocationStoreName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long? ParentPersonId { get; set; }

        public bool IsVisibleDayOff
        {
            get
            {
                if(IsOffDay!=null)
                {
                    if (IsOffDay == true)
                    {
                        return true;
                    }
                    else if (IsOffDay == false)
                    {
                        return false;
                    }
                    else
                    {
                        return false;
                    }
                }
                return false;
            }
        }

        public bool IsVisibleCheckInCheckOut
        {
            get
            {
                if ((CheckInDate != null || CheckoutDate != null) && (IsOffDay==false || IsOffDay==null))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
