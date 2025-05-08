using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Retail.Database;
using Retail.DependencyServices;
using Retail.Hepler;
using Retail.Infrastructure.ResponseModels;
using Retail.Infrastructure.ServiceLayer;
using Retail.Models;
using Retail.Resx;
using Retail.ViewModels.SalesTarget;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static Retail.Models.ReportsResponses;

namespace Retail.Views.SalesTargetViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MultiselectStorePopup : PopupPage
    {
       
        int _Flag;      
        TargetsOverviewViewModel viewModel { get; set; }
        Connection c = new Connection();
        private List<EntityKeyValueResponse> _Cities;
        public List<EntityKeyValueResponse> Cities
        {
            get { return _Cities; }
            set
            {
                _Cities = value;
                OnPropertyChanged("Cities");
            }
        }
        private List<EntityKeyValueResponse> _Locations;
        public List<EntityKeyValueResponse> Locations
        {
            get { return _Locations; }
            set
            {
                _Locations = value;
                OnPropertyChanged("Locations");
            }
        }
        public List<StoresList> StoresLists = new List<StoresList>();
        public List<CityList> CityLists = new List<CityList>();
        public List<SelectedItems> selectedItems { get; set; }
        public ObservableCollection<Cities> City { get; set; }
        public List<int> _CityIds { get; set; }

        private List<EntityKeyValueResponse> _CityBasedOnCountryList;
        public List<EntityKeyValueResponse> CityBasedOnCountryList
        {
            get { return _CityBasedOnCountryList; }
            set
            {
                _CityBasedOnCountryList = value;
                OnPropertyChanged(nameof(CityBasedOnCountryList));
            }
        }

        public MultiselectStorePopup(int flag, ObservableCollection<Cities> items)
        {
            InitializeComponent();
            BindingContext= viewModel = new TargetsOverviewViewModel(Navigation, flag);
            _Flag = flag;
            if (flag == 1)
            {
                if (items != null)
                {
                    City = new ObservableCollection<Cities>();
                    City = items;
                    GetLocations(City.ToList());
                }
            }
           
        }

        public MultiselectStorePopup(int flag, List<int>SelectedCountryIds)
        {
            InitializeComponent();
            BindingContext = viewModel = new TargetsOverviewViewModel(Navigation, flag);
            _Flag = flag;            
            if (flag == 2)
            {
                GetCitiesbySelectedCountry(SelectedCountryIds);
            }
        }
        //for multiselect cities
        public async void GetCitiesbySelectedCountry(List<int> SelectedCountries)
        {
            try
            {
                IsBusy = true;

                if (NetworkAvailable)
                {
                    CityBasedOnCountryList = new List<EntityKeyValueResponse>();
                    MasterDataManagementSL masterDataManagementSL = new MasterDataManagementSL();
                    List<EntityKeyValueResponse> CitiesBasedOnCountry = await masterDataManagementSL.GetCitiesByMultipleCountryId(SelectedCountries);
                    if (CitiesBasedOnCountry != null && CitiesBasedOnCountry.Count > 0)
                    {
                        CityBasedOnCountryList = CitiesBasedOnCountry;
                        CityLists.Add(new CityList() { ID = 0, Text = "Select all" });
                        foreach (var item in CitiesBasedOnCountry)
                        {
                            CityLists.Add(new CityList() { ID = item.Id, Text = item.Name });
                        }

                        var list = CityLists.Take(1).Concat(CityLists.Skip(1).OrderBy(c => c.Text)).ToList();
                        CityLists = new List<CityList>(list);
                        listView.ItemsSource = CityLists;
                    }

                }
                else
                {
                    //showToasterNoNetwork();
                }
            }
            catch (Exception ex)
            {
                Debugger.Log(0, null, ex.ToString());
            }
            finally
            {
                IsBusy = false;
            }
        }

        public async void GetCities()
        {
            try
            {
                IsBusy = true;

                if (NetworkAvailable)
                {
                    //int CountryId = 224;
                    MasterDataManagementSL masterDataManagementSL = new MasterDataManagementSL();
                    Cities = new List<EntityKeyValueResponse>();
                    Cities = await masterDataManagementSL.GetCitiesByCountryId(CommonAttribute.CustomeProfile.CountryId);
                    if (Cities!= null && Cities.Count != 0)
                    {
                        CityLists.Add(new CityList() { ID = 0, Text = "Select all" });
                        
                        foreach (var location in Cities)
                        {
                            CityLists.Add(new CityList() { ID = location.Id, Text = location.Name });
                            //_LocationIds.Add(location.Id); _LocationNames.Add(location.Name);

                        }

                        var list = CityLists.Take(1).Concat(CityLists.Skip(1).OrderBy(c => c.Text)).ToList();
                        CityLists = new List<CityList>(list);
                        listView.ItemsSource = CityLists;
                    }
                }
                else
                {
                    //var messageToast = DependencyService.Get<IMessage>();
                    //messageToast.DismissAlert();
                    //messageToast.ShortAlert("You are offline");
                }
            }
            catch (Exception ex)
            {
                Debugger.Log(0, null, ex.ToString());
            }
            finally
            {
                IsBusy = false;
            }
        }

        public async void GetLocations(List<Cities> selecteds)
        {
            try
            {
                IsBusy = true;

                if (NetworkAvailable)
                {
                    long? personId = CommonAttribute.CustomeProfile?.PersonId;
                    _CityIds = new List<int>();
                    foreach (var item in selecteds)
                    {
                        _CityIds.Add(item.ID);
                    }
                    MasterDataManagementSL masterDataManagementSL = new MasterDataManagementSL();
                    Locations = new List<EntityKeyValueResponse>();
                    Locations = await masterDataManagementSL.GetLocationsByCityIds(personId,_CityIds);
                    if (Locations !=null && Locations.Count != 0)
                    {
                        StoresLists.Add(new StoresList() { ID = 0, Text = "Select all" });
                        foreach (var location in Locations)
                        {
                            StoresLists.Add(new StoresList() { ID = location.Id, Text = location.Name });
                            //_LocationIds.Add(location.Id); _LocationNames.Add(location.Name);
                        }
                        var list = StoresLists.Take(1).Concat(StoresLists.Skip(1).OrderBy(c => c.Text)).ToList();
                        StoresLists = new List<StoresList>(list);
                        listView.ItemsSource = StoresLists;
                    }
                }
                else
                {
                    //var messageToast = DependencyService.Get<IMessage>();
                    //messageToast.DismissAlert();
                    //messageToast.ShortAlert("You are offline");
                }
            }
            catch (Exception ex)
            {
                Debugger.Log(0, null, ex.ToString());
            }
            finally
            {
                IsBusy = false;
            }
        }

        async void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            try
            {
                await Navigation.PopPopupAsync();
                if (_Flag == 1)
                {
                    selectedItems = new List<SelectedItems>();
                    var result = StoresLists.Where(w => w.IsChecked == true).ToList();
                    string s = "";
                    viewModel.Stores = new List<LocationModel>(); viewModel._LocationIds = new List<int>(); viewModel._LocationNames = new List<string>();
                    int index = 0;

                    foreach (var model in result)
                    {
                        if (model.ID != 0)
                        {
                            selectedItems.Add(new SelectedItems { ID = model.ID, Name = model.Text });
                        }
                        if (result.Count == 1)
                        {
                            s = model.Text;
                        }
                        else
                        {
                            s = s + model.Text;
                            if (index < result.Count - 1)
                            {
                                s = s + ",";
                            }
                            index++;
                        }
                    }

                    MessagingCenter.Send<List<SelectedItems>>(selectedItems, "SelectedStoreLists");

                }
                else if (_Flag == 2)
                {

                    var result = CityLists.Where(w => w.IsChecked == true).ToList();
                    string s = "";
                    selectedItems = new List<SelectedItems>();
                    int index = 0;
                    foreach (var model in result)
                    {
                        if (model.ID != 0)
                        {
                            selectedItems.Add(new SelectedItems { ID = model.ID, Name = model.Text });
                        }
                        if (result.Count == 1)
                        {
                            s = model.Text;
                        }
                        else
                        {
                            s = s + model.Text;
                            if (index < result.Count - 1)
                            {
                                s = s + ",";
                            }
                            index++;
                        }
                    }
                    MessagingCenter.Send<List<SelectedItems>>(selectedItems, "SelectedCityLists");
                }
            }
            catch (Exception ex)
            {
                Debugger.Log(0, null, ex.ToString());
            }
        }

        public bool NetworkAvailable { get { return Retail.Hepler.Extensions.CheckNetwrokAvailability(); } }

        public class StoresList : INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler PropertyChanged;

            // This method is called by the Set accessor of each property.  
            // The CallerMemberName attribute that is applied to the optional propertyName  
            // parameter causes the property name of the caller to be substituted as an argument.  
            private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }

            public StoresList()
            {
                IsChecked = false;
            }

            public string Text { get; set; }
            public int ID { get; set; }

            private bool isChecked;
            public bool IsChecked
            {
                get
                {
                    return isChecked;
                }
                set
                {
                    isChecked = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public class CityList : INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler PropertyChanged;

            // This method is called by the Set accessor of each property.  
            // The CallerMemberName attribute that is applied to the optional propertyName  
            // parameter causes the property name of the caller to be substituted as an argument.  
            private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }

            public CityList()
            {
                IsChecked = false;
            }

            public string Text { get; set; }
            public int ID { get; set; }

            private bool isChecked;
            public bool IsChecked
            {
                get
                {
                    return isChecked;
                }
                set
                {
                    isChecked = value;
                    NotifyPropertyChanged();
                }
            }
        }

        async void TapGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
        }

        void CheckBox_CheckedChanged(System.Object sender, Xamarin.Forms.CheckedChangedEventArgs e)
        {
            if (_Flag == 1)
            {
                var IscheckedSelectAll = StoresLists.Where(c => c.ID == 0).ToList();
                if (IscheckedSelectAll != null && IscheckedSelectAll.Count > 0)
                {
                    StoresLists.ForEach(c => c.IsChecked = !(c.IsChecked));
                    listView.ItemsSource = StoresLists;
                }
            }
            if (_Flag == 2)
            {
                var IscheckedSelectAll = CityLists.Where(c => c.ID == 0).ToList(); 
                if (IscheckedSelectAll != null && IscheckedSelectAll.Count > 0)
                {
                    CityLists.ForEach(c => c.IsChecked = !(c.IsChecked));
                    listView.ItemsSource = CityLists;
                }
            }
        }
    }
}


