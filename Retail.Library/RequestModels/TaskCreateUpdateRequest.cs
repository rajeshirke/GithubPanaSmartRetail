using Retail.Infrastructure.Models;
using Retail.Infrastructure.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Retail.Infrastructure.RequestModels
{
    public class TaskCreateUpdateRequest
    {
        public long TaskMasterId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; } 
        public bool IsActive { get; set; } = true;

        //public ICollection<TaskMasterQuestion> TaskMasterQuestions { get; set; }
        //public ICollection<VisitLocationTask> VisitLocationTasks { get; set; }

       
        public List<long> AllQuestionMasterIds { get; set; }

        public List<QuestionMaster> AllQuestionMasters{ get; set; }

        public List<long> SelectedQuestionMasterIds { get; set; }

        public List<QuestionMaster> SelectedQuestionMasters { get; set; }

        public List<long> UnSelectedQuestionMasterIds { get; set; }

        public List<QuestionMaster> UnSelectedQuestionMasters { get; set; }


    }
}
