using System;
using System.Collections.Generic;

#nullable disable

namespace Retail.Infrastructure.Models
{
    public partial class NotificationHistory
    {
        public int NotificationHistoryId { get; set; }
        public long ToPersonId { get; set; }
        public DateTime? SentDate { get; set; }
        public int? MessageTemplateId { get; set; }
        public bool? IsNotificationRead { get; set; }
        public DateTime? NotificationReadDate { get; set; }
        public string NotificationContent { get; set; }

        public virtual MessageTemplate MessageTemplate { get; set; }
        public virtual Person ToPerson { get; set; }
    }
}
