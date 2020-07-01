using SIXTReservationBL.CoreBL;
using SIXTReservationBL.Models;
using SIXTReservationBL.Models.Domain;
using SIXTReservationBL.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIXTReservationBL.Hendlers
{
   public class OpenBranchAssignment : SixtReservationHandler<AgentAssignmentVM>, IBuilder
    {
        public OpenBranchAssignment(int currentStep, long reservationNo, int reservationStatus, int savedLoggedId, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            CurrentStep = currentStep;
            ReservationNo = reservationNo;
            ReservationStatus = reservationStatus;
            SavedLoggedId = savedLoggedId;
        }

        public override bool PerformAction(AgentAssignmentVM request)
        {
            var date = DateTime.Now;
            // after pickupdate  24h disable action
            var reservation = unitOfWork.ReservationBL.FindOne(r => r.ReservationNum == ReservationNo);
            var DateDiff = (date - reservation.PickUpDate).Value.TotalHours;
            if (DateDiff >= 24)
            {
                return false;
            }
            else
            {

                // save notification to db 

                var assignObject = new ReservationAssignement
                {
                    CreationDate = date,
                    FromUser = request.FromUser,//loggedUser
                    ReservationNo = ReservationNo,
                    ToUser = request.userId,
                    ReservationLogId = SavedLoggedId
                };
                unitOfWork.ReservationAssignmentBL.Add(assignObject);



                if (request.IsFinalAssignment == true)
                {
                    //Modify Last step notification Is deleted
                    var LastNotification = unitOfWork.NotificationBL.Find(n => n.ReservationNo == ReservationNo &&
                                                                                   (n.GroupId == (int)NotificationGroupType.FinalNeedAssignmentNotificationOpen || n.GroupId == (int)NotificationGroupType.FinalNoAssignNotificationOpen))
                                                                                        .ToList();
                    for (int i = 0; i < LastNotification.Count(); i++)
                    {
                        var item = LastNotification[i];
                        item.IsDeleted = true;
                        unitOfWork.NotificationBL.Update(item);
                    }
                    //Add Notification To user
                    var NotifyObject = new Notification
                    {
                        ToUser = request.userId,
                        CreateDate = date,
                        IsSeen = false,
                        Status = false,
                        ReservationNo = ReservationNo,
                        NotificationText = "Reservation " + ReservationNo + " assigned to you",
                        NotificationType = (int)NotificationType.Open,
                        UrlNotification = "/Reservation/OpenSteps/" + ReservationNo,
                        IsDeleted = false,
                        GroupId = (int)NotificationGroupType.FinalAssignedToMeOpen,
                        ReservationLogId = SavedLoggedId,
                        ReservationStatusId = ReservationStatus

                    };
                    unitOfWork.NotificationBL.Add(NotifyObject);
                }
                else
                {

                    //Modify Last step notification Is deleted
                    var LastNotifications = unitOfWork.NotificationBL.Find(n => n.ReservationNo == ReservationNo &&
                                                                                   (n.GroupId == (int)NotificationGroupType.NeedAssignmentNotificationOpen || n.GroupId == (int)NotificationGroupType.NoAssignNotificationOpen))
                                                                                        .ToList();
                    for (int i = 0; i < LastNotifications.Count(); i++)
                    {
                        var item = LastNotifications[i];
                        item.IsDeleted = true;
                        unitOfWork.NotificationBL.Update(item);
                    }
                    //Add Notification To user
                    var NotifyObject = new Notification
                    {
                        ToUser = request.userId,
                        CreateDate = date,
                        IsSeen = false,
                        Status = false,
                        ReservationNo = ReservationNo,
                        NotificationText = "Reservation " + ReservationNo + " assigned to you",
                        NotificationType = (int)NotificationType.Open,
                        UrlNotification = "/Reservation/OpenSteps/" + ReservationNo,
                        IsDeleted = false,
                        GroupId = (int)NotificationGroupType.AssignedToMeOpen,
                        ReservationLogId = SavedLoggedId,
                        ReservationStatusId = ReservationStatus

                    };
                    unitOfWork.NotificationBL.Add(NotifyObject);
                }
                base.SetOpenNextStep(CurrentStep, ReservationNo, ReservationStatus);
                return true;
            }
        }
    }
}
