using System;
using System.Collections.Generic;

#nullable disable

namespace Retail.Infrastructure.Models
{
    public partial class ServiceCenter
    {
        public ServiceCenter()
        {
            PersonServiceCenters = new HashSet<PersonServiceCenter>();
            ServiceCenterProductClassifications = new HashSet<ServiceCenterProductClassification>();
        }

        public int ServiceCenterId { get; set; }
        public string Name { get; set; }
        public string ContactPersonName { get; set; }
        public int? CountryId { get; set; }
        public string CityName { get; set; }
        public string StreetAddress { get; set; }
        public int? CityId { get; set; }
        public string ContactNumber1 { get; set; }
        public string ContactNumber2 { get; set; }
        public int? CompanyId { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public TimeSpan? WorkingHoursStart { get; set; }
        public TimeSpan? WorkingHoursEnd { get; set; }
        public bool? IsAuthorisedServiceCenter { get; set; }
        public string Landmark { get; set; }
        public byte[] ServiceCenterLogo { get; set; }
        public int? PartMarkupPercentage { get; set; }
        public string StartDay { get; set; }
        public string EndDay { get; set; }
        public string Email { get; set; }

        public virtual Country Country { get; set; }
        public virtual ICollection<PersonServiceCenter> PersonServiceCenters { get; set; }
        public virtual ICollection<ServiceCenterProductClassification> ServiceCenterProductClassifications { get; set; }
    }
}
