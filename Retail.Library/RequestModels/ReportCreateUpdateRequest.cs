using System;
using System.Collections.Generic;

namespace Retail.Infrastructure.RequestModels
{
    public class ReportCreateUpdateRequest
    {
        public ReportCreateUpdateRequest()
        {
            CountryIds = new List<int?>();
            AccountIds = new List<int?>();
            LocationIds = new List<int?>();
            CityIds = new List<int?>();
            FromDates = new List<DateTime?>();
            PersonId = new List<long?>();
            Dates = new DateTime ();
            PersonId = new List<long?>();
            SinglePersonId = new long();
            ProductSubCategoryID = new List<int?>();
        }

        public List<int?> CountryIds { get; set; }
        public List<int?> AccountIds { get; set; }
        public List<int?> LocationIds { get; set; }
        public List<int?> CityIds { get; set; }
        public List<DateTime?> FromDates { get; set; }
        public DateTime Dates { get; set; }
        public List<long?> PersonId { get; set; }
        public long? SinglePersonId { get; set; }
        public List<int?> ProductSubCategoryID { get; set; }
    }
}
