using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SIXTReservationBL.Models.ViewModels
{
    public class CustomerModel
    {
        public String Name { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Invalid email format")]
        public string Email { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is required")]
        [Phone(ErrorMessage = "Invalid phone number format")]
        [Display(Name = "Phone number")]
        
        public string Phone { get; set; }
        public string Comment { get; set; }
        public string ReservationId { get; set; }
        
        public int CreatedBy { get; set; }
        public bool IsAnswer { get; set; }
        public DateTime ActionDate { get; set; }
      
        public int LoggedUser { get; set; }
        public int LoggedStep { get; set; }
        public bool IsOpenConfirm { get; set; }
        public bool IsDeny { get; set; }
    }
}
