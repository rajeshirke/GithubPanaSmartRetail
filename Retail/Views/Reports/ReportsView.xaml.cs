using System;
using System.Collections.Generic;
using Retail.ViewModels.Reports;
using Xamarin.Forms;

namespace Retail.Views.Reports
{
    public partial class ReportsView : ContentPage
    {
        public ReportsViewModel viewModel {get;set;}
        public ReportsView()
        {
            InitializeComponent();
            BindingContext = viewModel = new ReportsViewModel(Navigation);
        }

        protected override bool OnBackButtonPressed()
        {
            //Shell.Current.GoToAsync("../HomeRoute");
            Shell.Current.GoToAsync($"//MainRoute/HomeRoute");
            return true;
        }
    }
}
