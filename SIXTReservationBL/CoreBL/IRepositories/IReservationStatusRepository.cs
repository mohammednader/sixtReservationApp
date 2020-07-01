using SIXTReservationBL.Models.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIXTReservationBL.CoreBL.IRepositories
{
    public interface IReservationStatusRepository:IGenericRepository<ReservationStatus>
    {
          int GetReservationStatusOffLine(string status);
         
    }
}
