using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Retail.Database;
using Retail.DependencyServices;
using Retail.Hepler;
using Retail.Models;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Retail.ViewModels.Dashboard
{
    public class EventModel : ContentView
    {
        public EventCollection Events { get; set; }
        DateTime CurrentDateTime;
        public ICommand DayTappedCommand => new Command<DateTime>(async (date) => DayTapped(date));
        Connection c = new Connection();
        public EventModel()
        {
            //c.conn = DependencyService.Get<ISQLite>().GetConnection();
            Events = new EventCollection
            {
                [DateTime.Now] = new List<_EventModel>
                {
                    //new _EventModel{Name="Cool event1",Description="This is Cool event1's description!"},
                    //new _EventModel { Name = "Cool event2", Description = "This is Cool event2's description!" }
                },
            };

            for (int i = 1; i < 3; i++)
            {
                AttendanceHistory.Add(new AttendanceHistory
                {
                    StoreName = "Panasonic Store 1- lulu",
                    HistoryCheckInTime = "08:00 AM",
                    HistoryCheckOutTime = "04:30 PM"

                });
            }

            IsAttendanceVisible = true;
        }

        private void DayTapped(DateTime date)
        {
            if(date.ToString("dd/MM/yyyy") == DateTime.Now.ToString("dd/MM/yyyy"))
            {
                IsAttendanceVisible = true;
                IsHistoryVisible = false;
            }
            else
            {
                IsAttendanceVisible = false;
                IsHistoryVisible = true;
            }
        }

        public ObservableCollection<AttendanceHistory> AttendanceHistory { get; set; } =
            new ObservableCollection<AttendanceHistory>();

        private Boolean _IsAttendanceVisible;
        public Boolean IsAttendanceVisible
        {
            get { return _IsAttendanceVisible; }
            set
            {
                _IsAttendanceVisible = true;
                OnPropertyChanged("IsAttendanceVisible");
            }
        }

        private Boolean _IsHistoryVisible;
        public Boolean IsHistoryVisible
        {
            get { return _IsHistoryVisible; }
            set
            {
                _IsHistoryVisible = value;
                OnPropertyChanged("IsHistoryVisible");
            }
        }

        private Boolean _IsEnableCheckIn;
        public Boolean IsEnableCheckIn
        {
            get { return _IsEnableCheckIn; }
            set
            {
                _IsEnableCheckIn = value;
                OnPropertyChanged("IsEnableCheckIn");
            }
        }

        private Boolean _IsEnableCheckOut;
        public Boolean IsEnableCheckOut
        {
            get { return _IsEnableCheckOut; }
            set
            {
                _IsEnableCheckOut = value;
                OnPropertyChanged("IsEnableCheckOut");
            }
        }

        public Command CheckinCommand
        {
            get
            {
                return new Command(() =>
                {
                    if (CheckInTime == string.Empty || CheckInTime == null)
                    {
                        CheckInTime = DateTime.Now.ToString("HH:mm");
                        CheckInBackgroungColor = Color.LightGray;

                        
                        //PromoterAttendance tbl = new PromoterAttendance();
                        //tbl.CheckInDate = CheckInTime;
                        //tbl.StoreName = "Panasonic Store 1";
                        //tbl.Latitude = CommonAttribute.UserLocation.Latitude;
                        //tbl.Longitude = CommonAttribute.UserLocation.Longitude;
                        //tbl.AttendanceDate = DateTime.Now;
                        
                        //c.conn.Insert(tbl);
                    }
                    else
                    {
                        IsEnableCheckIn = false;
                        CheckInBackgroungColor = Color.FromHex("#E62E75");
                    }
                });
            }
        }
        public Command CheckoutCommand
        {
            get
            {
                return new Command(() =>
                {
                    if (CheckOutTime == string.Empty || CheckOutTime == null)
                    {
                        CheckOutTime = DateTime.Now.ToString("HH:mm");
                        CheckOutBackgroungColor = Color.LightGray;
                        //var data = c.conn.Table<PromoterAttendance>();
                        //var data1 = (from values in data
                        //             where values.AttendanceDate == DateTime.Now && values.CheckInTime!=string.Empty
                        //             select new PromoterAttendance
                        //             {
                        //                 StudentName = values.StudentName,
                        //                 NickName = values.NickName,
                        //                 Dept = values.Dept,
                        //                 Place = values.Place
                        //             }).ToList<PromoterAttendance>();


                    }
                    else
                    {
                        IsEnableCheckOut = false;
                        CheckOutBackgroungColor = Color.FromHex("#1E55A5");
                    }
                });
            }
        }

        private string _CheckInTime;
        public string CheckInTime
        {
            get { return _CheckInTime; }
            set
            {
                _CheckInTime = value;
                OnPropertyChanged("CheckInTime");
            }
        }

        private string _CheckOutTime;
        public string CheckOutTime
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
    }
}

public class _EventModel
{
    public string Name { get; set; }
    public string Description { get; set; }
    
}

public class Stores
{
    public string StoreName {get;set;}
}

public class AttendanceHistory
{
    public string StoreName { get; set; }
    public string HistoryCheckInTime { get; set; }
    public string HistoryCheckOutTime { get; set; }    
}