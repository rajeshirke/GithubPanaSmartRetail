using System;
using System.Collections.Generic;

#nullable disable

namespace Retail.Infrastructure.Models
{
    public partial class Language
    {
        public Language()
        {
            MessageTemplates = new HashSet<MessageTemplate>();
        }

        public int LanguageId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<MessageTemplate> MessageTemplates { get; set; }
    }
}
