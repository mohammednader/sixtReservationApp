using SIXTReservationBL.Models.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SIXTReservationApp.Models.UserManagement
{
    public class UserDto
    {
        public int Id { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is required")]
        public string Name { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is required")]
        [Phone(ErrorMessage = "Invalid phone number format")]
        [Display(Name = "Phone number")]
        [RegularExpression("01[0-9]{9}",ErrorMessage = "Please enter valid phone number")]
        public string PhoneNumber { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is required")]
        [Display(Name = "Job title")]
        public int? JobTitle { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is required")]
        [Display(Name = "Role(s)")]
        public List<int?> Roles { get; set; }

        public List<int?> Branch { get; set; }

        [Display(Name = "Active")]
        public bool IsActive { get; set; }

        public UserDto()
        {
            IsActive = true;
            Roles = new List<int?>();
            Branch = new List<int?>();
        }

        public UserDto(AppUser u)
        {
            if (u != null)
            {
                Id = u.Id;
                Name = u.FullName;
                Email = u.Email;
                PhoneNumber = u.PhoneNumber;
                JobTitle = u.JobTitleId;
                IsActive = u.IsActive ?? false;
                Branch = u.LnkUserBranch.Select(lnk => (int?)lnk.BranchId).ToList();
                Roles = u.LnkUserRole.Select(lnk => (int?)lnk.RoleId).ToList();
            } else
            {
                IsActive = true;
                Roles = new List<int?>();
                Branch = new List<int?>();

            }
        }
    }

    public class AddUserDto : UserDto
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is required")]
        [MinLength(6, ErrorMessage = "Password should be at least 6 characters")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "Password doesn't match")]
        [DataType(DataType.Password)]
        [Display(Name= "Confirm Password")]
        public string ConfirmPassword { get; set; }

        public AddUserDto() : base()
        {
        }

        public AddUserDto(AppUser u) : base(u)
        {
        }
    }
}
