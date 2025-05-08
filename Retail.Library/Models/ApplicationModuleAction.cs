using System;
using System.Collections.Generic;

#nullable disable

namespace Retail.Infrastructure.Models
{
    public partial class ApplicationModuleAction
    {
        public int ApplicationModuleActionId { get; set; }
        public int ApplicationModuleId { get; set; }
        public string ActionName { get; set; }

        public virtual ApplicationModule ApplicationModule { get; set; }
    }
}
