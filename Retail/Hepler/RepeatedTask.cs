using System;
using System.Threading;
using System.Threading.Tasks;
using Shiny;
using Shiny.Jobs;
using Shiny.Notifications;
using Xamarin.Forms;

namespace Retail.Hepler
{
    public class RepeatedTask : IJob
    {
        public bool NetworkAvailable { get { return Retail.Hepler.Extensions.CheckNetwrokAvailability(); } }
        readonly INotificationManager notificationManager;

        public RepeatedTask(INotificationManager _notificationManager)
        {
            this.notificationManager = _notificationManager;
        }

        async Task IJob.Run(JobInfo jobInfo, CancellationToken cancelToken)
        {
            //shiny notification
            //await SendNotificationNow("sample ", "sample123");
            //await ScheduleLocalNotification(DateTimeOffset.UtcNow.AddSeconds(2), "sample ", "sample123");


            if (CommonAttribute.CustomeProfile == null ||
                CommonAttribute.CustomeProfile?.PersonId == 0)
                return;

            // run background tasks
            BackgroundTask backgroundTask = new BackgroundTask();
            backgroundTask.BackgroundTasks();

        }

        // shiny notifications now.
        async Task SendNotificationNow(string _title, string _message)
        {
            var notification = new Shiny.Notifications.Notification
            {
                Title = _title,
                Message = _message,
            };

            var accessStatus = await ShinyHost.Resolve<Shiny.Notifications.INotificationManager>().RequestAccess();
            if (accessStatus is AccessState.Available)
                await ShinyHost.Resolve<Shiny.Notifications.INotificationManager>().Send(notification);
        }

        // shiny  notifications timely.
        async Task ScheduleLocalNotification(DateTimeOffset scheduleDate, string _title, string _message)
        {
            var notification = new Shiny.Notifications.Notification
            {
                Title = _title,
                Message = _message,
                ScheduleDate = scheduleDate,
            };

            var accessStatus = await ShinyHost.Resolve<Shiny.Notifications.INotificationManager>().RequestAccess();
            if (accessStatus is AccessState.Available)
                await ShinyHost.Resolve<Shiny.Notifications.INotificationManager>().Send(notification);
        }

    }

    public class BackgroundTask
    {
        public bool NetworkAvailable { get { return Retail.Hepler.Extensions.CheckNetwrokAvailability(); } }
        ISyncOfflineHandler _SyncOfflineHandler;
        INotificationHandler _NotificationHandler;
        IAlertHandler _AlertHandler;

        public BackgroundTask()
        {

        }

        public void BackgroundClearings()
        {
            Application.Current.Properties.Remove("SyncDateTime");
        }

        public void BackgroundTasks()
        {
            if (NetworkAvailable)
            {
                // for notification
                _NotificationHandler = new NotificationHandler();
                _NotificationHandler.SendNotification();

                // for syncing
                #region syncing
                DateTime SyncDateTime;
                DateTime CurrentDateTime = DateTime.Now;
                if (Application.Current.Properties.ContainsKey("SyncDateTime"))
                {
                    SyncDateTime = Convert.ToDateTime(Application.Current.Properties["SyncDateTime"].ToString());
                    //BtnSubmitSupWorkflowOffline = SupWorkflowOffline;
                } else
                {
                    Application.Current.Properties["SyncDateTime"] = CurrentDateTime.AddMinutes(-500);
                    SyncDateTime = Convert.ToDateTime(Application.Current.Properties["SyncDateTime"].ToString());
                }
                // sync after some hours.
                //480 minutes(8 hours) was the previous synch time.
                //DateTime SyncDateTimeForTesting = SyncDateTime.AddMinutes(480);
                if (SyncDateTime.AddMinutes(60) <= CurrentDateTime)
                {
                    Application.Current.Properties["SyncDateTime"] = CurrentDateTime;
                    _SyncOfflineHandler = new SyncOfflineHandler();
                    _SyncOfflineHandler.SyncOfflineDatas();
                }
                #endregion

                //for alert
                _AlertHandler = new AlertHandler();
            }
        }

    }


}
