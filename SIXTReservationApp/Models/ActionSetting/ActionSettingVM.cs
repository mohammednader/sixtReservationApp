using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SIXTReservationApp.Models.ActionSetting
{

    public class ActionSettingVM
    {
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is required")]
        public int? ReservationStatusId { get; set; }
        public string ReservationStatusText { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is required")]
        public int? ActionStepId { get; set; }
        public string ActionStepText { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is required")]
        public int? RateSegmentCategoryId { get; set; }
        public string RateSegmentText { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is required")]
        public int? BranchId { get; set; }
        public string BranchText { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is required")]
        public int? WeekDayId { get; set; }
        public string WeekDayText { get; set; }
        public bool? IsEnable { get; set; }

        public ActionSettingVM()
        {

        }
        public ActionSettingVM(SIXTReservationBL.Models.Domain.ActionSetting A)
        {
            Id = A.Id;
            ReservationStatusId = A.ReservationStatusId;
            ReservationStatusText = A.ReservationStatus?.Status.ToString();
            ActionStepId = A.ActionStepId;
            ActionStepText = A.ActionStep?.StepName.ToString();
            RateSegmentCategoryId = A.RateSegmentCategoryId;
            RateSegmentText = A.RateSegmentCategory?.RateSegmentCategoryName.ToString();
            BranchId = A.BranchId;
            BranchText = A.Branch?.Name.ToString();
            WeekDayId = A.WeekDayId;
            WeekDayText = A.WeekDay?.WeekDayName.ToString();
            IsEnable = A.IsEnable;

        }
    }
}
