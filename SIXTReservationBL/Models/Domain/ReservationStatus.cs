using System;
using System.Collections.Generic;

namespace SIXTReservationBL.Models.Domain
{
    public partial class ReservationStatus
    {
        public ReservationStatus()
        {
            ActionSetting = new HashSet<ActionSetting>();
            NotificationSetting = new HashSet<NotificationSetting>();
            Question = new HashSet<Question>();
            Reason = new HashSet<Reason>();
            Reservation = new HashSet<Reservation>();
            StatusStep = new HashSet<StatusStep>();
        }

        public int Id { get; set; }
        public string Status { get; set; }

        public virtual ICollection<ActionSetting> ActionSetting { get; set; }
        public virtual ICollection<NotificationSetting> NotificationSetting { get; set; }
        public virtual ICollection<Question> Question { get; set; }
        public virtual ICollection<Reason> Reason { get; set; }
        public virtual ICollection<Reservation> Reservation { get; set; }
        public virtual ICollection<StatusStep> StatusStep { get; set; }
    }
}
