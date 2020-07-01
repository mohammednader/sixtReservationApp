using System;
using System.Collections.Generic;

namespace SIXTReservationBL.Models.Domain
{
    public partial class Question
    {
        public int Id { get; set; }
        public int? ReservationStatus { get; set; }
        public int? ActionStep { get; set; }
        public string QuestionText { get; set; }

        public virtual StatusStep ActionStepNavigation { get; set; }
        public virtual ReservationStatus ReservationStatusNavigation { get; set; }
    }
}
