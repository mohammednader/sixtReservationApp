using SIXTReservationBL.CoreBL.IRepositories;
using SIXTReservationBL.Models.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIXTReservationBL.Repositories
{
   public class JobTitleRepository:GenericRepository<JobTitle>,IJobTitleRepository
    {
        public JobTitleRepository(SixtReservationContext context):base(context)
        {

        }
    }
}
