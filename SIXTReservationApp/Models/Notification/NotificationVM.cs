using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SIXTReservationApp.Models.Notification
{
    public class NotificationVM
    {
        public int Id { get; set; }

       
        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is required")]
        public List<int?> JobTitleId { get; set; }
        public string JobTitleText { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is required")]
        public int? ReservationStatusId { get; set; }
        public string ReservationStatusText { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is required")]
        public int? ActionStep { get; set; }
        public string ActionStepText { get; set; }

        public bool? IsDisable { get; set; }

        public NotificationVM()
        {

        }
        public NotificationVM(SIXTReservationBL.Models.Domain.NotificationSetting N)
        {
            Id = N.Id;
            ReservationStatusText = N.ReservationStatus?.Status.ToString();
            ReservationStatusId = N.ReservationStatusId;
            ActionStep = N.ActionStep;
            ActionStepText = N.ActionStepNavigation?.StepName;
            IsDisable = N.IsDisabled;
            JobTitleId = N.LnkNotificationJobTitle.Select(lnk => (int?)lnk.JobTitleId).ToList();
        }
    }
}
