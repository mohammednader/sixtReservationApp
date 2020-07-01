using System;
using System.Collections.Generic;

namespace SIXTReservationBL.Models.Domain
{
    public partial class LnkUserRole
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }

        public virtual Role Role { get; set; }
        public virtual AppUser User { get; set; }
    }
}
