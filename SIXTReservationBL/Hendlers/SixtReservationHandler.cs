using SIXTReservationBL.CoreBL;
using SIXTReservationBL.Models;
using SIXTReservationBL.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIXTReservationBL.Hendlers
{
    public abstract class SixtReservationHandler<T> where T : class
    {
        protected readonly IUnitOfWork unitOfWork;
        public int CurrentStep { get; set; }
        public long ReservationNo { get; set; }
        public int ReservationStatus { get; set; }
        public int SavedLoggedId { get; set; }

        public SixtReservationHandler(IUnitOfWork _UnitOfWork)
        {
            unitOfWork = _UnitOfWork;
        }
        /// <summary>
        ///  update reservation log 
        /// </summary>
        /// <param name="stepLogClass"></param>
        /// <returns></returns>
        public bool NextStep(int currentStep, long ReservationNo, int reservationStatus)
        {
            try
            {
                // close current step ;
                var CurrentStep = unitOfWork.ReservationStepLogBL.GetReservationLogWithDetails(r =>
                                                                            r.ReservationNo == (long)ReservationNo &&
                                                                            r.ReservationStatus == reservationStatus &&
                                                                            r.StepId == currentStep
                                                                            );

                DateTime now = DateTime.Now;
                // current step pis not done till now 
                if (CurrentStep != null && !CurrentStep.IsDone.GetValueOrDefault())
                {
                    CurrentStep.IsDone = true;
                    CurrentStep.CreationDate = now;

                    unitOfWork.ReservationStepLogBL.Update(CurrentStep);
                    // open next step  
                    if (!CurrentStep.Step.IsLast.GetValueOrDefault())
                    {
                        var step = CurrentStep.Step.StepOrder + 1;
                        var nextStep = unitOfWork.StatusStepBL.Find(s => s.StepOrder == step && s.ReseravationStatus == reservationStatus).FirstOrDefault();
                        if (nextStep != null)
                        {
                            unitOfWork.ReservationStepLogBL.Add(new ReservationStepsLog()
                            {
                                ReservationNo = ReservationNo,
                                ReservationStatus = reservationStatus,
                                StepId = nextStep.StepId,
                                IsDone = false,
                                CreationDate = now,

                            });
                        }
                        else
                        {
                            //Update reservation is completed
                            var existingRecord = unitOfWork.ReservationBL.FindOne(r => r.ReservationNum == ReservationNo);
                            existingRecord.IsCompleted = true;
                            unitOfWork.ReservationBL.Update(existingRecord);
                        }

                    }
                }
                return true;
            }
            catch (Exception e)
            {
                return false;

            }


        }
        public bool SetOpenNextStep(int currentStep, long ReservationNo, int reservationStatus)
        {
            try
            {

                // close current step ;
                var CurrentStep = unitOfWork.ReservationStepLogBL.GetReservationLogWithDetails(r =>
                                                                            r.ReservationNo == (long)ReservationNo &&
                                                                            r.ReservationStatus == reservationStatus &&
                                                                            r.StepId == currentStep
                                                                            );

                DateTime now = DateTime.Now;
                // current step pis not done till now 
                if (CurrentStep != null && !CurrentStep.IsDone.GetValueOrDefault())
                {
                    CurrentStep.IsDone = true;
                    CurrentStep.CreationDate = now;

                    unitOfWork.ReservationStepLogBL.Update(CurrentStep);



                    //Enable & disable actions based on Action setting

                    var steper = (int)CurrentStep.Step.StepOrder + 1;
                    var statusSteper = unitOfWork.StatusStepBL.FindOne(s => s.ReseravationStatus == reservationStatus && s.StepOrder == steper)?.StepId;
                    var actionsetting = unitOfWork.ActionSettingBL.Find(a => a.ReservationStatusId == reservationStatus).OrderBy(a=>a.ActionStepId).ToList();
                    if (actionsetting?.Count > 0)
                    {
                       var reservation = unitOfWork.ReservationBL.FindOne(a => a.ReservationNum == ReservationNo);
                        for (int i = 0; i < actionsetting.Count; i++)
                        {
                            if (statusSteper == actionsetting[i].ActionStepId && actionsetting[i].BranchId == reservation.PickUpBranchId && actionsetting[i].RateSegmentCategoryId == reservation.RateSegmentCategory && actionsetting[i].WeekDayId == GetWeekDay(reservation.PickUpweekDay) && actionsetting[i].IsEnable == false)
                            {
                                steper += 1;
                                statusSteper = unitOfWork.StatusStepBL.FindOne(s => s.ReseravationStatus == reservationStatus && s.StepOrder == steper).StepId;
                            }

                        }

                        var nextStep = unitOfWork.StatusStepBL.Find(s => s.StepOrder == steper && s.ReseravationStatus == reservationStatus).FirstOrDefault();
                        if (nextStep != null)
                        {
                            unitOfWork.ReservationStepLogBL.Add(new ReservationStepsLog()
                            {
                                ReservationNo = ReservationNo,
                                ReservationStatus = reservationStatus,
                                StepId = nextStep.StepId,
                                IsDone = false,
                                CreationDate = now,

                            });
                            return true;
                        }


                    }
                    if (CurrentStep.StepId == 3)
                    {
                        //get diff between booking date and pickupdate if diff days >= 3 disable action 1
                        var Reservation = unitOfWork.ReservationBL.FindOne(r => r.ReservationNum == ReservationNo);
                        int totalDays = Convert.ToInt32((Reservation.PickUpDate - Reservation.BookingDate).Value.Days);
                        if (totalDays > 3)
                        {
                            //next step Action2 first step
                            var stepOrder = (int)OpenStepEnum.FillCustomerForm;
                            var nextStep1 = unitOfWork.StatusStepBL.Find(s => s.StepOrder == stepOrder && s.ReseravationStatus == reservationStatus).FirstOrDefault();
                            if (nextStep1 != null)
                            {
                                unitOfWork.ReservationStepLogBL.Add(new ReservationStepsLog()
                                {
                                    ReservationNo = ReservationNo,
                                    ReservationStatus = reservationStatus,
                                    StepId = nextStep1.StepId,
                                    IsDone = false,
                                    CreationDate = now,

                                });
                            }

                        }
                        else
                        {
                            // open next step 
                            var step = CurrentStep.Step.StepOrder + 1;
                            var nextStep = unitOfWork.StatusStepBL.Find(s => s.StepOrder == step && s.ReseravationStatus == reservationStatus).FirstOrDefault();
                            if (nextStep != null)
                            {
                                unitOfWork.ReservationStepLogBL.Add(new ReservationStepsLog()
                                {
                                    ReservationNo = ReservationNo,
                                    ReservationStatus = reservationStatus,
                                    StepId = nextStep.StepId,
                                    IsDone = false,
                                    CreationDate = now,

                                });
                            }
                        }

                    }
                    else
                    {
                        // open next step 
                        var step = CurrentStep.Step.StepOrder + 1;
                        var nextStep = unitOfWork.StatusStepBL.Find(s => s.StepOrder == step && s.ReseravationStatus == reservationStatus).FirstOrDefault();
                        if (nextStep != null)
                        {
                            unitOfWork.ReservationStepLogBL.Add(new ReservationStepsLog()
                            {
                                ReservationNo = ReservationNo,
                                ReservationStatus = reservationStatus,
                                StepId = nextStep.StepId,
                                IsDone = false,
                                CreationDate = now,

                            });
                        }
                    }

                }
                return true;
            }
            
            catch
            {
                return false;

            }


        }

        public abstract bool PerformAction(T request);

        protected int GetWeekDay(string WeekDay)
        {
            switch (WeekDay.ToLower())
            {
                case "sun": return 1;
                case "mon": return 2;
                case "tue": return 3;
                case "wed": return 4;
                case "thu": return 5;
                case "fri": return 6;
                case "sat": return 7;

                default: return -1;
            }


        }

    }
}
