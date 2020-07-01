using Microsoft.EntityFrameworkCore;
using SIXTReservationBL.CoreBL.IRepositories;
using SIXTReservationBL.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIXTReservationBL.Repositories
{
   public class FormSubmittedRepository:GenericRepository<ReservationFormSubmit>,IFormSubmittedRepository
    {
        public FormSubmittedRepository(SixtReservationContext context):base(context)
        {

        }

        public List<ReservationFormSubmit> GetFormSubmitDetails(long reservationId)
        {
            var reservation = new List<ReservationFormSubmit>();
            var result = Context.ReservationFormSubmit
                                                      .Include(r => r.CreatedUserNavigation)
                                                      .Include(r=>r.Reason)
                                                      .AsQueryable();
            if (reservationId != 0)
            {
                result = result.Where(r =>r.ReservationNo == reservationId);
                reservation = result.ToList();
            }
            return reservation;
        }
    }
}
