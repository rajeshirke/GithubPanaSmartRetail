using Newtonsoft.Json;
using Retail.Infrastructure.Enums;
using Retail.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Retail.Infrastructure.ResponseModels
{
    public class VisitScheduleLocationResponse
    {
        public bool IsNotEditable { get; set; }
        public long VisitScheduleLocationId { get; set; }
        public long VisitScheduleId { get; set; }
        public int LocationId { get; set; }  
        public int VisitLocationTotalTaskCount { get; set; } = 0;
        public int VisitLocationTasksStatusId { get; set; }
        public string VisitLocationTasksStatusName { get; set; }
        public string VisitLocationTasksCountStatus { get; set; }
        public int? StatusId { get; set; } = 1;

        [JsonIgnore]
        public bool OfflineScheduleDraftLocation { get; set; }
        [JsonIgnore]
        public string OfflineScheduleStatusTextLocation { get; set; }

        public string VisitLocationStatusName
        {
            get
            {
                if (StatusId == (int)TaskStatusEnum.Open)
                {
                    return "Open";
                }
                else if (StatusId == (int)TaskStatusEnum.InProgress)
                {
                    return  "In Progress";
                }
                else if (StatusId == (int)TaskStatusEnum.Closed)
                {
                    return "Closed";
                }
                else if (StatusId == (int)TaskStatusEnum.Submitted)
                {
                    return "Submitted";
                }
                else
                {
                    return "Closed";                 
                }
            }
        }

        public Color StatusColor
        {
            get
            {
                if (StatusId == (int)TaskStatusEnum.Open)
                {  
                    return Color.Red;                    
                }   
                else if (StatusId == (int)TaskStatusEnum.InProgress)
                {  
                    return Color.Blue;                    
                }
                else if (StatusId == (int)TaskStatusEnum.Closed)
                {  
                    return Color.Green;
                }
                else if (StatusId == (int)TaskStatusEnum.Submitted)
                {
                    return Color.DarkGreen;
                }
                else
                { 
                    return Color.Red;
                }
            }
        }

        public LocationResponse Location { get; set; }
        public ICollection<VisitLocationTaskResponse> VisitLocationTasks { get; set; }

        public decimal? PersonLocationLongitude { get; set; }
        public decimal? PersonLocationLatitude { get; set; }
        public decimal? ProximityRange { get; set; }
    }
}
