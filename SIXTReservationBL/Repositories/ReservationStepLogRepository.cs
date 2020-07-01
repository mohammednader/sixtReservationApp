using Microsoft.EntityFrameworkCore;
using SIXTReservationBL.CoreBL.IRepositories;
using SIXTReservationBL.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace SIXTReservationBL.Repositories
{
    public class ReservationStepLogRepository : GenericRepository<ReservationStepsLog>, IReservationStepLogRepository
    {
        public ReservationStepLogRepository(SixtReservationContext context) : base(context)
        {

        }

        public List<ReservationStepsLog> GetAllReservationLogWithDetails(Expression<Func<ReservationStepsLog, bool>> predicate)
        {
            var query = Context.ReservationStepsLog.Include(l => l.Step).AsQueryable();
            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            return query.ToList();
        }

        public ReservationStepsLog GetReservationLogWithDetails(Expression<Func<ReservationStepsLog, bool>> predicate)
        {
            var query = Context.ReservationStepsLog.Include(l => l.Step).AsQueryable();
            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            return query.ToList().LastOrDefault();
        }

        public int SaveNewReservationLogToDB(ReservationStepsLog log)
        {
            try
            {
                if (log != null)
                {
                    return Context.ReservationStepsLog.Add(log).Context.SaveChanges();
                }
                else return -1;
            }
            catch (Exception e)
            {

                return -1;
            }

        }
        public int UpdateNewReservationLogToDB(ReservationStepsLog log)
        {
            try
            {
                if (log != null)
                {
                    Context.ReservationStepsLog
                                                 .Attach(log);
                    var entry = Context.Entry(log);
                    entry.State = EntityState.Modified;
                    return entry.Context.SaveChanges();
                }
                else return -1;
            }
            catch (Exception e)
            {

                return -1;
            }

        }

        //public ReservationStepsLog GetReservationLogWithFormSubmitDetails(Expression<Func<ReservationStepsLog, bool>> predicate)
        //{
        //    var query = Context.ReservationStepsLog.Include(l => l.ReservationFormSubmit).AsQueryable();
        //    if (predicate != null)
        //    {
        //        query = query.Where(predicate);
        //    }

        //    return query.ToList().LastOrDefault();

        //}
    }
}
