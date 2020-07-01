using Microsoft.EntityFrameworkCore;
using SIXTReservationBL.CoreBL.IRepositories;
using SIXTReservationBL.Models.Domain;
using SIXTReservationBL.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIXTReservationBL.Repositories
{

    public class UploadLogRepository : GenericRepository<UploadLog>, IUploadLogRepository
    {
        public UploadLogRepository(SixtReservationContext context) : base(context)
        {

        }

        public List<UploadLog> GetAllUploadsWithDetails(UploadSC uploadSC)
        {
            try
            {
                var query = Context.UploadLog.Include(u => u.User)
                                                         .AsQueryable();

                if (uploadSC != null)
                {
                    if (uploadSC.UploadId > 0)
                    {
                        query = query.Where(u => u.Id == uploadSC.UploadId);
                    }
                    if (uploadSC.UploadStatusId?.Length > 0 && !uploadSC.UploadStatusId.Contains(null))
                    {
                        query = query.Where(u => uploadSC.UploadStatusId.Contains(u.UploadStatus.GetValueOrDefault()));
                    }
                    if (uploadSC.UploadDateFrom.HasValue && uploadSC.UploadDateFrom.Value != DateTime.MinValue)
                    {
                        query = query.Where(u => u.CreationTime.Value.Date >= uploadSC.UploadDateFrom);
                    }
                    if (uploadSC.UploadDateTo.HasValue && uploadSC.UploadDateTo.Value != DateTime.MinValue)
                    {
                        query = query.Where(u => u.CreationTime.Value.Date <= uploadSC.UploadDateTo);
                    }
                }

                return query.OrderByDescending(u => u.CreationTime).ToList();
            }
            catch (Exception e)
            {
                return null;
            }
        }



    }
}
