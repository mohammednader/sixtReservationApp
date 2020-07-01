using SIXTReservationBL.Models.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SIXTReservationApp.Models.Reason
{
    public class ReasonVM
    {
        public int Id { get; set; }
       
        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is required")]
        public string Reason { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is required")]
        public int? ReservationStatusId { get; set; }
        public string ReservationStatus { get; set; }
        public bool? Status { get; set; }
        public bool? IsActive { get; set; }
        public int? IsAnswer { get; set; }

        public ReasonVM()
        {

        }
        public ReasonVM(SIXTReservationBL.Models.Domain.Reason R)
        {
            Id = R.Id;
            Reason = R.ReasonText;
            ReservationStatus = R.ReservationStatus?.Status.ToString();
            ReservationStatusId = R.ReservationStatusId;
            Status = R.IsAnswer;
            IsActive = R.IsActive;

        }
    }
}
