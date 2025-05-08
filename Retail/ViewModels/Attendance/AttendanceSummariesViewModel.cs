using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Retail.DependencyServices;
using Retail.Hepler;
using Retail.Infrastructure.Common;
using Retail.Infrastructure.RequestModels;
using Retail.Infrastructure.ResponseModels;
using Retail.Infrastructure.ServiceLayer;
using Retail.Models;
using Xamarin.Forms;
namespace Retail.ViewModels.Attendance
{
    public class AttendanceSummariesViewModel : BaseViewModel
    {
        public EventCollection Events { get; set; }
        public ICommand DayTappedCommand => new Command<DateTime>(async (date) => DayTapped(date));
        public ICommand SuperSwipeLeftCommand => new Command<DateTime>((date) => PreviousMonthAttendance(date));
        public ICommand SuperSwipeRightCommand => new Command<DateTime>((date) => NextMonthAttendance(date));
       
        public AttendanceSummariesViewModel(INavigation navigation) : base(navigation)
        {
            Events = new EventCollection
            {

            };
          
            CurrentDate = DateTime.Now.Date;
            SelectedDate = CurrentDate.ToString();
            IsBusy = true; 
            Device.BeginInvokeOnMainThread(async () =>
            {
                IsVisibleAttendanceDetails = true;
                int SelectedStoreID = 0;
                if (SelectedStore != null)
                {
                    SelectedStoreID = SelectedStore.Id;
                }
                //await GetDailyAttendanceHistory(SelectedStoreID);

                int SelectedPromoterID = 0;
                if (SelectedPromoter != null)
                {
                    SelectedPromoterID = SelectedPromoter.Id;
                }
                //await GetAttendanceByPromoterDate(SelectedPromoterID, (DateTime)CurrentDate);               
                IsBusy = false;

                //await GetMonthlyAttendanceDetail((DateTime)CurrentDate);
            });
            obAttendanceResponseHistory = new ObservableCollection<AttendanceResponse>();
        }

        private async void PreviousMonthAttendance(DateTime date)
        {
            SelectedDate = date.ToString("dd-MMM-yyyy");
            await GetMonthlyAttendanceDetail(date);
            if(obAttendanceResponseHistory!=null)
            {
                obAttendanceResponseHistory = new ObservableCollection<AttendanceResponse>();
            }
        }

        private async void NextMonthAttendance(DateTime date)
        {
            SelectedDate = date.ToString("dd-MMM-yyyy");
            await GetMonthlyAttendanceDetail(date);
            if (obAttendanceResponseHistory != null)
            {
                obAttendanceResponseHistory = new ObservableCollection<AttendanceResponse>();
            }
        }
               
        private async void DayTapped(DateTime date)
        {
            int SelectedPromoterID = 0;
            SelectedDate = date.ToString("dd-MMM-yyyy");
            if (SelectedPromoter != null)
            {
                SelectedPromoterID = SelectedPromoter.Id;
            }
            if(SelectedPromoterID==0)
            {
                await ErrorDisplayAlert("Please select promoter.");
                return;
            }
            try
            {
                await GetAttendanceByPromoterDate(SelectedPromoterID, date);
            }
            catch(Exception ex)
            {

            }
        }

        public async Task GetMonthlyAttendanceDetail(DateTime SelectedDate)
        {
            try
            {
                IsBusy = true;

                if (NetworkAvailable)
                {
                    if (SelectedPromoter.Id != 0)
                    {
                        List<PersonDailyAttendanceResponse> personDailyAttendanceResponses = new List<PersonDailyAttendanceResponse>();
                        AttendanceDataManagementSL attendanceDataManagementSL = new AttendanceDataManagementSL();
                        personDailyAttendanceResponses = await attendanceDataManagementSL.GetPersonMonthlyAttendance(SelectedPromoter.Id, SelectedDate.Month, SelectedDate.Year);
                        if (personDailyAttendanceResponses != null)
                        {
                            if (Events == null)
                            {
                                foreach (var item in personDailyAttendanceResponses)
                                {
                                    Events.Add(item.AttendanceDate, new DayEventCollection<PersonDailyAttendanceResponse>()
                                    { EventIndicatorColor = item.DayColor, EventIndicatorSelectedColor = item.DayColor });
                                }
                            }
                            else
                            {
                                Events.Clear();
                                foreach (var item in personDailyAttendanceResponses)
                                {
                                    Events.Add(item.AttendanceDate, new DayEventCollection<PersonDailyAttendanceResponse>()
                                    { EventIndicatorColor = item.DayColor, EventIndicatorSelectedColor = item.DayColor });
                                }
                            }

                        }
                    }
                    else
                    {
                        await ErrorDisplayAlert("Please select promoter.");
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

        public async Task GetAttendanceByPromoterDate(int PromoterID, DateTime Selecteddate)
        {
            try
            {
                IsBusy = true;

                if (NetworkAvailable)
                {
                    obAttendanceResponseHistory = new ObservableCollection<AttendanceResponse>();
                    lstPersonDailyAttendanceResponse = new List<PersonDailyAttendanceResponse>();
                    string date = CommonFunctions.GetYMDFormat(Selecteddate); int Month = Selecteddate.Month; int year = Selecteddate.Year;
                    AttendanceDataManagementSL attendanceDataManagementSL = new AttendanceDataManagementSL();
                    lstPersonDailyAttendanceResponse = await attendanceDataManagementSL.GetPersonMonthlyAttendance(PromoterID, Month, year);
                    if (lstPersonDailyAttendanceResponse.Count != 0 && lstPersonDailyAttendanceResponse != null)
                    {
                        List<PersonDailyAttendanceResponse> abc = lstPersonDailyAttendanceResponse.Where(s => s.AttendanceDate == Convert.ToDateTime(date)).ToList();
                        if (abc.Count != 0 && abc != null)
                        {
                            obAttendanceResponseHistory = new ObservableCollection<AttendanceResponse>(abc.FirstOrDefault().AttendanceMultiLocationResponses);
                            foreach (var item in obAttendanceResponseHistory)
                            {
                                if (item.IsOffDay == true)
                                {
                                    item.IsVisibleDayOff = true;
                                    item.IsVisibleWorkingHr = false;
                                }
                                else
                                {
                                    item.IsVisibleDayOff = false;
                                    item.IsVisibleWorkingHr = true;
                                }
                            }
                        }
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

        public async Task GetDailyAttendanceHistoryByLocationIds(List<int> LocationIds)
        {
            try
            {
                IsBusy = true;

                if (NetworkAvailable)
                {   
                    AttendanceDailySearchRequest attendanceDailySearchRequest = new AttendanceDailySearchRequest();
                    attendanceDailySearchRequest.locationIds = LocationIds;
                    attendanceDailySearchRequest.date = DateTime.Now.Date;

                    lstAttendanceResponseHistory = new List<AttendanceResponse>();
                    AttendanceDataManagementSL attendanceDataManagementSL = new AttendanceDataManagementSL();
                    lstAttendanceResponseHistory = await attendanceDataManagementSL.GetDailyAttendanceByLocationIdsDate(attendanceDailySearchRequest);
                    if (lstAttendanceResponseHistory != null && lstAttendanceResponseHistory.Count != 0)
                    {
                        foreach (var item in lstAttendanceResponseHistory)
                        {
                            if (item.IsOffDay == true)
                            {
                                item.IsVisibleDayOff = true;
                                item.IsVisibleWorkingHr = false;
                            }
                            else
                            {
                                item.IsVisibleDayOff = false;
                                item.IsVisibleWorkingHr = true;
                            }
                        }
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

        public async Task GetDailyAttendanceHistory(int LocationId)
        {
            try
            {
                IsBusy = true;

                if (NetworkAvailable)
                {
                    int SelectedStoreID = 0;
                    if (SelectedStore != null)
                    {
                        SelectedStoreID = SelectedStore.Id;
                    }
                    lstAttendanceResponseHistory = new List<AttendanceResponse>();
                    string date = DateTime.Now.ToString("yyyy-MM-dd");
                    AttendanceDataManagementSL attendanceDataManagementSL = new AttendanceDataManagementSL();
                    lstAttendanceResponseHistory = await attendanceDataManagementSL.GetDailyAttendanceByLocationDate(SelectedStoreID, date);
                    //obAttendanceResponseHistory = new ObservableCollection<AttendanceResponse>(lstAttendanceResponseHistory);
                    foreach (var item in lstAttendanceResponseHistory)
                    {
                        if (item.IsOffDay == true)
                        {
                            item.IsVisibleDayOff = true;
                            item.IsVisibleWorkingHr = false;
                        }
                        else
                        {
                            item.IsVisibleDayOff = false;
                            item.IsVisibleWorkingHr = true;
                        }
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

        public async Task GetLocation()
        {
            try
            {
                IsBusy = true;

                if (NetworkAvailable)
                {
                    StoreDropDown = new ObservableCollection<DropDownModel>();
                    MasterDataManagementSL masterDataManagementSL = new MasterDataManagementSL();
                    Locations = new List<EntityKeyValueResponse>();
                    Locations = await masterDataManagementSL.GetLocationsByCountryId((int)CommonAttribute.CustomeProfile.CountryId);
                    if (Locations.Count != 0)
                    {
                        foreach (var item in Locations)
                        {
                            StoreDropDown.Add(new DropDownModel { Id = item.Id, Title = item.Name });
                        }

                        SelectedStore = StoreDropDown[0];
                    }
                    else
                    {
                        if (Device.RuntimePlatform == Device.Android)
                        {
                            bool gpsStatus = DependencyService.Get<ILocSettings>().isGpsAvailable();
                            if (!gpsStatus)
                                await ErrorDisplayAlert("Your gps location is not accurate");
                        }
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

        public async void GetPromoterList(List<int?>SelectedCountryIds)
        {
            try
            {
                IsBusy = true;

                if (NetworkAvailable)
                {
                    if (SelectedCountryIds != null && SelectedCountryIds.Count > 0)
                    {
                        Locations = new List<EntityKeyValueResponse>();
                        MasterDataManagementSL masterDataManagement = new MasterDataManagementSL();
                        Locations = await masterDataManagement.GetLocationsByPersonIdCountryIds((int)CommonAttribute.CustomeProfile?.PersonId, SelectedCountryIds);

                        List<int> SelectedLocationIds = new List<int>();
                        //PromoterDropDown = new ObservableCollection<DropDownModel>();
                        //PromoterDropDown.Clear();
                        if (Locations != null && Locations.Count > 0)
                        {
                            foreach (var item in Locations)
                            {
                                SelectedLocationIds.Add(item.Id);
                            }
                            MasterDataManagementSL masterDataManagementSL = new MasterDataManagementSL();
                            lstPromoters = new List<EntityKeyValueResponse>();
                            lstPromoters = await masterDataManagementSL.GetPromotersByMultiLocation(SelectedLocationIds);
                            if (lstPromoters != null)
                            {
                                var PromoterList = new List<DropDownModel>();
                                PromoterDropDown = new ObservableCollection<DropDownModel>(PromoterList);
                                foreach (var item in lstPromoters)
                                {
                                    PromoterList.Add(new DropDownModel { Id = item.Id, Title = item.Name });
                                }
                                if (PromoterList != null && PromoterList.Count > 0)
                                {
                                    PromoterDropDown = new ObservableCollection<DropDownModel>(PromoterList);
                                    //SelectedPromoter = new DropDownModel();
                                    //SelectedPromoter.Id = 0;

                                    var list = PromoterList.OrderBy(n => n.Title).ToList();
                                    PromoterDropDown = new ObservableCollection<DropDownModel>(list);
                                }                                
                            }
                        }
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

        public Command SelectPromoterCommand
        {
            get
            {
                return new Command<DropDownModel>(async (obj) =>
                {
                    if (obj != null)
                    {
                        //SelectedPromoter = obj;
                        if(NetworkAvailable)
                        {                            
                            await GetMonthlyAttendanceDetail(Convert.ToDateTime(SelectedDate));
                            await GetAttendanceByPromoterDate(SelectedPromoter.Id, Convert.ToDateTime(SelectedDate));
                        }
                        else
                        {
                            showToasterNoNetwork();
                        }
                    }
                    //else
                    //    SelectedPromoter.Id = 0;
                });
            }
        }


        private List<EntityKeyValueResponse> _Promoters;
        public List<EntityKeyValueResponse> lstPromoters
        {
            get { return _Promoters; }
            set
            {
                _Promoters = value;
                OnPropertyChanged(nameof(lstPromoters));
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

        private ObservableCollection<DropDownModel> _StoreDropDown;
        public ObservableCollection<DropDownModel> StoreDropDown
        {
            get { return _StoreDropDown; }
            set
            {
                _StoreDropDown = value;
                OnPropertyChanged("StoreDropDown");
            }
        }

        private DropDownModel _SelectedStore;
        public DropDownModel SelectedStore
        {
            get { return _SelectedStore; }
            set
            {
                _SelectedStore = value;
                OnPropertyChanged("SelectedStore");
            }
        }

        private ObservableCollection<DropDownModel> _PromoterDropDown;
        public ObservableCollection<DropDownModel> PromoterDropDown
        {
            get { return _PromoterDropDown; }
            set
            {
                _PromoterDropDown = value;
                OnPropertyChanged("PromoterDropDown");
            }
        }

        private DropDownModel _SelectedPromoter;
        public DropDownModel SelectedPromoter
        {
            get { return _SelectedPromoter; }
            set
            {
                _SelectedPromoter = value;
                OnPropertyChanged(nameof(SelectedPromoter));
            }
        }

        private bool _IsVisibleOffDay;
        public bool IsVisibleOffDay
        {
            get { return _IsVisibleOffDay; }
            set
            {
                _IsVisibleOffDay = value;
                OnPropertyChanged("IsVisibleOffDay");
            }
        }

        private bool _IsVisibleAttendanceDetails;
        public bool IsVisibleAttendanceDetails
        {
            get { return _IsVisibleAttendanceDetails; }
            set
            {
                _IsVisibleAttendanceDetails = value;
                OnPropertyChanged("IsVisibleAttendanceDetails");
            }
        }

        private int _PickerId;
        public int PickerId
        {
            get { return _PickerId; }
            set
            {
                _PickerId = value;
                OnPropertyChanged("PickerId");
            }
        }

        private Color _EventIndicatorColor;
        public Color EventIndicatorColor
        {
            get { return _EventIndicatorColor; }
            set
            {
                _EventIndicatorColor = value;
                OnPropertyChanged("EventIndicatorColor");
            }
        }
        

        private Nullable<DateTime> _CurrentDate;
        public Nullable<DateTime> CurrentDate
        {
            get { return _CurrentDate; }
            set
            {
                _CurrentDate = value;
                OnPropertyChanged("CurrentDate");
            }
        }

        private string _SelectedDate;
        public string SelectedDate
        {
            get { return _SelectedDate; }
            set
            {
                _SelectedDate = value;
                OnPropertyChanged(nameof(SelectedDate));
            }
        }

        private Nullable<DateTime> _MonthYear;
        public Nullable<DateTime> MonthYear
        {
            get { return _MonthYear; }
            set
            {
                _MonthYear = value;
                OnPropertyChanged("MonthYear");
            }
        }


        private List<AttendanceResponse> _lstAttendanceResponseHistory;
        public List<AttendanceResponse> lstAttendanceResponseHistory
        {
            get { return _lstAttendanceResponseHistory; }
            set
            {
                _lstAttendanceResponseHistory = value;
                OnPropertyChanged("lstAttendanceResponseHistory");
            }
        }

        private ObservableCollection<AttendanceResponse> _obAttendanceResponseHistory;
        public ObservableCollection<AttendanceResponse> obAttendanceResponseHistory
        {
            get { return _obAttendanceResponseHistory; }
            set
            {
                _obAttendanceResponseHistory = value;
                OnPropertyChanged(nameof(obAttendanceResponseHistory));
            }
        }

        private List<PersonDailyAttendanceResponse> _PersonDailyAttendanceResponse;
        public List<PersonDailyAttendanceResponse> lstPersonDailyAttendanceResponse
        {
            get { return _PersonDailyAttendanceResponse; }
            set
            {
                _PersonDailyAttendanceResponse = value;
                OnPropertyChanged("lstPersonDailyAttendanceResponse");
            }
        }

    }
}

