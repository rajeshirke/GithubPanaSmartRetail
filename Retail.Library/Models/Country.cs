using System;
using System.Collections.Generic;

#nullable disable

namespace Retail.Infrastructure.Models
{
    public partial class Country
    {
        public Country()
        {
            CountryApplicationModules = new HashSet<CountryApplicationModule>();
            CountryCompanies = new HashSet<CountryCompany>();
            Locations = new HashSet<Location>();
            ModuleCountries = new HashSet<ModuleCountry>();
            PersonCountries = new HashSet<PersonCountry>();
            ServiceCenters = new HashSet<ServiceCenter>();
            States = new HashSet<State>();
        }

        public int CountryId { get; set; }
        public string Iso { get; set; }
        public string Name { get; set; }
        public string Iso3 { get; set; }
        public int? NumCode { get; set; }
        public int PhoneCode { get; set; }
        public string CurrencyCode { get; set; }
        public int? CurrencyId { get; set; }
        public bool? IsActive { get; set; }
        public int? RegionId { get; set; }
        public int? DutyHours { get; set; }

        public virtual CurrencyMaster Currency { get; set; }
        public virtual Region Region { get; set; }
        public virtual ICollection<CountryApplicationModule> CountryApplicationModules { get; set; }
        public virtual ICollection<CountryCompany> CountryCompanies { get; set; }
        public virtual ICollection<Location> Locations { get; set; }
        public virtual ICollection<ModuleCountry> ModuleCountries { get; set; }
        public virtual ICollection<PersonCountry> PersonCountries { get; set; }
        public virtual ICollection<ServiceCenter> ServiceCenters { get; set; }
        public virtual ICollection<State> States { get; set; }
    }
}
