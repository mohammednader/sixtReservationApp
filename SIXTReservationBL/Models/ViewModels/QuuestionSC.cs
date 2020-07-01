using System;
using System.Collections.Generic;
using System.Text;

namespace SIXTReservationBL.Models.ViewModels
{
   public class QuestionSC
    {
        public string QuestionText { get; set; }
        public int? ReservationStatus { get; set; }
        public int? ActionStep { get; set; }

    }
}
