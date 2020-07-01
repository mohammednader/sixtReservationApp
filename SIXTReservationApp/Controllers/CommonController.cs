using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SIXTReservationApp.Auth;
using SIXTReservationBL.CoreBL;
using SIXTReservationBL.Models;
using SIXTReservationBL.Models.Domain;

namespace SIXTReservationApp.Controllers
{

    public class CommonController : Controller
    {
        private readonly IUnitOfWork UnitOfWork;
        public CommonController(IUnitOfWork _unitOfWork)
        {
            UnitOfWork = _unitOfWork;
        }

        public JsonResult GetAllRoles()
        {
            var roles = UnitOfWork.RoleBL.GetAll();
            var response = roles.Select(r => new { r.Id, r.Name }).ToArray();
            return Json(response);
        }

        public JsonResult GetAllJobTitles()
        {
            var roles = UnitOfWork.JobTitleBL.GetAll();
            var response = roles.Select(r => new { Id = r.JobTitleId, r.Name }).ToArray();
            return Json(response);
        }
        public JsonResult GetAllBranches()
        {
            var roles = UnitOfWork.BranchBL.GetAll();
            var response = roles.Select(r => new { r.Id, r.Name }).ToArray();
            return Json(response);
        }

        public JsonResult GetAllWeekDays()
        {
            var days = UnitOfWork.WeekDayBL.GetAll();
            var response = days.Select(r => new { Id = r.Id, Name = r.WeekDayName }).ToArray();
            return Json(response);
        }
        public JsonResult GetAllReservationStatus()
        {
            var status = UnitOfWork.ReservationStatusBL.GetAll();
            var response = status.Select(r => new { r.Id, r.Status }).ToArray();
            return Json(response);
        }
        public JsonResult GetInvocedReservationStatus()
        {
            var status = UnitOfWork.ReservationStatusBL.Find(r => r.Id == 5);
            var response = status.Select(r => new { r.Id, r.Status }).ToArray();
            return Json(response);
        }

        public JsonResult GetAllRateSegementCategory()
        {
            var status = UnitOfWork.RateSegmentCategoryBL.GetAll();
            var response = status.Select(r => new { r.Id, Name = r.RateSegmentCategoryName }).ToArray();
            return Json(response);
        }


        public JsonResult GetReservationStep(int Id)
        {
            try
            {
                if (Id > 0)
                {
                    var actionSteps = UnitOfWork.StatusStepBL.GetStatusStepByReservationStatusId(Id);
                    var result = actionSteps.Select(r => new { Id = r.StepId, step = r.StepName }).ToArray();
                    return Json(result);
                }
                else
                {
                    return Json(null);
                }
            }


            catch (Exception)
            {

                throw;
            }
        }
        public JsonResult GetReservationStepByRSId(int Id)
        {
            try
            {
                if (Id > 0)
                {
                    var actionSteps = UnitOfWork.StatusStepBL.GetAllStatusStepByReservationStatusId(Id);
                    var result = actionSteps.Select(r => new { Id = r.StepId, step = r.StepName }).OrderBy(s => s.Id).ToArray();
                    return Json(result);
                }
                else
                {
                    return Json(null);
                }
            }


            catch (Exception)
            {

                throw;
            }
        }

        public JsonResult GetReservationStatus()
        {
            var status = Enum.GetValues(typeof(SixtCancellationStatusEnum))
                            .Cast<SixtCancellationStatusEnum>()
                            .Select(item => new
                            {
                                Id = (int)item,
                                Name = item.ToString(),
                            }).ToArray();

            return Json(status);
        }

        public JsonResult GetWeekDays()
        {
            var days = Enum.GetValues(typeof(WeekDayEnum))
                            .Cast<WeekDayEnum>()
                            .Select(item => new
                            {
                                Id = (int)item,
                                Name = item.ToString(),
                            }).ToArray();

            return Json(days);
        }
    }
}