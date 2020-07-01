using System;
using System.Collections.Generic;

namespace SIXTReservationBL.Models.Domain
{
    public partial class ReasonType
    {
        public ReasonType()
        {
            Reason = new HashSet<Reason>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Reason> Reason { get; set; }
    }
}
