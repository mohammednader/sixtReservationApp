using SIXTReservationBL.CoreBL;
using SIXTReservationBL.Models;
using SIXTReservationBL.Models.Domain;
using SIXTReservationBL.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIXTReservationBL.Hendlers
{
    public class OpenFormActionDecliend : SixtReservationHandler<FormActionVM>, IBuilder
    {
        public OpenFormActionDecliend(int currentStep, long reservationNo, int reservationStatus, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            CurrentStep = currentStep;
            ReservationNo = reservationNo;
            ReservationStatus = reservationStatus;
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



                var now = DateTime.Now;
                unitOfWork.ReservationStepLogBL.Add(new ReservationStepsLog()
                {
                    ReservationNo = ReservationNo,
                    ReservationStatus = ReservationStatus,
                    StepId = CurrentStep,
                    IsDone = false,
                    CreationDate = now,

                });
                SetOpenNextStep(CurrentStep, ReservationNo, ReservationStatus);
                return true;
            }

        }
    }
}
