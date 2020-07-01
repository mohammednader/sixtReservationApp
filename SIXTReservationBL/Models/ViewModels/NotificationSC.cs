using System;
using System.Collections.Generic;
using System.Text;

namespace SIXTReservationBL.Models.ViewModels
{
   public class NotificationSC
    {
        public int ReservationStatusId { get; set; }
        public int? ReservationStatus { get; set; }
        public int? ActionStep { get; set; }
        public List<int> JobTitleId { get; set; }
        public bool IsDisable { get; set; }

    }
}
