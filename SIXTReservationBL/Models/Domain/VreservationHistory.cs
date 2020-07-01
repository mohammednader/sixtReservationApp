using System;
using System.Collections.Generic;

namespace SIXTReservationBL.Models.Domain
{
    public partial class VreservationHistory
    {
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
        public string Status { get; set; }
        public int? AssignFromUser { get; set; }
        public int? AssignToUser { get; set; }
        public DateTime? AssignCreationDate { get; set; }
        public string StepName { get; set; }
        public int? FormSubmitUserId { get; set; }
        public int? FormSubmitReasonId { get; set; }
        public bool? ReasonStatus { get; set; }
        public string Comment { get; set; }
        public DateTime? FormSubmitCreationDate { get; set; }
        public string FormSubmitReason { get; set; }
        public string AssignTo { get; set; }
        public string AssignFrom { get; set; }
        public string UserSubmit { get; set; }
        public int? CustomerFormModifiedBy { get; set; }
        public DateTime? CustomerFormModifiedDate { get; set; }
        public string CustomerFormEmail { get; set; }
        public string CustomerFormPhone { get; set; }
        public string CustomerFormComment { get; set; }
        public string UsrCustomerForm { get; set; }
    }
}
