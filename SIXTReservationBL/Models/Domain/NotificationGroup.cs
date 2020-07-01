using System;
using System.Collections.Generic;

namespace SIXTReservationBL.Models.Domain
{
    public partial class NotificationGroup
    {
        public NotificationGroup()
        {
            Notification = new HashSet<Notification>();
        }

        public int Id { get; set; }
        public string GroupName { get; set; }
        public int? Order { get; set; }
        public int? ActionStep { get; set; }
        public string NotificationUrl { get; set; }

        public virtual ICollection<Notification> Notification { get; set; }
    }
}
