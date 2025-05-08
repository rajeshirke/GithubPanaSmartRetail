using Retail.Infrastructure.Models;
using System;
using System.Collections.Generic; 
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Retail.Infrastructure.ResponseModels
{
    public class AnswerUploadedFileResponse
    {
        public long AnswerUploadedFilesId { get; set; }
        public long FileInfoId { get; set; }
        public long TaskQuestionAnswerId { get; set; }

        public FileInfoResponse FileInfo { get; set; }
        //public virtual TaskQuestionAnswer TaskQuestionAnswer { get; set; }
    }
}
