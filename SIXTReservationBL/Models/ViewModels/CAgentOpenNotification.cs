using System;
using System.Collections.Generic;
using System.Text;

namespace SIXTReservationBL.Models.ViewModels
{
    public class CAgentOpenNotification
    {
        public string ReservationId { get; set; }
        public int ReservationLogId { get; set; }
        public int LoggedUser { get; set; }
    }
}
