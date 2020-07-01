﻿using SIXTReservationBL.CoreBL;
using SIXTReservationBL.Models;
using SIXTReservationBL.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIXTReservationBL.Hendlers
{
   public class OpenBranchNotification : SixtReservationHandler<NotificationVM>, IBuilder
    {
        public OpenBranchNotification(int currentStep, long reservationNo, int reservationStatus, IUnitOfWork _UnitOfWork) : base(_UnitOfWork)
        {
            CurrentStep = currentStep;
            ReservationNo = reservationNo;
            ReservationStatus = reservationStatus;

        }

        public override bool PerformAction(NotificationVM request)
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

                #region Add Notification To Branch Manger
                var notifications = new List<Notification>();

                // get users from notiication setting
                var notificationSetting = unitOfWork.NotificationSettingBL.GetNotificationWithDetails(u => u.ReservationStatusId == ReservationStatus && u.ActionStep == CurrentStep);
                var LnkJobTitles = notificationSetting?.LnkNotificationJobTitle.ToList();
                var JobTitles = LnkJobTitles?.Select(s => s.JobTitleId).ToList();

                // new ReservationStepsLog has been created on initilaize steper 

                if (JobTitles?.Count > 0)
                {
                    var ToUsers = unitOfWork.UserBL.Find(u => JobTitles.Contains(u.JobTitleId.GetValueOrDefault()) && u.IsActive == true);
                    for (int i = 0; i < ToUsers.Count; i++)
                    {
                        var notify = new Notification
                        {
                            ToUser = ToUsers[i].Id,
                            IsSeen = false,
                            NotificationText = "Reservation " + ReservationNo + " need assignment", // need to be modified  later 
                            Status = false,
                            ReservationStatusId = ReservationStatus,
                            ReservationNo = ReservationNo,
                            ReservationLogId = request.ReservationLogId,
                            CreateDate = DateTime.Now,
                            UrlNotification = "/Reservation/OpenSteps/" + ReservationNo,
                            NotificationType = (int)NotificationType.Open,
                            IsDeleted = false,
                            GroupId = (int)NotificationGroupType.NeedAssignmentNotificationOpen
                        };
                        notifications.Add(notify);
                        //var Count = unitOfWork.NotificationBL.GetCountUnSeenNotification(LoggedUserId);
                        //_ = Notify.UpdateUnseenCount(LoggedUserId, Count);
                        // unitOfWork.NotificationBL.SaveNewNotificationToDB(notify);
                    }
                    unitOfWork.NotificationBL.AddRange(notifications);
                }
                //else no seetting then confirm this step

                #endregion
                // get from db 

                base.SetOpenNextStep(CurrentStep, ReservationNo, ReservationStatus);
                return true;

            }
        }
    }
}
