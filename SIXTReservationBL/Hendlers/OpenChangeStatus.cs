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
    public class OpenChangeStatus : SixtReservationHandler<ChangeStatusVM>, IBuilder
    {
        public OpenChangeStatus(int currentStep, long reservationNo, int reservationStatus, int savedLoggedId, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            CurrentStep = currentStep;
            ReservationNo = reservationNo;
            ReservationStatus = reservationStatus;
            SavedLoggedId = savedLoggedId;
        }
        public override bool PerformAction(ChangeStatusVM request)
        {
            try
            {
                //update notification 
                var LastNotifications = unitOfWork.NotificationBL.Find(n =>
                                                                    n.ReservationNo == request.reservationId&&
                                                                    (n.GroupId == (int)NotificationGroupType.NeedToChangeReservationStatus))
                                                                         .ToList();

                for (int i = 0; i < LastNotifications.Count; i++)
                {
                    var item = LastNotifications[i];
                    item.IsDeleted = true;
                    unitOfWork.NotificationBL.Update(item);
                }



                //Change reservation status from Open >> any status  and save current state as history
                var now = DateTime.Now;
                var existingRecord = unitOfWork.ReservationBL.FindOne(r => r.ReservationNum == ReservationNo);
                var newHistory = unitOfWork.ReservationHistoryBL.GetHistoryObject(existingRecord, request.LoggedUser);
                newHistory.DateFrom = existingRecord.CreationDate;
                newHistory.DateTo = now;
                unitOfWork.ReservationHistoryBL.Add(newHistory);

                // update exitsig reservation status
                int id = existingRecord.Id;
                DateTime? creationdate = now;
                existingRecord.CreationDate = creationdate;
                existingRecord.ReservationStatusId = request.StatusId;
                existingRecord.RequestLevel = "0";
                existingRecord.OnRequest = false;
                existingRecord.IsCompleted = true;
                unitOfWork.ReservationBL.Update(existingRecord);
                unitOfWork.NotificationBL.UpdateDeletedNotification(request.reservationId);



                // update reservation step log
                var CurrentStep = unitOfWork.ReservationStepLogBL.GetReservationLogWithDetails(r =>
                                                                             r.ReservationNo == (long)ReservationNo &&
                                                                             r.ReservationStatus == (int)SixtCancellationStatusEnum.Open &&
                                                                             r.StepId == (int)OpenStepEnum.ChangeReservationStatus
                                                                             );

                // current step pis not done till now 
                if (CurrentStep != null && !CurrentStep.IsDone.GetValueOrDefault())
                {
                    CurrentStep.IsDone = true;
                    CurrentStep.CreationDate = now;

                    unitOfWork.ReservationStepLogBL.Update(CurrentStep);
                }



                return true;

            }
            catch (Exception e)
            {
                return false;
            }
        }


    }
}
