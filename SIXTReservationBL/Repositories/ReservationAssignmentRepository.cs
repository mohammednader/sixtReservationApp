using Microsoft.EntityFrameworkCore;
using SIXTReservationBL.CoreBL.IRepositories;
using SIXTReservationBL.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIXTReservationBL.Repositories
{
   public class ReservationAssignmentRepository:GenericRepository<ReservationAssignement>,IReservationAssignmentRepository
    {
        public ReservationAssignmentRepository(SixtReservationContext context):base(context)
        {

        }

        public List<ReservationAssignement> GetUserAssignment(long reservationId)
        {
            var reservation = new List<ReservationAssignement>();
            var result = Context.ReservationAssignement
                                                      .Include(r => r.FromUserNavigation)
                                                      .Include(r => r.ToUserNavigation)
                                                      .AsQueryable();
            if (reservationId != 0)
            {
                result = result.Where(r => r.ReservationNo == reservationId);
                reservation = result.ToList();
            }
            return reservation;
        }
    }
}
