using System;
using System.Collections.Generic;

namespace SIXTReservationBL.Models.Domain
{
    public partial class NotificationSetting
    {
        public NotificationSetting()
        {
            LnkNotificationJobTitle = new HashSet<LnkNotificationJobTitle>();
        }

        public int Id { get; set; }
        public int? ActionStep { get; set; }
        public bool? IsDisabled { get; set; }
        public int? ReservationStatusId { get; set; }

        public virtual StatusStep ActionStepNavigation { get; set; }
        public virtual ReservationStatus ReservationStatus { get; set; }
        public virtual ICollection<LnkNotificationJobTitle> LnkNotificationJobTitle { get; set; }
    }
}
