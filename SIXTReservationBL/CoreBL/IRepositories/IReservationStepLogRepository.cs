using SIXTReservationBL.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace SIXTReservationBL.CoreBL.IRepositories
{
    public interface IReservationStepLogRepository : IGenericRepository<ReservationStepsLog>
    {
        List<ReservationStepsLog> GetAllReservationLogWithDetails(Expression<Func<ReservationStepsLog, bool>> predicate);
        ReservationStepsLog GetReservationLogWithDetails(Expression<Func<ReservationStepsLog, bool>> predicate);
        int SaveNewReservationLogToDB(ReservationStepsLog log);
        int UpdateNewReservationLogToDB(ReservationStepsLog log);
    }
}
