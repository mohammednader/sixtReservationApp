using SIXTReservationBL.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIXTReservationApp.Models.UserManagement
{
    public class UserVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Roles { get; set; }
        public string Branch { get; set; }
        public string JobTitle { get; set; }
        public bool IsActive { get; set; }

        public UserVM(AppUser u)
        {
            Id = u.Id;
            Name = u.FullName;
            Email = u.Email;
            PhoneNumber = u.PhoneNumber;
            JobTitle = u.JobTitle?.Name;
            IsActive = u.IsActive ?? false;
        }
    }
}
