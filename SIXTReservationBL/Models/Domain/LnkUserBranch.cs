using System;
using System.Collections.Generic;

namespace SIXTReservationBL.Models.Domain
{
    public partial class LnkUserBranch
    {
        public int UserId { get; set; }
        public int BranchId { get; set; }

        public virtual Branch Branch { get; set; }
        public virtual AppUser User { get; set; }
    }
}
