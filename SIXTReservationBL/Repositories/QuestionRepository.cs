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
   public class QuestionRepository:GenericRepository<Question> ,IQuestionRepository
    {
        public QuestionRepository(SixtReservationContext context):base(context)
        {

        }
        public List<Question> SearchQuestion(QuestionSC search)
        {
            var result = new List<Question>();
            try
            {
                var query = Context.Question
                                          .Include(r => r.ReservationStatusNavigation)
                                          .Include(r=>r.ActionStepNavigation)
                                          .AsQueryable();
                if (search != null)
                {
                    if (!string.IsNullOrEmpty(search.QuestionText))
                    {
                        query = query.Where(r => r.QuestionText != null && r.QuestionText.Contains(search.QuestionText));
                    }
                    if (search.ReservationStatus != null && search.ReservationStatus > 0)
                    {
                        query = query.Where(r => r.ReservationStatus != null && r.ReservationStatusNavigation.Id == search.ReservationStatus);
                    }
                    if (search.ActionStep != null && search.ActionStep > 0)
                    {
                        query = query.Where(r => r.ActionStep != null && r.ActionStep == search.ActionStep);
                    }

                }
                result = query.ToList();
            }
            catch { }

            return result;
        }
    }
}
