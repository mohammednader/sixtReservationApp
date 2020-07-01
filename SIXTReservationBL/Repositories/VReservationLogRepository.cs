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

    public class VReservationLogRepository : GenericRepository<VreservationLog>, IVReservationLogRepository
    {
        public VReservationLogRepository(SixtReservationContext context) : base(context)
        {

        }

        public List<VreservationLog> GetAllNotAssignedReservations(Expression<Func<VreservationLog,bool>> predicate)
        {
            var result = new List<VreservationLog>();
            try
            {
                var query = Context.VreservationLog.AsQueryable();
                if (predicate != null)
                {
                    result = query.Where(predicate).ToList();
                    return result;
                }
                return null;


            }
            catch (Exception e)
            {

                return null;
            }

        }

    }
}
