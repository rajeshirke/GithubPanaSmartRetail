using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Retail.Hepler;
using Retail.Infrastructure.ResponseModels;
using Retail.Infrastructure.ServiceLayer;
using Xamarin.Forms;

namespace Retail.ViewModels.Dashboard
{
    public class NotificationsViewModel : BaseViewModel
    {
        public NotificationsViewModel(INavigation navigation) : base(navigation)
        {
            IsBusy = true;
            Device.BeginInvokeOnMainThread(async () => {
                await BindingData();
                IsBusy = false;
            });
        }
        public async Task BindingData()
        {
            UserManagementSL userManagementSL = new UserManagementSL();
            List<PersonNotificationResponse> personNotifications = await userManagementSL.GetUnreadNotifications(CommonAttribute.CustomeProfile.PersonId);
            if (personNotifications != null)
            {
                //if (Notifications == null)
                //    Notifications = new ObservableCollection<PersonNotificationResponse>();

                Notifications = new ObservableCollection<PersonNotificationResponse>(personNotifications);
            }
        }
        public Command SelectedItemCommand
        {
            get
            {
                //return new Command(async (item) =>
                return new Command<PersonNotificationResponse>(async (item) =>
                {
                    IsBusy = true;
                    UserManagementSL userManagementSL = new UserManagementSL();
                    var personNotifications = await userManagementSL.UpdateNotificationReadDate(CommonAttribute.CustomeProfile.PersonId, item.NotificationId);
                    if (personNotifications)
                    {
                        await BindingData();
                    }
                    IsBusy = false;
                });
            }
        }

        private ObservableCollection<PersonNotificationResponse> _Notifications;
        public ObservableCollection<PersonNotificationResponse> Notifications
        {
            get { return _Notifications; }
            set
            {
                _Notifications = value;
                OnPropertyChanged(nameof(Notifications));
            }
        }
    }
}

