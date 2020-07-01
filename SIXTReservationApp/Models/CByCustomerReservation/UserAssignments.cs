using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIXTReservationApp.Models.CByCustomerReservation
{
    public class UserAssignments
    {
        public string FromUser { get; set; }
        public string ToUser { get; set; }
        public DateTime? Date { get; set; }

        public UserAssignments(SIXTReservationBL.Models.Domain.ReservationAssignement r)
        {
            FromUser = r.FromUserNavigation?.FullName;
            ToUser = r.ToUserNavigation?.FullName;
            Date = r.CreationDate.Value;

        }
    }
}
