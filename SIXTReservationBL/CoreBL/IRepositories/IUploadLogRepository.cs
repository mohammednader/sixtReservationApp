using SIXTReservationBL.Models.Domain;
using SIXTReservationBL.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIXTReservationBL.CoreBL.IRepositories
{
    
    public interface IUploadLogRepository : IGenericRepository<UploadLog>
    {
        List<UploadLog> GetAllUploadsWithDetails(UploadSC uploadSC);
    }
}
