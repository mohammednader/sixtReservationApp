using SIXTReservationBL.CoreBL.IRepositories;
using SIXTReservationBL.Models.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIXTReservationBL.Repositories
{
    public class ReservationStatusRepository : GenericRepository<ReservationStatus>, IReservationStatusRepository
    {
        public ReservationStatusRepository(SixtReservationContext context) : base(context)
        {

        }
        public int GetReservationStatusOffLine(string status)
        {
            switch (status.ToLower())
            {
                case "cancellation by customer": return 1;
                case "cancellation by sixt": return 2;
                case "no show": return 3;
                case "open": return 4;
                case "invoice": return 5;

                default: return -1;
            }
        }

    }
}
