using SIXTReservationBL.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace SIXTReservationBL.CoreBL.IRepositories
{

    public interface IVReservationLogRepository : IGenericRepository<VreservationLog>
    {
        List<VreservationLog> GetAllNotAssignedReservations(Expression<Func<VreservationLog, bool>> predicate);
    }
}
