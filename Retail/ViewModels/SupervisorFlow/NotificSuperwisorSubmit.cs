using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Retail.Database;
using Retail.Database.SupervisorWorkflow;
using Retail.DependencyServices;
using Retail.Hepler;
using Retail.Infrastructure.Enums;
using Retail.Infrastructure.RequestModels;
using Retail.Infrastructure.ResponseModels;
using Retail.Infrastructure.ServiceLayer;
using Retail.Models;
using Xamarin.Forms;


namespace Retail.ViewModels.SupervisorFlow
{
    public class NotificSuperwisorSubmit
    {

        Connection c;
        VisitScheduleDb supVisitScheduleDb;
        INotificationManager notificationManager;

        public NotificSuperwisorSubmit(INotificationManager _notificationManager)
        {
            c = new Connection();
            c.conn = DependencyService.Get<ISQLite>().GetConnection();
            supVisitScheduleDb = new VisitScheduleDb();
            notificationManager = _notificationManager;

            CheckUserFinallySubmitterorNot();
        }

        private void CheckUserFinallySubmitterorNot()
        {
            try
            {
                // for submit notification
                DateTime SupervisorflowSubmitToday;
                if (Application.Current.Properties.ContainsKey("SupervisorflowSubmitToday"))
                {
                    SupervisorflowSubmitToday = Convert.ToDateTime(Application.Current.Properties["SupervisorflowSubmitToday"].ToString());
                }
                else
                {
                    SupervisorflowSubmitToday = DateTime.Today.AddDays(-1);
                }
                // do check everyday 1 time
                if (DateTime.Today != SupervisorflowSubmitToday)
                {
                    Application.Current.Properties["SupervisorflowSubmitToday"] = DateTime.Today;

                    var visitSchedules = supVisitScheduleDb.GetvisitScheduleByVisitScheduleStatusId((int)TaskStatusEnum.Open).ToList();
                    if (visitSchedules != null && visitSchedules.Count() > 0)
                    {
                        var Shifts = CommonAttribute.CustomeProfile?.Shifts?.ToList();
                        if (Shifts != null && Shifts.Count() > 0)
                        {
                            var shift = Shifts[0];
                            if (shift.ShiftEndTime.Ticks < DateTime.Now.Ticks)
                            {
                                string title = "Visits";
                                string message = "You have not submitted todays visits";
                                notificationManager.SendNotification(title, message);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debugger.Log(0, null, ex.ToString());
            }
        }

    }
}
