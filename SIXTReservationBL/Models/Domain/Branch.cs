using System;
using System.Collections.Generic;

namespace SIXTReservationBL.Models.Domain
{
    public partial class Branch
    {
        public Branch()
        {
            ActionSetting = new HashSet<ActionSetting>();
            LnkUserBranch = new HashSet<LnkUserBranch>();
            ReservationDropOffBranch = new HashSet<Reservation>();
            ReservationPickUpBranch = new HashSet<Reservation>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Email { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<ActionSetting> ActionSetting { get; set; }
        public virtual ICollection<LnkUserBranch> LnkUserBranch { get; set; }
        public virtual ICollection<Reservation> ReservationDropOffBranch { get; set; }
        public virtual ICollection<Reservation> ReservationPickUpBranch { get; set; }
    }
}
