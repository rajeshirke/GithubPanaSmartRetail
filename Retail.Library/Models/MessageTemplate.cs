using System;
using System.Collections.Generic;

#nullable disable

namespace Retail.Infrastructure.Models
{
    public partial class MessageTemplate
    {
        public MessageTemplate()
        {
            NotificationAttachments = new HashSet<NotificationAttachment>();
            NotificationHistories = new HashSet<NotificationHistory>();
        }

        public int MessageTemplateId { get; set; }
        public int NotificationEventTypeId { get; set; }
        public int ChannelTypeId { get; set; }
        public int LanguageId { get; set; }
        public string MessageSubject { get; set; }
        public string MessageContent { get; set; }
        public string Recipient { get; set; }

        public virtual NotificationChannelType ChannelType { get; set; }
        public virtual Language Language { get; set; }
        public virtual NotificationEventType NotificationEventType { get; set; }
        public virtual MessageTemplateFileInfo MessageTemplateFileInfo { get; set; }
        public virtual ICollection<NotificationAttachment> NotificationAttachments { get; set; }
        public virtual ICollection<NotificationHistory> NotificationHistories { get; set; }
    }
}
