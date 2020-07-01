using SIXTReservationBL.CoreBL.IRepositories;
using SIXTReservationBL.Models.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIXTReservationBL.Repositories
{
   public class EmailSettingRepository:GenericRepository<EmailSetting>,IEmailSettingRepository
    {
        public EmailSettingRepository(SixtReservationContext context):base(context)
        {

        }
    }
}
