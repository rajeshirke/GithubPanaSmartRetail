using System;
using System.Collections.Generic;
using Retail.ViewModels.Dashboard;
using Xamarin.Forms;

namespace Retail.Views.Dashboard
{
    public partial class SampleCalender : ContentPage
    {
        public EventModel viewModel { get; set; }
        List<Stores> StoreNamesList;
        public SampleCalender()
        {
            InitializeComponent();
            BindingContext = viewModel = new EventModel();

            StoreNamesList = new List<Stores>();
            StoreNamesList.Add(new Stores { StoreName = "Panasonis store Sharjah" });
            StoreNamesList.Add(new Stores { StoreName = "Panasonis store LULU" });

            dropdownStores.ItemsSource = StoreNamesList;
            dropdownStores.SelectedItem = StoreNamesList[0];
            StoreDropdown.ItemsSource = StoreNamesList;
            StoreDropdown.SelectedItem = StoreNamesList[0];
        }

        void dropdownStores_SelectedIndexChanged(System.Object sender, System.EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;
        }

        void StoreDropdown_SelectedIndexChanged(System.Object sender, System.EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;
        }

    }
}
