using System;
using System.Collections.Generic;

namespace Retail.Infrastructure.Models
{
    public class PromoterDetails
    {
        public List<int> PersonIds { get; set; }
        public List<int> LocationIds { get; set; }
        public List<string> LocationNames { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
    }
}
