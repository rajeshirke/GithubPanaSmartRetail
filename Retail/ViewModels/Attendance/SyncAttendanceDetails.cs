using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Retail.Database;
using Retail.DependencyServices;
using Retail.Hepler;
using Retail.Infrastructure.Enums;
using Retail.Infrastructure.RequestModels;
using Retail.Infrastructure.ServiceLayer;
using Retail.Models;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Retail.ViewModels.Attendance
{
    public class SyncAttendanceDetails
    {
        Connection c;
        AttendanceRequest CheckInCheckOut;
        public SyncAttendanceDetails()
        {
            c = new Connection();
            c.conn = DependencyService.Get<ISQLite>().GetConnection();
            c.conn.CreateTable<TblPromoterAttendance>();
            CheckInCheckOut = new AttendanceRequest();
        }

        // send to server
        public async Task<bool> AttendanceSyncing()
        {
            bool IsBusy = false;

            try
            {
                IsBusy = true;
                if (CommonAttribute.CustomeProfile == null)
                    return false;

                int SelectedStoreID = 0;//(int)CommonAttribute.CustomeProfile?.Locations?.FirstOrDefault().LocationId;
                var Stores = CommonAttribute.CustomeProfile?.Locations.ToList();
                if(Stores!=null && Stores.Count>0)
                {
                    foreach (var item in Stores)
                    {
                        SelectedStoreID = item.LocationId;
                        if (SelectedStoreID != 0)
                        {
                            string TodaysDate = DateTime.Now.Date.ToString("yyyy-MM-dd");
                            long PersonId = CommonAttribute.CustomeProfile.PersonId;

                            if (SelectedStoreID > 0 && c.conn != null) //NetworkState = NetworkAccess.Internet
                            {
                                var AttendanceEntry = c.conn.Query<TblPromoterAttendance>("Select * From TblPromoterAttendance Where AttendanceDate='" + TodaysDate + "' AND LocationId='" + SelectedStoreID + "' AND PersonId = '" + PersonId + "'");
                                if (AttendanceEntry != null && AttendanceEntry.Count != 0)
                                {
                                    CheckInCheckOut = new AttendanceRequest();
                                    CheckInCheckOut.AttendanceDate = Convert.ToDateTime(AttendanceEntry[0].AttendanceDate);
                                    CheckInCheckOut.CheckInDate = AttendanceEntry[0].CheckInDate;
                                    CheckInCheckOut.CheckoutDate = AttendanceEntry[0].CheckoutDate;
                                    CheckInCheckOut.LocationId = AttendanceEntry[0].LocationId;
                                    CheckInCheckOut.PersonId = AttendanceEntry[0].PersonId;
                                    CheckInCheckOut.ProximityRange = AttendanceEntry[0].ProximityRange;
                                    CheckInCheckOut.IsOffDay = AttendanceEntry[0].IsOffDay;
                                    CheckInCheckOut.AttendanceId = AttendanceEntry[0].AttendanceId;
                                    CheckInCheckOut.PersonLocationLatitude = AttendanceEntry[0].PersonLocationLatitude;
                                    CheckInCheckOut.PersonLocationLongitude = AttendanceEntry[0].PersonLocationLongitude;
                                    CheckInCheckOut.OnlineOffLineAttendanceStatus = (int)OnlineOffLineAttendanceStatusEnum.Offline;

                                    AttendanceDataManagementSL attendanceDataManagementSL = new AttendanceDataManagementSL();
                                    var response = await attendanceDataManagementSL.AttendanceCreateUpdateAttendance(CheckInCheckOut);
                                    if (response.Status == (int)APIResponseEnum.Success)
                                    {
                                        if (AttendanceEntry[0].CheckoutDate != null && AttendanceEntry[0].IsOffDay == false)
                                        {
                                            DeleteAllEntriesBasedDateStore(SelectedStoreID, TodaysDate, PersonId);
                                        }
                                        else if (AttendanceEntry[0].IsOffDay == true)
                                        {
                                            DeleteAllEntriesBasedDateStore(SelectedStoreID, TodaysDate, PersonId);
                                        }
                                    }
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

            return IsBusy;
        }

        //DELETE BASED ON DATE AND STORE
        public string DeleteAllEntriesBasedDateStore(Int32 LocationId, string AttendanceDate,long PersonId)
        {
            c.conn.Table<TblPromoterAttendance>().Delete(p => p.LocationId == LocationId && p.AttendanceDate == AttendanceDate && p.PersonId == PersonId);
            return "Success";
        }
    }
}
