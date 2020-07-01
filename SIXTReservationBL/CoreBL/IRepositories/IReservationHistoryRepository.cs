using SIXTReservationBL.Models.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIXTReservationBL.CoreBL.IRepositories
{
    public interface IReservationHistoryRepository : IGenericRepository<ReservationHistory>
    {
        ReservationHistory GetHistoryObject(Reservation reservation, int loggedUserId);

    }
}
