using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Retail.Database;
using Retail.Hepler;
using Retail.Infrastructure.RequestModels;
using Retail.Models;
using Retail.ViewModels.SalesTarget;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Retail.Views.SalesTargetViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConfirmSlesDataEntryPopup
    {
        public SalesDataEntryDb dataEntryDb;

        public ConfirmSlesDataEntryPopup(List<SalesEntryRequest> salesEntries ,int flag)
        {
            InitializeComponent();
            
            dataEntryDb = new SalesDataEntryDb();
            SalesDataEntryLists = new ObservableCollection<SalesEntryRequest> (salesEntries);
            foreach(var item in SalesDataEntryLists)
            {
                item.CurrencyCode = CommonAttribute.CustomeProfile?.CurrencyCode;
            }
            SalesDataLists.ItemsSource = SalesDataEntryLists;

            BindingContext = new ConfirmSlesDataEntryPopupViewModel(Navigation, salesEntries,flag);

            if (flag == 2)
                txtTotalEntries.Text = "Total Return Entries :";
            else
                txtTotalEntries.Text = "Total Entries :";
        }

        async void TapGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)
        {
            bool IsSubmitEnable = true;
            MessagingCenter.Send<object, bool>(this, "AllowSubmit", IsSubmitEnable);
            await PopupNavigation.Instance.PopAsync();
        }


        private ObservableCollection<SalesEntryRequest> _SalesDataEntryLists;
        public ObservableCollection<SalesEntryRequest> SalesDataEntryLists
        {
            get { return _SalesDataEntryLists; }
            set
            {
                _SalesDataEntryLists = value;
                OnPropertyChanged(nameof(SalesDataEntryLists));
            }
        }

    }
}
