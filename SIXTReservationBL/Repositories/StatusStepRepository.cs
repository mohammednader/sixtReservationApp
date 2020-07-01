using SIXTReservationBL.CoreBL.IRepositories;
using SIXTReservationBL.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Text;

namespace SIXTReservationBL.Repositories
{
   public class StatusStepRepository:GenericRepository<StatusStep>,IStatusStepRepository
    {
        public StatusStepRepository(SixtReservationContext context):base(context)
        {

        }
        public List<StatusStep> GetStatusStepByReservationStatusId(int reservationStatusId)
        {   
            var status = Context.StatusStep.Where(s=>s.ReseravationStatus==reservationStatusId && s.IsNotification==true);
            var response = status.OrderBy(s=>s.StepOrder).ToList();
            
            return response;
        }
        public List<StatusStep> GetAllStatusStepByReservationStatusId(int reservationStatusId)
        {
            var status = Context.StatusStep.Where(s => s.ReseravationStatus == reservationStatusId).Where(s=>s.StepId!=36);
            var response = status.OrderBy(s => s.StepOrder).ToList();

            return response;
        }
    }
}
