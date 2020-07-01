using SIXTReservationBL.CoreBL.IRepositories;
using SIXTReservationBL.Models.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIXTReservationBL.Repositories
{
    public class RateSegmentRepository : GenericRepository<RateSegment>, IRateSegmentRepository
    {
        public RateSegmentRepository(SixtReservationContext context) : base(context)
        {

        }

    }

    public class RateSegmentCategoryRepository : GenericRepository<RateSegmentCategory>, IRateSegmentCategoryRepository
    {
        public RateSegmentCategoryRepository(SixtReservationContext context) : base(context)
        {

        }

    }


}
