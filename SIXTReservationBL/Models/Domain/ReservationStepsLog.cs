using System;
using System.Collections.Generic;

namespace SIXTReservationBL.Models.Domain
{
    public partial class ReservationStepsLog
    {
        public ReservationStepsLog()
        {
            Notification = new HashSet<Notification>();
            ReservationAssignement = new HashSet<ReservationAssignement>();
        }

        public int Id { get; set; }
        public long? ReservationNo { get; set; }
        public int? StepId { get; set; }
        public bool? IsDone { get; set; }
        public string TableName { get; set; }
        public int? TableId { get; set; }
        public int? ReservationStatus { get; set; }
        public DateTime? CreationDate { get; set; }
        public int? ReservationId { get; set; }
        public int? ReservationStatusId { get; set; }
        public DateTime? Completedate { get; set; }

        public virtual StatusStep Step { get; set; }
        public virtual ICollection<Notification> Notification { get; set; }
        public virtual ICollection<ReservationAssignement> ReservationAssignement { get; set; }
    }
}
