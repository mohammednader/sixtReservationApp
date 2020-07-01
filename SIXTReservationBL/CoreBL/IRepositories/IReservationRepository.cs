using SIXTReservationBL.Models.Domain;
using SIXTReservationBL.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace SIXTReservationBL.CoreBL.IRepositories
{
    public interface IReservationRepository : IGenericRepository<Reservation>
    {
        void CopyObject(ref Reservation targetReservation, Reservation sourceReservation);
        List<Reservation> GetReservationWithDetails(Expression<Func<Reservation, bool>> predicate);
        List<Reservation> GetReservationWithDetails(ReservationSC reservationSC);
        Reservation GetReservationDetails(Expression<Func<Reservation, bool>> predicate);
    }
}
