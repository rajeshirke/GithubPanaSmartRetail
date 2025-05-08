using System;
using System.Collections.Generic;

#nullable disable

namespace Retail.Infrastructure.Models
{
    public partial class PersonCompany
    {
        public int PersonCompanyId { get; set; }
        public long PersonId { get; set; }
        public int CompanyId { get; set; }

        public virtual Company Company { get; set; }
        public virtual Person Person { get; set; }
    }
}
