using System;
using System.Collections.Generic;
using Xamarin.Forms;

#nullable disable

namespace Retail.Infrastructure.Models
{
    public partial class SalesEntryDailyBySubCategoryView
    {
        public DateTime? EntryDate { get; set; }
        public string Country { get; set; }
        public int CountryId { get; set; }
        public string CountryIso { get; set; }
        public string Region { get; set; }
        public int RegionId { get; set; }
        public int? CityId { get; set; }
        public string City { get; set; }
        public int? LocationAccountId { get; set; }
        public string LocationAccountName { get; set; }
        public int LocationId { get; set; }
        public string LocationStoreName { get; set; }
        public int ProductCategoryId { get; set; }
        public string ProductCategoryName { get; set; }
        public int? ProductSubCategoryId { get; set; }
        public string ProductSubCategoryName { get; set; }
        public decimal? AcheivedTargets { get; set; }
        public int? TargetMonth { get; set; }
        public int? TargetYear { get; set; }
        public decimal? TotalTargets { get; set; }
        public decimal? AchievedPercentage { get; set; }

        public int SubCategoryHeight
        {
            get
            {
                if (AchievedPercentage != null)
                {
                    return 30;
                }

                return 20;
            }
        }

        public Color AchievedTargetColor
        {
            get
            {
                if (AchievedPercentage >= TotalTargets)
                {
                    return Color.Green;
                }
                else
                {
                    return Color.Red;
                }
            }
        }

        //public DateTime? EntryDate { get; set; }
        //public string Country { get; set; }
        //public int CountryId { get; set; }
        //public string CountryIso { get; set; }
        //public string Region { get; set; }
        //public int RegionId { get; set; }
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
        //public decimal? AcheivedTargets { get; set; }
        //public int? TargetMonth { get; set; }
        //public int? TargetYear { get; set; }
    }
}
