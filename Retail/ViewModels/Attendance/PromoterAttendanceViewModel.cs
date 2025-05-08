using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Retail.Database;
using Retail.Database.SupervisorWorkflow;
using Retail.DependencyServices;
using Retail.Hepler;
using Retail.Infrastructure.Enums;
using Retail.Infrastructure.RequestModels;
using Retail.Infrastructure.ResponseModels;
using Retail.Infrastructure.ServiceLayer;
using Retail.Models;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Retail.ViewModels.Attendance
{
    public class PromoterAttendanceViewModel : BaseViewModel
    {
        public EventCollection Events { get; set; }
        public ICommand DayTappedCommand => new Command<DateTime>((date) => DayTapped(date));
        public ICommand SwipeLeftCommand => new Command<DateTime>((date) => PreviousMonthAttendance(date));
        public ICommand SwipeRightCommand => new Command<DateTime>((date) => NextMonthAttendance(date));
        Connection c = new Connection();

        LocationStoreDb locationStoreDb;
        SupLocationStoreDb supLocationStoreDb;
        PromoterLocationStoreDb promoterLocationStoreDb;

        public PromoterAttendanceViewModel(INavigation navigation) : base(navigation)
        {
            c.conn = DependencyService.Get<ISQLite>().GetConnection();
            c.conn.CreateTable<TblPromoterAttendance>();

            locationStoreDb = new LocationStoreDb();
            supLocationStoreDb = new SupLocationStoreDb();
            promoterLocationStoreDb = new PromoterLocationStoreDb();
            //#FF0000 -red , 84b6fe- blu shade , 
            IsAttendanceVisible = true;
            CheckInBackgroungColor = Color.FromHex("#1E55A5");
            CheckOutBackgroungColor = Color.FromHex("#1E55A5");
            IsDayoffBackgroungColor = Color.FromHex("#1E55A5");
            Events = new EventCollection
            {

            };
            IsDayOffVisible = true; IsCheckinCheckoutVisible = true;
            IsEnableCheckIn = true; IsEnableCheckOut = true; IsDayOffEnable = true;

            SelectedDate = DateTime.Now;
            CheckInTime = CheckOutTime = null;
            IsBusy = true;
            Device.BeginInvokeOnMainThread(async () =>
            {
                await GetMonthlyAttendanceDetail(SelectedDate);
                //await GetTodaysAttendance();
                //await GetTodaysOfflineAttendnace();
                IsBusy = false;
            });
            TodaysDate = DateTime.Now;


        }

        public async Task GetTodaysOfflineAttendnace()
        {
            try
            {
                obTodaysAttendanceHistoryOffline = new ObservableCollection<AttendanceResponse>();
                obTodaysAttendanceHistory = new ObservableCollection<AttendanceResponse>();

                IsHistoryVisible = false;
                IsAttendanceVisible = true;
                IsDayOffVisible = true;
                IsDayOffEnable = true;
                IsCheckinCheckoutVisible = true;
                IsDayOff = false;
                CheckInTime = null;CheckOutTime = null;
                var workinghours = 0;

                //if (!NetworkAvailable)
                //{
                int SelectedStoreID = 0;
                if (SelectedStore != null)
                {
                    SelectedStoreID = SelectedStore.Id;
                }
                if (SelectedStoreID != 0)
                {
                    string TodaysDate = DateTime.Now.Date.ToString("yyyy-MM-dd");

                    //var test = c.conn.Table<TblPromoterAttendance>();
                    //var AttendanceEntry = test.ToList().Where(d => d.AttendanceDate == TodaysDate && d.LocationId == SelectedStoreID && d.PersonId == CommonAttribute.CustomeProfile.PersonId)?.SingleOrDefault();//("Select * From TblPromoterAttendance Where AttendanceDate='" + TodaysDate + "' AND LocationId='" + SelectedStoreID + "' AND PersonId = '" + CommonAttribute.CustomeProfile.PersonId + "'");

                    obTodaysAttendanceHistoryOffline = new ObservableCollection<AttendanceResponse>();
                    string CheckIn = "Select * From TblPromoterAttendance Where AttendanceDate='" + TodaysDate + "' AND LocationId='" + SelectedStoreID + "' AND PersonId = '" + CommonAttribute.CustomeProfile.PersonId + "' Order By ID DESC";
                    var AttendanceEntry = c.conn.Query<TblPromoterAttendance>(CheckIn);

                    if (AttendanceEntry != null && AttendanceEntry.Count > 0)
                    {
                        foreach (var entry in AttendanceEntry)
                        {
                            if (entry.CheckoutDate != null)
                            {
                                TimeSpan timeSpan = (DateTime)entry.CheckoutDate - (DateTime)entry.CheckInDate;
                                workinghours = Convert.ToInt32(timeSpan.Hours);
                            }
                            else
                            {
                                workinghours = 0;
                            }

                            obTodaysAttendanceHistoryOffline.Add(new AttendanceResponse
                            {
                                AttendanceId = entry.AttendanceId,
                                AttendanceDate = DateTime.Parse(entry.AttendanceDate),
                                CheckInDate = entry.CheckInDate,
                                CheckoutDate = entry.CheckoutDate,
                                PersonId = entry.PersonId,
                                Latitude = entry.PersonLocationLatitude,
                                Longitude = entry.PersonLocationLongitude,
                                LocationId = entry.LocationId,
                                IsOffDay = entry.IsOffDay,
                                LocationName = SelectedStore.Title,
                                TotalHoursOfAttendanceForTheDay = workinghours,
                            });
                        }

                        obTodaysAttendanceHistory = new ObservableCollection<AttendanceResponse>(obTodaysAttendanceHistoryOffline);

                        if (AttendanceEntry != null && AttendanceEntry.Count > 0)
                        {
                            if (AttendanceEntry[0].CheckInDate != null)
                            {
                                CheckInTime = AttendanceEntry[0].CheckInTime;
                                //CheckInBackgroungColor = Color.LightGray;
                                //IsEnableCheckIn = false;
                                //IsEnableCheckOut = true;
                                IsDayOff = false;
                                IsDayOffEnable = false;
                                IsDayOffVisible = false;
                            }
                            else
                            {
                                //CheckInTime = null;
                                //CheckInBackgroungColor = Color.FromHex("#1E55A5");
                                //IsEnableCheckIn = true;
                                IsDayOff = false;
                                IsDayOffEnable = true;
                                IsDayOffVisible = true;
                            }
                            if (AttendanceEntry[0].CheckoutDate != null)
                            {
                                CheckOutTime = AttendanceEntry[0].CheckOutTime;
                                //CheckOutBackgroungColor = Color.LightGray;
                                //IsEnableCheckOut = false;
                                IsDayOff = false;
                                IsDayOffEnable = false;
                                IsDayOffVisible = false;
                            }
                            else
                            {
                                //CheckOutTime = null;
                                //CheckOutBackgroungColor = Color.FromHex("#1E55A5");
                                //IsEnableCheckOut = true;
                                IsDayOff = false;
                                IsDayOffEnable = true;
                                if (AttendanceEntry[0].CheckInDate != null)
                                {
                                    IsDayOffVisible = false;
                                }
                            }

                            if(AttendanceEntry[0].CheckInDate != null && AttendanceEntry[0].CheckoutDate !=null)
                            {
                                CheckInTime = null;CheckOutTime = null;
                            }

                            if (AttendanceEntry[0].IsOffDay == true)
                            {
                                IsDayOff = true;
                                IsDayOffEnable = false;
                                IsDayOffVisible = false;
                                IsDayoffBackgroungColor = Color.LightGray;
                                CheckOutBackgroungColor = Color.LightGray;
                                IsEnableCheckOut = false;
                                CheckInBackgroungColor = Color.LightGray;
                                IsEnableCheckIn = false;
                            }
                            else
                            {
                                IsDayOff = false;
                                IsDayOffEnable = true;
                                IsDayoffBackgroungColor = Color.FromHex("#1E55A5");
                                if (CheckInTime != null && CheckInTime.ToString() != string.Empty)
                                {
                                    IsEnableCheckIn = false;
                                    CheckInBackgroungColor = Color.LightGray;
                                    IsDayOffVisible = false;
                                }
                                else
                                {
                                    CheckInBackgroungColor = Color.FromHex("#1E55A5");
                                    IsEnableCheckIn = true;
                                    IsDayOffVisible = true;
                                }
                                if (CheckOutTime != null && CheckOutTime.ToString() != string.Empty)
                                {
                                    CheckOutBackgroungColor = Color.LightGray;
                                    IsEnableCheckOut = false;
                                    IsDayOffVisible = false;
                                }
                                else
                                {
                                    CheckOutBackgroungColor = Color.FromHex("#1E55A5");
                                    IsEnableCheckOut = true;
                                    IsDayOffVisible = true;
                                    if (AttendanceEntry[0].CheckInDate != null)
                                    {
                                        IsDayOffVisible = false;
                                    }
                                }
                            }

                        }
                        else
                        {
                            IsDayOff = false;
                            IsDayOffEnable = true;
                            IsDayOffVisible = true;
                            IsCheckinCheckoutVisible = true;
                            CheckOutTime = CheckInTime = null;
                            IsDayoffBackgroungColor = Color.FromHex("#1E55A5");
                            CheckInBackgroungColor = Color.FromHex("#1E55A5");
                            IsEnableCheckIn = true;
                            CheckOutBackgroungColor = Color.FromHex("#1E55A5");
                            IsEnableCheckOut = true;
                        }
                    }
                    

                    //var TodaysAttendance = c.conn.Table<TblPromoterAttendance>().Where(d => d.AttendanceDate == TodaysDate && d.PersonId == CommonAttribute.CustomeProfile.PersonId).ToList();
                    string IsDayOffQuery = "Select * From TblPromoterAttendance Where AttendanceDate='" + TodaysDate + "' AND PersonId = '" + CommonAttribute.CustomeProfile.PersonId + "' Order By ID DESC";
                    var TodaysAttendance = c.conn.Query<TblPromoterAttendance>(IsDayOffQuery);
                    if (TodaysAttendance != null && TodaysAttendance.Count > 0)
                    {
                        foreach (var item in TodaysAttendance)
                        {
                            if (item.IsOffDay == true)
                            {
                                IsDayOffVisible = true;
                                IsCheckinCheckoutVisible = false;
                                IsDayOff = true;
                                IsDayOffEnable = false;
                                break;
                            }

                            if (item.CheckInDate != null || item.CheckoutDate != null)
                            {
                                IsDayOffVisible = false;
                                IsCheckinCheckoutVisible = true;
                                break;
                            }

                        }
                    }
                }
                //}    
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

        public async Task GetMonthlyAttendanceDetail(DateTime SelectedDate)
        {
            try
            {
                IsBusy = true;

                if (NetworkAvailable)
                {
                    List<PersonDailyAttendanceResponse> personDailyAttendanceResponses = new List<PersonDailyAttendanceResponse>();
                    AttendanceDataManagementSL attendanceDataManagementSL = new AttendanceDataManagementSL();
                    personDailyAttendanceResponses = await attendanceDataManagementSL.GetPersonMonthlyAttendance((int)CommonAttribute.CustomeProfile.PersonId, SelectedDate.Month, SelectedDate.Year);
                    if (personDailyAttendanceResponses != null && personDailyAttendanceResponses.Count != 0)
                    {
                        if (Events == null)
                        {
                            obPersonDailyAttendanceResponse = new ObservableCollection<PersonDailyAttendanceResponse>(personDailyAttendanceResponses);
                            foreach (var item in obPersonDailyAttendanceResponse)
                            {
                                Events.Add(item.AttendanceDate, new DayEventCollection<PersonDailyAttendanceResponse>()
                                { EventIndicatorColor = item.DayColor, EventIndicatorSelectedColor = item.DayColor });
                            }
                        }
                        else
                        {
                            Events.Clear();
                            obPersonDailyAttendanceResponse = new ObservableCollection<PersonDailyAttendanceResponse>(personDailyAttendanceResponses);
                            foreach (var item in obPersonDailyAttendanceResponse)
                            {
                                Events.Add(item.AttendanceDate, new DayEventCollection<PersonDailyAttendanceResponse>()
                                { EventIndicatorColor = item.DayColor, EventIndicatorSelectedColor = item.DayColor });
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

        public async Task GetLocation()
        {
            try
            {
                IsBusy = true;
                List<LocationResponse> LocationDetails = new List<LocationResponse>();
                StoreDropDown = new ObservableCollection<LocationDetailsDropdown>();
                SelectedStore = new LocationDetailsDropdown();
                Locations = new ObservableCollection<LocationResponse>();

                if (NetworkAvailable)
                {
                    MasterDataManagementSL masterDataManagementSL = new MasterDataManagementSL();
                    LocationDetails = await masterDataManagementSL.GetLocationDetailsByPersonId((int)CommonAttribute.CustomeProfile.PersonId);
                }
                else
                {
                    //showToasterNoNetwork();

                    //fetch from sqlite
                    //var locationStores1 = locationStoreDb.GetlocationStores().ToList();
                    //var locationStores = supLocationStoreDb.GetlocationStores().ToList();

                    var locationStores = promoterLocationStoreDb.GetlocationStores().ToList();
                    if (locationStores != null && locationStores.Count() > 0)
                    {
                        foreach (var item in locationStores)
                        {
                            LocationDetails.Add(new LocationResponse()
                            {
                                LocationId = item.LocationId,
                                LocationStoreName = item.LocationStoreName,
                                Area = item.Area,
                                Longitude = item.Longitude,
                                Latitude = item.Latitude,
                                Distance = item.Distance,
                            });
                        }
                    }
                }

                if (LocationDetails != null && LocationDetails.Count != 0)
                {
                    Locations = new ObservableCollection<LocationResponse>(LocationDetails);
                    foreach (var item in Locations)
                    {
                        StoreDropDown.Add(new LocationDetailsDropdown
                        {
                            Id = item.LocationId,
                            Title = item.LocationStoreName,
                            Area = item.Area,
                            Longitude = item.Longitude,
                            Latitude = item.Latitude,
                            Distance = item.Distance,
                        });
                    }
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
            catch (Exception ex)
            {
                Debugger.Log(0, null, ex.ToString());
            }
            finally
            {
                IsBusy = false;
            }
        }

        public Command SelectStoreCommand
        {
            get
            {
                return new Command<LocationDetailsDropdown>(async (obj) =>
                {
                    if (obj != null)
                    {
                        SelectedStore = obj;
                        SelectedStore.Id = obj.Id;
                        if (SelectedDate.ToString("dd/MM/yyyy") == DateTime.Now.ToString("dd/MM/yyyy"))
                        {
                            await GetTodaysAttendance();
                            //await GetMonthlyAttendanceDetail(SelectedDate);
                        }
                        else
                        {
                            await GetAttendanceHistory();
                            //await GetMonthlyAttendanceDetail(SelectedDate);
                        }
                    }
                    else
                    {
                        SelectedStore.Id = 0;
                    }

                });
            }
        }

        public async Task<double> GetDistanceByPersonLocationAsync(double SelectedStoreLattitude, double SelectedStoreLongitude)
        {
            if (NetworkAvailable)
            {
                CommonAttribute.UserLocation = await Extensions.GetGeolocation();
                if (CommonAttribute.UserLocation == null)
                {
                    await ErrorDisplayAlert("Your gps location is not accurate.");
                    return 0;
                }
                if (CommonAttribute.UserLocation != null)
                {
                    Location sourceCoordinates = new Location(CommonAttribute.UserLocation.Latitude, CommonAttribute.UserLocation.Longitude);

                    Location destinationCoordinates = new Location(Convert.ToDouble(SelectedStoreLattitude), Convert.ToDouble(SelectedStoreLongitude));
                    double NearBydistance = Location.CalculateDistance(sourceCoordinates, destinationCoordinates, DistanceUnits.Kilometers);
                    Double Distance = Math.Round((1000 * NearBydistance), 2); //in meter

                    return Distance;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                if (CommonAttribute.UserLocation != null)
                {
                    var Lastlocation = await Geolocation.GetLastKnownLocationAsync();

                    Location sourceCoordinates = new Location(CommonAttribute.UserLocation.Latitude, CommonAttribute.UserLocation.Longitude);

                    Location destinationCoordinates = new Location(Convert.ToDouble(SelectedStoreLattitude), Convert.ToDouble(SelectedStoreLongitude));
                    double NearBydistance = Location.CalculateDistance(sourceCoordinates, destinationCoordinates, DistanceUnits.Kilometers);
                    Double Distance = Math.Round((1000 * NearBydistance), 2); //in meter

                    return Distance;
                }
                else
                {
                    return 0;
                }
            }

        }

        private async void PreviousMonthAttendance(DateTime date)
        {
            await GetMonthlyAttendanceDetail(date);
            if (obAttendanceResponseHistory != null || lstAttendanceResponseHistory != null)
            {
                obAttendanceResponseHistory = new ObservableCollection<AttendanceResponse>();
                lstAttendanceResponseHistory = new List<AttendanceResponse>();
            }
        }

        private async void NextMonthAttendance(DateTime date)
        {
            await GetMonthlyAttendanceDetail(date);
            if (obAttendanceResponseHistory != null || lstAttendanceResponseHistory != null)
            {
                obAttendanceResponseHistory = new ObservableCollection<AttendanceResponse>();
                lstAttendanceResponseHistory = new List<AttendanceResponse>();
            }
        }

        public async Task GetAttendanceHistory()
        {
            try
            {
                IsBusy = true;

                if (NetworkAvailable)
                {
                    IsCheckinCheckoutVisible = false;
                    IsHistoryVisible = true;
                    IsDayOffVisible = false;
                    lstAttendanceResponseHistory = new List<AttendanceResponse>();
                    int SelectedStoreID = 0;
                    if (SelectedStore != null)
                    {
                        SelectedStoreID = SelectedStore.Id;
                    }
                    string date = SelectedDate.ToString("yyyy-MM-dd");
                    AttendanceDataManagementSL attendanceDataManagementSL = new AttendanceDataManagementSL();
                    lstAttendanceResponseHistory = await attendanceDataManagementSL.GetAttendanceDetailsByDateLocationPersonID(SelectedStoreID, (int)CommonAttribute.CustomeProfile.PersonId, date, (int)CommonAttribute.CustomeProfile.CountryId);
                    if (lstAttendanceResponseHistory != null && lstAttendanceResponseHistory.Count != 0)
                    {
                        obAttendanceResponseHistory = new ObservableCollection<AttendanceResponse>(lstAttendanceResponseHistory);
                        foreach (var item in obAttendanceResponseHistory)
                        {
                            if (item.IsOffDay == true)
                            {
                                IsDayOffVisible = true;
                                IsDayOff = true;
                                IsDayOffEnable = false;
                                IsDayoffBackgroungColor = Color.LightGray;
                                IsHistoryVisible = false;
                            }
                            else
                            {
                                IsHistoryVisible = true;
                            }
                        }
                    }
                    else
                    {
                        IsHistoryVisible = false;
                    }

                    AttendanceDataManagementSL todaysattendanceDataManagementSL = new AttendanceDataManagementSL();
                    lstAttendanceResponseHistory = await attendanceDataManagementSL.GetAttendanceDetailsByDateLocationPersonID(0, (int)CommonAttribute.CustomeProfile.PersonId, date, (int)CommonAttribute.CustomeProfile.CountryId);
                    if (lstAttendanceResponseHistory != null && lstAttendanceResponseHistory.Count != 0)
                    {
                        foreach (var item in lstAttendanceResponseHistory)
                        {
                            if (item.IsOffDay == true)
                            {
                                IsDayOff = true;
                                IsDayOffEnable = false;
                                IsDayOffVisible = true;
                                IsDayoffBackgroungColor = Color.LightGray;
                                break;
                            }
                            else
                            {
                                IsDayOffVisible = false;
                                break;
                            }

                        }
                    }
                }
                else
                {
                    //await ErrorDisplayAlert("You are offline");
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

        public async Task GetTodaysAttendance()
        {
            try
            {
                IsBusy = true;
                //for testing
                //await GetTodaysOfflineAttendnace();

                if (NetworkAvailable)
                {
                    IsDayoffBackgroungColor = Color.FromHex("#1E55A5");
                    lstAttendanceResponseHistory = new List<AttendanceResponse>();
                    IsHistoryVisible = false;
                    IsAttendanceVisible = true;
                    IsDayOffVisible = true;
                    IsDayOffEnable = true;
                    IsCheckinCheckoutVisible = true;
                    IsDayOff = false;
                    int SelectedStoreID = 0;
                    if (SelectedStore != null)
                    {
                        SelectedStoreID = SelectedStore.Id;
                    }
                    obTodaysAttendanceHistory = new ObservableCollection<AttendanceResponse>();
                    string date = SelectedDate.ToString("yyyy-MM-dd");
                    AttendanceDataManagementSL attendanceDataManagementSL = new AttendanceDataManagementSL();
                    lstAttendanceResponseHistory = await attendanceDataManagementSL.GetAttendanceDetailsByDateLocationPersonID(SelectedStoreID, (int)CommonAttribute.CustomeProfile.PersonId, date, (int)CommonAttribute.CustomeProfile.CountryId);
                    if (lstAttendanceResponseHistory != null && lstAttendanceResponseHistory.Count != 0)
                    {
                        obTodaysAttendanceHistory = new ObservableCollection<AttendanceResponse>(lstAttendanceResponseHistory);
                        foreach (var item in lstAttendanceResponseHistory)
                        {
                            ////checkin
                            if (item.CheckInDate != null) //CHECK-IN
                            {
                                CheckInTime = item.CheckInDate;
                                CheckInBackgroungColor = Color.LightGray;
                                IsEnableCheckIn = false;
                                IsEnableCheckOut = true;
                                IsDayOff = false;
                                IsDayOffEnable = false;
                                IsDayOffVisible = false;
                            }
                            //else
                            //{
                            //    string CheckIn = "Select * From TblPromoterAttendance Where AttendanceDate='" + date + "' AND LocationId='" + SelectedStoreID + "' AND PersonId = '" + CommonAttribute.CustomeProfile.PersonId + "' Order by CheckInTime DESC";
                            //    var AttendanceEntry = c.conn.Query<TblPromoterAttendance>(CheckIn);
                            //    if (AttendanceEntry != null && AttendanceEntry.Count > 0)
                            //    {
                            //        if (AttendanceEntry[0].CheckInTime != null)
                            //        {
                            //            CheckInTime = AttendanceEntry[0].CheckInTime;
                            //            CheckInBackgroungColor = Color.LightGray;
                            //            IsEnableCheckIn = false;
                            //            IsEnableCheckOut = true;
                            //            IsDayOff = false;
                            //            IsDayOffEnable = false;
                            //            IsDayOffVisible = false;
                            //        }
                            //        else
                            //        {
                            //            //CheckInTime = null;
                            //            CheckInBackgroungColor = Color.FromHex("#1E55A5");
                            //            IsEnableCheckIn = true;
                            //            IsDayOff = false;
                            //            IsDayOffEnable = true;
                            //            IsDayOffVisible = true;
                            //        }
                            //    }
                            //    else
                            //    {
                            //        //CheckInTime = null;
                            //        CheckInBackgroungColor = Color.FromHex("#1E55A5");
                            //        IsEnableCheckIn = true;
                            //        IsDayOff = false;
                            //        IsDayOffEnable = true;
                            //        IsDayOffVisible = true;
                            //    }

                            //}

                            ////checkout
                            if (item.CheckoutDate != null) //CHECK-OUT
                            {
                                CheckOutTime = item.CheckoutDate;
                                CheckOutBackgroungColor = Color.LightGray;
                                IsEnableCheckOut = false;
                                IsDayOff = false;
                                IsDayOffEnable = false;
                                IsDayOffVisible = false;
                            }
                            //else
                            //{
                            //    string CheckIn = "Select * From TblPromoterAttendance Where AttendanceDate='" + date + "' AND LocationId='" + SelectedStoreID + "' AND PersonId = '" + CommonAttribute.CustomeProfile.PersonId + "' Order by CheckOutTime DESC";
                            //    var AttendanceEntry = c.conn.Query<TblPromoterAttendance>(CheckIn);
                            //    if (AttendanceEntry != null && AttendanceEntry.Count > 0)
                            //    {
                            //        if (AttendanceEntry[0].CheckOutTime != null)
                            //        {
                            //            CheckOutTime = AttendanceEntry[0].CheckOutTime;
                            //            CheckOutBackgroungColor = Color.LightGray;
                            //            IsEnableCheckOut = false;
                            //            IsDayOff = false;
                            //            IsDayOffEnable = false;
                            //            IsDayOffVisible = false;
                            //        }
                            //        else
                            //        {
                            //            //CheckOutTime = null;
                            //            CheckOutBackgroungColor = Color.FromHex("#1E55A5");
                            //            IsEnableCheckOut = true;
                            //            IsDayOff = false;
                            //            IsDayOffEnable = true;
                            //            IsDayOffVisible = true;
                            //            if (item.CheckInDate != null)
                            //            {
                            //                IsDayOffVisible = false;
                            //            }
                            //        }

                            //    }
                            //    else
                            //    {
                            //        //CheckOutTime = null;
                            //        CheckOutBackgroungColor = Color.FromHex("#1E55A5");
                            //        IsEnableCheckOut = true;
                            //        IsDayOff = false;
                            //        IsDayOffEnable = true;
                            //        IsDayOffVisible = true;
                            //        if (item.CheckInDate != null)
                            //        {
                            //            IsDayOffVisible = false;
                            //        }
                            //    }
                            //}

                            ////Day-OFF
                            if (item.IsOffDay == true) //IS-OFF-DAY
                            {
                                IsDayOff = true;
                                IsDayOffEnable = false;
                                IsDayOffVisible = true;
                                IsDayoffBackgroungColor = Color.LightGray;
                                CheckOutBackgroungColor = Color.LightGray;
                                IsEnableCheckOut = false;
                                CheckInBackgroungColor = Color.LightGray;
                                IsEnableCheckIn = false;

                            }
                            //else
                            //{
                            //    string CheckIn = "Select * From TblPromoterAttendance Where AttendanceDate='" + date + "' AND LocationId='" + SelectedStoreID + "' AND PersonId = '" + CommonAttribute.CustomeProfile.PersonId + "'";
                            //    var AttendanceEntry = c.conn.Query<TblPromoterAttendance>(CheckIn);
                            //    if (AttendanceEntry != null && AttendanceEntry.Count > 0)
                            //    {
                            //        if (AttendanceEntry[0].IsOffDay == true)
                            //        {
                            //            IsDayOff = true;
                            //            IsDayOffEnable = false;
                            //            IsDayOffVisible = true;
                            //            IsDayoffBackgroungColor = Color.LightGray;
                            //            CheckOutBackgroungColor = Color.LightGray;
                            //            IsEnableCheckOut = false;
                            //            CheckInBackgroungColor = Color.LightGray;
                            //            IsEnableCheckIn = false;
                            //        }
                            //        else
                            //        {
                            //            IsDayOff = false;
                            //            IsDayOffEnable = true;
                            //            IsDayoffBackgroungColor = Color.FromHex("#1E55A5");
                            //            if (CheckInTime != null && CheckInTime.ToString() != string.Empty)
                            //            {
                            //                IsEnableCheckIn = false;
                            //                CheckInBackgroungColor = Color.LightGray;
                            //                IsDayOffVisible = false;
                            //            }
                            //            else
                            //            {
                            //                CheckInBackgroungColor = Color.FromHex("#1E55A5");
                            //                IsEnableCheckIn = true;
                            //                IsDayOffVisible = true;
                            //            }
                            //            if (CheckOutTime != null && CheckOutTime.ToString() != string.Empty)
                            //            {
                            //                CheckOutBackgroungColor = Color.LightGray;
                            //                IsEnableCheckOut = false;
                            //                IsDayOffVisible = false;
                            //            }
                            //            else
                            //            {
                            //                CheckOutBackgroungColor = Color.FromHex("#1E55A5");
                            //                IsEnableCheckOut = true;
                            //                IsDayOffVisible = true;
                            //                if (item.CheckInDate != null)
                            //                {
                            //                    IsDayOffVisible = false;
                            //                }
                            //            }
                            //        }
                            //    }
                            //    else
                            //    {
                            //        IsDayOff = false;
                            //        IsDayOffEnable = true;
                            //        IsDayoffBackgroungColor = Color.FromHex("#1E55A5");
                            //        if (CheckInTime != null && CheckInTime.ToString() != string.Empty)
                            //        {
                            //            IsEnableCheckIn = false;
                            //            CheckInBackgroungColor = Color.LightGray;
                            //            IsDayOffVisible = false;
                            //        }
                            //        else
                            //        {
                            //            CheckInBackgroungColor = Color.FromHex("#1E55A5");
                            //            IsEnableCheckIn = true;
                            //            IsDayOffVisible = true;
                            //        }
                            //        if (CheckOutTime != null && CheckOutTime.ToString() != string.Empty)
                            //        {
                            //            CheckOutBackgroungColor = Color.LightGray;
                            //            IsEnableCheckOut = false;
                            //            IsDayOffVisible = false;
                            //        }
                            //        else
                            //        {
                            //            CheckOutBackgroungColor = Color.FromHex("#1E55A5");
                            //            IsEnableCheckOut = true;
                            //            IsDayOffVisible = true;
                            //            if (item.CheckInDate != null)
                            //            {
                            //                IsDayOffVisible = false;
                            //            }
                            //        }
                            //    }

                            //}

                            if (item.CheckInDate != null && item.CheckoutDate == null)
                            {
                                CheckOutTime = item.CheckoutDate;
                                CheckOutBackgroungColor = Color.FromHex("#1E55A5");
                                IsEnableCheckOut = true;
                                IsDayOff = false;
                                IsDayOffEnable = true;
                                IsDayOffVisible = true;
                                if (item.CheckInDate != null)
                                {
                                    IsDayOffVisible = false;
                                }


                            }
                            else if (item.CheckInDate != null && item.CheckoutDate != null)
                            {
                                CheckInTime = null;
                                //CheckInTime = item.CheckInDate;
                                CheckInBackgroungColor = Color.FromHex("#1E55A5");
                                IsEnableCheckIn = true;
                                IsDayOff = false;
                                IsDayOffEnable = true;
                                IsDayOffVisible = true;

                                CheckOutTime = null;
                                //CheckOutTime = item.CheckoutDate;
                                CheckOutBackgroungColor = Color.FromHex("#1E55A5");
                                IsEnableCheckOut = true;
                                IsDayOff = false;
                                IsDayOffEnable = true;
                                IsDayOffVisible = true;
                                if (item.CheckInDate != null)
                                {
                                    IsDayOffVisible = false;
                                }
                            }
                        }
                    }
                    //else
                    //{
                    //    string CheckIn = "Select * From TblPromoterAttendance Where AttendanceDate='" + date + "' AND LocationId='" + SelectedStoreID + "' AND PersonId = '" + CommonAttribute.CustomeProfile.PersonId + "' Order by ID DESC";
                    //    var AttendanceEntry = c.conn.Query<TblPromoterAttendance>(CheckIn);
                    //    if (AttendanceEntry != null && AttendanceEntry.Count > 0)
                    //    {
                    //        if (AttendanceEntry[0].CheckInDate != null) //CHECK-IN
                    //        {
                    //            CheckInTime = AttendanceEntry[0].CheckInTime;
                    //            CheckInBackgroungColor = Color.LightGray;
                    //            IsEnableCheckIn = false;
                    //            IsEnableCheckOut = true;
                    //            IsDayOff = false;
                    //            IsDayOffEnable = false;
                    //            IsDayOffVisible = false;
                    //        }
                    //        else
                    //        {
                    //            CheckInTime = null;
                    //            CheckInBackgroungColor = Color.FromHex("#1E55A5");
                    //            IsEnableCheckIn = true;
                    //            IsDayOff = false;
                    //            IsDayOffEnable = true;
                    //            IsDayOffVisible = true;


                    //        }
                    //        if (AttendanceEntry[0].CheckoutDate != null) //CHECK-OUT
                    //        {
                    //            CheckOutTime = AttendanceEntry[0].CheckOutTime;
                    //            CheckOutBackgroungColor = Color.LightGray;
                    //            IsEnableCheckOut = false;
                    //            IsDayOff = false;
                    //            IsDayOffEnable = false;
                    //            IsDayOffVisible = false;
                    //        }
                    //        else
                    //        {
                    //            CheckOutTime = null;
                    //            CheckOutBackgroungColor = Color.FromHex("#1E55A5");
                    //            IsEnableCheckOut = true;
                    //            IsDayOff = false;
                    //            IsDayOffEnable = true;
                    //            IsDayOffVisible = true;
                    //            if (AttendanceEntry[0].CheckInDate != null)
                    //            {
                    //                IsDayOffVisible = false;
                    //            }

                    //        }
                    //        if (AttendanceEntry[0].IsOffDay == true) //IS-OFF-DAY
                    //        {
                    //            IsDayOff = true;
                    //            IsDayOffEnable = false;
                    //            IsDayOffVisible = true;
                    //            IsDayoffBackgroungColor = Color.LightGray;
                    //            CheckOutBackgroungColor = Color.LightGray;
                    //            IsEnableCheckOut = false;
                    //            CheckInBackgroungColor = Color.LightGray;
                    //            IsEnableCheckIn = false;

                    //        }
                    //        else
                    //        {
                    //            IsDayOff = false;
                    //            IsDayOffEnable = true;
                    //            IsDayoffBackgroungColor = Color.FromHex("#1E55A5");
                    //            if (CheckInTime != null && CheckInTime.ToString() != string.Empty)
                    //            {
                    //                IsEnableCheckIn = false;
                    //                CheckInBackgroungColor = Color.LightGray;
                    //                IsDayOffVisible = false;
                    //            }
                    //            else
                    //            {
                    //                CheckInBackgroungColor = Color.FromHex("#1E55A5");
                    //                IsEnableCheckIn = true;
                    //                IsDayOffVisible = true;
                    //            }
                    //            if (CheckOutTime != null && CheckOutTime.ToString() != string.Empty)
                    //            {
                    //                CheckOutBackgroungColor = Color.LightGray;
                    //                IsEnableCheckOut = false;
                    //                IsDayOffVisible = false;
                    //            }
                    //            else
                    //            {
                    //                CheckOutBackgroungColor = Color.FromHex("#1E55A5");
                    //                IsEnableCheckOut = true;
                    //                IsDayOffVisible = true;
                    //                if (AttendanceEntry[0].CheckInDate != null)
                    //                {
                    //                    IsDayOffVisible = false;
                    //                }
                    //            }

                    //        }
                    //    }
                    //    else
                    //    {
                    //        IsDayOff = false;
                    //        IsDayOffEnable = true;
                    //        CheckOutTime = CheckInTime = null;
                    //        IsDayOffVisible = true;
                    //        IsDayoffBackgroungColor = Color.FromHex("#1E55A5");
                    //        CheckInBackgroungColor = Color.FromHex("#1E55A5");
                    //        IsEnableCheckIn = true;
                    //        CheckOutBackgroungColor = Color.FromHex("#1E55A5");
                    //        IsEnableCheckOut = true;
                    //    }

                    //}


                    AttendanceDataManagementSL todaysattendanceDataManagementSL = new AttendanceDataManagementSL();
                    lstAttendanceResponseHistory = await attendanceDataManagementSL.GetAttendanceDetailsByDateLocationPersonID(0, (int)CommonAttribute.CustomeProfile.PersonId, date, (int)CommonAttribute.CustomeProfile.CountryId);
                    if (lstAttendanceResponseHistory != null && lstAttendanceResponseHistory.Count != 0)
                    {
                        foreach (var item in lstAttendanceResponseHistory)
                        {
                            if (item.IsOffDay == true)
                            {
                                IsDayOff = true;
                                IsDayOffEnable = false;
                                IsDayOffVisible = true;
                                IsDayoffBackgroungColor = Color.LightGray;
                                CheckOutBackgroungColor = Color.LightGray;
                                IsEnableCheckOut = false;
                                CheckInBackgroungColor = Color.LightGray;
                                IsEnableCheckIn = false;

                                break;
                            }
                            else
                            {
                                IsDayOffVisible = false;
                                IsCheckinCheckoutVisible = true;
                                break;
                            }

                        }
                    }
                    //else
                    //{
                    //    string IsDayOffQuery = "Select * From TblPromoterAttendance Where AttendanceDate='" + date + "' AND PersonId = '" + CommonAttribute.CustomeProfile.PersonId + "' order by ID desc";
                    //    var TodaysAttendance = c.conn.Query<TblPromoterAttendance>(IsDayOffQuery);
                    //    if (TodaysAttendance != null && TodaysAttendance.Count > 0)
                    //    {
                    //        foreach (var item in TodaysAttendance)
                    //        {
                    //            if (item.IsOffDay == true)
                    //            {
                    //                IsDayOffVisible = true;
                    //                IsCheckinCheckoutVisible = false;
                    //                IsDayOff = true;
                    //                IsDayOffEnable = false;
                    //                break;
                    //            }

                    //            if (item.CheckInDate != null || item.CheckoutDate != null)
                    //            {
                    //                IsDayOffVisible = false;
                    //                IsCheckinCheckoutVisible = true;
                    //                break;
                    //            }

                    //        }
                    //    }
                    //}
                }
                else
                {
                    await GetTodaysOfflineAttendnace();
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

        private async void DayTapped(DateTime date)
        {
            try
            {
                if (Device.RuntimePlatform == Device.iOS)
                {
                    var timeCheckService = DependencyService.Get<IDeviceTimeService>();

                    if (timeCheckService != null)
                    {
                        bool isDeviceTimeValid = await timeCheckService.IsDeviceTimeValid();

                        if (isDeviceTimeValid)
                        {
                            Console.WriteLine("Device time is valid");
                        }
                        else
                        {
                            Console.WriteLine("Device time is invalid (potentially set in the future)");
                        }
                    }

                    //var deviceTimeService = DependencyService.Get<IDeviceTimeService>();

                    //if (deviceTimeService != null)
                    //{
                    //    bool isAutomatic = deviceTimeService.IsDeviceTimeAutomatic();

                    //    if (isAutomatic)
                    //    {
                    //        Console.WriteLine("Device time setting is automatic");
                    //        return;
                    //    }
                    //    else
                    //    {
                    //        Console.WriteLine("Device time setting is not automatic");
                    //        return;
                    //    }

                    //}

                    //var deviceTimeService = DependencyService.Get<IDeviceTimeService>();

                    //if (deviceTimeService != null)
                    //{
                    //    bool isAutomatic = deviceTimeService.IsDeviceTimeAutomatic();
                    //    DateTime currentDeviceTime = deviceTimeService.GetCurrentDeviceTime();

                    //    if (isAutomatic)
                    //    {
                    //        Console.WriteLine("Device time setting is automatic");
                    //        return;
                    //    }
                    //    else
                    //    {
                    //        Console.WriteLine("Device time setting is not automatic");
                    //        return;
                    //    }

                    //    deviceTimeService.DeviceTimeChanged += (sender, args) =>
                    //    {
                    //        // Handle the event when the device time changes
                    //        Console.WriteLine("Device time has changed");
                    //    };
                    //}
                }
            }
            catch (Exception ex)
            {

            }
            

            int SelectedStoreID = 0;
            if (SelectedStore != null)
            {
                SelectedStoreID = SelectedStore.Id;
            }
            if (SelectedStoreID == 0)
            {
                await ErrorDisplayAlert("Please select store.");
                return;
            }
            try
            {
                SelectedDate = date;
                if (date.ToString("dd/MM/yyyy") == DateTime.Now.ToString("dd/MM/yyyy"))
                {
                    //IsAttendanceVisible = true;
                    //IsCheckinCheckoutVisible = true;
                    //IsDayOffVisible = true;
                    //IsHistoryVisible = false;
                    await GetTodaysAttendance();

                }
                else
                {
                    //IsAttendanceVisible = false;
                    //IsCheckinCheckoutVisible = false;
                    //IsDayOffVisible = false;
                    //IsHistoryVisible = true;
                    if (NetworkAvailable)
                    {
                        await GetAttendanceHistory();
                    }
                    else
                    {
                        IsDayOffVisible = false;
                        IsCheckinCheckoutVisible = false;
                    }

                }

                await GetMonthlyAttendanceDetail(SelectedDate);
            }
            catch (Exception ex)
            { }
        }

        public Command CheckinCommand
        {
            get
            {
                return new Command(async () =>
                {
                    
                    //if (CheckInTime.ToString() == string.Empty || CheckInTime == null)
                    //{
                    ////IsDayOffVisible = false;
                    await InsertAttendance();
                       
                    //}
                    //else
                    //{
                    //    await ErrorDisplayAlert("You have already checked in.");
                    //    //IsEnableCheckIn = false;
                    //    if (CheckInTime.ToString() == string.Empty || CheckInTime == null)
                    //        CheckInBackgroungColor = Color.FromHex("#1E55A5");
                    //    else
                    //        CheckInBackgroungColor = Color.LightGray;//FromHex("#FF0000");

                    //}
                });
            }
        }

        public async Task InsertIsDayOff()
        {
            try
            {

                if (Device.RuntimePlatform == Device.iOS)
                {
                    ////iOS stuff
                    //var dateTimeSettings = DependencyService.Get<IDateTimeSettings>();
                    //dateTimeSettings.OpenDateTimeSettings();
                    //bool isAutomaticDateTimeEnabled = dateTimeSettings.IsAutomaticDateTimeEnabled();
                    //if(isAutomaticDateTimeEnabled)
                    //{
                    //    await DisplayAlert("True", "Automatic date and time are enabled.");

                    //}
                    //return;

                    //var deviceTimeService = DependencyService.Get<IDeviceTimeService>();

                    //if (deviceTimeService != null)
                    //{
                    //    DateTime referenceTime = deviceTimeService.GetReferenceTime();

                    //    if (deviceTimeService.HasDeviceTimeChanged())
                    //    {
                    //        // Handle the case where the device time has changed
                    //        await DisplayAlert("Error!", "Automatic date and time are not enabled.");
                    //        return;
                    //    }
                    //    else
                    //    {
                    //        // Device time is the same or cannot be determined
                    //        await DisplayAlert("Success", "Automatic date and time are enabled.");
                    //        return;
                    //    }
                    //}

                    //var deviceTimeService = DependencyService.Get<IDeviceTimeService>();

                    //if (deviceTimeService != null)
                    //{
                    //    bool isAutomatic = deviceTimeService.IsDeviceTimeAutomatic();
                    //    DateTime currentDeviceTime = deviceTimeService.GetCurrentDeviceTime();

                    //    if (isAutomatic)
                    //    {
                    //        Console.WriteLine("Device time setting is automatic");
                    //        return;
                    //    }
                    //    else
                    //    {
                    //        Console.WriteLine("Device time setting is not automatic");
                    //        return;
                    //    }

                    //    deviceTimeService.DeviceTimeChanged += (sender, args) =>
                    //    {
                    //        // Handle the event when the device time changes
                    //        Console.WriteLine("Device time has changed");
                    //    };
                    //}
                }
                else if (Device.RuntimePlatform == Device.Android)
                {
                    var dateTimeSettings = DependencyService.Get<IDateTimeSettings>();
                    bool isAutomaticDateTimeEnabled = dateTimeSettings.IsAutomaticDateTimeEnabled();

                    if (isAutomaticDateTimeEnabled)
                    {
                        // Automatic date and time are enabled
                        await DisplayAlert("True", "Automatic date and time are enabled.");
                        return;
                    }
                    else
                    {
                        // Automatic date and time are not enabled
                        await DisplayAlert("Error!", "Automatic date and time are not enabled.");
                        return;
                    }
                }

                

               

                IsBusy = true;

                if (IsDayOff == true)
                    IsAttendanceVisible = false;

                int SelectedStoreID = 0;

                string TodaysDate = DateTime.Now.Date.ToString("yyyy-MM-dd");
                if (NetworkAvailable)
                {
                    if (SelectedStore != null)
                    {
                        SelectedStoreID = SelectedStore.Id;
                    }
                    if (SelectedStoreID == 0)
                    {
                        await ErrorDisplayAlert("Please select store.");
                        return;
                    }
                    double NearbyDistance = await GetDistanceByPersonLocationAsync((double)SelectedStore.Latitude, (double)SelectedStore.Longitude);
                    CheckInCheckOut = new AttendanceRequest();
                    CheckInCheckOut.PersonId = CommonAttribute.CustomeProfile.PersonId;
                    CheckInCheckOut.LocationId = SelectedStoreID;
                    CheckInCheckOut.AttendanceDate = DateTime.Now.Date;
                    CheckInCheckOut.CheckInDate = null;
                    CheckInCheckOut.CheckoutDate = null;
                    CheckInCheckOut.PersonLocationLatitude = (decimal?)CommonAttribute.UserLocation.Latitude;
                    CheckInCheckOut.PersonLocationLongitude = (decimal?)CommonAttribute.UserLocation.Longitude;
                    CheckInCheckOut.IsOffDay = true;
                    CheckInCheckOut.ProximityRange = (decimal?)NearbyDistance;
                    CheckInCheckOut.OnlineOffLineAttendanceStatus = (int)OnlineOffLineAttendanceStatusEnum.Online;
                    AttendanceDataManagementSL attendanceDataManagementSL = new AttendanceDataManagementSL();
                    var response = await attendanceDataManagementSL.AttendanceCreateUpdateAttendance(CheckInCheckOut);
                    if (response.Status == Convert.ToInt16(APIResponseEnum.Success))
                    {
                        await DisplayAlert("Success", "Day-Off submitted successfully.");
                        IsDayOffEnable = false;
                        IsDayoffBackgroungColor = Color.LightGray;
                    }
                    else
                    {
                        await DisplayAlert("Error!", "Failed to submit attendance.");
                    }

                    //c.conn.Table<TblPromoterAttendance>().Delete(d => d.AttendanceDate == TodaysDate && d.LocationId == SelectedStoreID && d.PersonId == CommonAttribute.CustomeProfile.PersonId);

                    //TblPromoterAttendance tbl = new TblPromoterAttendance();
                    //tbl.CheckInDate = CheckInTime;
                    //tbl.PersonId = CommonAttribute.CustomeProfile.PersonId;
                    //tbl.LocationId = SelectedStoreID;
                    //tbl.AttendanceDate = TodaysDate;
                    //tbl.CheckInDate = null;
                    //tbl.CheckoutDate = null;
                    //tbl.PersonLocationLatitude = (decimal?)CommonAttribute.UserLocation.Latitude;
                    //tbl.PersonLocationLongitude = (decimal?)CommonAttribute.UserLocation.Longitude;
                    //tbl.IsOffDay = true;
                    //tbl.ProximityRange = (decimal?)NearbyDistance;
                    //c.conn.Insert(tbl);
                }
                else
                {
                    double NearbyDistance = await GetDistanceByPersonLocationAsync((double)CommonAttribute.CustomeProfile?.Locations?.FirstOrDefault().Latitude, (double)CommonAttribute.CustomeProfile?.Locations?.FirstOrDefault().Longitude);

                    if (SelectedStore != null)
                    {
                        SelectedStoreID = SelectedStore.Id;
                    }
                    if (SelectedStoreID == 0)
                    {
                        await ErrorDisplayAlert("Please select store.");
                        return;
                    }
                    c.conn.Table<TblPromoterAttendance>().Delete(d => d.AttendanceDate == TodaysDate && d.LocationId == SelectedStoreID && d.PersonId == CommonAttribute.CustomeProfile.PersonId);

                    TblPromoterAttendance tbl = new TblPromoterAttendance();
                    tbl.CheckInDate = CheckInTime;
                    tbl.PersonId = CommonAttribute.CustomeProfile.PersonId;
                    tbl.LocationId = SelectedStoreID;
                    tbl.AttendanceDate = TodaysDate;
                    tbl.CheckInDate = null;
                    tbl.CheckoutDate = null;
                    tbl.PersonLocationLatitude = (decimal?)CommonAttribute.UserLocation.Latitude;
                    tbl.PersonLocationLongitude = (decimal?)CommonAttribute.UserLocation.Longitude;
                    tbl.IsOffDay = true;
                    tbl.ProximityRange = (decimal?)NearbyDistance;
                    c.conn.Insert(tbl);

                    IsDayOffEnable = false;
                    IsDayoffBackgroungColor = Color.LightGray;
                    await ErrorDisplayAlert("Offline day-off submitted successfully.");
                }
            }
            catch (Exception ex)
            {
                Debugger.Log(0, null, ex.ToString());
                showToasterMessage("Please allow location permission.");
            }
            finally
            {
                IsBusy = false;
            }
        }

        public async Task InsertAttendance()
        {
            string _SelectedDate = SelectedDate.Date.ToString("yyyy-MM-dd");

            int SelectedStoreID = 0;
            try
            {
                IsBusy = true;
                IsEnableCheckIn = false;
                if (NetworkAvailable)
                {
                    if (SelectedStore != null)
                    {
                        SelectedStoreID = SelectedStore.Id;
                    }
                    if (SelectedStoreID == 0)
                    {
                        IsEnableCheckIn = true;
                        await ErrorDisplayAlert("Please select store.");
                        return;
                    }
                    if(CheckInTime != null && CheckOutTime == null)
                    {
                        await DisplayAlert("Warning!", "You cannot check in multiple times at the same time.Please do CheckOut for selected location.");
                        return;
                    }
                    var Selecteddate = DateTime.Now.Date.ToString("yyyy-MM-dd");
                    double NearbyDistance = await GetDistanceByPersonLocationAsync((double)SelectedStore.Latitude, (double)SelectedStore?.Longitude);

                    if (NearbyDistance <= 1000) //1km
                    {
                        CheckInCheckOut = new AttendanceRequest();
                        CheckInCheckOut.PersonId = CommonAttribute.CustomeProfile.PersonId;
                        CheckInCheckOut.LocationId = SelectedStoreID;
                        CheckInCheckOut.AttendanceDate = DateTime.Now.Date;
                        CheckInCheckOut.CheckInDate = DateTime.UtcNow;
                        CheckInCheckOut.CheckoutDate = null;
                        CheckInCheckOut.PersonLocationLatitude = (decimal?)CommonAttribute.UserLocation.Latitude;
                        CheckInCheckOut.PersonLocationLongitude = (decimal?)CommonAttribute.UserLocation.Longitude;
                        CheckInCheckOut.IsOffDay = false;
                        CheckInCheckOut.ProximityRange = (decimal?)NearbyDistance;
                        CheckInCheckOut.OnlineOffLineAttendanceStatus = (int)OnlineOffLineAttendanceStatusEnum.Online;

                        AttendanceDataManagementSL attendanceDataManagementSL = new AttendanceDataManagementSL();
                        var response = await attendanceDataManagementSL.AttendanceCreateUpdateAttendance(CheckInCheckOut);
                        if (response.Status == Convert.ToInt16(APIResponseEnum.Success))
                        {
                            await DisplayAlert("Success", "Check-In done successfully.");
                            CheckInTime = DateTime.Now;
                            //CheckInBackgroungColor = Color.LightGray;
                            IsDayOffEnable = false;
                        }
                        else
                        {
                            await DisplayAlert("Error!", "Failed to submit attendance.");
                        }

                        //Offline Saving CheckIn Attendance if user has done checkin ONLINE
                        //and after saving online attendance using API, he is doing checkout attendance
                        //for that purpose we're saving this entry 

                        string TodaysDate = DateTime.Now.Date.ToString("yyyy-MM-dd");
                        TblPromoterAttendance tbl = new TblPromoterAttendance();
                        //tbl.CheckInDate = CheckInTime;
                        //tbl.PersonId = CommonAttribute.CustomeProfile.PersonId;
                        //tbl.LocationId = SelectedStoreID;
                        //tbl.AttendanceDate = TodaysDate;
                        //tbl.CheckInDate = DateTime.UtcNow;
                        //tbl.CheckoutDate = null;
                        //tbl.PersonLocationLatitude = (decimal?)CommonAttribute.UserLocation.Latitude;
                        //tbl.PersonLocationLongitude = (decimal?)CommonAttribute.UserLocation.Longitude;
                        //tbl.IsOffDay = false;
                        //tbl.ProximityRange = (decimal?)NearbyDistance;
                        //tbl.CheckInTime = DateTime.Now;
                        //tbl.CheckOutTime = null;
                        //c.conn.Insert(tbl);
                    }
                    else
                    {
                        await DisplayAlert("Warning!", "Unable to submit attendance. You are not within the 1 km check-in area.");

                    }
                }
                else
                {
                    //SAVING OFFLINE
                    double NearbyDistance = await GetDistanceByPersonLocationAsync((double)CommonAttribute.CustomeProfile?.Locations?.FirstOrDefault().Latitude, (double)CommonAttribute.CustomeProfile?.Locations?.FirstOrDefault().Longitude);

                    if (SelectedStore != null)
                    {
                        SelectedStoreID = SelectedStore.Id;
                    }
                    if (SelectedStoreID == 0)
                    {
                        IsEnableCheckIn = true;
                        await ErrorDisplayAlert("Please select store.");
                        return;
                    }

                    if (CheckInTime != null && CheckOutTime == null)
                    {
                        await DisplayAlert("Warning!", "You cannot check in multiple times at the same time.Please do CheckOut for selected location.");
                        return;
                    }
                    string TodaysDate = DateTime.Now.Date.ToString("yyyy-MM-dd");
                    //c.conn.Table<TblPromoterAttendance>().Delete(d => d.AttendanceDate == TodaysDate && d.LocationId == SelectedStoreID && d.PersonId == CommonAttribute.CustomeProfile.PersonId);
                    //double NearbyDistanceoffline = await GetDistanceByPersonLocationAsync((double)SelectedStore.Latitude, (double)SelectedStore?.Longitude);
                    if(NearbyDistance <= 1000)
                    {
                        string CheckIn = "Select * From TblPromoterAttendance Where AttendanceDate='" + TodaysDate + "' AND LocationId='" + SelectedStoreID + "' AND PersonId = '" + CommonAttribute.CustomeProfile.PersonId + "'";
                        var CheckinEntry = c.conn.Query<TblPromoterAttendance>(CheckIn);
                        if (CheckinEntry != null && CheckinEntry.Count != 0)
                        {
                            if (CheckinEntry[0].AttendanceDate == TodaysDate && CheckinEntry[0].LocationId == SelectedStoreID && CheckinEntry[0].PersonId == CommonAttribute.CustomeProfile.PersonId)
                            {
                                CheckinEntry[0].CheckInDate = CheckInTime;
                                CheckinEntry[0].PersonId = CommonAttribute.CustomeProfile.PersonId;
                                CheckinEntry[0].LocationId = SelectedStoreID;
                                CheckinEntry[0].AttendanceDate = TodaysDate;
                                CheckinEntry[0].CheckInDate = DateTime.UtcNow;
                                CheckinEntry[0].CheckoutDate = null;
                                CheckinEntry[0].PersonLocationLatitude = (decimal?)CommonAttribute.UserLocation.Latitude;
                                CheckinEntry[0].PersonLocationLongitude = (decimal?)CommonAttribute.UserLocation.Longitude;
                                CheckinEntry[0].IsOffDay = false;
                                CheckinEntry[0].ProximityRange = (decimal?)NearbyDistance;
                                CheckinEntry[0].CheckInTime = DateTime.Now;
                                CheckinEntry[0].CheckOutTime = null;

                                var UpdatedEntry = c.conn.Update(CheckinEntry[0]);
                            }

                        }
                        else
                        {
                            TblPromoterAttendance tbl = new TblPromoterAttendance();
                            tbl.CheckInDate = CheckInTime;
                            tbl.PersonId = CommonAttribute.CustomeProfile.PersonId;
                            tbl.LocationId = SelectedStoreID;
                            tbl.AttendanceDate = TodaysDate;
                            tbl.CheckInDate = DateTime.UtcNow;
                            tbl.CheckoutDate = null;
                            tbl.PersonLocationLatitude = (decimal?)CommonAttribute.UserLocation.Latitude;
                            tbl.PersonLocationLongitude = (decimal?)CommonAttribute.UserLocation.Longitude;
                            tbl.IsOffDay = false;
                            tbl.ProximityRange = (decimal?)NearbyDistance;
                            tbl.CheckInTime = DateTime.Now;
                            tbl.CheckOutTime = null;
                            c.conn.Insert(tbl);
                        }
                        CheckInTime = DateTime.Now;
                        CheckInBackgroungColor = Color.LightGray;
                        await ErrorDisplayAlert("Offline check-in done successfully.");
                        IsDayOffEnable = false;
                    }
                    else
                    {
                        await DisplayAlert("Warning!", "Unable to submit attendance. You are not within the 1 km check-in area.");

                    }
                }
            }
            catch (Exception ex)
            {
                Debugger.Log(0, null, ex.ToString());
                showToasterMessage("Please allow location permission.");
                IsEnableCheckIn = true;
            }
            finally
            {
                IsBusy = false;
            }
        }

        public Command CheckoutCommand
        {
            get
            {
                return new Command(async () =>
                {
                    
                    IsEnableCheckOut = false;
                    if (CheckInTime != null && CheckInTime.ToString() != string.Empty)
                    {
                        //double UserNearbyDistance = await GetDistanceByPersonLocationAsync((double)SelectedStore.Latitude, (double)SelectedStore?.Longitude);
                        //if (UserNearbyDistance <= 1000)
                        //{
                        //    //if (CheckOutTime.ToString() == string.Empty || CheckOutTime == null)
                        //    //{

                        //    //}
                        //    //else
                        //    //{
                        //    //    await ErrorDisplayAlert("You have already checked out.");
                        //    //    if (CheckOutTime.ToString() == string.Empty || CheckOutTime == null)
                        //    //        CheckOutBackgroungColor = Color.FromHex("#1E55A5");
                        //    //    else
                        //    //        CheckOutBackgroungColor = Color.LightGray;
                        //    //}
                        //}
                        //else
                        //{
                        //    await DisplayAlert("Warning!", "Unable to submit attendance. You are not within the 1 km check-in area.");

                        //}

                        lstAttendanceResponseHistory = new List<AttendanceResponse>();
                        int SelectedStoreID = 0;

                        try
                        {
                            IsBusy = true;

                            if (NetworkAvailable)
                            {

                                if (SelectedStore != null)
                                {
                                    SelectedStoreID = SelectedStore.Id;
                                }
                                if (SelectedStoreID == 0)
                                {
                                    IsEnableCheckOut = true;
                                    await ErrorDisplayAlert("Please select store.");
                                    return;
                                }

                                string date = SelectedDate.ToString("yyyy-MM-dd");
                                double NearbyDistance = await GetDistanceByPersonLocationAsync((double)SelectedStore.Latitude, (double)SelectedStore?.Longitude);
                                if (NearbyDistance <= 1000)
                                {
                                    AttendanceDataManagementSL attendanceDataManagementSL = new AttendanceDataManagementSL();
                                    lstAttendanceResponseHistory = await attendanceDataManagementSL.GetAttendanceDetailsByDateLocationPersonID(SelectedStoreID, (int)CommonAttribute.CustomeProfile.PersonId, date, (int)CommonAttribute.CustomeProfile.CountryId);

                                    AttendanceResponse obj = lstAttendanceResponseHistory.FirstOrDefault();
                                    if (obj != null)
                                    {
                                        CheckInCheckOut = new AttendanceRequest();
                                        CheckInCheckOut.AttendanceId = obj.AttendanceId;
                                        CheckInCheckOut.PersonId = obj.PersonId;
                                        CheckInCheckOut.LocationId = obj.LocationId;
                                        CheckInCheckOut.AttendanceDate = obj.AttendanceDate;
                                        CheckInCheckOut.CheckInDate = obj.CheckInDate;
                                        CheckInCheckOut.CheckoutDate = DateTime.UtcNow;
                                        CheckInCheckOut.PersonLocationLatitude = (decimal?)CommonAttribute.UserLocation.Latitude;
                                        CheckInCheckOut.PersonLocationLongitude = (decimal?)CommonAttribute.UserLocation.Longitude;
                                        CheckInCheckOut.ProximityRange = (decimal?)NearbyDistance;
                                        IsDayOff = false;
                                        CheckInCheckOut.OnlineOffLineAttendanceStatus = (int)OnlineOffLineAttendanceStatusEnum.Online;

                                        ////OFFLINE CHECK-OUT SAVE
                                        //string CheckIn = "Select * From TblPromoterAttendance Where AttendanceDate='" + date + "' AND LocationId='" + SelectedStoreID + "' AND PersonId = '" + CommonAttribute.CustomeProfile.PersonId + "' Order By ID DESC Limit 1";
                                        //var AttendanceEntry = c.conn.Query<TblPromoterAttendance>(CheckIn);

                                        //if (AttendanceEntry != null && AttendanceEntry.Count != 0)
                                        //{
                                        //    AttendanceEntry[0].PersonId = CommonAttribute.CustomeProfile.PersonId;
                                        //    AttendanceEntry[0].LocationId = SelectedStoreID;
                                        //    AttendanceEntry[0].AttendanceDate = date;
                                        //    AttendanceEntry[0].CheckoutDate = DateTime.UtcNow;
                                        //    AttendanceEntry[0].PersonLocationLatitude = (decimal?)CommonAttribute.UserLocation.Latitude;
                                        //    AttendanceEntry[0].PersonLocationLongitude = (decimal?)CommonAttribute.UserLocation.Longitude;
                                        //    AttendanceEntry[0].IsOffDay = false;
                                        //    AttendanceEntry[0].ProximityRange = (decimal?)NearbyDistance;
                                        //    AttendanceEntry[0].CheckInTime = AttendanceEntry[0].CheckInTime;
                                        //    AttendanceEntry[0].CheckOutTime = DateTime.Now;
                                        //    var UpdatedEntry = c.conn.Update(AttendanceEntry[0]);
                                        //}


                                        var response = await attendanceDataManagementSL.AttendanceCreateUpdateAttendance(CheckInCheckOut);
                                        if (response.Status == Convert.ToInt16(APIResponseEnum.Success))
                                        {
                                            await DisplayAlert("Success", "Check-Out done successfully.");
                                            CheckOutTime = DateTime.Now;
                                            CheckOutBackgroungColor = Color.LightGray;

                                            CheckInTime = null; CheckOutTime = null;
                                            await GetTodaysAttendance();
                                        }
                                        else
                                        {
                                            await DisplayAlert("Error!", "Failed to submit attendance.");
                                        }
                                    }
                                    else
                                    {
                                        //OFFLINE CHECK-OUT SAVE
                                        string CheckIn = "Select * From TblPromoterAttendance Where AttendanceDate='" + date + "' AND LocationId='" + SelectedStoreID + "' AND PersonId = '" + CommonAttribute.CustomeProfile.PersonId + "' Order By ID DESC Limit 1";
                                        var AttendanceEntry = c.conn.Query<TblPromoterAttendance>(CheckIn);

                                        if (AttendanceEntry != null && AttendanceEntry.Count != 0)
                                        {
                                            AttendanceEntry[0].PersonId = CommonAttribute.CustomeProfile.PersonId;
                                            AttendanceEntry[0].LocationId = SelectedStoreID;
                                            AttendanceEntry[0].AttendanceDate = date;
                                            AttendanceEntry[0].CheckoutDate = DateTime.UtcNow;
                                            AttendanceEntry[0].PersonLocationLatitude = (decimal?)CommonAttribute.UserLocation.Latitude;
                                            AttendanceEntry[0].PersonLocationLongitude = (decimal?)CommonAttribute.UserLocation.Longitude;
                                            AttendanceEntry[0].IsOffDay = false;
                                            AttendanceEntry[0].ProximityRange = (decimal?)NearbyDistance;
                                            AttendanceEntry[0].CheckInTime = AttendanceEntry[0].CheckInTime;
                                            AttendanceEntry[0].CheckOutTime = DateTime.Now;
                                            var UpdatedEntry = c.conn.Update(AttendanceEntry[0]);
                                        }

                                        await DisplayAlert("Success", "Check-Out done successfully.");
                                        CheckOutTime = DateTime.Now;
                                        CheckOutBackgroungColor = Color.LightGray;                                        
                                    }
                                }
                                else
                                {
                                    await DisplayAlert("Warning!", "Unable to submit attendance. You are not within the 1 km check-in area.");

                                }

                            }
                            else
                            {
                                double NearbyDistance = await GetDistanceByPersonLocationAsync((double)CommonAttribute.CustomeProfile?.Locations?.FirstOrDefault().Latitude, (double)CommonAttribute.CustomeProfile?.Locations?.FirstOrDefault().Longitude);

                                if (SelectedStore != null)
                                {
                                    SelectedStoreID = SelectedStore.Id;
                                }
                                if (SelectedStoreID == 0)
                                {
                                    IsEnableCheckOut = true;
                                    await ErrorDisplayAlert("Please select store.");
                                    return;
                                }
                                //double NearbyDistanceoffline = await GetDistanceByPersonLocationAsync((double)SelectedStore.Latitude, (double)SelectedStore?.Longitude);
                                if (NearbyDistance <= 1000)
                                {
                                    string TodaysDate = DateTime.Now.Date.ToString("yyyy-MM-dd");
                                    //c.conn.Table<TblPromoterAttendance>().Delete(d => d.AttendanceDate == TodaysDate && d.LocationId == SelectedStoreID && d.PersonId == CommonAttribute.CustomeProfile.PersonId);
                                    string CheckIn = "Select * From TblPromoterAttendance Where AttendanceDate='" + TodaysDate + "' AND LocationId='" + SelectedStoreID + "' AND PersonId = '" + CommonAttribute.CustomeProfile.PersonId + "'";
                                    var AttendanceEntry = c.conn.Query<TblPromoterAttendance>(CheckIn);

                                    if (AttendanceEntry != null && AttendanceEntry.Count != 0)
                                    {
                                        AttendanceEntry[0].PersonId = CommonAttribute.CustomeProfile.PersonId;
                                        AttendanceEntry[0].LocationId = SelectedStoreID;
                                        AttendanceEntry[0].AttendanceDate = TodaysDate;
                                        AttendanceEntry[0].CheckoutDate = DateTime.UtcNow;
                                        AttendanceEntry[0].PersonLocationLatitude = (decimal?)CommonAttribute.UserLocation.Latitude;
                                        AttendanceEntry[0].PersonLocationLongitude = (decimal?)CommonAttribute.UserLocation.Longitude;
                                        AttendanceEntry[0].IsOffDay = false;
                                        AttendanceEntry[0].ProximityRange = (decimal?)NearbyDistance;
                                        AttendanceEntry[0].CheckInTime = AttendanceEntry[0].CheckInTime;
                                        AttendanceEntry[0].CheckOutTime = DateTime.Now;
                                        var UpdatedEntry = c.conn.Update(AttendanceEntry[0]);
                                    }
                                    //else
                                    //{
                                    //    TblPromoterAttendance tbl = new TblPromoterAttendance();
                                    //    tbl.CheckInDate = CheckInTime;
                                    //    tbl.PersonId = CommonAttribute.CustomeProfile.PersonId;
                                    //    tbl.LocationId = SelectedStoreID;
                                    //    tbl.AttendanceDate = TodaysDate;
                                    //    tbl.CheckInDate = DateTime.UtcNow;
                                    //    tbl.CheckoutDate = null;
                                    //    tbl.PersonLocationLatitude = (decimal?)CommonAttribute.UserLocation.Latitude;
                                    //    tbl.PersonLocationLongitude = (decimal?)CommonAttribute.UserLocation.Longitude;                                       
                                    //    tbl.IsOffDay = false;
                                    //    tbl.ProximityRange = (decimal?)NearbyDistance;
                                    //    tbl.CheckInTime = AttendanceEntry[0].CheckInTime;
                                    //    tbl.CheckOutTime = DateTime.Now;
                                    //    c.conn.Insert(tbl);
                                    //}
                                    CheckOutTime = DateTime.Now;
                                    CheckOutBackgroungColor = Color.LightGray;
                                    await ErrorDisplayAlert("Offline check-out done successfully.");
                                }
                                else
                                {
                                    await DisplayAlert("Warning!", "Unable to submit attendance. You are not within the 1 km check-in area.");

                                }
                            }

                        }
                        catch (Exception ex)
                        {
                            Debugger.Log(0, null, ex.ToString());
                            showToasterMessage("Please allow location permission.");
                            IsEnableCheckOut = true;
                        }
                        finally
                        {
                            IsBusy = false;
                        }

                    }
                    else
                    {
                        IsEnableCheckOut = true;
                        await ErrorDisplayAlert("Please do check-in first.");
                        return;
                    }

                });
            }
        }

        public Command SaveIsDayOffCommand
        {
            get
            {
                return new Command(async () =>
                {
                    if (IsDayOff == true)
                    {
                        IsCheckinCheckoutVisible = false;
                        await InsertIsDayOff();
                    }
                    else
                    {
                        await ErrorDisplayAlert("Please select Day-Off");
                        IsCheckinCheckoutVisible = true;
                    }
                });
            }
        }

        public async Task IsDayOffCheckedChange()
        {
            if (IsDayOff == true)
            {
                IsCheckinCheckoutVisible = false;
            }
            else
            {
                IsCheckinCheckoutVisible = true;
            }
        }

        
        #region Properties

        
        private int _FlagIsDayOff;
        public int FlagIsDayOff
        {
            get { return _FlagIsDayOff; }
            set
            {
                _FlagIsDayOff = value;
                OnPropertyChanged(nameof(FlagIsDayOff));
            }
        }

        private ObservableCollection<PersonDailyAttendanceResponse> _obPersonDailyAttendanceResponse;
        public ObservableCollection<PersonDailyAttendanceResponse> obPersonDailyAttendanceResponse
        {
            get { return _obPersonDailyAttendanceResponse; }
            set
            {
                _obPersonDailyAttendanceResponse = value;
                OnPropertyChanged(nameof(obPersonDailyAttendanceResponse));
            }
        }

        private ObservableCollection<LocationDetailsDropdown> _StoreDropDown;
        public ObservableCollection<LocationDetailsDropdown> StoreDropDown
        {
            get { return _StoreDropDown; }
            set
            {
                _StoreDropDown = value;
                OnPropertyChanged(nameof(StoreDropDown));
            }
        }
        private LocationDetailsDropdown _SelectedStore;
        public LocationDetailsDropdown SelectedStore
        {
            get { return _SelectedStore; }
            set
            {
                _SelectedStore = value;
                OnPropertyChanged(nameof(SelectedStore));
            }
        }

        private bool _IsAttendanceVisible;
        public bool IsAttendanceVisible
        {
            get { return _IsAttendanceVisible; }
            set
            {
                _IsAttendanceVisible = true;
                OnPropertyChanged("IsAttendanceVisible");
            }
        }

        private bool _IsHistoryVisible;
        public bool IsHistoryVisible
        {
            get { return _IsHistoryVisible; }
            set
            {
                _IsHistoryVisible = value;
                OnPropertyChanged("IsHistoryVisible");
            }
        }

        private bool _IsEnableCheckIn;
        public bool IsEnableCheckIn
        {
            get { return _IsEnableCheckIn; }
            set
            {
                _IsEnableCheckIn = value;
                OnPropertyChanged(nameof(IsEnableCheckIn));
            }
        }

        private bool _IsEnableCheckOut;
        public bool IsEnableCheckOut
        {
            get { return _IsEnableCheckOut; }
            set
            {
                _IsEnableCheckOut = value;
                OnPropertyChanged(nameof(IsEnableCheckOut));
            }
        }

        private ObservableCollection<LocationResponse> _Locations;
        public ObservableCollection<LocationResponse> Locations
        {
            get { return _Locations; }
            set
            {
                _Locations = value;
                OnPropertyChanged("Locations");
            }
        }

        private AttendanceRequest _CheckInCheckOut;
        public AttendanceRequest CheckInCheckOut
        {
            get { return _CheckInCheckOut; }
            set
            {
                _CheckInCheckOut = value;
                OnPropertyChanged("CheckInCheckOut");
            }
        }
        
        private ObservableCollection<AttendanceResponse> _obTodaysAttendanceHistory;
        public ObservableCollection<AttendanceResponse> obTodaysAttendanceHistory
        {
            get { return _obTodaysAttendanceHistory; }
            set
            {
                _obTodaysAttendanceHistory = value;
                OnPropertyChanged(nameof(obTodaysAttendanceHistory));
            }
        }

        private ObservableCollection<AttendanceResponse> _obTodaysAttendanceHistoryOffline;
        public ObservableCollection<AttendanceResponse> obTodaysAttendanceHistoryOffline
        {
            get { return _obTodaysAttendanceHistoryOffline; }
            set
            {
                _obTodaysAttendanceHistoryOffline = value;
                OnPropertyChanged(nameof(obTodaysAttendanceHistoryOffline));
            }
        }

        private List<AttendanceResponse> _AttendanceResponseHistory;
        public List<AttendanceResponse> lstAttendanceResponseHistory
        {
            get { return _AttendanceResponseHistory; }
            set
            {
                _AttendanceResponseHistory = value;
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

        private ObservableCollection<AttendanceResponse> _AttendanceResponse;
        public ObservableCollection<AttendanceResponse> AttendanceResponses
        {
            get { return _AttendanceResponse; }
            set
            {
                _AttendanceResponse = value;
                OnPropertyChanged("AttendanceResponse");
            }
        }

        private bool _IsDayOffVisible;
        public bool IsDayOffVisible
        {
            get { return _IsDayOffVisible; }
            set
            {
                _IsDayOffVisible = value;
                OnPropertyChanged("IsDayOffVisible");
            }
        }

        private bool _IsCheckinCheckoutVisible;
        public bool IsCheckinCheckoutVisible
        {
            get { return _IsCheckinCheckoutVisible; }
            set
            {
                _IsCheckinCheckoutVisible = value;
                OnPropertyChanged("IsCheckinCheckoutVisible");
            }
        }

        private bool _IsDayOffEnable;
        public bool IsDayOffEnable
        {
            get { return _IsDayOffEnable; }
            set
            {
                _IsDayOffEnable = value;
                OnPropertyChanged(nameof(IsDayOffEnable));
            }
        }


        private bool _IsDayOff;
        public bool IsDayOff
        {
            get { return _IsDayOff; }
            set
            {
                _IsDayOff = value;
                OnPropertyChanged("IsDayOff");
            }
        }

        private DateTime _SelectedDate;
        public DateTime SelectedDate
        {
            get { return _SelectedDate; }
            set
            {
                _SelectedDate = value;
                OnPropertyChanged("SelectedDate");
            }
        }

        private string _LocationName;
        public string LocationName
        {
            get { return _LocationName; }
            set
            {
                _LocationName = value;
                OnPropertyChanged("LocationName");
            }
        }

        private int _LocationId;
        public int LocationId
        {
            get { return _LocationId; }
            set
            {
                _LocationId = value;
                OnPropertyChanged("LocationId");
            }
        }

        private Nullable<DateTime> _CheckInTime;
        public Nullable<DateTime> CheckInTime
        {
            get { return _CheckInTime; }
            set
            {
                _CheckInTime = value;
                OnPropertyChanged("CheckInTime");
            }
        }

        private Nullable<DateTime> _CheckOutTime;
        public Nullable<DateTime> CheckOutTime
        {
            get { return _CheckOutTime; }
            set
            {
                _CheckOutTime = value;
                OnPropertyChanged("CheckOutTime");
            }
        }

        private Color _CheckInBackgroungColor;
        public Color CheckInBackgroungColor
        {
            get { return _CheckInBackgroungColor; }
            set
            {
                _CheckInBackgroungColor = value;
                OnPropertyChanged("CheckInBackgroungColor");
            }
        }

        private Color _CheckOutBackgroungColor;
        public Color CheckOutBackgroungColor
        {
            get { return _CheckOutBackgroungColor; }
            set
            {
                _CheckOutBackgroungColor = value;
                OnPropertyChanged("CheckOutBackgroungColor");
            }
        }

        private Color _IsDayoffBackgroungColor;
        public Color IsDayoffBackgroungColor
        {
            get { return _IsDayoffBackgroungColor; }
            set
            {
                _IsDayoffBackgroungColor = value;
                OnPropertyChanged(nameof(IsDayoffBackgroungColor));
            }
        }

        private Nullable<DateTime> _TodaysDate;
        public Nullable<DateTime> TodaysDate
        {
            get { return _TodaysDate; }
            set
            {
                _TodaysDate = value;
                OnPropertyChanged(nameof(TodaysDate));
            }
        }

        #endregion
    }
}

