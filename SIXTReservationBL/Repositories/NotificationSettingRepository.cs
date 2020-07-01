using Microsoft.EntityFrameworkCore;
using SIXTReservationBL.CoreBL.IRepositories;
using SIXTReservationBL.Models.Domain;
using SIXTReservationBL.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace SIXTReservationBL.Repositories
{
    public class NotificationSettingRepository : GenericRepository<NotificationSetting>, INotificationSettingRepository
    {
        public NotificationSettingRepository(SixtReservationContext context) : base(context)
        {

        }

        public List<NotificationSetting> SearchNotification(NotificationSC search)
        {
            var result = new List<NotificationSetting>();
            try
            {
                var query = Context.NotificationSetting
                                          .Include(r => r.ReservationStatus)
                                          .Include(r => r.ActionStepNavigation)
                                          .Include(r => r.LnkNotificationJobTitle)
                                          .ThenInclude(r => r.JobTitle)
                                          .AsQueryable();
                if (search != null)
                {

                    if (search.ReservationStatusId != null && search.ReservationStatusId > 0)
                    {
                        query = query.Where(r => r.ReservationStatus != null && r.ReservationStatus.Id == search.ReservationStatusId);
                    }
                    if (search.ActionStep != null && search.ActionStep > 0)
                    {
                        query = query.Where(r => r.ActionStep != null && r.ActionStep == search.ActionStep);
                    }
                    if (search.JobTitleId != null && search.JobTitleId.Count() > 0)
                    {
                        query = query.Where(r => r.LnkNotificationJobTitle.Any(lnk => search.JobTitleId.Contains(lnk.JobTitleId)));
                    }

                }
                result = query.ToList();
            }
            catch { }

            return result;
        }

        public List<JobTitle> GetNotificationJobTitle(int NotificationId)
        {
            var result = new List<JobTitle>();
            try
            {
                result = Context.JobTitle
                                .Where(r => r.LnkNotificationJobTitle.Any(lnk => lnk.NotificationId == NotificationId))
                                .ToList();
            }
            catch { }
            return result;
        }

        public NotificationSetting GetNotificationWithDetails(Expression<Func<NotificationSetting, bool>> predicate)
        {
            var result = new List<NotificationSetting>();
            var query = Context.NotificationSetting

                                    .Include(u => u.LnkNotificationJobTitle)
                                    .ThenInclude(u => u.JobTitle).AsQueryable();
            try
            {
                if (predicate != null)
                {
                    result = query.Where(predicate).ToList();
                }
                    return result.FirstOrDefault();

            }
            catch (Exception)
            {

                return null;
            }
        }
        public void DetachNotificationJobTitle(int notificationId, List<int> titles = null)
        {
            try
            {
                var links = Context.LnkNotificationJobTitle
                                   .Where(lnk => lnk.NotificationId == notificationId);

                if (titles != null)
                {
                    links = links.Where(lnk => titles.Contains(lnk.JobTitleId));
                }

                Context.LnkNotificationJobTitle.RemoveRange(links.ToList());
            }
            catch { }
        }




    }
}
