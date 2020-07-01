using System;
using System.Collections.Generic;

namespace SIXTReservationBL.Models.Domain
{
    public partial class Weekdays
    {
        public Weekdays()
        {
            ActionSetting = new HashSet<ActionSetting>();
        }

        public int Id { get; set; }
        public string WeekDayName { get; set; }

        public virtual ICollection<ActionSetting> ActionSetting { get; set; }
    }
}
