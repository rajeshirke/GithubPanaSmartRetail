using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Retail.Hepler;
using Retail.Infrastructure.ResponseModels;
using Retail.Infrastructure.ServiceLayer;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Forms;
using Rg.Plugins.Popup.Services;
using Retail.Resx;
using Xamarin.Essentials;
using System.Diagnostics;
using Retail.DependencyServices;
using System.Threading.Tasks;
using Retail.Infrastructure.RequestModels;
using static Retail.Models.ReportsResponses;
using Retail.Models;

namespace Retail.Views.CommonPages
{

    public partial class MultiselectPopupView
    {
        public event EventHandler<ObservableCollection<SelectedItems>> ProcessCompleted;
        int _flag, _LocationId, _PersonId,_flgMultiStoreSup;
        private List<EntityKeyValueResponse> _Location;
        public List<EntityKeyValueResponse> Location
        {
            get { return _Location; }
            set
            {
                _Location = value;
                OnPropertyChanged("Location");
            }
        }
        private List<EntityKeyValueResponse> _Promoters;
        public List<EntityKeyValueResponse> Promoters
        {
            get { return _Promoters; }
            set
            {
                _Promoters = value;
                OnPropertyChanged("Promoters");
            }
        }
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
        public List<SubCategory> SubcategoriesLists = new List<SubCategory>();
        public List<Locations> LocationsLists = new List<Locations>();
        public ObservableCollection<Locations> LocationsListsAll = new ObservableCollection<Locations>();
        public List<Promoter> PromoterList = new List<Promoter>();
        public List<LstCity> CityList = new List<LstCity>();
        public List<Countries> CountryList = new List<Countries>();
        public List<Accounts> AccountList = new List<Accounts>();
        public List<Supervisors> SupervisorList = new List<Supervisors>();
        public ObservableCollection<SelectedItems> selectedItems { get; set; }
        public List<int> _LocationIds { get; set; }

        public MultiselectPopupView(int PersonId, List<int> LocationIds, int Flag)
        {
            InitializeComponent();
            _flag = Flag; _PersonId = PersonId;
            _LocationIds = new List<int>(); _LocationIds = LocationIds;
            if (Flag == 1)
            {
                if (PersonId != 0)
                {
                    GetPromoterList();
                }
            }
            else if (Flag == 2)
            {
                if (_LocationIds.Count != 0)
                {
                    //GetLocations();
                    GetLocationbyPersonId();
                }
            }
        }

        public MultiselectPopupView(int Flag, List<int> SelectedCountryCity)
        {
            InitializeComponent();
            _flag = Flag;
            IsBusy = true;
            Device.BeginInvokeOnMainThread(async () =>
            {
                if (_flag == 5)
                {
                    await GetCountries();
                }
                if (_flag == 6)
                {
                    await GetCities(SelectedCountryCity);
                }
                if (_flag == 7)
                {
                    await GetAccounts(SelectedCountryCity);
                }
                if (_flag == 11)
                {
                    await GetCountries();
                }

                IsBusy = false;
            });
        }

        public MultiselectPopupView(int Flag, List<int> SelectedCountryCity,string status)
        {
            InitializeComponent();
            _flag = Flag;
            IsBusy = true;
            Device.BeginInvokeOnMainThread(async () =>
            {
                if (_flag == 10)
                {
                    await GetCountries();
                }               
                IsBusy = false;
            });
        }

        public MultiselectPopupView(int Flag, ReportCreateUpdateRequest reportCreateUpdateRequest,int multiplestoresup)
        {
            InitializeComponent();
            _flag = Flag;
            _flgMultiStoreSup = multiplestoresup;
            IsBusy = true;
            Device.BeginInvokeOnMainThread(async () =>
            {
                if (_flag == 8)
                {
                    await GetLocationsByMultipleCountryAccount(reportCreateUpdateRequest);
                }
                if (_flag == 9)
                {
                    await GetSupervisors(reportCreateUpdateRequest);
                }
                IsBusy = false;
            });
        }

        public MultiselectPopupView(int PersonId, int Flag, int selectall)
        {
            InitializeComponent();
            _flag = Flag;
            if (Flag == 13)
            {
                SearchGrid.IsVisible = true;
            }
            GetLocationbyPersonId(13);
        }

        public MultiselectPopupView(List<string> stores, int PersonId, int Flag)
        {
            InitializeComponent();
            _flag = Flag;
            GetLocationbyPersonId(stores);
        }

        public MultiselectPopupView(int Flag)
        {
            InitializeComponent();

            _flag = Flag;
           
            Device.BeginInvokeOnMainThread(async () =>
            {
                if (_flag == 12)
                {
                    await GetSubcategory();
                }
                IsBusy = false;
            });
        }

        public async void GetLocationbyPersonId(List<string> stores)
        {

            try
            {
                IsBusy = true;

                if (NetworkAvailable)
                {
                    Location = new List<EntityKeyValueResponse>();
                    MasterDataManagementSL masterDataManagementSL = new MasterDataManagementSL();
                    Location = await masterDataManagementSL.GetLocationsByPersonId((int)CommonAttribute.CustomeProfile.PersonId);
                    if (Location != null && Location.Count > 0)
                    {
                        LocationsLists.Add(new Locations() { ID = 0, Text = "Select all" });
                        
                        foreach (var location in Location)
                        {
                            LocationsLists.Add(new Locations() { ID = location.Id, Text = location.Name });

                            if (_LocationId == location.Id)
                            {
                                LocationsLists.Where(l => l.IsChecked == true).Select(s => s.ID == _LocationId);
                            }

                            if (stores != null)
                            {
                                foreach (var st in stores)
                                {
                                    if (location.Name == st)
                                    {
                                        LocationsLists.Where(s => s.Text == st).Any(st => st.IsChecked == true);
                                    }
                                }
                            }
                        }
                        var list = LocationsLists.Take(1).Concat(LocationsLists.Skip(1).OrderBy(c => c.Text)).ToList();
                        LocationsLists = new List<Locations>(list);
                        listView.ItemsSource = LocationsLists;
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

        public MultiselectPopupView(int Flag, List<int?> Countries, int status)
        {
            InitializeComponent();
            _flag = Flag;
            List<int?> CountryIds = new List<int?>();
            if (Countries != null)
            {
                CountryIds = Countries;
            }
                GetLocationbyCountryPersonId(CountryIds);
        }

        public async void GetLocationbyCountryPersonId(List<int?> CountryIds)
        {

            try
            {
                IsBusy = true;

                if (NetworkAvailable)
                {
                    Location = new List<EntityKeyValueResponse>();
                    MasterDataManagementSL masterDataManagementSL = new MasterDataManagementSL();
                    Location = await masterDataManagementSL.GetLocationsByPersonIdCountryIds((int)CommonAttribute.CustomeProfile?.PersonId, CountryIds);
                    if (Location!= null && Location != null)
                    {
                        LocationsLists.Add(new Locations() { ID = 0, Text = "Select all" });

                        foreach (var location in Location)
                        {
                            LocationsLists.Add(new Locations() { ID = location.Id, Text = location.Name });

                            if (_LocationId == location.Id)
                            {
                                LocationsLists.Where(l => l.IsChecked == true).Select(s => s.ID == _LocationId);

                            }
                        }

                        var list = LocationsLists.Take(1).Concat(LocationsLists.Skip(1).OrderBy(c => c.Text)).ToList();
                        LocationsLists = new List<Locations>(list);
                        listView.ItemsSource = LocationsLists;
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

        public async void GetLocationbyPersonId()
        {

            try
            {
                IsBusy = true;

                if (NetworkAvailable)
                {
                    Location = new List<EntityKeyValueResponse>();
                    MasterDataManagementSL masterDataManagementSL = new MasterDataManagementSL();
                    Location = await masterDataManagementSL.GetLocationsByPersonId((int)CommonAttribute.CustomeProfile.PersonId);
                    if (Location != null && Location.Count > 0)
                    {
                        LocationsLists.Add(new Locations() { ID = 0, Text = "Select all" });

                        foreach (var location in Location)
                        {
                            LocationsLists.Add(new Locations() { ID = location.Id, Text = location.Name });

                            if (_LocationId == location.Id)
                            {
                                LocationsLists.Where(l => l.IsChecked == true).Select(s => s.ID == _LocationId);

                            }
                        }
                        var list = LocationsLists.Take(1).Concat(LocationsLists.Skip(1).OrderBy(c => c.Text)).ToList();
                        LocationsLists = new List<Locations>(list);
                        listView.ItemsSource = LocationsLists;
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

        public async void GetLocationbyPersonId(int flag)
        {

            try
            {
                IsBusy = true;
                if (flag == 13)
                {
                    if (NetworkAvailable)
                    {
                        
                        Location = new List<EntityKeyValueResponse>();
                        LocationsLists = new List<Locations>();
                        MasterDataManagementSL masterDataManagementSL = new MasterDataManagementSL();
                        Location = await masterDataManagementSL.GetLocationsByPersonId((int)CommonAttribute.CustomeProfile.PersonId);
                        if (Location != null && Location.Count > 0)
                        {
                            LocationsLists.Add(new Locations() { ID = 0, Text = "Select all"});

                            foreach (var location in Location)
                            {
                                LocationsLists.Add(new Locations() { ID = location.Id, Text = location.Name });

                                if (_LocationId == location.Id)
                                {
                                    LocationsLists.Where(l => l.IsChecked == true).Select(s => s.ID == _LocationId);

                                }
                            }
                            var list = LocationsLists.Take(1).Concat(LocationsLists.Skip(1).OrderBy(c => c.Text)).ToList();                           
                            LocationsLists = new List<Locations>(list);
                           
                            //var IscheckedSelectAll = LocationsLists.Where(c => c.ID == 0).ToList();
                            //if (IscheckedSelectAll != null && IscheckedSelectAll.Count > 0)
                            //{
                            //    LocationsLists.ForEach(c => c.IsChecked = !(c.IsChecked));
                            //    listView.ItemsSource = LocationsLists;
                            //}
                            
                            listView.ItemsSource = LocationsLists;
                        }
                    }
                    else
                    {
                        //var messageToast = DependencyService.Get<IMessage>();
                        //messageToast.DismissAlert();
                        //messageToast.ShortAlert("You are offline");
                    }
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

        public async void GetLocations()
        {

            try
            {
                IsBusy = true;

                if (NetworkAvailable)
                {
                    MasterDataManagementSL masterDataManagementSL = new MasterDataManagementSL();

                    Location = await masterDataManagementSL.GetLocationsByPersonId(_PersonId);
                    if (Location != null && Location.Count != 0)
                    {
                        LocationsLists.Add(new Locations() { ID = 0, Text = "Select all" });
                        foreach (var location in Location)
                        {
                            LocationsLists.Add(new Locations() { ID = location.Id, Text = location.Name });

                            if (_LocationId == location.Id)
                            {
                                LocationsLists.Where(l => l.IsChecked == true).Select(s => s.ID == _LocationId);

                            }
                        }
                        var list = LocationsLists.Take(1).Concat(LocationsLists.Skip(1).OrderBy(c => c.Text)).ToList();
                        LocationsLists = new List<Locations>(list);
                        listView.ItemsSource = LocationsLists;
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

        public async void GetPromoterList()
        {

            try
            {
                IsBusy = true;

                if (NetworkAvailable)
                {
                    MasterDataManagementSL masterDataManagementSL = new MasterDataManagementSL();
                    Promoters = new List<EntityKeyValueResponse>();
                    Promoters = await masterDataManagementSL.GetPromotersByMultiLocation(_LocationIds);
                    if (Promoters != null && Promoters.Count != 0)
                    {
                        PromoterList.Add(new Promoter() { ID = 0, Text = "Select all" });
                        foreach (var person in Promoters)
                        {
                            PromoterList.Add(new Promoter() { ID = person.Id, Text = person.Name });
                            if (_PersonId == person.Id)
                            {
                                PromoterList.Where(s => s.ID == _LocationId).Select(l => l.IsChecked == true);
                            }
                        }

                        var list = PromoterList.Take(1).Concat(PromoterList.Skip(1).OrderBy(c => c.Text)).ToList();
                        PromoterList = new List<Promoter>(list);
                        listView.ItemsSource = PromoterList;
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

        public async void GetLocationsByCountryId()
        {
            try
            {
                IsBusy = true;

                if (NetworkAvailable)
                {
                    MasterDataManagementSL masterDataManagementSL = new MasterDataManagementSL();

                    Location = await masterDataManagementSL.GetLocationsByCountryId((int)CommonAttribute.CustomeProfile?.CountryId);
                    if (Location.Count != 0)
                    {
                        foreach (var location in Location)
                        {
                            LocationsLists.Add(new Locations() { ID = location.Id, Text = location.Name });

                            if (_LocationId == location.Id)
                            {
                                LocationsLists.Where(l => l.IsChecked == true).Select(s => s.ID == _LocationId);

                            }
                        }

                        var list = LocationsLists.Take(1).Concat(LocationsLists.Skip(1).OrderBy(c => c.Text)).ToList();
                        LocationsLists = new List<Locations>(list);
                        listView.ItemsSource = LocationsLists;
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

        public bool NetworkAvailable { get { return Retail.Hepler.Extensions.CheckNetwrokAvailability(); } }

        //for multiselect cities
        public async Task GetCities(List<int> SelectedCountries)
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
                        CityList.Add(new LstCity() { ID = 0, Text = "Select all" });
                        foreach (var item in CitiesBasedOnCountry)
                        {
                            CityList.Add(new LstCity() { ID = item.Id, Text = item.Name });
                        }

                        var list = CityList.Take(1).Concat(CityList.Skip(1).OrderBy(c => c.Text)).ToList();
                        CityList = new List<LstCity>(list);
                        listView.ItemsSource = CityList;
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

        //for multiselect countries
        public async Task GetCountries()
        {
            try
            {
                IsBusy = true;

                if (NetworkAvailable)
                {
                    long? PersonID = CommonAttribute.CustomeProfile?.PersonId;
                    ////Whatever is assigned in web app only those will appear here
                    MasterDataManagementSL masterDataManagementSL = new MasterDataManagementSL();
                    List<EntityKeyValueResponse> ActiveCountryList = await masterDataManagementSL.GetMultipleCountryIdByPersonId((long)PersonID);

                    if (ActiveCountryList != null && ActiveCountryList.Count > 0)
                    {
                        CountryList.Add(new Countries() { ID = 0, Text = "Select all", IsAllChecked = false, IsChecked = false });
                        foreach (var item in ActiveCountryList)
                        {
                            CountryList.Add(new Countries() { ID = item.Id, Text = item.Name, IsAllChecked = false, IsChecked = false });
                        }

                        var list = CountryList.Take(1).Concat(CountryList.Skip(1).OrderBy(c => c.Text)).ToList();
                        CountryList = new List<Countries>(list);
                        listView.ItemsSource = CountryList;
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

        //for multiselect Accounts
        public async Task GetAccounts(List<int> SelectedCountryIds)
        {
            try
            {
                IsBusy = true;

                if (NetworkAvailable)
                {
                    long? PersonId = CommonAttribute.CustomeProfile?.PersonId;
                    MasterDataManagementSL masterDataManagementSL = new MasterDataManagementSL();
                    List<EntityKeyValueResponse> AccountsBasedOnCity = await masterDataManagementSL.GetAccountsByMultipleCountryId(PersonId,SelectedCountryIds);
                    if (AccountsBasedOnCity != null && AccountsBasedOnCity.Count > 0)
                    {
                        AccountList.Add(new Accounts() { ID = 0, Text = "Select all" });
                        foreach (var item in AccountsBasedOnCity)
                        {
                            AccountList.Add(new Accounts() { ID = item.Id, Text = item.Name });
                        }

                        var list = AccountList.Take(1).Concat(AccountList.Skip(1).OrderBy(c => c.Text)).ToList();
                        AccountList = new List<Accounts>(list);
                        listView.ItemsSource = AccountList;
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

        //for multiselect supervisors
        public async Task GetSupervisors(ReportCreateUpdateRequest reportCreateUpdateRequest)
        {
            try
            {
                IsBusy = true;

                if (NetworkAvailable)
                {

                    MasterDataManagementSL masterDataManagementSL = new MasterDataManagementSL();
                    List<EntityKeyValueResponse> ListSupervisor = await masterDataManagementSL.GetSupervisorsListByCountryAccountIdMultiple(reportCreateUpdateRequest);
                    if (ListSupervisor != null && ListSupervisor.Count != 0)
                    {
                        SupervisorList.Add(new Supervisors { ID = 0, Text = "Select all" });
                        foreach (var item in ListSupervisor)
                        {
                            SupervisorList.Add(new Supervisors { ID = item.Id, Text = item.Name });
                        }

                        var list = SupervisorList.Take(1).Concat(SupervisorList.Skip(1).OrderBy(c => c.Text)).ToList();
                        SupervisorList = new List<Supervisors>(list);
                        listView.ItemsSource = SupervisorList;
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

        //for multiselect Accounts
        public async Task GetLocationsByMultipleCountryAccount(ReportCreateUpdateRequest reportCreateUpdateRequest)
        {
            try
            {
                IsBusy = true;
                LocationsLists = new List<Locations>();
                if (NetworkAvailable)
                {
                    long? PersonId = CommonAttribute.CustomeProfile?.PersonId;
                    MasterDataManagementSL masterDataManagementSL = new MasterDataManagementSL();
                    List<EntityKeyValueResponse> LocationByCountryAccount = await masterDataManagementSL.GetLocationByAccountsMultipleCountryId(PersonId,reportCreateUpdateRequest);
                    if (LocationByCountryAccount != null && LocationByCountryAccount.Count > 0)
                    {
                        LocationsLists.Add(new Locations() { ID = 0, Text = "Select all" });
                        foreach (var item in LocationByCountryAccount)
                        {
                            LocationsLists.Add(new Locations() { ID = item.Id, Text = item.Name });
                        }

                        var list = LocationsLists.Take(1).Concat(LocationsLists.Skip(1).OrderBy(c => c.Text)).ToList();
                        LocationsLists = new List<Locations>(list);
                        listView.ItemsSource = LocationsLists;
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


        //for multiselect Category
        public async Task GetSubcategory()
        {
            try
            {
                IsBusy = true;
                SubcategoriesLists = new List<SubCategory>();
                if (NetworkAvailable)
                {
                    MasterDataManagementSL masterDataManagementSL = new MasterDataManagementSL();
                    List<ProductCategoryResponse> productCategoryResponse = await masterDataManagementSL.GetAllSubcategoriesforOffline();
                    if (productCategoryResponse != null && productCategoryResponse.Count > 0)
                    {
                        SubcategoriesLists.Add(new SubCategory() { ID = 0, Text = "Select all" });
                        foreach (var item in productCategoryResponse)
                        {
                            SubcategoriesLists.Add(new SubCategory() { ID = item.ProductCategoryId, Text = item.Name });
                        }

                        var list = SubcategoriesLists.Take(1).Concat(SubcategoriesLists.Skip(1).OrderBy(c => c.Text)).ToList();
                        SubcategoriesLists = new List<SubCategory>(list);
                        listView.ItemsSource = SubcategoriesLists;
                    }
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

        public class SubCategory : INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler PropertyChanged;

            // This method is called by the Set accessor of each property.  
            // The CallerMemberName attribute that is applied to the optional propertyName  
            // parameter causes the property name of the caller to be substituted as an argument.  
            private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }

            public SubCategory()
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

        public class Supervisors : INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler PropertyChanged;

            // This method is called by the Set accessor of each property.  
            // The CallerMemberName attribute that is applied to the optional propertyName  
            // parameter causes the property name of the caller to be substituted as an argument.  
            private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }

            public Supervisors()
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

        public class Accounts : INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler PropertyChanged;

            // This method is called by the Set accessor of each property.  
            // The CallerMemberName attribute that is applied to the optional propertyName  
            // parameter causes the property name of the caller to be substituted as an argument.  
            private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }

            public Accounts()
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

        public class Countries : INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler PropertyChanged;

            // This method is called by the Set accessor of each property.  
            // The CallerMemberName attribute that is applied to the optional propertyName  
            // parameter causes the property name of the caller to be substituted as an argument.  
            private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }

            public Countries()
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

            private bool isAllChecked;
            public bool IsAllChecked
            {
                get
                {
                    return isAllChecked;
                }
                set
                {
                    isAllChecked = value;
                    NotifyPropertyChanged();
                }
            }

        }

        public class LstCity : INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler PropertyChanged;

            // This method is called by the Set accessor of each property.  
            // The CallerMemberName attribute that is applied to the optional propertyName  
            // parameter causes the property name of the caller to be substituted as an argument.  
            private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }

            public LstCity()
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

        public class Locations : INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler PropertyChanged;

            // This method is called by the Set accessor of each property.  
            // The CallerMemberName attribute that is applied to the optional propertyName  
            // parameter causes the property name of the caller to be substituted as an argument.  
            private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }

            public Locations()
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

        public class Promoter : INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler PropertyChanged;

            // This method is called by the Set accessor of each property.  
            // The CallerMemberName attribute that is applied to the optional propertyName  
            // parameter causes the property name of the caller to be substituted as an argument.  
            private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }

            public Promoter()
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

        async void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            try
            {
                await Navigation.PopPopupAsync();

                if (_flag == 1) //list of promoters
                {
                    var result = PromoterList.Where(w => w.IsChecked == true).ToList();

                    selectedItems = new ObservableCollection<SelectedItems>();
                    foreach (var model in result)
                    {
                        if (model.ID != 0)
                        {
                            selectedItems.Add(new SelectedItems { ID = model.ID, Name = model.Text });
                        }
                    }

                    MessagingCenter.Send<ObservableCollection<SelectedItems>>(selectedItems, "SelectedPersonLists");
                }

                else if (_flag == 2) //list of locations/stores
                {
                    selectedItems = new ObservableCollection<SelectedItems>();
                    var result = LocationsLists.Where(w => w.IsChecked == true).ToList();
                    foreach (var model in result)
                    {
                        if (model.ID != 0)
                        {
                            selectedItems.Add(new SelectedItems { ID = model.ID, Name = model.Text });
                        }
                    }

                    MessagingCenter.Send<ObservableCollection<SelectedItems>>(selectedItems, "SelectedLocationLists");
                }
                else if (_flag == 3) //list of locations by person id
                {
                    selectedItems = new ObservableCollection<SelectedItems>();
                    var result = LocationsLists.Where(w => w.IsChecked == true).ToList();
                    foreach (var model in result)
                    {
                        if (model.ID != 0)
                        {
                            selectedItems.Add(new SelectedItems { ID = model.ID, Name = model.Text });
                        }
                    }

                     MessagingCenter.Send<ObservableCollection<SelectedItems>>(selectedItems, "SelectedLocationListsbyPerId");
                   // await viewModel.SelectedLocationListsbyPerId();
                }
                else if (_flag == 4) //list of stores 
                {
                    selectedItems = new ObservableCollection<SelectedItems>();
                    var result = LocationsLists.Where(w => w.IsChecked == true).ToList();
                    foreach (var model in result)
                    {
                        if (model.ID != 0)
                        {
                            selectedItems.Add(new SelectedItems { ID = model.ID, Name = model.Text });
                        }
                    }

                    MessagingCenter.Send<ObservableCollection<SelectedItems>>(selectedItems, "SelectedLocationLists");
                }

                else if (_flag == 5) //list of countries 
                {
                    selectedItems = new ObservableCollection<SelectedItems>();
                    var result = CountryList.Where(w => w.IsChecked == true).ToList();
                    foreach (var model in result)
                    {
                        //selectedItems.Add(new SelectedItems { ID = model.ID, Name = model.Text });
                        if (model.ID != 0)
                        {
                            selectedItems.Add(new SelectedItems { ID = model.ID, Name = model.Text });
                        }
                    }

                    MessagingCenter.Send<ObservableCollection<SelectedItems>>(selectedItems, "SelectedCountryLists");
                }
                else if (_flag == 6) //list of selected cities 
                {
                    selectedItems = new ObservableCollection<SelectedItems>();
                    var result = CityList.Where(w => w.IsChecked == true).ToList();
                    foreach (var model in result)
                    {
                        //selectedItems.Add(new SelectedItems { ID = model.ID, Name = model.Text });
                        if (model.ID != 0)
                        {
                            selectedItems.Add(new SelectedItems { ID = model.ID, Name = model.Text });
                        }
                    }

                    MessagingCenter.Send<ObservableCollection<SelectedItems>>(selectedItems, "SelectedCityLists");
                }
                else if (_flag == 7) //list of selected accounts 
                {
                    selectedItems = new ObservableCollection<SelectedItems>();
                    var result = AccountList.Where(w => w.IsChecked == true).ToList();
                    foreach (var model in result)
                    {
                        //selectedItems.Add(new SelectedItems { ID = model.ID, Name = model.Text });
                        if (model.ID != 0)
                        {
                            selectedItems.Add(new SelectedItems { ID = model.ID, Name = model.Text });
                        }
                    }

                    MessagingCenter.Send<ObservableCollection<SelectedItems>>(selectedItems, "SelectedAccountLists");
                }
                else if (_flag == 8) //list of selected Locations based on country account 
                {
                    selectedItems = new ObservableCollection<SelectedItems>();
                    var result = LocationsLists.Where(w => w.IsChecked == true).ToList();
                    foreach (var model in result)
                    {
                        //selectedItems.Add(new SelectedItems { ID = model.ID, Name = model.Text });
                        if (model.ID != 0)
                        {
                            selectedItems.Add(new SelectedItems { ID = model.ID, Name = model.Text });
                        }
                    }

                    MessagingCenter.Send<ObservableCollection<SelectedItems>>(selectedItems, "SelectedLocationList");
                }
                else if (_flag == 9) //list of selected supervisors 
                {
                    selectedItems = new ObservableCollection<SelectedItems>();
                    var result = SupervisorList.Where(w => w.IsChecked == true).ToList();
                    foreach (var model in result)
                    {
                        //selectedItems.Add(new SelectedItems { ID = model.ID, Name = model.Text });
                        if (model.ID != 0)
                        {
                            selectedItems.Add(new SelectedItems { ID = model.ID, Name = model.Text });
                        }
                    }

                    MessagingCenter.Send<ObservableCollection<SelectedItems>>(selectedItems, "SelectedSupervisorList");
                }
                else if (_flag == 10) //list of countries 
                {
                    selectedItems = new ObservableCollection<SelectedItems>();
                    var result = CountryList.Where(w => w.IsChecked == true).ToList();
                    foreach (var model in result)
                    {
                        if (model.ID != 0)
                        {
                            selectedItems.Add(new SelectedItems { ID = model.ID, Name = model.Text });
                        }
                    }
                    MessagingCenter.Send<ObservableCollection<SelectedItems>>(selectedItems, "SelectedCountries");
                }
                else if (_flag == 11) //list of countries 
                {
                    selectedItems = new ObservableCollection<SelectedItems>();
                    var result = CountryList.Where(w => w.IsChecked == true).ToList();
                    foreach (var model in result)
                    {
                        //selectedItems.Add(new SelectedItems { ID = model.ID, Name = model.Text });
                        if (model.ID != 0)
                        {
                            selectedItems.Add(new SelectedItems { ID = model.ID, Name = model.Text });
                        }
                    }

                    MessagingCenter.Send<ObservableCollection<SelectedItems>>(selectedItems, "SelectedCountryListsAttendance");
                }

                else if (_flag == 12) //list of countries 
                {
                    selectedItems = new ObservableCollection<SelectedItems>();
                    var result = SubcategoriesLists.Where(w => w.IsChecked == true).ToList();
                    foreach (var model in result)
                    {
                        if (model.ID != 0)
                        {
                            selectedItems.Add(new SelectedItems { ID = model.ID, Name = model.Text });
                        }
                    }

                    MessagingCenter.Send<ObservableCollection<SelectedItems>>(selectedItems, "SelectedSubcategoryLists");
                }
                else if (_flag == 13) //list of locations by person id
                {
                    selectedItems = new ObservableCollection<SelectedItems>();
                    var result = LocationsLists.Where(w => w.IsChecked == true).ToList();
                    foreach (var model in result)
                    {
                        if (model.ID != 0)
                        {
                            selectedItems.Add(new SelectedItems { ID = model.ID, Name = model.Text });
                        }
                    }
                    ProcessCompleted?.Invoke(this, selectedItems);
                    //  MessagingCenter.Send<ObservableCollection<SelectedItems>>(selectedItems, "SelectedLocationListsbyPerId");

                }
            }
            catch (Exception ex)
            {
                Debugger.Log(0, null, ex.ToString());
            }
        }

        async void TapGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
        }

        void CheckBox_CheckedChanged_1(System.Object sender, Xamarin.Forms.CheckedChangedEventArgs e)
        {
            if (_flag == 1)
            {
                var IscheckedSelectAll = PromoterList.Where(c => c.ID == 0).ToList();
                if (IscheckedSelectAll != null && IscheckedSelectAll.Count > 0)
                {
                    PromoterList.ForEach(c => c.IsChecked = !(c.IsChecked));
                    listView.ItemsSource = PromoterList;
                }               
            }
            if (_flag == 2)
            {
                var IscheckedSelectAll = LocationsLists.Where(c => c.ID == 0).ToList();
                if (IscheckedSelectAll != null && IscheckedSelectAll.Count > 0)
                {
                    LocationsLists.ForEach(c => c.IsChecked = !(c.IsChecked));
                    listView.ItemsSource = LocationsLists;
                }
            }
            if (_flag == 3)
            {
                var IscheckedSelectAll = LocationsLists.Where(c => c.ID == 0).ToList(); 
                if (IscheckedSelectAll != null && IscheckedSelectAll.Count > 0)
                {
                    LocationsLists.ForEach(c => c.IsChecked = !(c.IsChecked));
                    listView.ItemsSource = LocationsLists;
                }
            }
            if (_flag == 4)
            {
                var IscheckedSelectAll = LocationsLists.Where(c => c.ID == 0).ToList();
                if (IscheckedSelectAll != null && IscheckedSelectAll.Count > 0)
                {
                    LocationsLists.ForEach(c => c.IsChecked = !(c.IsChecked));
                    listView.ItemsSource = LocationsLists;
                }
            }
            if (_flag == 5)
            {
                var IscheckedSelectAll = CountryList.Where(c => c.ID == 0).ToList(); 
                if (IscheckedSelectAll != null && IscheckedSelectAll.Count > 0)
                {
                    CountryList.ForEach(c => c.IsChecked = !(c.IsChecked));
                    listView.ItemsSource = CountryList;
                }
            }
            if (_flag == 6)
            {
                var IscheckedSelectAll = CityList.Where(c => c.ID == 0).ToList(); 
                if (IscheckedSelectAll != null && IscheckedSelectAll.Count > 0)
                {
                    CityList.ForEach(c => c.IsChecked = !(c.IsChecked));
                    listView.ItemsSource = CityList;
                }
            }
            if (_flag == 7)
            {
                var IscheckedSelectAll = AccountList.Where(c => c.ID == 0).ToList(); 
                if (IscheckedSelectAll != null && IscheckedSelectAll.Count > 0)
                {
                    AccountList.ForEach(c => c.IsChecked = !(c.IsChecked));
                    listView.ItemsSource = AccountList;
                }
            }
            if (_flag == 8)
            {
                var IscheckedSelectAll = LocationsLists.Where(c => c.ID == 0).ToList(); 
                if (IscheckedSelectAll != null && IscheckedSelectAll.Count > 0)
                {
                    LocationsLists.ForEach(c => c.IsChecked = !(c.IsChecked));
                    listView.ItemsSource = LocationsLists;
                }
            }
            if (_flag == 9)
            {
                var IscheckedSelectAll = SupervisorList.Where(c => c.ID == 0).ToList(); 
                if (IscheckedSelectAll != null && IscheckedSelectAll.Count > 0)
                {
                    SupervisorList.ForEach(c => c.IsChecked = !(c.IsChecked));
                    listView.ItemsSource = SupervisorList;
                }
            }
            if (_flag == 10)
            {
                var IscheckedSelectAll = CountryList.Where(c => c.ID == 0).ToList();
                if (IscheckedSelectAll != null && IscheckedSelectAll.Count > 0)
                {
                    CountryList.ForEach(c => c.IsChecked = !(c.IsChecked));
                    listView.ItemsSource = CountryList;
                }
            }
            if (_flag == 11)
            {
                var IscheckedSelectAll = CountryList.Where(c => c.ID == 0).ToList();
                if (IscheckedSelectAll != null && IscheckedSelectAll.Count > 0)
                {
                    CountryList.ForEach(c => c.IsChecked = !(c.IsChecked));
                    listView.ItemsSource = CountryList;
                }
            }

            if (_flag == 12)
            {
                var IscheckedSelectAll = SubcategoriesLists.Where(c => c.ID == 0).ToList();
                if (IscheckedSelectAll != null && IscheckedSelectAll.Count > 0)
                {
                    SubcategoriesLists.ForEach(c => c.IsChecked = !(c.IsChecked));
                    listView.ItemsSource = SubcategoriesLists;
                }
            }
            if (_flag == 13)
            {
                var IscheckedSelectAll = LocationsLists.Where(c => c.ID == 0).ToList();
                if (IscheckedSelectAll != null && IscheckedSelectAll.Count > 0)
                {
                    LocationsLists.ForEach(c => c.IsChecked = !(c.IsChecked));
                    listView.ItemsSource = LocationsLists;
                }
            }
        }

        void SearchStore_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.NewTextValue))
            {
                LocationsListsAll = new ObservableCollection<Locations>(LocationsLists.Where(x => x.Text.ToLower().Contains(e.NewTextValue.ToString().ToLower())).ToList());
                LocationsLists = new List<Locations>(LocationsListsAll);
                listView.ItemsSource = LocationsLists;
            }
            else
                GetLocationbyPersonId(13);
        }
    }
}
