using SIXTReservationBL.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIXTReservationApp.Models.Notification
{
    public class NotificationDto
    {
        public int Id { get; set; }
        public string NotificationText { get; set; }
        public string ToUser { get; set; }
        public string ReservationNum { get; set; }
        public string Date { get; set; }
        public bool? IsSeen { get; set; }
        public string URL { get; set; }
        public string SeenDate { get; set; }
        public NotificationDto(SIXTReservationBL.Models.Domain.Notification n)
        {
            Id = n.Id;
            NotificationText = n.NotificationText;
            ReservationNum = n.ReservationNo.ToString();
            Date = n.CreateDate?.ToLongDateString();
            IsSeen = n.IsSeen;
            SeenDate = n.SeenDate?.ToString();
            URL = n.UrlNotification;

        }
    }
}
