using System;
using System.Collections.Generic;
using System.Text;

namespace SIXTReservationBL.Models.ViewModels
{
    public class AgentAssignmentVM
    {
        public int userId { get; set; }
        public string reservationId { get; set; }
        public string currentStep { get; set; }
        public int FromUser { get; set; }
        public string Comment { get; set; }
        public DateTime AssignmentDate { get; set; }
        public int LoggedStep { get; set; }

        public int  AssigneeId  { get; set; }
        public long[] ReservationNo { get; set; }
        public bool IsFinalAssignment { get; set; }


    }
}
