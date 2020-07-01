using Org.BouncyCastle.Ocsp;
using SIXTReservationApp.Models;
using SIXTReservationBL.CoreBL;
using SIXTReservationBL.Models.Domain;
using SIXTReservationBL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIXTReservationApp.DesignPattern
{

    public class NotifyHandler : ChainOfResponsabilites<NotifyModel>
    {
        private readonly IUnitOfWork unitOfWork;
        public NotifyHandler(IUnitOfWork _UnitOfWork)
        {
            unitOfWork = _UnitOfWork;
        }
        public override NotifyModel Handle(string request)
        {
            if (request.ToString() == "Contact Center Managment  Notification")
            {
                // get all contact center
                var notifiedIds = new List<int>();
                var notifications = new List<Notification>();
                var contactCenterUsers = unitOfWork.UserBL.Find(u => u.JobTitleId == 1);
                for (int i = 0; i < contactCenterUsers.Count(); i++)
                {
                    notifiedIds.Add(contactCenterUsers[i].Id);
                    notifications.Add(new Notification
                    {
                        ToUser = contactCenterUsers[i].Id,
                        IsSeen = false,
                        NotificationText = "new notification reservation canceled by customer",
                        Status = false,
                       
                    });
                }
                unitOfWork.NotificationBL.AddRange(notifications);
                if (unitOfWork.Complete() > 0)
                {
                    return new NotifyModel() { NotifiedIds = notifiedIds };
                }
                else
                {
                    return new NotifyModel();
                }
            }
            else
            {
                return base.Handle(request);
            }
        }
    }

    public class AssignHandler : ChainOfResponsabilites<NotifyModel>
    {
        public override NotifyModel Handle(string request)
        {
            if (request.ToString() == "Contact Center Agent Assignment ")
            {
                return new NotifyModel();
            }
            else
            {
                return base.Handle(request);
            }
        }
    }

    public class FormSubmitHandler : ChainOfResponsabilites<FormSubmited>
    {
        public override FormSubmited Handle(string request)
        {
            if (request.ToString() == "Agent Form Sumbitted")
            {
                return new FormSubmited();
            }
            else
            {
                return base.Handle(request);
            }
        }
    }


    public class NotifyModel
    {
        public int Status { get; set; }
        public List<int> NotifiedIds { get; set; }
        public int? ReservationNo { get; set; }
        public int? ReservationLogId { get; set; }
        public string From { get; set; }
        public string To { get; set; }
    }
    public class assignModel
    {
        public string From { get; set; }
        public string To { get; set; }
    }

    public class FormSubmited
    {
        public int UserId { get; set; }
        public bool IsAnswer { get; set; }
        public string Reason { get; set; }
    }

}
