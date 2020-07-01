using System;
using System.Collections.Generic;
using System.Text;

namespace SIXTReservationBL.Models.ViewModels
{
  public  class ChangeStatusVM
    {
        public long reservationId { get; set; }
        public int StatusId { get; set; }
        public long[] ReservationNo { get; set; }
        public int LoggedUser { get; set; }
    }
}
