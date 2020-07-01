using SIXTReservationBL.CoreBL.IRepositories;
using SIXTReservationBL.Models.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIXTReservationBL.Repositories
{
   public class WeekDayRepository:GenericRepository<Weekdays>,IWeekDayRepository
    {
        public WeekDayRepository(SixtReservationContext context):base(context)
        {

        }
    }
}
