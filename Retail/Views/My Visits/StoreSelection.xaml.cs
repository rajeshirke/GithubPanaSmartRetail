using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Retail.Infrastructure.ResponseModels;
using Retail.ViewModels.MyVisits;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Retail.Views.MyVisits
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StoreSelection
    {
        StoreSelectionViewModel viewModel { get; set; }
        public StoreSelection(ObservableCollection<VisitScheduleLocationResponse> locationResponses)
        {
            InitializeComponent();
            BindingContext = viewModel = new StoreSelectionViewModel(Navigation, locationResponses);
        }

        async void SearchStore_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.NewTextValue))
            {
                viewModel.SearchLocationCommand.Execute(e.NewTextValue);
            }
            else
                await viewModel.GetLocation();
        }

        async void TapGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
        }
    }
}
