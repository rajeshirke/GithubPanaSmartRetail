using System;
using System.Collections.Generic;

namespace Retail.Infrastructure.Models
{
    public partial class Notification
    {
        public Notification()
        {
            NotificationAttachments = new HashSet<NotificationAttachment>();
            NotificationHistories = new HashSet<NotificationHistory>();
        }

        public int NotificationId { get; set; }
        public string Subject { get; set; }
        public string NotificationContent { get; set; }
        public int? NotificationEventTypeId { get; set; }
        public int? NotificationChannelTypeId { get; set; }

        public virtual NotificationChannelType NotificationChannelType { get; set; }
        public virtual NotificationEventType NotificationEventType { get; set; }
        public virtual ICollection<NotificationAttachment> NotificationAttachments { get; set; }
        public virtual ICollection<NotificationHistory> NotificationHistories { get; set; }
    }
}
