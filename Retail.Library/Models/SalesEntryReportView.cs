using System;
using System.Collections.Generic;

#nullable disable

namespace Retail.Infrastructure.Models
{
    public partial class SalesEntryReportView
    {
        public long SalesTargetEntryId { get; set; }
        public DateTime? EntryDate { get; set; }
        public int? TargetMonth { get; set; }
        public int? TargetYear { get; set; }
        public long PersonId { get; set; }
        public string PersonName { get; set; }
        public string Region { get; set; }
        public int RegionId { get; set; }
        public string Country { get; set; }
        public int CountryId { get; set; }
        public string CountryIso { get; set; }
        public int CityId { get; set; }
        public string City { get; set; }
        public int? LocationAccountId { get; set; }
        public string LocationAccountName { get; set; }
        public int LocationId { get; set; }
        public string LocationStoreName { get; set; }
        public int ProductCategoryId { get; set; }
        public string ProductCategoryName { get; set; }
        public int? ProductSubCategoryId { get; set; }
        public string ProductSubCategoryName { get; set; }
        public string ProductModelNumber { get; set; }
        public string CategoryLevel3 { get; set; }
        public string CategoryLevel4 { get; set; }
        public int Quantity { get; set; }
        public decimal? SalesValueOnly { get; set; }
        public string UnitPrice { get; set; }
        public decimal? UnitPriceOnly { get; set; }
        public string SalesValue { get; set; }
        public string SalesValueUsd { get; set; }
        public string SalesValueUsdonly { get; set; }
        public decimal? ExchangeRate { get; set; }
        public int TargetInOutStatusId { get; set; }
        public DateTime? CreatedDate { get; set; }


        //public long SalesTargetEntryId { get; set; }
        //public DateTime? EntryDate { get; set; }
        //public int? TargetMonth { get; set; }
        //public int? TargetYear { get; set; }
        //public long PersonId { get; set; }
        //public string PersonName { get; set; }
        //public string Region { get; set; }
        //public int RegionId { get; set; }
        //public string Country { get; set; }
        //public int CountryId { get; set; }
        //public string CountryIso { get; set; }
        //public int CityId { get; set; }
        //public string City { get; set; }
        //public int LocationAccountId { get; set; }
        //public string LocationAccountName { get; set; }
        //public int LocationId { get; set; }
        //public string LocationStoreName { get; set; }
        //public int ProductCategoryId { get; set; }
        //public string ProductCategoryName { get; set; }
        //public int? ProductSubCategoryId { get; set; }
        //public string ProductSubCategoryName { get; set; }
        //public string ProductModelNumber { get; set; }
        //public string CategoryLevel3 { get; set; }
        //public string CategoryLevel4 { get; set; }
        //public int Quantity { get; set; }
        //public decimal? SalesValueOnly { get; set; }
        //public string UnitPrice { get; set; }
        //public string SalesValue { get; set; }
        //public string SalesValueUsd { get; set; }
        //public decimal? ExchangeRate { get; set; }
        //public int TargetInOutStatusId { get; set; }
    }
}
