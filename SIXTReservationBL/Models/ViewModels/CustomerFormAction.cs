using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SIXTReservationBL.Models.ViewModels
{
   public class CustomerFormAction
    {
        public string ReservationId { get; set; }
        public int CreatedBy { get; set; }
        public bool IsAnswer { get; set; }
        public DateTime ActionDate { get; set; }
        [Required]
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Comment { get; set; }
        public int LoggedUser { get; set; }
        public int LoggedStep { get; set; }
        public bool IsOpenConfirm { get; set; }
        public bool IsDeny { get; set; }

    }
}
