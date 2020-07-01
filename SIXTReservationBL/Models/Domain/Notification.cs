using System;
using System.Collections.Generic;

namespace SIXTReservationBL.Models.Domain
{
    public partial class Notification
    {
        public int Id { get; set; }
        public string NotificationText { get; set; }
        public int? ToUser { get; set; }
        public bool? IsSeen { get; set; }
        public DateTime? SeenDate { get; set; }
        public long? ReservationNo { get; set; }
        public int? ReservationLogId { get; set; }
        public bool? Status { get; set; }
        public int? ReservationStatusId { get; set; }
        public DateTime? CreateDate { get; set; }
        public string UrlNotification { get; set; }
        public int? NotificationType { get; set; }
        public int? GroupId { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual NotificationGroup Group { get; set; }
        public virtual ReservationStepsLog ReservationLog { get; set; }
        public virtual AppUser ToUserNavigation { get; set; }
    }
}
