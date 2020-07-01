using System;
using System.Collections.Generic;

namespace SIXTReservationBL.Models.Domain
{
    public partial class Reason
    {
        public Reason()
        {
            ReservationFormSubmit = new HashSet<ReservationFormSubmit>();
        }

        public int Id { get; set; }
        public string ReasonText { get; set; }
        public bool? IsAnswer { get; set; }
        public bool? IsActive { get; set; }
        public int? ReservationStatusId { get; set; }

        public virtual ReservationStatus ReservationStatus { get; set; }
        public virtual ICollection<ReservationFormSubmit> ReservationFormSubmit { get; set; }
    }
}
