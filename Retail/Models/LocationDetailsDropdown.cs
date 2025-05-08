using System;
namespace Retail.Models
{
    public class LocationDetailsDropdown
    {
        public int Id { get; set; }
        public string LocationCode { get; set; }
        public string Title { get; set; }
        public string Area { get; set; }
        public decimal? Longitude { get; set; }
        public decimal? Latitude { get; set; }
        public string City { get; set; }
        public double Distance { get; set; }
    }
}
