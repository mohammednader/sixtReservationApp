using System;
using System.Collections.Generic;
using System.Text;

namespace SIXTReservationBL.Models
{
    public class NotificationVM
    {
        public int[] ToUserIds { get; set; }
        public int  ReservationLogId { get; set; }
    }
}
