using Microsoft.EntityFrameworkCore;
using SIXTReservationBL.CoreBL.IRepositories;
using SIXTReservationBL.Models.Domain;
using SIXTReservationBL.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIXTReservationBL.Repositories
{
    public class NotificationRepository : GenericRepository<Notification>, INotificationRepository
    {
        public NotificationRepository(SixtReservationContext context) : base(context)
        {

        }

        public int GetCountUnSeenNotification(int LoggedUser)
        {
            var ModelCount = Context.Notification
                                                  .Where(f => f.ToUser == LoggedUser)
                                                  .Count(n => n.IsDeleted == false);
            return ModelCount;

        }
        public Notification UpdateCountUnSeenNotification(int LoggedUser, int notificationNo)
        {
            var Model = Context.Notification
                                            .Where(f => f.ToUser == LoggedUser && f.Id == notificationNo).FirstOrDefault();

            if (Model != null)
            {
                Model.IsSeen = true;
                Model.SeenDate = DateTime.Now;
                return Model;
            }
            return null;

        }
        public int SaveNewNotificationToDB(Notification notification)
        {

            try
            {
                if (notification != null)
                {
                    return Context.Notification.Add(notification).Context.SaveChanges();
                }
                else return -1;
            }
            catch (Exception e)
            {

                return -1;
            }

        }

        public int SaveNotificationRangeToDB(List<Notification> notifications)
        {

            try
            {
                if (notifications != null)
                {
                    return 0;
                    //  return Context.Notification.AddRange(notifications).Context.SaveChanges();
                }
                else return -1;
            }
            catch (Exception e)
            {

                return -1;
            }



        }
        public List<Notification> GetNotifiedUser(List<int?> usersId, long ReservationId,int StepId)
        {
            var result = new List<Notification>();
            try
            {
                var query = Context.Notification.Include(n => n.ToUserNavigation)
                                                .Include(n=>n.ReservationLog)
                                  .AsQueryable();

                if (usersId != null && usersId.Count > 0)
                {
                    query = query.Where(n => usersId.Contains(n.ToUser) && n.ReservationNo == ReservationId);
                                 //.Where(n => n.ReservationLog.StepId == StepId);
                    result = query.ToList();

                }

            }
            catch (Exception e)
            { }
            return result;

        }

        public List<Notification> GetAllNotification(NotificationDateRangeSC notification)
        {
            var result = new List<Notification>();
            try
            {
                var query = Context.Notification
                                                .Include(n => n.ToUserNavigation)
                                                .Include(n => n.Group)
                                                .Where(n => n.IsDeleted == false)

                                                .AsQueryable();
                if (notification != null)
                {
                    if (notification.ToUser != 0)
                    {
                        query = query.Where(r => r.ToUser == notification.ToUser);
                    }
                    if (notification.BookingDateFrom.HasValue && notification.BookingDateFrom.Value != DateTime.MinValue)
                    {
                        query = query.Where(r => r.CreateDate >= notification.BookingDateFrom);
                    }
                    if (notification.BookingDateTo.HasValue && notification.BookingDateTo.Value != DateTime.MinValue)
                    {
                        query = query.Where(r => r.CreateDate <= notification.BookingDateTo);
                    }
                    result = query.ToList();
                }
            }
            catch
            {

            }
            return result;
        }

        public List<NotificationGroupVM> GetNotDeletedNotification(int LoggedUser)
        {
            var result = new List<NotificationGroup>();

            try
            {
                var query = Context.NotificationGroup
                                                .Include(n => n.Notification)
                                                .AsQueryable();

                var res = query.Select(g => new NotificationGroupVM
                {
                    NotificationCount = g.Notification.Where(n =>
                                                                      n.ToUser == LoggedUser &&
                                                                      n.IsDeleted == false).Distinct().Count(),

                    GroupName = g.GroupName,
                    Order = g.Order.GetValueOrDefault(),
                    ActionFilter = g.ActionStep.GetValueOrDefault(),
                    Url = g.NotificationUrl ?? string.Empty
                })
                    .OrderBy(g => g.Order)
                    .ToList();

                //query = query.Where(g => g.Notification.Any(c =>
                //                                       c.GroupId == g.Id &&
                //                                       c.ToUser == LoggedUser &&
                //                                        c.IsDeleted == false));
                return res;



            }
            catch
            {
                return null;
            }
            //  return result;


        }


        public void UpdateDeletedNotification(long ReservationNo)
        {
            try
            {
                if ( ReservationNo >0)
                {
                    var notificationList = Context.Notification.Where(n => n.ReservationNo == ReservationNo).ToList();


                    for (int i = 0; i < notificationList.Count(); i++)
                    {
                        var item = notificationList[i];
                        item.IsDeleted = true;
                        Context.Notification.Update(item);
                    }
                }
                else
                {

                }
               
            }
            catch (Exception e)
            {

                
            }



        }

    }


}
