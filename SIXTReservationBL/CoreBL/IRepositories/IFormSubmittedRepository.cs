using SIXTReservationBL.Models.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIXTReservationBL.CoreBL.IRepositories
{
    public interface IFormSubmittedRepository:IGenericRepository<ReservationFormSubmit>
    {
        List<ReservationFormSubmit> GetFormSubmitDetails(long reservationId);
    }
}
