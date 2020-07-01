using System;
using System.Collections.Generic;

namespace SIXTReservationBL.Models.Domain
{
    public partial class RateSegmentCategory
    {
        public RateSegmentCategory()
        {
            ActionSetting = new HashSet<ActionSetting>();
        }

        public int Id { get; set; }
        public string RateSegmentCategoryName { get; set; }

        public virtual ICollection<ActionSetting> ActionSetting { get; set; }
    }
}
