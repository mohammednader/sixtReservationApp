using SIXTReservationBL.Models.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIXTReservationBL.Models.ViewModels
{
    public class VReservationListModel
    {
        public long ReservationNo { get; set; }
        public  VreservationList  ReservationList { get; set; }
        public string NextStep { get; set; }
        //public string Assignee { get; set; }
        //public string AssignFrom { get; set; }
        //public string AssignCreationDate { get; set; }
        public bool IsAssigned { get; set; }
        public bool IsAssignedToCCAgent { get; set; }

        public bool IsFormSubmitted { get; set; }
        public bool IsFormSubmittedToCCAgent { get; set; }
        public AssignmentModel AssignmentModel { get; set; }
        public AssignmentModel CCAgentAssignmentModel { get; set; }
        public FormSumbitModel FormSumbitModel { get; set; }
        public FormSumbitModel CCAgentFormSumbitModel { get; set; }

        public bool IsNeedToAssign { get; set; }
        public bool IsNeedToAssignToCCAgent { get; set; }
        public int DiffDateNotify { get; internal set; }
        public int DiffDateNotifyToCCAgent { get; internal set; }
        public int DiffDateNotifyToBM { get; internal set; }
        public bool IsNeedToAssignToBM { get; internal set; }
        public bool IsFormSubmittedToBM { get; internal set; }
        public bool IsAssignedToBM { get; internal set; }
        public AssignmentModel BMAssignmentModel { get; internal set; }
        public FormSumbitModel BMFormSumbitModel { get; internal set; }
    }

    public class AssignmentModel
    {
        public string AssignID { get; set; }
        public string AssignFromName { get; set; }
        public string AssignToName { get; set; }
        public string AssignmentDate { get; set; }
        public int AssignToId { get; set; }
        public int DiffDate { get; internal set; }
    }
    public class FormSumbitModel
    {
        public string FormSubmitID { get; set; }
        public string ReasonText { get; set; }
        public string ReasonStatus { get; set; }
        public string FormSubmitionDate { get; set; }
        public string FormSubmitedComment{ get; set; }
        public string FormSubmitedBy { get; set; }
    }



}
