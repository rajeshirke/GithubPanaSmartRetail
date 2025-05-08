using Retail.Infrastructure.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Retail.Infrastructure.RequestModels
{
    public class MarketInsightFileRequest
    {
        public long MarketInsightFileId { get; set; }
        public long MarketInsightId { get; set; }
        public long FileInfoId { get; set; }

        //public virtual FileInfo FileInfo { get; set; }
        public virtual FileInfoResponse FileInfo { get; set; }
        //public virtual MarketInsight MarketInsight { get; set; }
    }
}
