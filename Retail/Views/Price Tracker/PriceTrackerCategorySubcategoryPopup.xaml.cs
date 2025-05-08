using System;
using System.Collections.Generic;
using Retail.Infrastructure.ResponseModels;
using Retail.Models;
using Retail.ViewModels.PriceTracker;
using Retail.ViewModels.SalesTarget;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Retail.Hepler;

namespace Retail.Views.PriceTracker
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PriceTrackerCategorySubcategoryPopup
    {
        public PriceTrackerCategorySubcategoryViewModel viewModel { get; set; }
        public PriceTrackerCategorySubcategoryPopup(long SelectedStoreID)
        {
            InitializeComponent();
            BindingContext = viewModel = new PriceTrackerCategorySubcategoryViewModel(Navigation, SelectedStoreID);
            if(CommonAttribute.PrvSelectedCatgeory != null)
            {
                viewModel.SelectedCategoryText = CommonAttribute.PrvSelectedCatgeory.Title;
                viewModel.SelectedCategory = CommonAttribute.PrvSelectedCatgeory;
            }
            if(CommonAttribute.PrvSelectedSubCategory != null)
            {
                viewModel.SelectedSubCategoryText = CommonAttribute.PrvSelectedSubCategory.Title;
                viewModel.SelectedSubCategory = CommonAttribute.PrvSelectedSubCategory;
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            MessagingCenter.Subscribe<object, DropDownModel>(this, "Selectedbcategory", (obj, Selectedbcategory) =>
            {
                viewModel.SelectedCategoryText = Selectedbcategory.Title;
                viewModel.SelectedCategory = Selectedbcategory;
                CommonAttribute.PrvSelectedCatgeory = Selectedbcategory;
            });

            MessagingCenter.Subscribe<object, SubcategoryDropdownModel>(this, "SelectedSubCategory", (obj, SelectedSubCategory) =>
            {
                viewModel.SelectedSubCategoryText = SelectedSubCategory.Title;
                viewModel.SelectedSubCategory = SelectedSubCategory;
                CommonAttribute.PrvSelectedSubCategory = SelectedSubCategory;
            });
        }

        async void autoComplete_SelectionChanged(System.Object sender, Syncfusion.SfAutoComplete.XForms.SelectionChangedEventArgs e)
        {
            //var SelctedCategoryAutocomplete = autoComplete.SelectedItem;
            //viewModel.SelectedCategory = (Models.DropDownModel)autoComplete.SelectedItem;
            //if(viewModel.SelectedCategory != null)
            //{
            //    await viewModel.GetSubCategoryForPriceDisplayTracker((long)viewModel.SelectedCategory.Id);
            //}
        }

        async void TapGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
        }

        void autoCompleteSubCat_SelectionChanged(System.Object sender, Syncfusion.SfAutoComplete.XForms.SelectionChangedEventArgs e)
        {
            //var SelctedSubCategoryAutocomplete = autoComplete.SelectedItem;
            //viewModel.SelectedSubCategory = (Models.SubcategoryDropdownModel)autoCompleteSubCat.SelectedItem;
        }

        async void SearchModel_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.NewTextValue))
            {
                viewModel.SearchModelByInitialCommand.Execute(e.NewTextValue);
            }
            else
                await viewModel.GetProductModelsForPriceDisplayTracker();
        }

        async void txtCategory_Focused(System.Object sender, Xamarin.Forms.FocusEventArgs e)
        {
            await Navigation.PushPopupAsync(new CategorySubCategorySearchPopup(1, null));
            txtSubcategory.Text = "";
        }

        async void TapGestureRecognizer_Tapped_1(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushPopupAsync(new CategorySubCategorySearchPopup(1, null));
            txtSubcategory.Text = "";
        }

        async void txtSubcategory_Focused(System.Object sender, Xamarin.Forms.FocusEventArgs e)
        {
            await Navigation.PushPopupAsync(new CategorySubCategorySearchPopup(2, viewModel.SelectedCategory));
        }

        async void TapGestureRecognizer_Tapped_2(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushPopupAsync(new CategorySubCategorySearchPopup(2, viewModel.SelectedCategory));
        }
    }
}

