using System;
using System.Collections.Generic;
using Retail.ViewModels.SalesTarget;
using Xamarin.Forms;

namespace Retail.Views.SalesTargetViews
{
    public partial class TargetsByProductCategoryView : ContentPage
    {
        public TargetsByProductCategoryViewModel TargetsByProductCategoryViewModel { get; set; }
        public TargetsByProductCategoryView(string ModelName)
        {
            InitializeComponent();
            BindingContext = TargetsByProductCategoryViewModel = new TargetsByProductCategoryViewModel(Navigation, ModelName);

        }

        void SearchBar_SearchButtonPressed(System.Object sender, System.EventArgs e)
        {
            var SearchButton = sender as SearchBar;
            if(!string.IsNullOrEmpty(SearchButton.Text))
            {
                TargetsByProductCategoryViewModel.SearchTargetByProduct(SearchButton.Text.Trim());
            }
        }

        void SearchBar_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            string TargetProductName = string.Empty;
            if (!string.IsNullOrEmpty(e.NewTextValue))            
                TargetProductName = e.NewTextValue.Trim();

            TargetsByProductCategoryViewModel.SearchTargetByProduct(TargetProductName);
        }
    }
}
