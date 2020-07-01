using System;
using System.Collections.Generic;

namespace SIXTReservationBL.Models.Domain
{
    public partial class ReservationAssignement
    {
        public int Id { get; set; }
        public int? FromUser { get; set; }
        public int? ToUser { get; set; }
        public DateTime? CreationDate { get; set; }
        public long? ReservationNo { get; set; }
        public int? ReservationLogId { get; set; }

        public virtual AppUser FromUserNavigation { get; set; }
        public virtual ReservationStepsLog ReservationLog { get; set; }
        public virtual AppUser ToUserNavigation { get; set; }
    }
}
