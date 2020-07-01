using SIXTReservationApp.CustomValidation;
using SIXTReservationBL.Models.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SIXTReservationApp.Models.UserManagement
{
    public class ChangePasswordDto
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is required")]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is required")]
        [MinLength(6, ErrorMessage = "Password should be at least 6 characters")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DisplayName("New Password")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is required")]
        [MinLength(6, ErrorMessage = "Password should be at least 6 characters")]
        [DataType(DataType.Password)]
        [Unlike(nameof(Password), ErrorMessage = "New and old passwords cannot be the same")]
        public string NewPassword { get; set; }

        [DisplayName("Confirm New Password")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is required")]
        [Compare(nameof(NewPassword), ErrorMessage = "Passwords are not matching")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        public ChangePasswordDto() { }
        public ChangePasswordDto(AppUser u)
        {
            Email = u.Email;
        }
    }
}
