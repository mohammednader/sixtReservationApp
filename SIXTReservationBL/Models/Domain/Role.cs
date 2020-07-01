using System;
using System.Collections.Generic;

namespace SIXTReservationBL.Models.Domain
{
    public partial class Role
    {
        public Role()
        {
            LnkRolePermission = new HashSet<LnkRolePermission>();
            LnkUserRole = new HashSet<LnkUserRole>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool? IsSysRole { get; set; }
        public int? DefaultPageId { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreationDate { get; set; }
        public int? LastModifiedBy { get; set; }
        public DateTime? LastModificationDate { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual Permission DefaultPage { get; set; }
        public virtual ICollection<LnkRolePermission> LnkRolePermission { get; set; }
        public virtual ICollection<LnkUserRole> LnkUserRole { get; set; }
    }
}
