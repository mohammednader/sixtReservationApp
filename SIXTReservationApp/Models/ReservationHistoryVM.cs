using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIXTReservationApp.Models
{
    public class ReservationHistoryVM
    {
        public long? ReservationNo { get; set; }
        public string Status { get; set; }
        public string StepName { get; set; }
        public int? FormSubmitUserId { get; set; }
        public DateTime? FormSubmitCreationDate { get; set; }
        public string FormSubmitReason { get; set; }
        public string UserSubmit { get; set; }
        public string AssignFrom { get; set; }
        public string AssignTo { get; set; }
        public DateTime? AssignCreationDate { get; set; }
        public string Comment { get; set; }
        public int? FormCustomerModifiedBy { get; set; }
        public string FormCustomerEmail { get; set; }
        public string FormCustomerPhone { get; set; }
        public string FormCustomerComment { get; set; }

        public string FormCustomerModifiedByName { get; set; }
        public DateTime? FormCustomerCreationDate { get; set; }



        public ReservationHistoryVM(SIXTReservationBL.Models.Domain.VreservationHistory r)
        {
            ReservationNo = r.ReservationNo;
            Status = r.Status;
            StepName = r.StepName;
            FormSubmitUserId = r.FormSubmitUserId;
            FormSubmitCreationDate = r.FormSubmitCreationDate;
            FormSubmitReason = r.FormSubmitReason ?? string.Empty;
            UserSubmit = r.UserSubmit ?? string.Empty;
            AssignFrom = r.AssignFrom ?? string.Empty;
            AssignTo = r.AssignTo??string.Empty;
            AssignCreationDate = r.AssignCreationDate;
            Comment = r.Comment ?? string.Empty;
            FormCustomerModifiedBy = r.CustomerFormModifiedBy;
            FormCustomerEmail = r.CustomerFormEmail ?? String.Empty;
            FormCustomerPhone = r.CustomerFormPhone ?? String.Empty;
            FormCustomerComment = r.CustomerFormComment ?? string.Empty;
            FormCustomerModifiedByName = r.UsrCustomerForm ?? String.Empty;
            FormCustomerCreationDate = r.CustomerFormModifiedDate;
        }
    }

}
