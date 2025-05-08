using System;
using System.Collections.Generic;

#nullable disable

namespace Retail.Infrastructure.Models
{
    public partial class Location
    {
        public Location()
        {
            Attendances = new HashSet<Attendance>();
            Contacts = new HashSet<Contact>();
            InventoryStockEntries = new HashSet<InventoryStockEntry>();
            LocationFiles = new HashSet<LocationFile>();
            PersonLocations = new HashSet<PersonLocation>();
            SalesTargets = new HashSet<SalesTarget>();
            VisitScheduleLocations = new HashSet<VisitScheduleLocation>();
        }

        public int LocationId { get; set; }
        public string LocationCode { get; set; }
        public string LocationStoreName { get; set; }
        public int? LocationAccountId { get; set; }
        public int? FloorNumber { get; set; }
        public string Street { get; set; }
        public string BuildingName { get; set; }
        public string Area { get; set; }
        public int? PostalCode { get; set; }
        public decimal? Longitude { get; set; }
        public decimal? Latitude { get; set; }
        public string City { get; set; }
        public int? CityId { get; set; }
        public bool? IsActive { get; set; }
        public string State { get; set; }
        public int? CountryId { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreationDate { get; set; }

        public virtual City CityNavigation { get; set; }
        public virtual Country Country { get; set; }
        public virtual LocationAccount LocationAccount { get; set; }
        public virtual ICollection<Attendance> Attendances { get; set; }
        public virtual ICollection<Contact> Contacts { get; set; }
        public virtual ICollection<InventoryStockEntry> InventoryStockEntries { get; set; }
        public virtual ICollection<LocationFile> LocationFiles { get; set; }
        public virtual ICollection<PersonLocation> PersonLocations { get; set; }
        public virtual ICollection<SalesTarget> SalesTargets { get; set; }
        public virtual ICollection<VisitScheduleLocation> VisitScheduleLocations { get; set; }
    }
}
