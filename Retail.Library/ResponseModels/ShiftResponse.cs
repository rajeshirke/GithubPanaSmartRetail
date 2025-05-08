using System;
namespace Retail.Infrastructure.ResponseModels
{
    public class ShiftResponse
    {
        public long ShiftId { get; set; }
        public long PersonId { get; set; }
        public TimeSpan ShiftStartTime { get; set; }
        public TimeSpan ShiftEndTime { get; set; }
        public string OffDays { get; set; }
        public string OffDaysNumbers { get; set; }
        public int? StartDayOfWeek { get; set; }
    }
}
