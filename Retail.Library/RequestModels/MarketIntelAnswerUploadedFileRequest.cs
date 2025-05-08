using Retail.Infrastructure.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Retail.Infrastructure.RequestModels
{
    public class MarketIntelAnswerUploadedFileRequest
    {
        public long MarketIntelAnswerUploadedFileId { get; set; }
        public long MarketIntelQuestionAnswerId { get; set; }
        public long FileInfoId { get; set; }

        // public virtual FileInfo FileInfo { get; set; }

        public virtual FileInfoResponse FileInfo { get; set; }


        //public virtual MarketIntelQuestionAnswer MarketIntelQuestionAnswer { get; set; }
    }
}
