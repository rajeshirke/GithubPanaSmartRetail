using System;
using System.Collections.Generic;

#nullable disable

namespace Retail.Infrastructure.Models
{
    public partial class NotificationEventType
    {
        public NotificationEventType()
        {
            MessageTemplates = new HashSet<MessageTemplate>();
        }

        public int NotificationEventTypeId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<MessageTemplate> MessageTemplates { get; set; }
    }
}
