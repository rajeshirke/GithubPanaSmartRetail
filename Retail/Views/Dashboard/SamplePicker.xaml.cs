using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Retail.Views.Dashboard
{
    public partial class SamplePicker : ContentPage
    {
        List<CountryClass> countryList = new List<CountryClass>();
        List<StateClass> stateList = new List<StateClass>();

        public SamplePicker()
        {
            InitializeComponent();

            List<StateClass> stateList1 = new List<StateClass>();
            stateList1.Add(new StateClass { Name = "StateA1"});
            stateList1.Add(new StateClass { Name = "StateA2"});

            List<StateClass> stateList2 = new List<StateClass>();
            stateList2.Add(new StateClass { Name = "StateB1"});
            stateList2.Add(new StateClass { Name = "StateB2"});

            countryList.Add(new CountryClass { Name = "CountryA", State = stateList1 }); ;
            countryList.Add(new CountryClass { Name = "CountryB", State = stateList2 });

            dropdown.ItemsSource = countryList;
            dropdown.SelectedItem = countryList[0];
        }

        void dropdown_SelectedIndexChanged(System.Object sender, System.EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;

            if (selectedIndex != -1)
            {
                stateList = countryList[selectedIndex].State;
                dropdown1.ItemsSource = stateList;
                dropdown1.SelectedItem = stateList[0];
            }
        }

        void dropdown1_SelectedIndexChanged(System.Object sender, System.EventArgs e)
        {
            
        }
    }
}

public class CountryClass
{
    public string Name { get; set; }
    public string Details { get; set; }
    public string ImageUrl { get; set; }

    public List<StateClass> State { get; set; }
}

public class StateClass
{
    public string Name { get; set; }
    public string Details { get; set; }
    public string ImageUrl { get; set; }

}