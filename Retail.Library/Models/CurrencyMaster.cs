using System;
using System.Collections.Generic;

#nullable disable

namespace Retail.Infrastructure.Models
{
    public partial class CurrencyMaster
    {
        public CurrencyMaster()
        {
            Countries = new HashSet<Country>();
            ExchangeRates = new HashSet<ExchangeRate>();
            SalesTargetEntries = new HashSet<SalesTargetEntry>();
        }

        public int CurrencyId { get; set; }
        public string Name { get; set; }
        public decimal ExchangeRate { get; set; }
        public string Code { get; set; }
        public int CountryId { get; set; }

        public virtual ICollection<Country> Countries { get; set; }
        public virtual ICollection<ExchangeRate> ExchangeRates { get; set; }
        public virtual ICollection<SalesTargetEntry> SalesTargetEntries { get; set; }
    }
}
