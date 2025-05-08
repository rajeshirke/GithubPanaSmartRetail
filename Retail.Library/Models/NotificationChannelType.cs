using System;
using System.Collections.Generic;

#nullable disable

namespace Retail.Infrastructure.Models
{
    public partial class NotificationChannelType
    {
        public NotificationChannelType()
        {
            MessageTemplates = new HashSet<MessageTemplate>();
        }

        public int NotificationChannelTypeId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<MessageTemplate> MessageTemplates { get; set; }
    }
}
