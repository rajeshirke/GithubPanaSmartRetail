using System;
namespace Retail.Models
{
    public class UserCurrentLocation
    {
        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public string Area { get; set; }

        public string SubArea { get; set; }

        public string CountryName { get; set; }

        public string CountryCode { get; set; }

        public string Locality { get; set; }

        public string SubLocality { get; set; }

        public string PostalCode { get; set; }

        public string MobileNumber { get; set; }
    }
}
