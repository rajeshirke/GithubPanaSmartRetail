using System;
using System.Collections.Generic;

#nullable disable

namespace Retail.Infrastructure.Models
{
    public partial class Company
    {
        public Company()
        {
            CompanyRegions = new HashSet<CompanyRegion>();
            CountryCompanies = new HashSet<CountryCompany>();
            PersonCompanies = new HashSet<PersonCompany>();
        }

        public int CompanyId { get; set; }
        public string Name { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<CompanyRegion> CompanyRegions { get; set; }
        public virtual ICollection<CountryCompany> CountryCompanies { get; set; }
        public virtual ICollection<PersonCompany> PersonCompanies { get; set; }
    }
}
