
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SIXTReservationApp.ViewModels 
{
    public class BranchVM
    {
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is required")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is required")]
        public string Code { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Invalid email format")]
        public string Email { get; set; }
        public bool? IsActive { get; set; }
        public BranchVM()
        {

        }
        public BranchVM(SIXTReservationBL.Models.Domain.Branch b)
        { 
            if (b != null)
            {
                Id = b.Id;
                Name = b.Name;
                Code = b.Code;
                Email = b.Email;
                IsActive = b.IsActive; 
            }
        }
    }
}
