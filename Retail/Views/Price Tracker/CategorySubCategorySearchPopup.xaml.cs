using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Retail.Infrastructure.ResponseModels;
using Retail.Models;
using Retail.ViewModels.PriceTracker;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using static Retail.Views.CommonPages.MultiselectPopupView;

namespace Retail.Views.PriceTracker
{
    public partial class CategorySubCategorySearchPopup
    {
        public PriceTrackerCategorySubcategoryViewModel viewModel { get; set; }
        public int _Flag { get; set; }

        public CategorySubCategorySearchPopup(int flag, DropDownModel categoryDropdownModel)
        {
            InitializeComponent();
            _Flag = flag;
            BindingContext = viewModel = new PriceTrackerCategorySubcategoryViewModel(Navigation);
            if (flag == 1)
            {
                viewModel.IsCatgeory = true;
                viewModel.IsSubCatgeory = false;
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await viewModel.GetCategoryForPriceDisplayTracker();
                });
            }
            if (flag == 2)
            {
                viewModel.IsCatgeory = false;
                viewModel.IsSubCatgeory = true;
                Device.BeginInvokeOnMainThread(async () =>
                {
                    if (categoryDropdownModel != null)
                    {
                        viewModel.SelectedCategory = categoryDropdownModel;
                        await viewModel.GetSubCategoryForPriceDisplayTracker((long)categoryDropdownModel.Id);
                    }                    
                });
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            MessagingCenter.Subscribe<object, EntityKeyValueResponse>(this, "SelectedModelNoPriceDisplayTracker", async (obj, entityKeyValueResponse) =>
            {
                if (entityKeyValueResponse != null)
                {

                }
            });
        }

        async void search_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.NewTextValue))
            {
                if (_Flag == 1)
                {
                    if (!string.IsNullOrEmpty(e.NewTextValue))
                    {
                        viewModel.SearchCatgeoryCommand.Execute(e.NewTextValue);
                    }
                    else
                        await viewModel.GetCategoryForPriceDisplayTracker();
                }
                else if (_Flag == 2)
                {
                    if (!string.IsNullOrEmpty(e.NewTextValue))
                    {
                        viewModel.SearchSubcategoryCommand.Execute(e.NewTextValue);
                    }
                    else
                        await viewModel.GetSubCategoryForPriceDisplayTracker(viewModel.SelectedCategory.Id);
                }
            }
            else
            {
                if (_Flag == 1)
                {
                    await viewModel.GetCategoryForPriceDisplayTracker();
                }
                else if (_Flag == 2)
                {
                    await viewModel.GetSubCategoryForPriceDisplayTracker(viewModel.SelectedCategory.Id);
                }
            }
        }

        async void TapGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
        }
    }
}

