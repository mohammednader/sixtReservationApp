using System;
using System.Collections.Generic;
using System.Text;

namespace SIXTReservationBL.Models
{
    public class SystemEnums
    {
        public static int GetWeekDay(string day)
        {
            switch (day.ToLower())
            {
                case "sun": return 0;
                case "mon": return 1;
                case "tue": return 2;
                case "wed": return 3;
                case "thu": return 4;
                case "fri": return 5;
                case "sut": return 6;
                default: return -1;
            }

        }
    }
    public enum WeekDayEnum
    {
        Sunday = 1,
        Monday = 2,
        Tuesday = 3,
        Thursday = 4,
        Wednesday = 5,
        Friday = 6,
        Saturday = 7
    }
    public enum ActionStepEnum
    {
        Action_1 = 1,
        Action_2 = 2,
        Action_3 = 3,
        Action_4 = 4

    }

    public enum SixtCancellationStatusEnum
    {
        CancelledByCustomer = 1,
        CancelledBySixt = 2,
        NoShow = 3,
        Open = 4,
        Invoiced = 5
    }

    public enum JobTitleEnum
    {
        ContactCenterAgent = 1,
        ContactCenterManagement = 2,
        BranchAgent = 3,
        BranchManager = 4,
        OperationsManager = 5,
        ReservationManager = 6,
        Management = 7,
        Admin = 8

    }
    public enum RequestLevelEnum
    {
        Zero = 0,
        OnRequest_open = 1,
        OnRequest_confirmed = 2,
        OnRequest_declined = 3,
    }

    public enum OpenStepEnum
    {
        BranchManagmentNotification = 19,
        NoAssignmentNotification = 20,
        BranchAgentAssignment = 21,
        NoFormsumbittedNotification = 22,
        AgentFormSumbitted = 23,
        ContactManagmentNotification = 24,
        ContactManagmentNoAssignmentNotification = 25,
        ContactManagmentAssignment = 26,
        ContactManagmentNoFormsumbittedNotification = 27,
        ContactManagmentFormSumbitted = 28,
        FillCustomerForm=29,
        SendAutomatedEmailToCustomer=30,
        BranchAgentNotification = 31,
        BranchAgentNoAssignmentNotification = 32,
        BranchAgentAssignmentConfirmed = 33,
        BranchAgentNoFormsumbittedNotification = 34,
        BranchAgentFormSumbitted = 35,
        ChangeReservationStatus=36
    }
    public enum NoShowStepEnum
    {
        ContactCenterManagmentNotification = 13,
        NoAssignmentNotification = 14,
        ContactCenterAgentAssignment = 15,
        NoFormsumbittedNotification = 16,
        AgentFormSumbitted = 17,
        Finished = 18,
    }

    public enum CacelledBySixtStepEnum
    {
        OperationReservationManagementNotification = 7,
        NoAssignmentNotification = 8,
        OperationReservationManagementAssignment = 9,
        NoFormsumbittedNotification = 10,
        OperationReservationManagementFormSumbitted = 11,
        Finished = 12,
    }
    public enum CacelledByCustomerStepEnum
    {
        ContactCenterManagmentNotification = 1,
        NoAssignmentNotification = 2,
        ContactCenterAgentAssignment = 3,
        NoFormsumbittedNotification = 4,
        AgentFormSumbitted = 5,
        Finished = 6,
    }

    public enum UploadStatusEnum
    {
        Uploaded = 1,
        Parsed = 2,
        Failed = 3,
        Canacelled = 4
    }

    public enum NotificationType
    {
        Open=5,
        NoShow=4,
        CancelledBySixt=3,
        CancelledByCustomer = 1,
        UploadLog = 2,

    }
    public enum NotificationGroupType
    {
        NeedAssignmentNotification = 1,
        NoAssignmentNotification = 2,
        NoFormSubmitNotification = 3,
        AssignedToMe = 4,
        NeedAssignmentNotificationCBySixt = 5,
        NoAssignmentNotificationCBySixt = 6,
        NoFormSubmitNotificationCBySixt = 7,
        AssignedToMeCBySixt = 8,
        NeedAssignmentNotificationNoShow = 9,
        NoAssignNotificationNoShow = 10,
        NoFormSubmitNotificationNoShow = 11,
        AssignedToMeNoShow=12,
        NeedAssignmentNotificationOpen = 13,
        NoAssignNotificationOpen= 14,
        NoFormSubmitNotificationOpen = 15,
        AssignedToMeOpen = 16,
        NeedAssignmentNotificationOpenConfirmed = 17,
        NoAssignNotificationOpenConfirmed = 18,
        NoFormSubmitNotificationOpenConfirmed = 19,
        AssignedToMeOpenConfirmed = 20,
        FinalNeedAssignmentNotificationOpen = 21,
        FinalNoAssignNotificationOpen = 22,
        FinalNoFormSubmitNotificationOpen = 23,
        FinalAssignedToMeOpen = 24,
        NeedToChangeReservationStatus = 25,


    }


}
