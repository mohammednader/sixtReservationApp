using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIXTReservationApp.Models.CByCustomerReservation
{
    public class FormSubmitVM
    {   
        public string ReservationId { get; set; }
        public bool ReasonStatus { get; set; }
        public int ReasonId { get; set; }
        public string Comment { get; set; }
        public bool IsOpenConfirm { get; set; }
        public int CancelBy { get; set; }
    }
}
