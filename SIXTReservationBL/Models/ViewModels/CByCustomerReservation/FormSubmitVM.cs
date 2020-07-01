using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIXTReservationBL.Models
{
    public class FormSubmitVM
    {   
        public string ReservationId { get; set; }
        public bool ReasonStatus { get; set; }
        public int ReasonId { get; set; }
        public string Comment { get; set; }
    }
}
