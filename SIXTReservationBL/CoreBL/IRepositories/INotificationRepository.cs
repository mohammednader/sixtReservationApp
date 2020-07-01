using SIXTReservationBL.Models.Domain;
using SIXTReservationBL.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIXTReservationBL.CoreBL.IRepositories
{
   public interface INotificationRepository:IGenericRepository<Notification>
    {
        int GetCountUnSeenNotification(int LoggedUser);
        int SaveNewNotificationToDB(Notification notification);
        public List<Notification> GetNotifiedUser(List<int?> usersId,long ReservationId,int StepId);
        Notification UpdateCountUnSeenNotification(int LoggedUser, int notificationNo);
        List<Notification> GetAllNotification(NotificationDateRangeSC notification);

        List<NotificationGroupVM> GetNotDeletedNotification(int LoggedUser);
        void UpdateDeletedNotification(long ReservationNo);
    }
}
