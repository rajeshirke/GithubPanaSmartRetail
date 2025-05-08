using Retail.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Retail.Infrastructure.RequestModels
{
    public class MarketInsightRequest
    {  
        public long MarketInsightId { get; set; }
        public string Comment { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreationDate { get; set; }
        public int? LocationId { get; set; }

        //public virtual Person CreatedByNavigation { get; set; }
        //public virtual Location Location { get; set; }
        public virtual ICollection<MarketInsightFileRequest> MarketInsightFiles { get; set; }

    }
}
