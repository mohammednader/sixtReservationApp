using System;
using System.Collections.Generic;
using System.Text;

namespace SIXTReservationBL.Models.ViewModels
{
   public class FormActionVM
    {
        public string ReservationId { get; set; }
        public bool? ReasonStatus { get; set; }
        public int ReasonId { get; set; }
        public int CreatedBy { get; set; }
        public bool IsAnswer { get; set; }
        public DateTime ActionDate { get; set; } 
        public string Comment { get; set; }
        public int LoggedUser { get; set; }
        public int LoggedStep { get; set; }
        public bool IsOpenConfirm { get; set; }
        public bool IsDeny { get; set; }
        public int CancelBy { get; set; }
    }
}
