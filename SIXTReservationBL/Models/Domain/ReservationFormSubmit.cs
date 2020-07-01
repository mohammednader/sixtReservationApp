using System;
using System.Collections.Generic;

namespace SIXTReservationBL.Models.Domain
{
    public partial class ReservationFormSubmit
    {
        public int Id { get; set; }
        public int? CreatedUser { get; set; }
        public int? ReasonId { get; set; }
        public bool? ReasonStatus { get; set; }
        public string Comment { get; set; }
        public DateTime? CreationDate { get; set; }
        public long? ReservationNo { get; set; }
        public int? ReservationLogId { get; set; }
        public bool? IsOpenConfirm { get; set; }

        public virtual AppUser CreatedUserNavigation { get; set; }
        public virtual Reason Reason { get; set; }
    }
}
