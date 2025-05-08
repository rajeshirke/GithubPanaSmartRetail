using System;
using System.Collections.Generic;

#nullable disable

namespace Retail.Infrastructure.Models
{
    public partial class MessageTemplateFileInfo
    {
        public int MessageTemplateId { get; set; }
        public long FileInfoId { get; set; }

        public virtual FileInfo FileInfo { get; set; }
        public virtual MessageTemplate MessageTemplate { get; set; }
    }
}
