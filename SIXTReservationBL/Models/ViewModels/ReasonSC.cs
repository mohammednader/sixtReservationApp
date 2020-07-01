using System;
using System.Collections.Generic;
using System.Text;

namespace SIXTReservationBL.Models.ViewModels
{
   public class ReasonSC
    {
        public string Reason { get; set; }
        public int? ReservationStatus { get; set; }
        public bool? Status { get; set; }
        public bool? IsActive { get; set; }
        public int IsAnswer { get; set; }
    }
}
