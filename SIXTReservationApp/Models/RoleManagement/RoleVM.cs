using SIXTReservationBL.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIXTReservationApp.Models.RoleManagement
{
    public class RoleVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
         public string CreatedBy { get; set; }
        public string CreationDate { get; set; }
        public string LastModifiedBy { get; set; }
        public string LastModificationDate { get; set; }

        public RoleVM(Role r)
        {
            Id = r.Id;
            Name = r.Name;
            Description = r.Description;
            CreationDate = r.CreationDate?.ToString("dd/MM/yyyy HH:mm");
            LastModificationDate = r.LastModificationDate?.ToString("dd/MM/yyyy HH:mm");
        }
    }
}
