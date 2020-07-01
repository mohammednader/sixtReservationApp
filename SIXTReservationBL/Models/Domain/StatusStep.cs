using System;
using System.Collections.Generic;

namespace SIXTReservationBL.Models.Domain
{
    public partial class StatusStep
    {
        public StatusStep()
        {
            ActionSetting = new HashSet<ActionSetting>();
            NotificationSetting = new HashSet<NotificationSetting>();
            Question = new HashSet<Question>();
            ReservationStepsLog = new HashSet<ReservationStepsLog>();
        }

        public int StepId { get; set; }
        public string StepName { get; set; }
        public int? ReseravationStatus { get; set; }
        public int? StepOrder { get; set; }
        public bool? IsNotification { get; set; }
        public bool? IsLast { get; set; }

        public virtual ReservationStatus ReseravationStatusNavigation { get; set; }
        public virtual ICollection<ActionSetting> ActionSetting { get; set; }
        public virtual ICollection<NotificationSetting> NotificationSetting { get; set; }
        public virtual ICollection<Question> Question { get; set; }
        public virtual ICollection<ReservationStepsLog> ReservationStepsLog { get; set; }
    }
}
