using System;
using System.Collections.Generic;

namespace SIXTReservationBL.Models.Domain
{
    public partial class LnkRolePermission
    {
        public int RoleId { get; set; }
        public int PermissionId { get; set; }

        public virtual Permission Permission { get; set; }
        public virtual Role Role { get; set; }
    }
}
