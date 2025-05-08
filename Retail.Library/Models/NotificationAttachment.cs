using System;
using System.Collections.Generic;

#nullable disable

namespace Retail.Infrastructure.Models
{
    public partial class NotificationAttachment
    {
        public int NotificationAttachmentId { get; set; }
        public int MessageTemplateId { get; set; }
        public string Name { get; set; }
        public string MimeType { get; set; }
        public string ContentPath { get; set; }

        public virtual MessageTemplate MessageTemplate { get; set; }
    }
}
