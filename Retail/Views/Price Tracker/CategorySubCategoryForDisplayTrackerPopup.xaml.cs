using System;
using System.Collections.Generic;
using Retail.ViewModels.PriceTracker;
using Xamarin.Forms;
using Retail.Models;
using Rg.Plugins.Popup.Services;

namespace Retail.Views.PriceTracker
{
    public partial class CategorySubCategoryForDisplayTrackerPopup
    {
        public CategorySubCategoryDisplayTrackerViewModel viewModel { get; set; }
        public int _Flag { get; set; }

        public CategorySubCategoryForDisplayTrackerPopup(int flag, DropDownModelForDisplayCategory categoryDropdownModel, DropDownModel SelectedStore)
        {
            InitializeComponent();
            BindingContext = viewModel = new CategorySubCategoryDisplayTrackerViewModel(Navigation, SelectedStore);
            if (flag == 1)
            {
                viewModel.IsCatgeory = true;
                viewModel.IsSubCatgeory = false;
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await viewModel.GetCategoryForDisplayTracker();
                });
            }
            else if (flag == 2)
            {

                viewModel.IsCatgeory = false;
                viewModel.IsSubCatgeory = true;
                Device.BeginInvokeOnMainThread(async () =>
                {
                    if (categoryDropdownModel != null)
                    {
                        viewModel.SelectedCategory = categoryDropdownModel;
                        viewModel.GetSubCategoryForDisplayTracker((long)categoryDropdownModel.Id);
                    }
                });
            }
        }

        async void TapGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
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
                        await viewModel.GetCategoryForDisplayTracker();
                }
                else if (_Flag == 2)
                {
                    if (!string.IsNullOrEmpty(e.NewTextValue))
                    {
                        viewModel.SearchSubcategoryCommand.Execute(e.NewTextValue);
                    }
                    else
                        viewModel.GetSubCategoryForDisplayTracker(viewModel.SelectedCategory.Id);
                }
            }
            else
            {
                if (_Flag == 1)
                {
                    await viewModel.GetCategoryForDisplayTracker();
                }
                else if (_Flag == 2)
                {
                    viewModel.GetSubCategoryForDisplayTracker(viewModel.SelectedCategory.Id);
                }
            }
        }
    }
}

