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
   public class ActionSettingRepository:GenericRepository<ActionSetting>,IActionSettingRepository
    {
        public ActionSettingRepository(SixtReservationContext context):base(context)
        {

        }

        public List<ActionSetting> SearchActionSetting(ActionSettingSC search)
        {
            var result = new List<ActionSetting>();
            try
            {
                var query = Context.ActionSetting
                                          .Include(r => r.ReservationStatus)
                                          .Include(r => r.ActionStep)
                                          .Include(r => r.RateSegmentCategory)
                                          .Include(r => r.Branch)
                                          .Include(r=>r.WeekDay)
                                          .AsQueryable();
                if (search != null)
                {

                    if (search.ReservationStatusId != null && search.ReservationStatusId > 0)
                    {
                        query = query.Where(r => r.ReservationStatus != null && r.ReservationStatus.Id == search.ReservationStatusId);
                    }
                    if (search.ActionStepId != null && search.ActionStepId > 0)
                    {
                        query = query.Where(r => r.ActionStepId != null && r.ActionStepId == search.ActionStepId);
                    }
                    if (search.BranchId != null && search.BranchId > 0)
                    {
                        query = query.Where(r => r.BranchId != null && r.BranchId == search.BranchId);
                    }
                    if (search.RateSegmentCategoryId != null && search.RateSegmentCategoryId > 0)
                    {
                        query = query.Where(r => r.RateSegmentCategoryId != null && r.RateSegmentCategoryId == search.RateSegmentCategoryId);
                    }
                    if (search.WeekDayId != null && search.WeekDayId > 0)
                    {
                        query = query.Where(r => r.WeekDayId != null && r.WeekDayId == search.WeekDayId);
                    }
                }
                result = query.ToList();
            }
            catch { }

            return result;
        }
    }
}
