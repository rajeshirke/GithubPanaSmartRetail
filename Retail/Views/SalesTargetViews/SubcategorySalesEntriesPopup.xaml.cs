using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Retail.DependencyServices;
using Retail.Hepler;
using Retail.Infrastructure.ResponseModels;
using Retail.Infrastructure.ServiceLayer;
using Retail.Resx;
using Rg.Plugins.Popup.Services;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Retail.Views.SalesTargetViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SubcategorySalesEntriesPopup
    {
        public SubcategorySalesEntriesPopup(int SelectedSubcategoryId,DateTime  SelectedMonthYear,string SelectedSubcategoryName)
        {
            InitializeComponent();
            SubcategoryName.Text = SelectedSubcategoryName;
            IsBusy = true;
            
            Device.BeginInvokeOnMainThread(async () => {
                await GetSalesEntriesBySubcategory(SelectedSubcategoryId, SelectedMonthYear);
                IsBusy = false;
            });

        }

        public bool NetworkAvailable { get { return Retail.Hepler.Extensions.CheckNetwrokAvailability(); } }

        public async Task GetSalesEntriesBySubcategory(int SubcategoryId,DateTime SelectedDate)
        {
            try
            {
                IsBusy = true;

                if (NetworkAvailable)
                {
                    IsBusy = true;
                    DateTime FromDate = new DateTime(SelectedDate.Year, SelectedDate.Month, 1);
                    DateTime Todate = FromDate.AddMonths(1).AddDays(-1);

                    SalesTargetManagementSL salesTargetManagementSL = new SalesTargetManagementSL();
                    List<SalesTargetEntryResponse> salesTargetEntryResponse = await salesTargetManagementSL.GetSalesEntryByPersonIdSubCategoryId((int)CommonAttribute.CustomeProfile?.PersonId, SubcategoryId, FromDate.ToString("yyyy-MM-dd"), Todate.ToString("yyyy-MM-dd"));
                    if (salesTargetEntryResponse != null)
                    {
                        foreach (var item in salesTargetEntryResponse)
                        {
                            item.CurrencyCode = CommonAttribute.CustomeProfile?.CurrencyCode;
                        }

                        SalesEntriesBySubcategory.ItemsSource = salesTargetEntryResponse.OrderByDescending(d => d.EntryDate);
                    }
                }
                else
                {
                    //var messageToast = DependencyService.Get<IMessage>();
                    //messageToast.DismissAlert();
                    //messageToast.ShortAlert("You are offline");
                }
            }
            catch (Exception ex)
            {
                Debugger.Log(0, null, ex.ToString());
            }
            finally
            {
                IsBusy = false;
            }
        }

        async void TapGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
        }
    }
}
