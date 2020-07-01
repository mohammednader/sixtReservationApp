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
   public class ReasonRepository :GenericRepository<Reason>,IReasonRepository
    {
        public ReasonRepository(SixtReservationContext context):base(context)
        {

        }
        public List<Reason> SearchReason(ReasonSC search)
        {
            var result = new List<Reason>();
            try
            {
                var query = Context.Reason
                                          .Include(r=>r.ReservationStatus)
                                          .AsQueryable();
                if (search != null)
                {
                    if (!string.IsNullOrEmpty(search.Reason))
                    {
                        query = query.Where(r => r.ReasonText != null && r.ReasonText.Contains(search.Reason));
                    }
                    if (search.ReservationStatus != null && search.ReservationStatus > 0)
                    {
                        query = query.Where(r => r.ReservationStatusId != null && r.ReservationStatus.Id == search.ReservationStatus);
                    }
                    if (search.IsAnswer==1)
                    {
                        query = query.Where(r=> r.IsAnswer == true);
                    }
                    if (search.IsAnswer == 2)
                    {
                        query = query.Where(r => r.IsAnswer == false);
                    }
                    if (search.IsAnswer==3)
                    {
                        query = query.Where(r => r.IsAnswer == null);
                    }

                }
                result = query.ToList();
            }
            catch { }

            return result;
        }

    }
}
