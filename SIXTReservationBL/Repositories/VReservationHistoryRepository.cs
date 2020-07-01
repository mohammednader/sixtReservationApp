using SIXTReservationBL.CoreBL.IRepositories;
using SIXTReservationBL.Models.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIXTReservationBL.Repositories
{
   public class VReservationHistoryRepository:GenericRepository<VreservationHistory>, IVReservationHistoryRepository
    {
        public VReservationHistoryRepository(SixtReservationContext context):base(context)
        {

        }
    }
}
