using System;
namespace Retail.Infrastructure.ResponseModels
{
    public class PersonNotificationResponse
    {
        public int NotificationId { get; set; }
        public long PersonId { get; set; }
        public string MessageSubject { get; set; }
        public string NotificationContent { get; set; }
        public bool? IsNotificationRead { get; set; }
        public int NotificationEventTypeId { get; set; }
        public string NotificationEventTypeName { get; set; }

        public string StatusColor
        {
            get
            {
                if (Convert.ToBoolean(IsNotificationRead))
                    return "white";
                else
                    return "#5a60c9";

            }
        }

        public DateTime? NotificationReadDate { get; set; }
    }
}
