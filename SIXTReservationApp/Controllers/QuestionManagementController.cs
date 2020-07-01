using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SIXTReservationApp.Auth;
using SIXTReservationApp.Models.Question;
using SIXTReservationBL.CoreBL;
using SIXTReservationBL.Models.Domain;
using SIXTReservationBL.Models.ViewModels;

namespace SIXTReservationApp.Controllers
{
    [AppAuthorize]
    public class QuestionManagementController : Controller
    {
        private readonly IUnitOfWork UnitOfWork;
        public QuestionManagementController(IUnitOfWork _unitOfWork)
        {
            UnitOfWork = _unitOfWork;
        }
        [PermissionNotRequired]
        public IActionResult QuestionIndex()
        {
            return View();
        }

        public PartialViewResult _QuestionList(QuestionSC search)
        {
            QuestionVM[] Questions = null;
            try
            {
                Questions = UnitOfWork.QuestionBL.SearchQuestion(search).Select(b => new QuestionVM(b)).ToArray();
            }
            catch (Exception e)
            {
                Questions = null;
            }

            return PartialView(Questions);

        }

        public PartialViewResult CreateQuestion()
        {
            ViewBag.Title = "Create Question";
            return PartialView("_CreateQuestion", new QuestionVM());
        }

        [HttpPost]
        public JsonResult CreateQuestion(QuestionVM model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(null);
                }
                else
                {
                    var QuestionExist = UnitOfWork.QuestionBL.CheckExist(b => b.QuestionText.ToLower() == model.QuestionText.ToLower() && b.ReservationStatus == model.ReservationStatus);
                    if (QuestionExist)
                    {
                        return Json(new { success = false, Message = "Question with same name already exists" });
                    }
                    var Question = new Question
                    {
                        QuestionText = model.QuestionText,
                        ReservationStatus = model.ReservationStatus,
                        ActionStep = model.ActionStep
                    };
                    UnitOfWork.QuestionBL.Add(Question);
                    if (UnitOfWork.Complete() > 0)
                    {
                        return Json(new { success = true, Message = "Question added successfully" });
                    }
                    else
                    {
                        return Json(new { success = false, Message = "Failed to add Question" });

                    }
                }
            }
            catch (Exception)
            {
                return Json(new { success = false, Message = "An error occured , please try again later" });
            }

        }

        public PartialViewResult UpdateQuestion(int id)
        {
            ViewBag.Title = "Update Question";
            var model = UnitOfWork.QuestionBL.GetByID(id);
            return PartialView("_CreateQuestion", new QuestionVM(model));
        }


        [HttpPost]
        public JsonResult UpdateQuestion(QuestionVM model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(null);
                }
                else
                {
                    var QuestionExist = UnitOfWork.QuestionBL.CheckExist(b => b.QuestionText.ToLower() == model.QuestionText.ToLower() && b.ReservationStatus == model.ReservationStatus);
                    if (QuestionExist)
                    {
                        return Json(new { success = false, Message = "Question with same name already exists" });
                    }
                    var Question = UnitOfWork.QuestionBL.GetByID(model.Id);
                    if (Question != null)
                    {
                        Question.QuestionText = model.QuestionText;
                        Question.ReservationStatus = model.ReservationStatus;
                        Question.ActionStep = model.ActionStep;

                        UnitOfWork.QuestionBL.Update(Question);
                        if (UnitOfWork.Complete() > 0)
                        {
                            return Json(new { success = true, Message = "Question updated successfully" });
                        }
                        else
                        {
                            return Json(new { success = false, Message = "Failed to update Question" });

                        }
                    }
                    else
                    {
                        return Json(new { success = false, Message = "Question not found" });
                    }
                }
            }
            catch (Exception e)
            {
                return Json(new { success = false, Message = "An error occured , please try again later" });
            }
        }

        [HttpPost]
        public JsonResult DeleteQuestion(int id)
        {
            try
            {
                var question = UnitOfWork.QuestionBL.GetByID(id);
                if (question != null)
                {
                    UnitOfWork.QuestionBL.Remove(question);
                    if (UnitOfWork.Complete() > 0)
                    {
                        return Json(new
                        {
                            Success = true,
                            Message = "Question deleted successfully",
                        });
                    }
                    else
                    {
                        return Json(new
                        {
                            Success = false,
                            Message = "Failed to delete Question",
                        });
                    }

                }
                else
                {
                    return Json(new
                    {
                        Success = false,
                        Message = "Question not found",
                    });
                }

            }
            catch (Exception)
            {
                return Json(new { success = false, Message = "An error occured , please try again later" });
            }


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
}
}