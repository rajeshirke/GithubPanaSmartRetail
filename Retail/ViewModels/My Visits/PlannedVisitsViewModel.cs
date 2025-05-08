using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Retail.DependencyServices;
using Retail.Enums;
using Retail.Hepler;
using Retail.Infrastructure.ResponseModels;
using Retail.Infrastructure.ServiceLayer;
using Retail.Models;
using Retail.Views.MyVisits;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Retail.ViewModels.MyVisits
{
    public class PlannedVisitsViewModel : BaseViewModel
    {
        bool SupWorkflowOffline = false;
        public Command SelectDateCommand { get; }
        public Command SwipeLeftCommand { get; }
        public Command SwipeRightCommand { get; }

        public PlannedVisitsViewModel(INavigation navigation) : base(navigation)
        {
            IsLoading = true;
            //SelectedDate.dateTime = DateTime.Now;
            loadDates(DateTime.Today);

            SelectDateCommand = new Command<DateItem>(async (model) => await ExecuteSelectDateCommand(model));
            SwipeLeftCommand = new Command<string>((model) =>  ExecuteSwipeLeftRightCommand(model));
            SwipeRightCommand = new Command<string>((model) =>  ExecuteSwipeLeftRightCommand(model));

            
            Device.BeginInvokeOnMainThread(async () => {
                await GetScheduledVisits();
                IsLoading = false;
            });
        }

        private void ExecuteSwipeLeftRightCommand(string model)
        {
            var today = CurrentDate;
            var month = new DateTime(today.Year, today.Month, 1);

            if (model == "+1")
            {
                var first = month.AddMonths(1);
                loadDates(first);
            }
            else if (model == "-1")
            {
                var first = month.AddMonths(-1);
                loadDates(first);
            }
        }

        private async Task ExecuteSelectDateCommand(DateItem model)
        {
            var Dates1 = new ObservableCollection<DateItem>();
            Dates1 = Dates;

            Dates1.ToList().ForEach((item) =>
            {
                item.selected = false;
                item.backgroundColor = "#FFFFFF";
                item.textColor = "#000000";
            });

            var index = Dates1.ToList().FindIndex(p => p.day == model.day && p.dayWeek == model.dayWeek);
            if (index > -1)
            {
                Dates1[index].backgroundColor = "#1E55A5";
                Dates1[index].textColor = "#FFFFFF";
                Dates1[index].selected = true;
            }

            Dates = new ObservableCollection<DateItem>();
            Dates = Dates1;
            await GetScheduledVisits();
        }

        private void loadDates(DateTime today)
        {
            try
            {
                CurrentDate = today;
                var Dates1 = new ObservableCollection<DateItem>();
                int thisYear = today.Year;
                int thisMonth = today.Month;
                var dateInit = new DateTime(thisYear, thisMonth, 1);
                var dateEnd = new DateTime(dateInit.Year, dateInit.Month, DateTime.DaysInMonth(dateInit.Year, dateInit.Month));
                Heading = string.Format("{0} {1}",dateInit.ToString("MMM").FirstLetterUpperCase(), dateInit.ToString("yy"));

                for (int i = 1; i <= dateEnd.Day; i++)
                {
                    string myDay = string.Format("{0:00}", i);
                    Dates1.Add(new DateItem()
                    {
                        day = myDay,
                        month = dateInit.ToString("MMM").FirstLetterUpperCase(),
                        dayWeek = new DateTime(dateInit.Year, dateInit.Month, i).DayOfWeek.ToString().Substring(0, 3),
                        selected = i == DateTime.Today.Day,
                        backgroundColor = i == DateTime.Today.Day ? "#1E55A5" : "#FFFFFF",
                        textColor = i == DateTime.Today.Day ? "#FFFFFF" : "#000000",
                        dateTime = new DateTime(thisYear, thisMonth, Convert.ToInt32(myDay))
                    });

                    if(Dates1[i-1].dateTime == DateTime.Today)
                    {
                        SelectedDate = Dates1[i-1];
                    }
                }
                Dates = Dates1;

            }
            catch (Exception ex)
            {
                Debugger.Log(0, null, ex.ToString());
            }
        }

        public async Task GetScheduledVisits()
        {
            try
            {
                IsLoading = true;

                if (NetworkAvailable)
                {
                    Application.Current.Properties["SupWorkflowOffline"] = false;

                    VisitScheduleLocationResponses = new ObservableCollection<VisitScheduleLocationResponse>();
                    string VisitDate = SelectedDate.dateTime.ToString("yyyy-MM-dd");
                    int PersonID = (int)CommonAttribute.CustomeProfile.PersonId;
                    VisitsDataManagementSL visitsDataManagementSL = new VisitsDataManagementSL();

                    //VisitScheduleResponse visitSchedule = await visitsDataManagementSL.GetVisitScheduleByPersonIdDate(PersonID, VisitDate, (int)TaskTypeEnum.Survey);

                    ////new api
                    //VisitScheduleResponse visitSchedule = await visitsDataManagementSL.GetVisitScheduleByPersonIdDates(PersonID, VisitDate, (int)TaskTypeEnum.Survey);

                    VisitScheduleResponses = new VisitScheduleResponse();

                    //New API for multiple visits at same time
                    List<VisitScheduleResponse> visitScheduleResponses= await visitsDataManagementSL.GetAllVisitScheduleByPersonIdDate(PersonID, VisitDate, (int)TaskTypeEnum.Survey);
                    if (visitScheduleResponses != null && visitScheduleResponses.Count > 0)
                    {
                        var VisitSchedules = new List<VisitScheduleLocationResponse>();

                        foreach (var visit in visitScheduleResponses)
                        {
                            //VisitScheduleLocationResponses = new ObservableCollection<VisitScheduleLocationResponse>(visit.VisitScheduleLocations.ToList());
                            VisitSchedules.AddRange(visit.VisitScheduleLocations.ToList());
                            VisitScheduleLocationResponses = new ObservableCollection<VisitScheduleLocationResponse>(VisitSchedules.OrderBy(n => n.Location.LocationStoreName));
                            lstVisitScheduleLocationResponses = new List<VisitScheduleLocationResponse>(VisitScheduleLocationResponses);
                            if (VisitScheduleLocationResponses != null)
                            {
                                if (CommonAttribute.UserLocation != null)
                                {
                                    Location sourceCoordinates = new Location(CommonAttribute.UserLocation.Latitude, CommonAttribute.UserLocation.Longitude);
                                    foreach (var item in VisitScheduleLocationResponses)
                                    {
                                        Location destinationCoordinates = new Location(Convert.ToDouble(item.Location.Latitude), Convert.ToDouble(item.Location.Longitude));
                                        double distance = Location.CalculateDistance(sourceCoordinates, destinationCoordinates, DistanceUnits.Kilometers);
                                        item.Location.Distance = Math.Round(distance, 2);

                                    }
                                }
                            }
                        }
                    }


                    
                    //if (visitSchedule != null)
                    //{
                    //    VisitScheduleLocationResponses = new ObservableCollection<VisitScheduleLocationResponse>(visitSchedule.VisitScheduleLocations.ToList());
                    //    if (VisitScheduleLocationResponses != null)
                    //    {
                    //        if (CommonAttribute.UserLocation != null)
                    //        {
                    //            Location sourceCoordinates = new Location(CommonAttribute.UserLocation.Latitude, CommonAttribute.UserLocation.Longitude);
                    //            foreach (var item in VisitScheduleLocationResponses)
                    //            {
                    //                Location destinationCoordinates = new Location(Convert.ToDouble(item.Location.Latitude), Convert.ToDouble(item.Location.Longitude));
                    //                double distance = Location.CalculateDistance(sourceCoordinates, destinationCoordinates, DistanceUnits.Kilometers);
                    //                item.Location.Distance = Math.Round(distance, 2);

                    //            }
                    //        }
                    //    }
                    //}
                    IsBusy = false;
                }
                else
                {
                    Application.Current.Properties["SupWorkflowOffline"] = true;
                    //showToasterNoNetwork();
                }

                #region SupWorkflowOffline check
                if (Application.Current.Properties.ContainsKey("SupWorkflowOffline"))
                {
                    SupWorkflowOffline = Convert.ToBoolean(Application.Current.Properties["SupWorkflowOffline"].ToString());
                    //BtnSubmitSupWorkflowOffline = SupWorkflowOffline;
                }
                #endregion

            }
            catch (Exception ex)
            {
                Debugger.Log(0, null, ex.ToString());
            }
            finally
            {
                IsLoading = false;
            }
        }

        public Command SelectVisitLocationCommand
        {
            get
            {
                return new Command<VisitScheduleLocationResponse>((obj) =>
                {
                    CommonAttribute.VisitLocation = obj.Location;                
                    Navigation.PushAsync(new VisitLocation());
                });
            }
        }

        public Command CountCommand
        {
            get
            {
                return new Command<VisitScheduleLocationResponse>((item) =>
                {
                    Application.Current.Properties["SupWorkflowOffline"] = false;
                    Navigation.PushAsync(new VisitTasksView(item.Location.LocationStoreName,item.Location.Area,item.Location.Distance,item.VisitScheduleLocationId, CheckList: false));
                });
            }
        }


        public Command SearchLocationCommand
        {
            get
            {
                return new Command<string>((item) =>
                {
                    if (VisitScheduleLocationResponses != null && VisitScheduleLocationResponses.Count > 0)
                    {
                        var Result = lstVisitScheduleLocationResponses.Where(x=>x.Location.LocationStoreName.ToLower().Contains(item.ToString().ToLower())).ToList();
                        VisitScheduleLocationResponses = new ObservableCollection<VisitScheduleLocationResponse>(Result);

                    }

                    IsBusy = false;
                });
            }
        }

        private DateTime _CurrentDate;
        public DateTime CurrentDate
        {
            set
            {
                if (value != null)
                {
                    _CurrentDate = value;
                    NotifyPropertyChanged(nameof(CurrentDate));
                }
            }
            get
            {
                return _CurrentDate;
            }
        }

        private Color textColor;
        public Color StatusColor
        {
            set
            {
                if (value != null)
                {
                    textColor = value;
                    NotifyPropertyChanged();
                }
            }
            get
            {
                return textColor;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private VisitScheduleResponse _VisitScheduleResponse;
        public VisitScheduleResponse VisitScheduleResponses
        {
            get { return _VisitScheduleResponse; }
            set
            {
                _VisitScheduleResponse = value;
                OnPropertyChanged("VisitScheduleResponses");
            }
        }        

        private ObservableCollection<VisitScheduleLocationResponse> _VisitScheduleLocationResponses;
        public ObservableCollection<VisitScheduleLocationResponse> VisitScheduleLocationResponses
        {
            get { return _VisitScheduleLocationResponses; }
            set
            {
                _VisitScheduleLocationResponses = value;
                OnPropertyChanged(nameof(VisitScheduleLocationResponses));
            }
        }

        private List<VisitScheduleLocationResponse> _lstVisitScheduleLocationResponses;
        public List<VisitScheduleLocationResponse> lstVisitScheduleLocationResponses
        {
            get { return _lstVisitScheduleLocationResponses; }
            set
            {
                _lstVisitScheduleLocationResponses = value;
                OnPropertyChanged(nameof(lstVisitScheduleLocationResponses));
            }
        }


        private string _StoreName;
        public string StoreName
        {
            get { return _StoreName; }
            set
            {
                _StoreName = value;
                OnPropertyChanged("StoreName");
            }
        }

        private string _StoreAddress;
        public string StoreAddress
        {
            get { return _StoreAddress; }
            set
            {
                _StoreAddress = value;
                OnPropertyChanged("StoreAddress");
            }
        }

        private double _Distance;
        public double Distance
        {
            get { return _Distance; }
            set
            {
                _Distance = value;
                OnPropertyChanged("Distance");
            }
        }

        private string _Count;
        public string Count
        {
            get { return _Count; }
            set
            {
                _Count = value;
                OnPropertyChanged("Count");
            }
        }

        private DateItem _selectedDate;
        public DateItem SelectedDate
        {
            get => _selectedDate;
            set => SetProperty(ref _selectedDate, value);
        }

        private ObservableCollection<DateItem> _Dates;
        public ObservableCollection<DateItem> Dates
        {
            get => _Dates;
            set => SetProperty(ref _Dates, value);
        }

        private string _Heading;
        public string Heading
        {
            get => _Heading;
            set => SetProperty(ref _Heading, value);
        }
    }
}