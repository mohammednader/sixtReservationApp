using SIXTReservationBL.Models.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIXTReservationBL.CoreBL.IRepositories
{
   public interface IReservationAssignmentRepository:IGenericRepository<ReservationAssignement>
    {
       List<ReservationAssignement> GetUserAssignment(long reservationId);
    }
}
