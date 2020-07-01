using System;
using System.Collections.Generic;

namespace SIXTReservationBL.Models.Domain
{
    public partial class ActionSetting
    {
        public int Id { get; set; }
        public int? ReservationStatusId { get; set; }
        public int? RateSegmentCategoryId { get; set; }
        public int? BranchId { get; set; }
        public int? ActionStepId { get; set; }
        public int? WeekDayId { get; set; }
        public bool? IsEnable { get; set; }

        public virtual StatusStep ActionStep { get; set; }
        public virtual Branch Branch { get; set; }
        public virtual RateSegmentCategory RateSegmentCategory { get; set; }
        public virtual ReservationStatus ReservationStatus { get; set; }
        public virtual Weekdays WeekDay { get; set; }
    }
}
