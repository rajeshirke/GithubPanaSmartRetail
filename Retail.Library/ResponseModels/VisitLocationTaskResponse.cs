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
    public  class VisitLocationTaskResponse
    {
        public long VisitLocationTaskId { get; set; }
        [JsonIgnore]
        public long PVisitLocationTaskId { get; set; }

        public long TaskMasterId { get; set; }
        public long VisitScheduleLocationId { get; set; }
        public int? TaskStatusId { get; set; }
        public DateTime? TaskCompletionDate { get; set; }
        public long? AssignedToPersonId { get; set; }       
        public string TaskStatusName
        {
            get
            {
                if (TaskStatusId == (int)TaskStatusEnum.Open)
                    return "Open";
                else if (TaskStatusId == (int)TaskStatusEnum.InProgress)
                    return "In Progress";
                else if (TaskStatusId == (int)TaskStatusEnum.Closed)
                    return "Closed";

                return "-";

            }
        }
        public bool IsVisibleCompletionDate
        {
            get
            {
                if (TaskCompletionDate != null)
                {
                    return true;
                }
                else
                    return false;

                //return false;
            }
        } 
        public Color StatusColor
        {
            get
            {
                if (TaskStatusId == (int)TaskStatusEnum.Open)
                    return Color.Red;
                else if (TaskStatusId == (int)TaskStatusEnum.InProgress)
                    return Color.Blue;
                else if (TaskStatusId == (int)TaskStatusEnum.Closed)
                    return Color.Green;

                return Color.Red;
            }
        }

        public virtual TaskMasterResponse TaskMaster { get; set; }
       // public virtual TaskStatu TaskStatus { get; set; }
        public virtual VisitScheduleLocationResponse VisitScheduleLocation { get; set; }
        public virtual ICollection<TaskQuestionAnswerResponse> TaskQuestionAnswers { get; set; }

        public int LocationAccountID { get; set; }
    }
}
