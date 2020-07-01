using System;
using System.Collections.Generic;
using System.Text;

namespace SIXTReservationBL.Models.ViewModels
{
    public class NotificationGroupVM
    {
        public int NotificationCount { get; set; }
        public string GroupName { get; set; }
        public int Order { get; set; }
        public int ActionFilter { get; set; }
        public string Url { get; set; }
    }
}
