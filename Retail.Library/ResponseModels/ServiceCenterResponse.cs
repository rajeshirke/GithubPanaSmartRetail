using System;
namespace Retail.Infrastructure.ResponseModels
{
    public class ServiceCenterResponse
    {
        public int ServiceCenterId { get; set; }
        public string Name { get; set; }
        public string StreetAddress { get; set; }
        public int? CountryId { get; set; }
        public int? CityId { get; set; }
        public int? CompanyId { get; set; }
        public decimal? Longitude { get; set; }
        public decimal? Latitude { get; set; }
        public string ContactPersonName { get; set; }
        public string ContactNumber1 { get; set; }
        public string ContactNumber2 { get; set; }
        public TimeSpan? WorkingHoursStart { get; set; }
        public TimeSpan? WorkingHoursEnd { get; set; }
        public bool? IsAuthorisedServiceCenter { get; set; }
        public string Landmark { get; set; }
        public byte[] ServiceCenterLogo { get; set; }
        public int? PartMarkupPercentage { get; set; }
        public string CityName { get; set; }
    }
}
