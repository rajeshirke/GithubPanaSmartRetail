using System;
using System.Collections.Generic;

#nullable disable

namespace Retail.Infrastructure.Models
{
    public partial class ExchangeRate
    {
        public int ExchageRateId { get; set; }
        public decimal Rate { get; set; }
        public int CurrencyId { get; set; }

        public virtual CurrencyMaster Currency { get; set; }
    }
}
