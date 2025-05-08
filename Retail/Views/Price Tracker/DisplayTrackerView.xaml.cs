using System;
using System.Collections.Generic;
using System.Globalization;
using Retail.Hepler;
using Retail.Models;
using Retail.ViewModels.PriceTracker;
using Retail.Views.CommonPages;
using Rg.Plugins.Popup.Extensions;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;

namespace Retail.Views.PriceTracker
{
    public partial class DisplayTrackerView : ContentPage
    {
        public DisplayTrackerViewModel viewModel { get; set; }

        public DisplayTrackerView()
        {
            InitializeComponent();

            BindingContext = viewModel = new DisplayTrackerViewModel(Navigation);

            selectDate.Unfocused += SelectDate_Unfocused;

            //CultureInfo customCulture = (CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            //customCulture.NumberFormat.NumberDecimalSeparator = ".";
            //System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;
            //var mindate = DateTime.Now; //DateTime.Now.AddDays(-4);
            //selectDate.MinimumDate = mindate;
            if (!(String.IsNullOrEmpty(CommonAttribute.PrvSelectedDateDisplay)))
            {
                viewModel.SelectedDate = CommonAttribute.PrvSelectedDateDisplay;
                selectDate.SelectedDate = Convert.ToDateTime(CommonAttribute.PrvSelectedDateDisplay);
            }
            if (CommonAttribute.PrvSelectedStoreDisplay != null)
            {
                viewModel.SelectedStore = CommonAttribute.PrvSelectedStoreDisplay;
                viewModel.SelectedStoreText = viewModel.SelectedStore.Title.ToString();
                txtStore.Text = viewModel.SelectedStore.Title.ToString();
            }
            if(CommonAttribute.PrvModelForDisplayCategory != null)
            {
                viewModel.SelectedCategoryText = CommonAttribute.PrvModelForDisplayCategory.Title;
                viewModel.SelectedCategory = CommonAttribute.PrvModelForDisplayCategory;
            }
            if (CommonAttribute.PrvModelForDisplaySubCategory != null)
            {
                viewModel.SelectedSubCategoryText = CommonAttribute.PrvModelForDisplaySubCategory.Title;
                viewModel.SelectedSubCategory = CommonAttribute.PrvModelForDisplaySubCategory;
            }
        }

        void OnLabelTapped(object sender, EventArgs e)
        {
            Label label = sender as Label;
            Expander expander = label.Parent.Parent.Parent as Expander;

            if (label.FontSize == Device.GetNamedSize(NamedSize.Default, label))
            {
                label.FontSize = Device.GetNamedSize(NamedSize.Large, label);
            }
            else
            {
                label.FontSize = Device.GetNamedSize(NamedSize.Default, label);
            }
            expander.ForceUpdateSize();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            MessagingCenter.Subscribe<object, Models.DropDownModel>(this, "SelectedStoreDisplay", async (obj, SelectedStore) =>
            {
                if (SelectedStore != null)
                {
                    CommonAttribute.PrvSelectedStoreDisplay = SelectedStore;
                    viewModel.SelectedStoreText = SelectedStore.Title.ToString();
                    viewModel.SelectedStore = SelectedStore;                   
                    txtStore.Text = SelectedStore.Title.ToString();
                }
            });
            MessagingCenter.Subscribe<object, DropDownModelForDisplayCategory>(this, "SelectedbcategoryDisplay", (obj, Selectedbcategory) =>
            {
                CommonAttribute.PrvModelForDisplayCategory = Selectedbcategory;
                viewModel.SelectedCategoryText = Selectedbcategory.Title;
                viewModel.SelectedCategory = Selectedbcategory;
            });

            MessagingCenter.Subscribe<object, DropDownModelForDisplaySubCategory>(this, "SelectedSubCategoryDisplay", (obj, SelectedSubCategory) =>
            {
                CommonAttribute.PrvModelForDisplaySubCategory = SelectedSubCategory;
                viewModel.SelectedSubCategoryText = SelectedSubCategory.Title;
                viewModel.SelectedSubCategory = SelectedSubCategory;
                if (SelectedSubCategory != null)
                {
                    //IsBusy = true;
                    //viewModel.GetNoOfUnitsForModels();
                    //IsBusy = false;
                }
                else
                {
                    viewModel.ObProductModelResponses = new System.Collections.ObjectModel.ObservableCollection<Infrastructure.RequestModels.DisplayTrackerProductModelEntryRequest>();
                    viewModel.ObDispalyModelsResponse = new System.Collections.ObjectModel.ObservableCollection<Infrastructure.RequestModels.BrandNameRequest>();
                }
            });
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            Navigation.RemovePage(this);
        }

        protected override bool OnBackButtonPressed()
        {
            //Shell.Current.GoToAsync("../HomeRoute");

            Shell.Current.GoToAsync($"//MainRoute/HomeRoute");
            return true;
        }

        private void SelectDate_Unfocused(object sender, FocusEventArgs e)
        {
            var dp = sender as DatePicker;
            viewModel.SelectedDate = dp.Date.ToString("dd,MMMM,yyyy");
            CommonAttribute.PrvSelectedDateDisplay = dp.Date.ToString("dd,MMMM,yyyy");

        }

        void TapGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)
        {
            Device.InvokeOnMainThreadAsync(() =>
            {
                if (selectDate.IsFocused)
                    selectDate.Unfocus();

                var dp = sender as DatePicker;
                CommonAttribute.PrvSelectedDateDisplay = dp.Date.ToString("dd,MMMM,yyyy");

                selectDate.Focus();
            });
        }

        async void txtModelNo_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            await viewModel.ValidateModelNumber();
        }
        #region Category Selection
        async void txtCategory_Focused(System.Object sender, Xamarin.Forms.FocusEventArgs e)
        {
            await Navigation.PushPopupAsync(new CategorySubCategoryForDisplayTrackerPopup(1,null,viewModel.SelectedStore));
            txtSubcategory.Text = "";
        }

        async void TapGestureRecognizer_Tapped_2(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushPopupAsync(new CategorySubCategoryForDisplayTrackerPopup(1, null, viewModel.SelectedStore));
            txtSubcategory.Text = "";
        }
        #endregion

        #region SubCategory Selection
        async void txtSubcategory_Focused(System.Object sender, Xamarin.Forms.FocusEventArgs e)
        {
            await Navigation.PushPopupAsync(new CategorySubCategoryForDisplayTrackerPopup(2,viewModel.SelectedCategory, viewModel.SelectedStore));
        }

        async void TapGestureRecognizer_Tapped_1(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushPopupAsync(new CategorySubCategoryForDisplayTrackerPopup(2, viewModel.SelectedCategory, viewModel.SelectedStore));
            viewModel.IsBusy = false;
        }
        #endregion

        public void Entry_Unfocused(System.Object sender, Xamarin.Forms.FocusEventArgs e)
        {
            if (!(string.IsNullOrEmpty(e.ToString())))
            {
                //viewModel.TotalCompetitorUnits = viewModel.TotalCompetitorUnits + viewModel.TotalOtherCompetitorUnits;
                //viewModel.TotalDisplay = viewModel.TotalDisplay + viewModel.TotalOtherCompetitorUnits;
                 viewModel.TotalOtherCompetitorUnitsCommand.Execute(null);
            }
        }

        async void txtStore_Focused(System.Object sender, Xamarin.Forms.FocusEventArgs e)
        {
            await Navigation.PushPopupAsync(new StoreSelectionPopupPriceTracker(1)); //1=display store selection
        }

        void selectDate_ItemTapped(System.Object sender, System.EventArgs e)
        {
            var dp = sender as DatePicker;
            viewModel.SelectedDate = dp.Date.ToString("dd,MMMM,yyyy");
            CommonAttribute.PrvSelectedDateDisplay = dp.Date.ToString("dd,MMMM,yyyy");
        }

        async void TapGestureRecognizer_Tapped_3(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushPopupAsync(new StoreSelectionPopupPriceTracker(1)); //1=display store selection
        }
    }
}

