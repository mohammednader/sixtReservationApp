using System;
using System.Collections.Generic;

namespace SIXTReservationBL.Models.Domain
{
    public partial class Permission
    {
        public Permission()
        {
            LnkRolePermission = new HashSet<LnkRolePermission>();
            Role = new HashSet<Role>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public int? ParentId { get; set; }
        public int? Order { get; set; }
        public int? PermissionType { get; set; }
        public string PermissionUrl { get; set; }
        public string StyleClass { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual ICollection<LnkRolePermission> LnkRolePermission { get; set; }
        public virtual ICollection<Role> Role { get; set; }
    }
}
