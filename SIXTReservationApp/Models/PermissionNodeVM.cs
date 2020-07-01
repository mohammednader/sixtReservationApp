using SIXTReservationBL.Models.Domain;
using System.Collections.Generic;
using System.Linq;

namespace SIXTReservationApp.Models
{
    public class PermissionNodeVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }  
        public string PermissionUrl { get; set; }
        public int? ParentId { get; set; } 
        public string StyleClass { get; set; }
        public virtual List<PermissionNodeVM> Children { get; set; }

        public PermissionNodeVM()
        {
            Children = new List<PermissionNodeVM>();
        }

        public PermissionNodeVM(Permission permission)
        {
            Id = permission.Id;
            Name = permission.Name;
            DisplayName = permission.DisplayName;
            PermissionUrl = permission.PermissionUrl;
            ParentId = permission.ParentId ?? 0; 
            StyleClass = permission.StyleClass;

            Children = new List<PermissionNodeVM>();
        }

        public bool HasMenuChildren
        {
            get
            {
                return Children.Count > 0;
            }
        }

        public string ActiveId
        {
            get
            {
                return PermissionUrl?.Replace("/", "-").ToLower();
            }
        }

        public string ShowId
        {
            get
            {
                return Name.Trim().ToLower() + "-container";
            }
        }
    }
}
