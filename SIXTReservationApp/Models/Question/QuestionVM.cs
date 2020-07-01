using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SIXTReservationApp.Models.Question
{
    public class QuestionVM
    {
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is required")]
        public string QuestionText { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is required")]
        public int? ReservationStatus { get; set; }
        public string ReservationStatusText { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is required")]
        public int? ActionStep { get; set; }
        public String ActionStepText { get; set; }



        public QuestionVM()
        {

        }
        public QuestionVM(SIXTReservationBL.Models.Domain.Question Q)
        {
            Id = Q.Id;
            QuestionText = Q.QuestionText;
            ReservationStatusText = Q.ReservationStatusNavigation?.Status.ToString();
            ReservationStatus = Q.ReservationStatus;
            ActionStep = Q.ActionStep;
            ActionStepText = Q.ActionStepNavigation?.StepName;

        }
    }
}
