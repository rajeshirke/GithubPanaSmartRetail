using System;
namespace Retail.Infrastructure.ResponseModels
{
    public class PriceTrackerResponseList
    {
        public string ProductModelName { get; set; }
        public decimal? RRP { get; set; }
        public decimal? NetRRP { get; set; }
        public decimal? Promo { get; set; }
    }
}

