using System;
using System.Collections.Generic;

namespace SIXTReservationBL.Models.Domain
{
    public partial class LnkNotificationJobTitle
    {
        public int NotificationId { get; set; }
        public int JobTitleId { get; set; }

        public virtual JobTitle JobTitle { get; set; }
        public virtual NotificationSetting Notification { get; set; }
    }
}
