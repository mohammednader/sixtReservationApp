using System;
using System.Collections.Generic;

namespace SIXTReservationBL.Models.Domain
{
    public partial class JobTitle
    {
        public JobTitle()
        {
            AppUser = new HashSet<AppUser>();
            LnkNotificationJobTitle = new HashSet<LnkNotificationJobTitle>();
        }

        public int JobTitleId { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }

        public virtual ICollection<AppUser> AppUser { get; set; }
        public virtual ICollection<LnkNotificationJobTitle> LnkNotificationJobTitle { get; set; }
    }
}
