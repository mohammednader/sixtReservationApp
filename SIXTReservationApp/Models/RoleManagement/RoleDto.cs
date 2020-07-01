using SIXTReservationBL.Models.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SIXTReservationApp.Models.RoleManagement
{
    public class RoleDto
    {
        public int Id { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is required")]
        public string Name { get; set; }
        public string Description { get; set; }
        //[Required(ErrorMessage = "This field is required")]
         public string SelectedPermissions { get; set; } 

        public RoleDto()
        {

        }

        public RoleDto(Role r)
        {
            Id = r.Id;
            Name = r.Name;
            Description = r.Description;
        }
    }
}
