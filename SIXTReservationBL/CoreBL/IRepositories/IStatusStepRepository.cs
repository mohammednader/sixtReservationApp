using SIXTReservationBL.Models.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIXTReservationBL.CoreBL.IRepositories
{
    public interface IStatusStepRepository:IGenericRepository<StatusStep>
    {
        List<StatusStep> GetStatusStepByReservationStatusId(int reservationStatusId);
        List<StatusStep> GetAllStatusStepByReservationStatusId(int reservationStatusId);
    }
}
