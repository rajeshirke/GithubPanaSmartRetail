using System;
using System.Collections.Generic;

#nullable disable

namespace Retail.Infrastructure.Models
{
    public partial class CountryApplicationModule
    {
        public int CountryApplicationModuleId { get; set; }
        public int CountryId { get; set; }
        public int ApplicationModuleId { get; set; }

        public virtual ApplicationModule ApplicationModule { get; set; }
        public virtual Country Country { get; set; }
    }
}
