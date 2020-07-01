using System;
using System.Collections.Generic;

namespace SIXTReservationBL.Models.Domain
{
    public partial class RateSegment
    {
        public RateSegment()
        {
            Reservation = new HashSet<Reservation>();
        }

        public int Id { get; set; }
        public string RateSegmentName { get; set; }

        public virtual ICollection<Reservation> Reservation { get; set; }
    }
}
