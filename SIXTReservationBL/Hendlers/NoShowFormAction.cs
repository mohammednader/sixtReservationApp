using SIXTReservationBL.CoreBL;
using SIXTReservationBL.Models;
using SIXTReservationBL.Models.Domain;
using SIXTReservationBL.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIXTReservationBL.Hendlers
{
    public class NoShowFormAction : SixtReservationHandler<FormActionVM>, IBuilder
    {
        public NoShowFormAction(int currentStep, long reservationNo, int reservationStatus,int savedLoggedId, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            CurrentStep = currentStep;
            ReservationNo = reservationNo;
            ReservationStatus = reservationStatus;
            SavedLoggedId = savedLoggedId;
        }
        public override bool PerformAction(FormActionVM request)
        {

            //Modify Last step notification Is deleted

            var LastNotifications = unitOfWork.NotificationBL.Find(n =>
                                                                n.ReservationNo == long.Parse(request.ReservationId) &&
                                                                (n.GroupId == (int)NotificationGroupType.NoFormSubmitNotificationNoShow || n.GroupId == (int)NotificationGroupType.AssignedToMeNoShow))
                                                                    .ToArray();
            for (int i = 0; i < LastNotifications.Length; i++)
            {
                var item = LastNotifications[i];
                item.IsDeleted = true;
                unitOfWork.NotificationBL.Update(item);
            }

            // save notification to db  
            var FormSubmitObject = new ReservationFormSubmit
            {
                CreationDate = DateTime.Now,
                CreatedUser = request.LoggedUser,//logedUser
                ReservationNo = long.Parse(request.ReservationId),
                Comment = request.Comment,
                ReasonId = request.ReasonId,
                ReasonStatus = request.ReasonStatus,
                ReservationLogId = SavedLoggedId

            };
            unitOfWork.FormSubmittedBL.Add(FormSubmitObject);


            base.NextStep(CurrentStep, ReservationNo, ReservationStatus);
            return true;
        }

    }

}
