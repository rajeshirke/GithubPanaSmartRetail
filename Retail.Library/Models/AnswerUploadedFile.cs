using System;
using System.Collections.Generic;

#nullable disable

namespace Retail.Infrastructure.Models
{
    public partial class AnswerUploadedFile
    {
        public long AnswerUploadedFilesId { get; set; }
        public long FileInfoId { get; set; }
        public long TaskQuestionAnswerId { get; set; }

        public virtual FileInfo FileInfo { get; set; }
        public virtual TaskQuestionAnswer TaskQuestionAnswer { get; set; }
    }
}
