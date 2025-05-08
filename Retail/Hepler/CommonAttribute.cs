using System;
using Retail.Infrastructure.RequestModels;
using System.Collections.ObjectModel;
using Retail.Infrastructure.ResponseModels;
using Retail.Models;
using Xamarin.Forms;

namespace Retail.Hepler
{
    public class CommonAttribute
    {
        public CommonAttribute()
        {
        }

        ///*panasonic production*/
        public static string ImageBaseUrl { get; set; } = "http://devsrv01.panasonic.ae:92/Files/";

        /*CommonAttribute properties*/
        public static UserCurrentLocation UserLocation { get; set; }
        public static PersonLoginResponse CustomeProfile { get; set; }
        public static int ScreenHeight { get; set; }
        public static int ScreenWidth { get; set; }
        public static int CartCount { get; set; }
        public static string Custpwd { get; set; }
        public static FlowDirection flowDirection { get; set; }
        public static DateTime PreferData { get; set; }
        public static TextAlignment TextAlignment { get; set; }
        public static LanguageModel selectedLang { get; set; }
        public static LocationResponse VisitLocation { get; set; }
        public static int VideoLength { get; set; } = 10000000; //bytes
        public static ObservableCollection<DisplayTrackerEntryImageRequest> UploadedobDisplayTrackerEntryImageRequest { get; set; }

        /*Price Tracker Previous Selected Values*/
        public static string PrvSelectedDate { get; set; }
        public static DropDownModel PrvSelectedStore { get; set; }
        public static ProductModelForPriceDisplayResponse PrvSelectedModelNo { get; set; }
        public static DropDownModel PrvSelectedCatgeory { get; set; }
        public static SubcategoryDropdownModel PrvSelectedSubCategory { get; set; }
        public static ObservableCollection<ProductModelForPriceDisplayResponse> AllModelNumbers { get; set; }
        //***

        //***Display Tracker Previous Selected Values
        public static string PrvSelectedDateDisplay { get; set; }
        public static DropDownModel PrvSelectedStoreDisplay { get; set; }
        public static DropDownModelForDisplayCategory PrvModelForDisplayCategory { get; set; }
        public static DropDownModelForDisplaySubCategory PrvModelForDisplaySubCategory { get; set; }

    }
}
