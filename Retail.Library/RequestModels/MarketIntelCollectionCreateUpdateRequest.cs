using Newtonsoft.Json;
using Retail.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Retail.Infrastructure.RequestModels
{
    public class MarketIntelCollectionCreateUpdateRequest
    {
        //public MarketIntelCollection()
        //{
        //    MarketIntelQuestionAnswers = new HashSet<MarketIntelQuestionAnswer>();
        //}

        public long MarketIntelCollectionId { get; set; }
        public int MarketIntelId { get; set; }
        public long AttendedByPersonId { get; set; }
        public DateTime AttendedDate { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int StatusId { get; set; }

        /// <summary>
        /// 1 for start, 2 for stop
        /// </summary>
        [JsonIgnore]
        public int TimerType { get; set; }

        //public virtual Person AttendedByPerson { get; set; }
        //public virtual MarketIntel MarketIntel { get; set; }
        // public virtual ICollection<MarketIntelQuestionAnswer> MarketIntelQuestionAnswers { get; set; }
    }
}
