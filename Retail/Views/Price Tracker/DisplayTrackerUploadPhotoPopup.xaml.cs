using System;
using System.Collections.Generic;
using Retail.Hepler;
using Retail.ViewModels.PriceTracker;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Retail.Views.PriceTracker
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DisplayTrackerUploadPhotoPopup
    {
        public DisplayTrackerViewModel viewModel { get; set; }
        public DisplayTrackerUploadPhotoPopup()
        {
            InitializeComponent();
            BindingContext= viewModel = new DisplayTrackerViewModel(Navigation);
        }

        async void TapGestureRecognizer_Tapped_1(System.Object sender, System.EventArgs e)
        {
            if (viewModel.obDisplayTrackerEntryImageRequest != null && viewModel.obDisplayTrackerEntryImageRequest.Count > 0)
            {
                CommonAttribute.UploadedobDisplayTrackerEntryImageRequest = new System.Collections.ObjectModel.ObservableCollection<Infrastructure.RequestModels.DisplayTrackerEntryImageRequest>();
                CommonAttribute.UploadedobDisplayTrackerEntryImageRequest = viewModel.obDisplayTrackerEntryImageRequest;
            }
            
            await PopupNavigation.Instance.PopAsync();
        }

        async void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            if (viewModel.obDisplayTrackerEntryImageRequest != null && viewModel.obDisplayTrackerEntryImageRequest.Count > 0)
            {
                CommonAttribute.UploadedobDisplayTrackerEntryImageRequest = new System.Collections.ObjectModel.ObservableCollection<Infrastructure.RequestModels.DisplayTrackerEntryImageRequest>();
                CommonAttribute.UploadedobDisplayTrackerEntryImageRequest = viewModel.obDisplayTrackerEntryImageRequest;
            }
            await PopupNavigation.Instance.PopAsync();
        }
    }
}

