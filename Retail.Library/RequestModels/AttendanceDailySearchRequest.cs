﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Retail.Infrastructure.RequestModels
{
    public class AttendanceDailySearchRequest
    {
        public List<int> locationIds{ get; set; }
        public DateTime date { get; set; }
    }
}
