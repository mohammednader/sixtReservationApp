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
   public class CancelledBySixtAssignment : SixtReservationHandler<AgentAssignmentVM>, IBuilder
    {

        public CancelledBySixtAssignment(int currentStep, long reservationNo, int reservationStatus, int savedLoggedId,IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            CurrentStep = currentStep;
            ReservationNo = reservationNo;
            ReservationStatus = reservationStatus;
            SavedLoggedId = savedLoggedId;
        }

        public override bool PerformAction(AgentAssignmentVM request)
        {
            var date = DateTime.Now;
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


            //Modify Last step notification Is deleted
            var LastNotifications = unitOfWork.NotificationBL.Find(n => n.ReservationNo == ReservationNo &&
                                                                           (n.GroupId == (int)NotificationGroupType.NeedAssignmentNotificationCBySixt || n.GroupId == (int)NotificationGroupType.NoAssignmentNotificationCBySixt))
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
                NotificationType = (int)NotificationType.CancelledBySixt,
                UrlNotification = "/Reservation/CBySixtSteps/" + ReservationNo,
                IsDeleted = false,
                GroupId = (int)NotificationGroupType.AssignedToMeCBySixt,
                ReservationLogId = SavedLoggedId,
                ReservationStatusId = ReservationStatus

            };
            unitOfWork.NotificationBL.Add(NotifyObject);

            base.NextStep(CurrentStep, ReservationNo, ReservationStatus);
            return true;
        }

    }

}

