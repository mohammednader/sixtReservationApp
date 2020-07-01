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
    public class OpenFillCustomerFormAction : SixtReservationHandler<CustomerModel>, IBuilder
    {
        public OpenFillCustomerFormAction(int currentStep, long reservationNo, int reservationStatus, int savedLoggedId, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            CurrentStep = currentStep;
            ReservationNo = reservationNo;
            ReservationStatus = reservationStatus;
            SavedLoggedId = savedLoggedId;
        }
        public override bool PerformAction(CustomerModel request)
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
                var FormCustomerStepDone = unitOfWork.ReservationStepLogBL.GetReservationLogWithDetails(r =>
                                                                               r.ReservationNo == ReservationNo &&
                                                                               r.ReservationStatus == ReservationStatus &&
                                                                               r.StepId == (int)OpenStepEnum.FillCustomerForm
                                                                               );
                var Reservation = unitOfWork.ReservationBL.FindOne(r => r.ReservationNum == ReservationNo);
                var Customer = unitOfWork.CustomerBL.FindOne(c => c.Id == Reservation.CustomerId);
                // Customer Form Action  
                if (Customer != null)
                {
                    Customer.ModifiedDate = DateTime.Now;
                    Customer.ModifiedBy = request.LoggedUser;//logedUser
                    Customer.ReservationNo = ReservationNo;
                    Customer.StepLogId = SavedLoggedId;
                    Customer.Email = request.Email;
                    Customer.Phone = request.Phone;
                    Customer.Comment = request.Comment;
                    unitOfWork.CustomerBL.Update(Customer);
                }
                else
                {
                    Reservation.CustomerId = unitOfWork.ReservationBL.FindOne(r => r.ReservationNum == ReservationNo).Id + 1;
                    var newCustomer = new Customer
                    {
                        CreatedDate = DateTime.Now,
                        CreatedBy = request.LoggedUser,//logedUser
                        ReservationNo = ReservationNo,
                        StepLogId = SavedLoggedId,
                        Email = request.Email,
                        Phone = request.Phone,
                        Comment = request.Comment,
                    };
                    unitOfWork.CustomerBL.Add(Customer);
                }

                // save notification to db  

                #region Add Notification To Branch Manger
                var notifications = new List<Notification>();

                // get users from notiication setting
                var notificationSetting = unitOfWork.NotificationSettingBL.GetNotificationWithDetails(u => u.ReservationStatusId == ReservationStatus && u.ActionStep == (int)OpenStepEnum.BranchAgentNotification);
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
                            ReservationLogId = SavedLoggedId,
                            CreateDate = DateTime.Now,
                            UrlNotification = "/Reservation/OpenSteps/" + ReservationNo,
                            NotificationType = (int)NotificationType.Open,
                            IsDeleted = false,
                            GroupId = (int)NotificationGroupType.FinalNeedAssignmentNotificationOpen
                        };
                        notifications.Add(notify);
                    }
                    unitOfWork.NotificationBL.AddRange(notifications);
                }
                #endregion

                #region add step done
                var nextStep = unitOfWork.StatusStepBL.Find(s => s.StepId==(int)OpenStepEnum.BranchAgentNotification).FirstOrDefault();
                if (nextStep != null)
                {
                    unitOfWork.ReservationStepLogBL.Add(new ReservationStepsLog()
                    {
                        ReservationNo = ReservationNo,
                        ReservationStatus = ReservationStatus,
                        StepId = nextStep.StepId,
                        IsDone = true,
                        CreationDate = date,

                    });
                }
                #endregion

                if (FormCustomerStepDone.IsDone == false)
                {
                    base.SetOpenNextStep(CurrentStep, ReservationNo, ReservationStatus);
                }


                return true;
            }

        }
    }
}
