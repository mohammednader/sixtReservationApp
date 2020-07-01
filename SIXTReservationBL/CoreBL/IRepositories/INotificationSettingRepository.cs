using SIXTReservationBL.Models.Domain;
using SIXTReservationBL.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace SIXTReservationBL.CoreBL.IRepositories
{
   public interface INotificationSettingRepository:IGenericRepository<NotificationSetting>
    {
        List<NotificationSetting> SearchNotification(NotificationSC search);
        List<JobTitle> GetNotificationJobTitle(int NotificationId);
        NotificationSetting GetNotificationWithDetails(Expression<Func<NotificationSetting, bool>> predicate);
        void DetachNotificationJobTitle(int notificationId, List<int> titles = null);
    }
}
