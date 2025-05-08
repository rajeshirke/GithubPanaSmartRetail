using System;
using System.Collections.Generic;

#nullable disable

namespace Retail.Infrastructure.Models
{
    public partial class CountryCompany
    {
        public int CountryCompanyId { get; set; }
        public int CountryId { get; set; }
        public int CompanyId { get; set; }

        public virtual Company Company { get; set; }
        public virtual Country Country { get; set; }
    }
}
