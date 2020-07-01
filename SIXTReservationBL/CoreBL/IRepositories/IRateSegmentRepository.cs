using SIXTReservationBL.Models.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIXTReservationBL.CoreBL.IRepositories
{
    public interface IRateSegmentRepository : IGenericRepository<RateSegment>
    {
    } 
    
    
    public interface IRateSegmentCategoryRepository : IGenericRepository<RateSegmentCategory>
    {
    }

}
