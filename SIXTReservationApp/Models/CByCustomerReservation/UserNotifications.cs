using SIXTReservationBL.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIXTReservationApp.Models.CByCustomerReservation
{
    public class UserNotifications
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public DateTime? NotifiedDate { get; set; }
        public UserNotifications(SIXTReservationBL.Models.Domain.Notification n)
        {
            UserName = n.ToUserNavigation?.FullName;
            Email = n.ToUserNavigation?.Email;
            NotifiedDate = n.CreateDate;
        }
    }
}
