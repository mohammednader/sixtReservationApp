using SIXTReservationBL.CoreBL;
using SIXTReservationBL.Models;
using SIXTReservationBL.Models.Domain;
using SIXTReservationBL.Models.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIXTReservationBL.Hendlers
{
    public class OpenFormAction : SixtReservationHandler<FormActionVM>, IBuilder
    {
        public OpenFormAction(int currentStep, long reservationNo, int reservationStatus, int savedLoggedId, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            CurrentStep = currentStep;
            ReservationNo = reservationNo;
            ReservationStatus = reservationStatus;
            SavedLoggedId = savedLoggedId;
        }
        public override bool PerformAction(FormActionVM request)
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


                var LastNotifications = new List<Notification>();
                //Modify Last step notification Is deleted
                if (CurrentStep == (int)OpenStepEnum.AgentFormSumbitted)
                {
                    LastNotifications = unitOfWork.NotificationBL.Find(n =>
                                                                  n.ReservationNo == long.Parse(request.ReservationId) &&
                                                                  (n.GroupId == (int)NotificationGroupType.NoFormSubmitNotificationOpen || n.GroupId == (int)NotificationGroupType.AssignedToMeOpen))
                                                                      .ToList();
                }
                else
                {
                    LastNotifications = unitOfWork.NotificationBL.Find(n =>
                                                                       n.ReservationNo == long.Parse(request.ReservationId) &&
                                                                       (n.GroupId == (int)NotificationGroupType.NoFormSubmitNotificationOpenConfirmed || n.GroupId == (int)NotificationGroupType.AssignedToMeOpenConfirmed))
                                                                           .ToList();
                }
                for (int i = 0; i < LastNotifications.Count; i++)
                {
                    var item = LastNotifications[i];
                    item.IsDeleted = true;
                    unitOfWork.NotificationBL.Update(item);
                }

                if (request.IsDeny == true)
                {
                    //Deny Action
                    var FormSubmitObject = new ReservationFormSubmit
                    {
                        ReasonStatus = request.ReasonStatus,
                        CreationDate = DateTime.Now,
                        CreatedUser = request.LoggedUser,//logedUser
                        ReservationNo = long.Parse(request.ReservationId),
                        Comment = request.Comment,
                        ReasonId = request.ReasonId,
                        ReservationLogId = SavedLoggedId

                    };
                    unitOfWork.FormSubmittedBL.Add(FormSubmitObject);
                    if (request.CancelBy == (int)SixtCancellationStatusEnum.CancelledBySixt)
                    {
                        //Change reservation status from Open >> cancel by sixt  and save current state as history
                        var now = DateTime.Now;
                        var existingRecord = unitOfWork.ReservationBL.FindOne(r => r.ReservationNum == long.Parse(request.ReservationId));
                        var newHistory = unitOfWork.ReservationHistoryBL.GetHistoryObject(existingRecord, SavedLoggedId);
                        newHistory.DateFrom = existingRecord.CreationDate;
                        newHistory.DateTo = now;
                        unitOfWork.ReservationHistoryBL.Add(newHistory);

                        // update exitsig reservation status
                        int id = existingRecord.Id;
                        DateTime? creationdate = now;
                        existingRecord.CreationDate = creationdate;
                        existingRecord.ReservationStatusId = (int)SixtCancellationStatusEnum.CancelledBySixt;
                        existingRecord.RequestLevel = "0";
                        existingRecord.OnRequest = false;
                        existingRecord.IsCompleted = true;
                        unitOfWork.ReservationBL.Update(existingRecord);
                        unitOfWork.NotificationBL.UpdateDeletedNotification(long.Parse(request.ReservationId));

                    }
                    else if (request.CancelBy == (int)SixtCancellationStatusEnum.CancelledByCustomer)
                    {
                        //Change reservation status from Open >> cancel by sixt  and save current state as history
                        var now = DateTime.Now;
                        var existingRecord = unitOfWork.ReservationBL.FindOne(r => r.ReservationNum == long.Parse(request.ReservationId));
                        var newHistory = unitOfWork.ReservationHistoryBL.GetHistoryObject(existingRecord, SavedLoggedId);
                        newHistory.DateFrom = existingRecord.CreationDate;
                        newHistory.DateTo = now;
                        unitOfWork.ReservationHistoryBL.Add(newHistory);

                        // update exitsig reservation status
                        int id = existingRecord.Id;
                        DateTime? creationdate = now;
                        existingRecord.CreationDate = creationdate;
                        existingRecord.ReservationStatusId = (int)SixtCancellationStatusEnum.CancelledByCustomer;
                        existingRecord.RequestLevel = "0";
                        existingRecord.OnRequest = false;
                        existingRecord.IsCompleted = true;
                        unitOfWork.ReservationBL.Update(existingRecord);
                        unitOfWork.NotificationBL.UpdateDeletedNotification(long.Parse(request.ReservationId));
                    }


                    base.NextStep(CurrentStep, ReservationNo, ReservationStatus);
                }
                else
                {
                    // Confirmation Action  
                    var FormSubmitObject = new ReservationFormSubmit
                    {
                        ReasonStatus = request.ReasonStatus,
                        CreationDate = DateTime.Now,
                        CreatedUser = request.LoggedUser,//logedUser
                        ReservationNo = ReservationNo,
                        Comment = request.Comment,
                        ReservationLogId = SavedLoggedId,
                        IsOpenConfirm = request.IsOpenConfirm,
                    };
                    unitOfWork.FormSubmittedBL.Add(FormSubmitObject);

                    //Change request level with confirm in reservation data
                    var existingRecord = unitOfWork.ReservationBL.FindOne(r => r.ReservationNum == long.Parse(request.ReservationId));
                    existingRecord.RequestLevel = "2 - OnRequest - confirmed";
                    unitOfWork.ReservationBL.Update(existingRecord);

                    base.SetOpenNextStep(CurrentStep, ReservationNo, ReservationStatus);
                }


                return true;
            }

        }
    }
}
