using System;
using System.Collections.Generic;

namespace SIXTReservationBL.Models.Domain
{
    public partial class VreservationLog
    {
        public Guid? UniqueId { get; set; }
        public long ReservationNum { get; set; }
        public int? StatusId { get; set; }
        public DateTime? BookingDate { get; set; }
        public DateTime? PickUpDate { get; set; }
        public DateTime? DropOffDate { get; set; }
        public DateTime? CancelledDate { get; set; }
        public int LogId { get; set; }
        public long? ReservationNo { get; set; }
        public int? StepId { get; set; }
        public bool? IsDone { get; set; }
        public int? ReservationStatus { get; set; }
        public DateTime? CreationDate { get; set; }
        public int? ReservationId { get; set; }
        public int? ReservationStatusId { get; set; }
        public DateTime? CompleteDate { get; set; }
        public string StatusTitle { get; set; }
        public int? AssignmentId { get; set; }
        public int? AssignFromUser { get; set; }
        public int? AssignToUser { get; set; }
        public DateTime? AssignCreationDate { get; set; }
        public long? AssignReservationNo { get; set; }
        public int? AssignLogId { get; set; }
        public int? DiffDateNotifyAssign { get; set; }
        public int? DiffDateAssignSubmit { get; set; }
        public int? DiffCurrPickUpDate { get; set; }
    }
}
