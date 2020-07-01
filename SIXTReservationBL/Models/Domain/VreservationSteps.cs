using System;
using System.Collections.Generic;

namespace SIXTReservationBL.Models.Domain
{
    public partial class VreservationSteps
    {
        public long ReservationNum { get; set; }
        public int? ReservationStatusId { get; set; }
        public DateTime? BookingDate { get; set; }
        public int? StepId { get; set; }
        public bool? IsDone { get; set; }
        public string StepName { get; set; }
        public int? LogId { get; set; }
        public long? LogReservationNo { get; set; }
        public int? LogStatusId { get; set; }
        public DateTime? LogCreationDate { get; set; }
        public DateTime? CompleteDate { get; set; }
        public string LogStatusName { get; set; }
        public int? Id { get; set; }
        public int? FromUser { get; set; }
        public int? ToUser { get; set; }
        public DateTime? AssignCreationDate { get; set; }
        public string AssignToName { get; set; }
        public string AssignFromName { get; set; }
        public int? CreatedUser { get; set; }
        public string FormSubmitCreatedByName { get; set; }
        public int? ReasonId { get; set; }
        public string ReasonText { get; set; }
        public string Comment { get; set; }
        public DateTime? FormSubmitCreationDate { get; set; }
        public string ReasonStatus { get; set; }
    }
}
