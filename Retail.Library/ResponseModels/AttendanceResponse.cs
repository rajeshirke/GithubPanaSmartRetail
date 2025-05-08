using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Retail.Infrastructure.ResponseModels
{
    public class AttendanceResponse
    {
        public long AttendanceId { get; set; }

        public DateTime AttendanceDate { get; set; }
        public DateTime? CheckInDate { get; set; }
        public DateTime? CheckoutDate { get; set; }
        public long PersonId { get; set; }
        public string PersonName { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public int? LocationId { get; set; }
        public string LocationName { get; set; }
        public int? TotalHoursOfAttendanceForTheDay { get; set; }
        public bool? IsOffDay { get; set; }
        public bool? IsVisibleDayOff { get; set; } = false;
        public bool? IsVisibleWorkingHr { get; set; } = false;
        public bool? IsOffDayVisible { get; set; } = true;

        public int CountryDutyHours { get; set; }

        public Color DayColor
        {
            get
            {
                if (IsOffDay == true)
                    return Color.Yellow;
                else if (TotalHoursOfAttendanceForTheDay >= CountryDutyHours)
                    return Color.Green;
                else if (TotalHoursOfAttendanceForTheDay < CountryDutyHours)
                    return Color.Red;


                return Color.Red;
            }
        }

        public int? TotalHoursDay
        {
            get
            {
                if (TotalHoursOfAttendanceForTheDay != null)
                {
                    return TotalHoursOfAttendanceForTheDay;
                }
                else
                {
                    return 0;
                }             
            }
        }
    }




    public class PersonMonthlyAttendanceResponse
    {
        public List<PersonDailyAttendanceResponse> PersonDailyAttendanceResponses { get; set; }
    }

    public class PersonDailyAttendanceResponse
    {
        public int AttendanceMonth { get; set; }
        public int AttendanceYear { get; set; }
        public DateTime AttendanceDate { get; set; }
        //public int TotalDutyHours { get; set; }
        public bool IsOffDay { get; set; }
        //public int? DutyHours { get; set; }
        public int TotalDutyHours { get; set; }//----person completed dutyhours
        public int CountryDutyHours { get; set; }//---CountryDutyHours
        public Color DayColor
        {
            get
            {
                if (IsOffDay == true)
                    return Color.Yellow;
                else if (TotalDutyHours >= CountryDutyHours)
                    return Color.Green;
                else if (TotalDutyHours < CountryDutyHours)
                    return Color.Red;
            

                return Color.Red;
            }
        }
        public List<AttendanceResponse> AttendanceMultiLocationResponses { get; set; }

    }


    //public class AttendanceHistoryResponse
    //{
    //    public long AttendanceId { get; set; }
    //    public DateTime? CheckInDate { get; set; }
    //    public DateTime? CheckoutDate { get; set; }
    //    public long PersonId { get; set; }
    //    public int? LocationId { get; set; }
    //    public string LocationName { get; set; }
    //    public int? TotalHoursOfAttendanceForTheDay { get; set; }

    //}

}
