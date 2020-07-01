using SIXTReservationBL.Models.Domain;
using SIXTReservationBL.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIXTReservationBL.CoreBL.IRepositories
{
   public interface IQuestionRepository:IGenericRepository<Question>
    {
        List<Question> SearchQuestion(QuestionSC search);
    }
}
