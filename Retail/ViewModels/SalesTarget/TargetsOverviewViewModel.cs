using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Retail.Database;
using Retail.Infrastructure.RequestModels;
using Retail.Infrastructure.ResponseModels;
using Retail.Infrastructure.ServiceLayer;
using Retail.Models;
using Retail.ViewModels.MonthYearPickerViewModel;
using Retail.Views.SalesTargetViews;
using Retail.Views.SupervisorFlow;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Forms;
using static Retail.Models.ReportsResponses;

namespace Retail.ViewModels.SalesTarget
{
    public class TargetsOverviewViewModel : BaseViewModel
    {
        DateTime CurrentDate;int _Month, _Year,_Flag;
        public List<int> _CityIds { get; set; }
        public List<string> _CityNames { get; set; }
        public List<int> _LocationIds { get; set; }
        public List<string> _LocationNames { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public bool IsExpanded { get; set; }
        Connection c = new Connection();

        public TargetsOverviewViewModel(INavigation navigation, int flag):base(navigation)
        {
            _Flag = flag;
            CurrentDate = DateTime.Now;
            _Month = CurrentDate.Month;
            _Year = CurrentDate.Year;
            IsExpanded = true;
        }

        public async Task GetTargetSummary(ObservableCollection<Cities> cities, ObservableCollection<SelectedItems> locations)
        {
            IsBusy = true;
           // await Task.Delay(1000);
            _CityIds = new List<int>(); _CityNames = new List<string>(); _LocationIds = new List<int>(); _LocationNames = new List<string>();
            foreach(var item in cities)
            {
                _CityIds.Add(item.ID); _CityNames.Add(item.Name);
            }
            foreach (var item in locations)
            {
                _LocationIds.Add(item.ID); _LocationNames.Add(item.Name);
            }
            
            TargetsOverviewByCityStoreRequests = new TargetsOverviewByCityStoreRequest()
            {
                
                CityIds = _CityIds,
                CityNames = _CityNames,
                LocationIds = _LocationIds,
                LocationNames = _LocationNames,
                Month = DateTime.Now.Month,
                Year = DateTime.Now.Year
            };
            if (TargetsOverviewByCityStoreRequests != null)
            {
                
                try
                {
                    IsBusy = true;

                    if (NetworkAvailable)
                    {
                        SalesTargetManagementSL salesTargetManagementSL = new SalesTargetManagementSL();
                        SupervisorTargetsOverviewResponse supervisorTargets = await salesTargetManagementSL.GetSupervisorTargetsOverview(TargetsOverviewByCityStoreRequests);

                        if (supervisorTargets.PersonLocationTargets != null && supervisorTargets.SalesTargetSummary!=null)
                        {
                            var list = supervisorTargets.PersonLocationTargets.ToList().OrderBy(p => p.PersonName);
                            PersonLocationTargets = new List<PersonLocationTarget>(list);
                            //PersonLocationTargets = supervisorTargets.PersonLocationTargets.ToList();
                            Targets = supervisorTargets.SalesTargetSummary.TotalTargets;
                            AchievedTargets = supervisorTargets.SalesTargetSummary.AchievedTargets;
                            BalanceTargets = supervisorTargets.SalesTargetSummary.BalanceTargets;
                            AchievedTargetPercentage = supervisorTargets.SalesTargetSummary.AchievedTargetPercentage;
                        }
                        else
                        {
                            PersonLocationTargets = new List<PersonLocationTarget>();
                            Targets = 0;
                            AchievedTargets = 0;
                            BalanceTargets = 0;
                            AchievedTargetPercentage = 0;
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
        }

        public Command PromoterTargetsCommand
        {
            get
            {
                return new Command<PersonLocationTarget>((item) =>
                {
                    Navigation.PushAsync(new PromoterTargetsView(item));
                });
            }
        }

        //public Command SelectLocationCommand
        //{
        //    get
        //    {
        //        return new Command(async () =>
        //        {
        //            await Navigation.PushPopupAsync(new MultiselectStorePopup(2, null));
        //        });
        //    }
        //}

        //public Command SelectCitiesCommand
        //{
        //    get
        //    {
        //        return new Command(async () =>
        //        {
        //            await Navigation.PushPopupAsync(new MultiselectStorePopup(2, null));
        //        });
        //    }
        //}

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

        private List<PersonLocationTarget>  _personLocationTargets;
        public List<PersonLocationTarget>  PersonLocationTargets
        {
            get { return _personLocationTargets; }
            set
            {
                _personLocationTargets = value;
                OnPropertyChanged("PersonLocationTargets");
            }
        }

        private string _txtLocationNames;
        public string txtLocationNames

        {
            get { return _txtLocationNames; }
            set
            {
                _txtLocationNames = value;
                OnPropertyChanged("txtLocationNames");
            }
        }

        private string _txtCityNames;
        public string txtCityNames

        {
            get { return _txtCityNames; }
            set
            {
                _txtCityNames = value;
                OnPropertyChanged("txtCityNames");
            }
        }

        private int _TotalTargets;
        public int Targets

        {
            get { return _TotalTargets; }
            set
            {
                _TotalTargets = value;
                OnPropertyChanged("Targets");
            }
        }

        private int _AchievedTargets;
        public int AchievedTargets

        {
            get { return _AchievedTargets; }
            set
            {
                _AchievedTargets = value;
                OnPropertyChanged("AchievedTargets");
            }
        }

        private int _BalanceTargets;
        public int BalanceTargets

        {
            get { return _BalanceTargets; }
            set
            {
                _BalanceTargets = value;
                OnPropertyChanged("BalanceTargets");
            }
        }
        

        private double _AchievedTargetPercentage;
        public double AchievedTargetPercentage

        {
            get { return _AchievedTargetPercentage; }
            set
            {
                _AchievedTargetPercentage = value;
                OnPropertyChanged("AchievedTargetPercentage");
            }
        }


        private TargetsOverviewByCityStoreRequest _targetsOverviewByCityStoreRequests;
        public TargetsOverviewByCityStoreRequest TargetsOverviewByCityStoreRequests

        {
            get { return _targetsOverviewByCityStoreRequests; }
            set
            {
                _targetsOverviewByCityStoreRequests = value;
                OnPropertyChanged("TargetsOverviewByCityStoreRequests");
            }
        }

        private List<CityModel> _cityModels;
        public List<CityModel> cityModels

        {
            get { return _cityModels; }
            set
            {
                _cityModels = value;
                OnPropertyChanged("cityModels");
            }
        }

        private List<LocationModel> _Stores;
        public List<LocationModel> Stores

        {
            get { return _Stores; }
            set
            {
                _Stores = value;
                OnPropertyChanged("Stores");
            }
        }

        private SupervisorTargetsOverviewResponse _supervisorTargetsOverviewResponses;
        public SupervisorTargetsOverviewResponse  SupervisorTargetsOverviewResponses
        {
            get { return _supervisorTargetsOverviewResponses; }
            set
            {
                _supervisorTargetsOverviewResponses = value;
                OnPropertyChanged("SupervisorTargetsOverviewResponses");
            }
        }

       

    }
}


