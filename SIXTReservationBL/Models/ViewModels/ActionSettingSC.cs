using System;
using System.Collections.Generic;
using System.Text;

namespace SIXTReservationBL.Models.ViewModels
{
   public class ActionSettingSC
    {
        public int? ReservationStatusId { get; set; }
        public int? ActionStepId { get; set; }
        public int? RateSegmentCategoryId { get; set; }
        public int? BranchId { get; set; }
        public int? WeekDayId { get; set; }
        public bool IsDisable { get; set; }

    }
}
