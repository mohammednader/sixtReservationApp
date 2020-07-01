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
    public class StepHandler
    {
        private readonly IUnitOfWork unitOfWork;
        private long ReservationNo { get; set; }
        private int ReservationStatus { get; set; }
        public int StepId { get; set; }
        public int SavedLogId { get; set; }
        public StepHandler(IUnitOfWork _UnitOfWork)
        {
            unitOfWork = _UnitOfWork;
        }
        public IBuilder InitiateSteperCByCustomer(long reservationNo, int reservationStatus)
        {
            ReservationNo = reservationNo;
            ReservationStatus = reservationStatus;
            int step = 0;
            var stepLog = new List<ReservationStepsLog>();
            //check database ReservationNo  log  

            // comment last one 
            bool exist = unitOfWork.ReservationStepLogBL.CheckExist(r => r.ReservationNo == reservationNo && r.ReservationStatus == ReservationStatus);
            if (exist)
            {
                var lastStepNotDone = unitOfWork.ReservationStepLogBL.FindOne(r => r.ReservationNo == reservationNo &&
                                                                                      (r.ReservationStatusId == ReservationStatus || r.ReservationStatus == ReservationStatus) &&
                                                                                       r.IsDone == false);
                if (lastStepNotDone != null)
                {
                    step = (int)lastStepNotDone.StepId;
                    SavedLogId = lastStepNotDone.Id;
                }
                else
                {
                    // all is completed 
                    step = 0;
                }

            }
            else
            {
                var firstStep = unitOfWork.StatusStepBL.FindOne(s => s.ReseravationStatus == ReservationStatus && s.StepOrder == 1);

                //foreach (var stepp in steps)
                //{
                var newLog = new ReservationStepsLog
                {
                    ReservationNo = reservationNo,
                    StepId = firstStep.StepId,
                    CreationDate = DateTime.Now,
                    IsDone = false,
                    ReservationStatus = reservationStatus,
                };
                //    stepLog.Add(RSLog);
                //}
                unitOfWork.ReservationStepLogBL.SaveNewReservationLogToDB(newLog);
                step = 1;
                SavedLogId = newLog.Id;
            }
            StepId = step;
            switch (step)
            {
                case 1:
                    {
                        return new CancelledByCustomerNotification((int)CacelledByCustomerStepEnum.ContactCenterManagmentNotification, ReservationNo, ReservationStatus, unitOfWork);
                    }
                case 3:
                    {
                        return new CancelledByCustomerAgentAssignment((int)CacelledByCustomerStepEnum.ContactCenterAgentAssignment, ReservationNo, ReservationStatus, SavedLogId, unitOfWork);
                    }
                case 5:
                    {
                        return new CancelledByCustomerFormAction((int)CacelledByCustomerStepEnum.AgentFormSumbitted, ReservationNo, ReservationStatus, SavedLogId, unitOfWork);
                    }
                default:
                    return null;
            }
        }

        public IBuilder InitiateSteperCBySixt(long reservationNo, int reservationStatus)
        {
            ReservationNo = reservationNo;
            ReservationStatus = reservationStatus;
            int step = 0;
            var stepLog = new List<ReservationStepsLog>();
            //check database ReservationNo  log  

            // comment last one 
            bool exist = unitOfWork.ReservationStepLogBL.CheckExist(r => r.ReservationNo == reservationNo && r.ReservationStatus == ReservationStatus);
            if (exist)
            {
                var lastStepNotDone = unitOfWork.ReservationStepLogBL.FindOne(r => r.ReservationNo == reservationNo &&
                                                                                        (r.ReservationStatusId == ReservationStatus || r.ReservationStatus == ReservationStatus) &&
                                                                                       r.IsDone == false);
                if (lastStepNotDone != null)
                {
                    step = (int)lastStepNotDone.StepId;
                    SavedLogId = lastStepNotDone.Id;
                }
                else
                {
                    // all is completed 
                    step = 0;
                }

            }
            else
            {
                var firstStep = unitOfWork.StatusStepBL.FindOne(s => s.ReseravationStatus == ReservationStatus && s.StepOrder == 1);

                //foreach (var stepp in steps)
                //{
                var newLog = new ReservationStepsLog
                {
                    ReservationNo = reservationNo,
                    StepId = firstStep.StepId,
                    CreationDate = DateTime.Now,
                    IsDone = false,
                    ReservationStatus = reservationStatus,
                };
                //    stepLog.Add(RSLog);
                //}
                unitOfWork.ReservationStepLogBL.SaveNewReservationLogToDB(newLog);
                step = 7;  // operation and reservation notification step
                SavedLogId = newLog.Id;
            }
            StepId = step;
            switch (step)
            {
                case 7:
                    {
                        return new CancelledBySixtNotification((int)CacelledBySixtStepEnum.OperationReservationManagementNotification, ReservationNo, ReservationStatus, unitOfWork);
                    }
                case 9:
                    {
                        return new CancelledBySixtAssignment((int)CacelledBySixtStepEnum.OperationReservationManagementAssignment, ReservationNo, ReservationStatus, SavedLogId, unitOfWork);
                    }
                case 11:
                    {
                        return new CancelledBySixtFormAction((int)CacelledBySixtStepEnum.OperationReservationManagementFormSumbitted, ReservationNo, ReservationStatus, SavedLogId, unitOfWork);
                    }
                default:
                    return null;
            }
        }



        public IBuilder InitiateSteperNoShow(long reservationNo, int reservationStatus)
        {
            ReservationNo = reservationNo;
            ReservationStatus = reservationStatus;
            int step = 0;
            var stepLog = new List<ReservationStepsLog>();
            //check database ReservationNo  log  

            // comment last one 
            bool exist = unitOfWork.ReservationStepLogBL.CheckExist(r => r.ReservationNo == reservationNo && r.ReservationStatus == ReservationStatus);
            if (exist)
            {
                var lastStepNotDone = unitOfWork.ReservationStepLogBL.FindOne(r => r.ReservationNo == reservationNo &&
                                                                                      (r.ReservationStatusId == ReservationStatus || r.ReservationStatus == ReservationStatus) &&
                                                                                       r.IsDone == false);
                if (lastStepNotDone != null)
                {
                    step = (int)lastStepNotDone.StepId;
                    SavedLogId = lastStepNotDone.Id;
                }
                else
                {
                    // all is completed 
                    step = 0;
                }

            }
            else
            {
                var firstStep = unitOfWork.StatusStepBL.FindOne(s => s.ReseravationStatus == ReservationStatus && s.StepOrder == 1);

                //foreach (var stepp in steps)
                //{
                var newLog = new ReservationStepsLog
                {
                    ReservationNo = reservationNo,
                    StepId = firstStep.StepId,
                    CreationDate = DateTime.Now,
                    IsDone = false,
                    ReservationStatus = reservationStatus,
                };
                //    stepLog.Add(RSLog);
                //}
                unitOfWork.ReservationStepLogBL.SaveNewReservationLogToDB(newLog);
                step = 13;
                SavedLogId = newLog.Id;
            }
            StepId = step;
            switch (step)
            {
                case 13:
                    {
                        return new NoShowNotification((int)NoShowStepEnum.ContactCenterManagmentNotification, ReservationNo, ReservationStatus, unitOfWork);
                    }
                case 15:
                    {
                        return new NoShowAgentAssignment((int)NoShowStepEnum.ContactCenterAgentAssignment, ReservationNo, ReservationStatus, SavedLogId, unitOfWork);
                    }
                case 17:
                    {
                        return new NoShowFormAction((int)NoShowStepEnum.AgentFormSumbitted, ReservationNo, ReservationStatus, SavedLogId, unitOfWork);
                    }
                default:
                    return null;
            }
        }


        public IBuilder InitiateSteperOpen(long reservationNo, int reservationStatus)
        {
            ReservationNo = reservationNo;
            ReservationStatus = reservationStatus;
            int step = 0;
            var stepLog = new List<ReservationStepsLog>();
            //check database ReservationNo  log  

            // comment last one 
            bool exist = unitOfWork.ReservationStepLogBL.CheckExist(r => r.ReservationNo == reservationNo && r.ReservationStatus == ReservationStatus);
            if (exist)
            {
                var lastStepNotDone = unitOfWork.ReservationStepLogBL.FindOne(r => r.ReservationNo == reservationNo &&
                                                                                      (r.ReservationStatusId == ReservationStatus || r.ReservationStatus == ReservationStatus) &&
                                                                                       r.IsDone == false);
                if (lastStepNotDone != null)
                {

                    step = (int)lastStepNotDone.StepId;
                    SavedLogId = lastStepNotDone.Id;

                }
                else
                {
                    // all is completed 
                    step = 0;
                }

            }
           
            else
            {
                var firstStep = unitOfWork.StatusStepBL.FindOne(s => s.ReseravationStatus == ReservationStatus && s.StepOrder == 1);

                //foreach (var stepp in steps)
                //{
                var newLog = new ReservationStepsLog
                {
                    ReservationNo = reservationNo,
                    StepId = firstStep.StepId,
                    CreationDate = DateTime.Now,
                    IsDone = false,
                    ReservationStatus = reservationStatus,
                };
                //    stepLog.Add(RSLog);
                //}
                unitOfWork.ReservationStepLogBL.SaveNewReservationLogToDB(newLog);
                step = 19;
                SavedLogId = newLog.Id;
            }
            StepId = step;
            switch (step)
            {
                case 19:
                    {
                        return new OpenBranchNotification((int)OpenStepEnum.BranchManagmentNotification, ReservationNo, ReservationStatus, unitOfWork);
                    }
                case 21:
                    {
                        return new OpenBranchAssignment((int)OpenStepEnum.BranchAgentAssignment, ReservationNo, ReservationStatus, SavedLogId, unitOfWork);
                    }
                case 23:
                    {
                        return new OpenFormAction((int)OpenStepEnum.AgentFormSumbitted, ReservationNo, ReservationStatus, SavedLogId, unitOfWork);
                    }
                case 24:
                    {
                        return new OpenContactAgentNotification((int)OpenStepEnum.ContactManagmentNotification, ReservationNo, ReservationStatus, SavedLogId, unitOfWork);
                    }
                case 26:
                    {
                        return new OpenContactCenterAssignment((int)OpenStepEnum.ContactManagmentAssignment, ReservationNo, ReservationStatus, SavedLogId, unitOfWork);
                    }
                case 28:
                    {
                        return new OpenFormAction((int)OpenStepEnum.ContactManagmentFormSumbitted, ReservationNo, ReservationStatus, SavedLogId, unitOfWork);
                    }
                case 29:
                    {
                        return new OpenFillCustomerFormAction((int)OpenStepEnum.FillCustomerForm, ReservationNo, ReservationStatus, SavedLogId, unitOfWork);
                    }
                case 33:
                    {
                        return new OpenBranchAssignment((int)OpenStepEnum.BranchAgentAssignmentConfirmed, ReservationNo, ReservationStatus, SavedLogId, unitOfWork);
                    }
                case 35:
                    {
                        return new OpenFormAction((int)OpenStepEnum.BranchAgentFormSumbitted, ReservationNo, ReservationStatus, SavedLogId, unitOfWork);
                    }
                default:
                    return null;
            }
        }

        public IBuilder InitiateSteperOpenConfirm(long reservationNo, int reservationStatus)
        {
            ReservationNo = reservationNo;
            ReservationStatus = reservationStatus;
            int step = 0;
            var stepLog = new List<ReservationStepsLog>();
            //check database ReservationNo  log  

            // comment last one 
            bool exist = unitOfWork.ReservationStepLogBL.CheckExist(r => r.ReservationNo == reservationNo && r.ReservationStatus == ReservationStatus);
            if (exist)
            {
                var lastStepNotDone = unitOfWork.ReservationStepLogBL.FindOne(r => r.ReservationNo == reservationNo &&
                                                                                      (r.ReservationStatusId == ReservationStatus || r.ReservationStatus == ReservationStatus) &&
                                                                                       r.IsDone == false);
                if (lastStepNotDone != null)
                {

                    step = (int)lastStepNotDone.StepId;
                    SavedLogId = lastStepNotDone.Id;

                }
                else
                {
                    // all is completed 
                    step = 0;
                }

            }

            else
            {
                var firstStep = unitOfWork.StatusStepBL.FindOne(s => s.ReseravationStatus == ReservationStatus && s.StepOrder == 4);

                //foreach (var stepp in steps)
                //{
                var newLog = new ReservationStepsLog
                {
                    ReservationNo = reservationNo,
                    StepId = firstStep.StepId,
                    CreationDate = DateTime.Now,
                    IsDone = false,
                    ReservationStatus = reservationStatus,
                };
                //    stepLog.Add(RSLog);
                //}
                unitOfWork.ReservationStepLogBL.SaveNewReservationLogToDB(newLog);
                step =24 ;
                SavedLogId = newLog.Id;
            }
            StepId = step;
            switch (step)
            {
               
                case 24:
                    {
                        return new OpenContactAgentNotification((int)OpenStepEnum.ContactManagmentNotification, ReservationNo, ReservationStatus, SavedLogId, unitOfWork);
                    }
                case 26:
                    {
                        return new OpenContactCenterAssignment((int)OpenStepEnum.ContactManagmentAssignment, ReservationNo, ReservationStatus, SavedLogId, unitOfWork);
                    }
                case 28:
                    {
                        return new OpenFormAction((int)OpenStepEnum.ContactManagmentFormSumbitted, ReservationNo, ReservationStatus, SavedLogId, unitOfWork);
                    }
                default:
                    return null;
            }
        }

        public IBuilder InitiateSteperOpenDecliend(long reservationNo, int reservationStatus)
        {
            ReservationNo = reservationNo;
            ReservationStatus = reservationStatus;
            int step = 0;
            var stepLog = new List<ReservationStepsLog>();
            //check database ReservationNo  log  

            // comment last one 
            bool exist = unitOfWork.ReservationStepLogBL.CheckExist(r => r.ReservationNo == reservationNo && r.ReservationStatus == ReservationStatus);
            if (exist)
            {
                var lastStepNotDone = unitOfWork.ReservationStepLogBL.FindOne(r => r.ReservationNo == reservationNo &&
                                                                                      (r.ReservationStatusId == ReservationStatus || r.ReservationStatus == ReservationStatus) &&
                                                                                       r.IsDone == false);
                if (lastStepNotDone != null)
                {

                    step = (int)lastStepNotDone.StepId;
                    SavedLogId = lastStepNotDone.Id;

                }
                else
                {
                    // all is completed 
                    step = 0;
                }

            }

            else
            {
                //var firstStep = unitOfWork.StatusStepBL.FindOne(s => s.ReseravationStatus == ReservationStatus && s.StepOrder == 3);

                ////foreach (var stepp in steps)
                ////{
                //var newLog = new ReservationStepsLog
                //{
                //    ReservationNo = reservationNo,
                //    StepId = firstStep.StepId,
                //    CreationDate = DateTime.Now,
                //    IsDone = false,
                //    ReservationStatus = reservationStatus,
                //};
                ////    stepLog.Add(RSLog);
                ////}
                //unitOfWork.ReservationStepLogBL.SaveNewReservationLogToDB(newLog);
                step = 23;
                //SavedLogId = newLog.Id;
            }
            StepId = step;
            switch (step)
            {
                case 23:
                    {
                        return new OpenFormActionDecliend((int)OpenStepEnum.AgentFormSumbitted, ReservationNo, ReservationStatus, unitOfWork);
                    }

                default:
                    return null;
            }
        }


    }
}
