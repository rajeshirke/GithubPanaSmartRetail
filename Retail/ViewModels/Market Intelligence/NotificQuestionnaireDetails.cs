using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Retail.DependencyServices;
using Retail.Hepler;
using Retail.Infrastructure.Enums;
using Retail.Infrastructure.RequestModels;
using Retail.Infrastructure.ServiceLayer;
using Shiny;
using Xamarin.Forms;

namespace Retail.ViewModels.MarketIntelligence
{
    public class NotificQuestionnaireDetails
    {
        const string AppOpened = "AppOpened";
        INotificationManager notificationManager;

        public NotificQuestionnaireDetails(INotificationManager _notificationManager)
        {
            notificationManager = _notificationManager;
            QuestionnairExpNotification();
        }

        private async void QuestionnairExpNotification()
        {
            try
            {
                if (Application.Current.Properties.ContainsKey(AppOpened))
                {
                    DateTime AppOpenedDateTimeVal = Convert.ToDateTime(Application.Current.Properties[AppOpened].ToString());
                    if (AppOpenedDateTimeVal != DateTime.Today)
                    {
                        Application.Current.Properties[AppOpened] = DateTime.Today;

                        bool result = await CheckQuestionnaireForAlert();
                        if (result)
                        {
                            string title = "Questionnair";
                            string message = "You have pending tasks on questionnair";
                            notificationManager.SendNotification(title, message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debugger.Log(0, null, ex.ToString());
            }
        }

        public async Task<bool> CheckQuestionnaireForAlert()
        {
            bool result = false;
            int CountryId = (int)CommonAttribute.CustomeProfile.CountryId; //1;
            int PersonId = (int)CommonAttribute.CustomeProfile.PersonId; //1;
            MarketIntelDataManagementSL visitsDataManagementSL = new MarketIntelDataManagementSL();
            APIResponse aPIResponse = await visitsDataManagementSL.CheckQuestionnaireForAlert(CountryId, PersonId);
            if (aPIResponse != null)
            {
                if (aPIResponse.Status == (int)APIResponseEnum.Success)
                {
                    result = true;
                }
            }

            return result;
        }

    }
}
