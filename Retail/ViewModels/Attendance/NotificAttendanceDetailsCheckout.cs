using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Retail.Database;
using Retail.DependencyServices;
using Retail.Hepler;
using Retail.Infrastructure.Enums;
using Retail.Infrastructure.RequestModels;
using Retail.Infrastructure.ResponseModels;
using Retail.Infrastructure.ServiceLayer;
using Retail.Models;
using Xamarin.Forms;

namespace Retail.ViewModels.Attendance
{
    public class NotificAttendanceDetailsCheckout
    {
        Connection c;
        List<AttendanceResponse> lstAttendanceResponseHistory;
        INotificationManager notificationManager;

        public NotificAttendanceDetailsCheckout(INotificationManager _notificationManager)
        {
            c = new Connection();
            c.conn = DependencyService.Get<ISQLite>().GetConnection();
            //c.conn.CreateTable<TblPromoterAttendance>();
            lstAttendanceResponseHistory = new List<AttendanceResponse>();
            notificationManager = _notificationManager;

            //for promoter only
            List<int> lstRoleIds = CommonAttribute.CustomeProfile.PersonAssignedRoles.Select(x => x.PersonRoleId).ToList();
            if ((!lstRoleIds.Contains((int)PersonRoleEnum.Supervisor))
                && (!lstRoleIds.Contains((int)PersonRoleEnum.Manager)) && (!lstRoleIds.Contains((int)PersonRoleEnum.Admin)))
            {
                CheckinCheckoutoutValidateNotification();
            }
        }

        private async void CheckinCheckoutoutValidateNotification()
        {
            try
            {
                // for checkout
                bool resultCheckout = await CheckinCheckoutNotification((int)CheckinCheckoutStatus.Checkout);
                if (resultCheckout)
                {
                    string title = "Attendance";
                    string message = "You have not checked out today";
                    notificationManager.SendNotification(title, message);
                }

                // for checkin
                DateTime CheckinToday;
                if (Application.Current.Properties.ContainsKey("CheckinToday"))
                {
                    CheckinToday = Convert.ToDateTime(Application.Current.Properties["CheckinToday"].ToString());
                } else
                {
                    CheckinToday = DateTime.Today.AddDays(-1);
                }
                // do checkin everyday 1 time
                if (DateTime.Today != CheckinToday)
                {
                    Application.Current.Properties["CheckinToday"] = DateTime.Today;
                    bool resultCheckin = await CheckinCheckoutNotification((int)CheckinCheckoutStatus.Checkin);
                    if (resultCheckin)
                    {
                        string title = "Attendance";
                        string message = "You have not checked in today";
                        notificationManager.SendNotification(title, message);
                    }
                }
            }
            catch (Exception ex)
            {
                Debugger.Log(0, null, ex.ToString());
            }
        }

        private async Task<bool> CheckinCheckoutNotification(int checkinCheckoutStatus)
        {
            var result = false;

            try
            {
                int SelectedStoreID = (int)CommonAttribute.CustomeProfile?.Locations?.FirstOrDefault().LocationId;
                string TodaysDate = DateTime.Now.Date.ToString("yyyy-MM-dd");
                long PersonId = CommonAttribute.CustomeProfile.PersonId;
                long CountryId = CommonAttribute.CustomeProfile.CountryId;
                var Shifts = CommonAttribute.CustomeProfile?.Shifts?.ToList();

                if (SelectedStoreID > 0 && c.conn != null) //NetworkState = NetworkAccess.Internet
                {
                    AttendanceDataManagementSL attendanceDataManagementSL = new AttendanceDataManagementSL();
                    var lstAttendanceResponseHistory = await attendanceDataManagementSL.GetAttendanceDetailsByDateLocationPersonID(SelectedStoreID, (int)PersonId, TodaysDate, (int)CountryId);

                    AttendanceResponse obj = lstAttendanceResponseHistory.FirstOrDefault();
                    // checkout time
                    if (checkinCheckoutStatus == (int)CheckinCheckoutStatus.Checkout
                        && (obj != null && obj.CheckInDate != null) && obj?.IsOffDay != true)
                    {
                        var CheckInDate = (DateTime)obj.CheckInDate;
                        var CheckoutDate = obj.CheckoutDate;
                        var CountryDutyHours = (int)obj.CountryDutyHours;

                        if (((CheckInDate.AddHours(CountryDutyHours)) < DateTime.Now)
                            && CheckoutDate == null)
                        {
                            // not checked out
                            result = true;
                        }
                    }
                    //checkin time
                    else if (checkinCheckoutStatus == (int)CheckinCheckoutStatus.Checkin
                        && (obj == null || obj?.CheckInDate == null) && obj?.IsOffDay != true)
                    {
                        if (Shifts != null && Shifts.Count() > 0)
                        {
                            var shift = Shifts[0];
                            if (shift.ShiftStartTime.Ticks < DateTime.Now.Ticks)
                            {
                                // not checked in
                                result = true;
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Debugger.Log(0, null, ex.ToString());
            }

            return result;
        }
    }

    enum CheckinCheckoutStatus
    {
        Checkin = 1,
        Checkout = 2
    }
}
