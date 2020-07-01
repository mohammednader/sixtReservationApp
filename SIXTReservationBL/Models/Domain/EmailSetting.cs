using System;
using System.Collections.Generic;

namespace SIXTReservationBL.Models.Domain
{
    public partial class EmailSetting
    {
        public int Id { get; set; }
        public int? ReseravationStatus { get; set; }
        public string EmailText { get; set; }
    }
}
