using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIXTReservationApp.Models.CByCustomerReservation
{
    public class UserFormSubmit
    {
        public bool? AnswerStatus { get; set; }
        public string ReasonText { get; set; }
        public string Comment { get; set; }
        public string User { get; set; }
        public string Date { get; set; }
        public bool? IsConfirmed { get; set; }

        public UserFormSubmit(SIXTReservationBL.Models.Domain.ReservationFormSubmit r)
        {
            AnswerStatus = r.ReasonStatus;
            ReasonText = r.Reason?.ReasonText??"";
            Comment = r.Comment??"";
            User = r.CreatedUserNavigation?.FullName;
            Date = r.CreationDate.ToString()??"";
            IsConfirmed = r.IsOpenConfirm;

        }

    }
}
