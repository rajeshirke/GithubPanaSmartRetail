using System;
using System.Collections.Generic;

#nullable disable

namespace Retail.Infrastructure.Models
{
    public partial class MarketIntelAnswerUploadedFile
    {
        public long MarketIntelAnswerUploadedFileId { get; set; }
        public long MarketIntelQuestionAnswerId { get; set; }
        public long FileInfoId { get; set; }

        public virtual FileInfo FileInfo { get; set; }
        public virtual MarketIntelQuestionAnswer MarketIntelQuestionAnswer { get; set; }
    }
}
