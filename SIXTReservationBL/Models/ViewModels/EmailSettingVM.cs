using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SIXTReservationBL.Models.ViewModels
{
    public class EmailSettingVM
    {
        public int Id { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is required")]
        public int? ReservationStatus { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is required")]
        public string EmailText { get; set; }

        public EmailSettingVM()
        {

        }
        public EmailSettingVM(SIXTReservationBL.Models.Domain.EmailSetting e)
        {
            if (e != null)
            {
                Id = e.Id;
                ReservationStatus = e.ReseravationStatus;
                EmailText = e.EmailText;
               
            }
        }
    }
}
