using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SIXTReservationBL.Models.ViewModels
{
    public class UserSC
    {
        public string Name { get; set; }
        public string Email { get; set; }
        [Display(Name= "Phone number")]
        public string PhoneNumber { get; set; }
        [Display(Name = "Role(s)")]
        public List<int> Role { get; set; }
        [Display(Name= "Job title")]
        public int JobTitle { get; set; }
        public List<int> Branch { get; set; }
        public int Status { get; set; } 

        public UserSC()
        {
            Role = new List<int>();
            Branch = new List<int>();
        }
    }
}
