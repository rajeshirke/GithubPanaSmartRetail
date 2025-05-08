using System;
using System.Collections.Generic;

#nullable disable

namespace Retail.Infrastructure.Models
{
    public partial class ModuleCountry
    {
        public int Id { get; set; }
        public int ModuleId { get; set; }
        public int CountryId { get; set; }

        public virtual Country Country { get; set; }
    }
}
