using SIXTReservationBL.CoreBL.IRepositories;
using SIXTReservationBL.Models;
using SIXTReservationBL.Models.Domain;
using SIXTReservationBL.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace SIXTReservationBL.Repositories
{
    public class VReservationListRepository : GenericRepository<VreservationList>, IVReservationListRepository
    {
        public VReservationListRepository(SixtReservationContext context) : base(context)
        { }


        public PaginationResult<VReservationListModel> GetAllReservations(ReservationSC reservationSC, int? page = null, int? pageSize = null)
        {
            var result = new PaginationResult<VReservationListModel>
            {
                Page = page ?? 0,
                PageSize = pageSize ?? 0,
            };

            try
            {
                var query = Context.VreservationList.AsQueryable();
                if (reservationSC != null)
                {
                    if (reservationSC.ReservationNum.HasValue && reservationSC.ReservationNum.Value > 0)
                    {
                        query = query.Where(r => r.ReservationNum == reservationSC.ReservationNum);
                    }
                    if (reservationSC.ReservationId.HasValue && reservationSC.ReservationId.Value > 0)
                    {
                        query = query.Where(r => r.Id == reservationSC.ReservationId);
                    }

                    if (reservationSC.ReservationStatusId.HasValue && reservationSC.ReservationStatusId.Value > 0)
                    {
                        query = query.Where(r => r.ReservationStatusId == reservationSC.ReservationStatusId);
                    }
                    if (reservationSC.BookingDateFrom.HasValue && reservationSC.BookingDateFrom.Value != DateTime.MinValue)
                    {
                        query = query.Where(r => r.BookingDate >= reservationSC.BookingDateFrom);
                    }
                    if (reservationSC.BookingDateTo.HasValue && reservationSC.BookingDateTo.Value != DateTime.MinValue)
                    {
                        query = query.Where(r => r.BookingDate <= reservationSC.BookingDateTo);
                    }
                    if (reservationSC.PickUpDateFrom.HasValue && reservationSC.PickUpDateFrom.Value != DateTime.MinValue)
                    {
                        query = query.Where(r => r.PickUpDate >= reservationSC.PickUpDateFrom);
                    }
                    if (reservationSC.PickUpDateTo.HasValue && reservationSC.PickUpDateTo.Value != DateTime.MinValue)
                    {
                        query = query.Where(r => r.PickUpDate <= reservationSC.PickUpDateTo);
                    }

                    if (reservationSC.CancelDateFrom.HasValue && reservationSC.CancelDateFrom.Value != DateTime.MinValue)
                    {
                        query = query.Where(r => r.CancelledDate >= reservationSC.CancelDateFrom);
                    }
                    if (reservationSC.CancelDateTo.HasValue && reservationSC.CancelDateTo.Value != DateTime.MinValue)
                    {
                        query = query.Where(r => r.CancelledDate <= reservationSC.CancelDateTo);
                    }

                    if (reservationSC.DropOffDate.HasValue && reservationSC.DropOffDate.Value != DateTime.MinValue)
                    {
                        query = query.Where(r => r.DropOffDate <= reservationSC.DropOffDate);
                    }

                    if (!string.IsNullOrWhiteSpace(reservationSC.CustomerName))
                    {
                        query = query.Where(r => r.CustomerName.Contains(reservationSC.CustomerName));
                    }
                    if (!string.IsNullOrWhiteSpace(reservationSC.VehicleAcriss))
                    {
                        query = query.Where(r => r.VehicleAcriss == reservationSC.VehicleAcriss);
                    }
                    if (reservationSC.CancelledAfterBookingDays != null)
                    {
                        query = query.Where(r => r.CancelledAfterBookingInDays == reservationSC.CancelledAfterBookingDays);
                    }
                    if (reservationSC.RentalDays.HasValue && reservationSC.RentalDays.Value > 0)
                    {
                        query = query.Where(r => r.RentalDays == reservationSC.RentalDays);
                    }

                    if (reservationSC.PickUpBranchIds?.Length > 0 && !reservationSC.PickUpBranchIds.Contains(null))
                    {
                        query = query.Where(r => reservationSC.PickUpBranchIds.Contains(r.PickUpBranchId.GetValueOrDefault()));
                    }
                    DateTime yesterday = DateTime.Now.AddDays(-1);
                    if (reservationSC.ActionStepId.HasValue && reservationSC.ActionStepId.Value > 0)
                    {
                        if (reservationSC.ActionStepId == 1) // need assignment 
                        {
                            query = query.Where(r => r.StepId == ((int)CacelledByCustomerStepEnum.ContactCenterAgentAssignment) && r.IsDone == false);
                        }

                        //else if (reservationSC.ActionStepId == 2) // notified but no asignment 
                        //{

                        //    query = query.Where(r => r.StepId == ((int)CacelledByCustomerStepEnum.ContactCenterAgentAssignment) && r.IsDone == false && r.DiffDateAssignSubmit >= 24); //
                        //}

                        //else if (reservationSC.ActionStepId == 3) // assigned but no form sumbitted 
                        //{
                        //    query = query.Where(r => r.StepId == ((int)CacelledByCustomerStepEnum.AgentFormSumbitted) && r.IsDone == false && r.DiffDateAssignSubmit >= 24);//
                        //}

                        else if (reservationSC.ActionStepId == 4) // form not sumbitted 
                        {
                            query = query.Where(r => r.StepId == ((int)CacelledByCustomerStepEnum.AgentFormSumbitted) && r.IsDone == false);
                        }

                        //else if (reservationSC.ActionStepId == 5) // Assigned ToMe 
                        //{
                        //    query = query.Where(r => r.ToUser ==
                        //    reservationSC.AssignedToId && r.StepId == ((int)CacelledByCustomerStepEnum.ContactCenterAgentAssignment) && r.IsDone == true);
                        //}
                    }
                    //if (  reservationSC.AssignedToMe == true)
                    //{
                    //    query = query.Where(r => r.ToUser == reservationSC.AssignedToId);
                    // }

                    var temp = query.OrderBy(s => s.StepId).ToList();
                    var Group = temp
                                    .GroupBy(b => b.ReservationNum)
                                    .Select(grp => new VReservationListModel
                                    {
                                        ReservationNo = grp.Key,
                                        ReservationList = grp.LastOrDefault(),
                                        NextStep = grp.LastOrDefault(s => s.IsDone == false) != null ?
                                                                    grp.LastOrDefault(s => s.IsDone == false).StepName :
                                                                    string.Empty,
                                        IsAssigned = grp.Any(s => s.StepId == ((int)CacelledByCustomerStepEnum.ContactCenterAgentAssignment) && s.IsDone == true),
                                        IsFormSubmitted = grp.Any(s => s.StepId == ((int)CacelledByCustomerStepEnum.AgentFormSumbitted) && s.IsDone == true),
                                        IsNeedToAssign = grp.Any(s => s.StepId == ((int)CacelledByCustomerStepEnum.ContactCenterAgentAssignment) && s.IsDone == false),
                                        DiffDateNotify = grp.LastOrDefault(s => s.StepId == ((int)CacelledByCustomerStepEnum.ContactCenterManagmentNotification))?.DiffDateAssignSubmit ?? 0,

                                        //Assignee = grp
                                        //               .LastOrDefault(s => s.StepId == ((int)CacelledByCustomerStepEnum.ContactCenterAgentAssignment)) != null ?
                                        //               grp.LastOrDefault(s => s.StepId == ((int)CacelledByCustomerStepEnum.ContactCenterAgentAssignment)).AssignToName :
                                        //               string.Empty,

                                        //AssignFrom = grp.LastOrDefault(s => s.StepId == ((int)CacelledByCustomerStepEnum.ContactCenterAgentAssignment)) != null ? grp.LastOrDefault(s => s.StepId == ((int)CacelledByCustomerStepEnum.ContactCenterAgentAssignment)).AssignFromName : "",
                                        //AssignCreationDate = grp.LastOrDefault(s => s.StepId == ((int)CacelledByCustomerStepEnum.ContactCenterAgentAssignment)) != null ? grp.LastOrDefault(s => s.StepId == ((int)CacelledByCustomerStepEnum.ContactCenterAgentAssignment)).AssignCreationDate.ToString() : ""
                                        //,
                                        AssignmentModel = grp.Any(s => s.StepId == ((int)CacelledByCustomerStepEnum.ContactCenterAgentAssignment) && s.IsDone == true) ?
                                        new AssignmentModel()
                                        {
                                            AssignID = grp.LastOrDefault(s => s.StepId == ((int)CacelledByCustomerStepEnum.ContactCenterAgentAssignment)).AssignId.ToString(),
                                            AssignFromName = grp.LastOrDefault(s => s.StepId == ((int)CacelledByCustomerStepEnum.ContactCenterAgentAssignment)).AssignFromName,
                                            AssignToName = grp.LastOrDefault(s => s.StepId == ((int)CacelledByCustomerStepEnum.ContactCenterAgentAssignment)).AssignToName,
                                            AssignmentDate = grp.LastOrDefault(s => s.StepId == ((int)CacelledByCustomerStepEnum.ContactCenterAgentAssignment)).AssignCreationDate.ToString(),
                                            AssignToId = grp.LastOrDefault(s => s.StepId == ((int)CacelledByCustomerStepEnum.ContactCenterAgentAssignment)).ToUser ?? 0,
                                            DiffDate = grp.LastOrDefault(s => s.StepId == ((int)CacelledByCustomerStepEnum.ContactCenterAgentAssignment))?.DiffDateAssignSubmit ?? 0,

                                        } : null,
                                        FormSumbitModel = grp.Any(s => s.StepId == ((int)CacelledByCustomerStepEnum.AgentFormSumbitted) && s.IsDone == true) ?
                                        new FormSumbitModel()
                                        {
                                            FormSubmitID = grp.LastOrDefault(s => s.StepId == ((int)CacelledByCustomerStepEnum.AgentFormSumbitted)).FormSubmitId.ToString(),
                                            FormSubmitedBy = grp.LastOrDefault(s => s.StepId == ((int)CacelledByCustomerStepEnum.AgentFormSumbitted)).FormSubmitCreatedByName.ToString(),
                                            ReasonStatus = grp.LastOrDefault(s => s.StepId == ((int)CacelledByCustomerStepEnum.AgentFormSumbitted)).ReasonStatus.ToString(),
                                            ReasonText = grp.LastOrDefault(s => s.StepId == ((int)CacelledByCustomerStepEnum.AgentFormSumbitted)).ReasonText.ToString(),
                                            FormSubmitionDate = grp.LastOrDefault(s => s.StepId == ((int)CacelledByCustomerStepEnum.AgentFormSumbitted)).FormSubmitCreationDate.ToString(),
                                            FormSubmitedComment = grp.LastOrDefault(s => s.StepId == ((int)CacelledByCustomerStepEnum.AgentFormSumbitted)).Comment ?? string.Empty,
                                        } : null
                                    });

                    if (reservationSC.ActionStepId.HasValue && reservationSC.ActionStepId.Value > 0)
                    {
                        if (reservationSC.ActionStepId == 2) // notified but no asignment 
                        {
                            //query = query.Where(r => r.StepId == ((int)CacelledByCustomerStepEnum.ContactCenterAgentAssignment) && r.IsDone == false && r.DiffDateAssignSubmit >= 24); //
                            Group = Group.Where(g => g.IsAssigned != true && g.IsNeedToAssign == true && g.DiffDateNotify >= 24);//
                        }
                        else if (reservationSC.ActionStepId == 3) // assigned but no form sumbitted 
                        {
                            Group = Group.Where(g => g.IsFormSubmitted != true && g.IsAssigned == true && g.AssignmentModel.DiffDate >= 24);//
                        }
                        else if (reservationSC.ActionStepId == 5) // Assigned ToMe 
                        {
                            Group = Group.Where(g => g.IsFormSubmitted != true && g.IsAssigned == true && g.AssignmentModel.AssignToId == reservationSC.AssignedToId);
                        }
                    }
                    result.Total = Group.Count();
                    Group = Group.OrderByDescending(p => p.ReservationNo);
                    if (page.GetValueOrDefault() > 0 && pageSize.GetValueOrDefault() > 0)
                    {
                        Group = Group
                                    .Skip((page.Value - 1) * pageSize.Value)
                                    .Take(pageSize.Value);
                    }
                    result.Items = Group.ToList();

                    return result;

                }
                return null;


            }
            catch (Exception e)
            {

                return null;
            }



        }
        public List<VReservationListModel> GetReservations(ReservationSC reservationSC)
        {
            try
            {
                var query = Context.VreservationList.AsQueryable();
                if (reservationSC != null)
                {
                    if (reservationSC.ReservationNum.HasValue && reservationSC.ReservationNum.Value > 0)
                    {
                        query = query.Where(r => r.ReservationNum == reservationSC.ReservationNum);
                    }
                    if (reservationSC.ReservationId.HasValue && reservationSC.ReservationId.Value > 0)
                    {
                        query = query.Where(r => r.Id == reservationSC.ReservationId);
                    }

                    if (reservationSC.ReservationStatusId.HasValue && reservationSC.ReservationStatusId.Value > 0)
                    {
                        query = query.Where(r => r.ReservationStatusId == reservationSC.ReservationStatusId);
                    }
                    if (reservationSC.BookingDateFrom.HasValue && reservationSC.BookingDateFrom.Value != DateTime.MinValue)
                    {
                        query = query.Where(r => r.BookingDate >= reservationSC.BookingDateFrom);
                    }
                    if (reservationSC.BookingDateTo.HasValue && reservationSC.BookingDateTo.Value != DateTime.MinValue)
                    {
                        query = query.Where(r => r.BookingDate <= reservationSC.BookingDateTo);
                    }
                    if (reservationSC.PickUpDateFrom.HasValue && reservationSC.PickUpDateFrom.Value != DateTime.MinValue)
                    {
                        query = query.Where(r => r.PickUpDate >= reservationSC.PickUpDateFrom);
                    }
                    if (reservationSC.PickUpDateTo.HasValue && reservationSC.PickUpDateTo.Value != DateTime.MinValue)
                    {
                        query = query.Where(r => r.PickUpDate <= reservationSC.PickUpDateTo);
                    }

                    if (reservationSC.CancelDateFrom.HasValue && reservationSC.CancelDateFrom.Value != DateTime.MinValue)
                    {
                        query = query.Where(r => r.CancelledDate >= reservationSC.CancelDateFrom);
                    }
                    if (reservationSC.CancelDateTo.HasValue && reservationSC.CancelDateTo.Value != DateTime.MinValue)
                    {
                        query = query.Where(r => r.CancelledDate <= reservationSC.CancelDateTo);
                    }

                    if (reservationSC.DropOffDate.HasValue && reservationSC.DropOffDate.Value != DateTime.MinValue)
                    {
                        query = query.Where(r => r.DropOffDate <= reservationSC.DropOffDate);
                    }

                    if (!string.IsNullOrWhiteSpace(reservationSC.CustomerName))
                    {
                        query = query.Where(r => r.CustomerName.Contains(reservationSC.CustomerName));
                    }
                    if (!string.IsNullOrWhiteSpace(reservationSC.VehicleAcriss))
                    {
                        query = query.Where(r => r.VehicleAcriss == reservationSC.VehicleAcriss);
                    }
                    if (reservationSC.CancelledAfterBookingDays != null)
                    {
                        query = query.Where(r => r.CancelledAfterBookingInDays == reservationSC.CancelledAfterBookingDays);
                    }
                    if (reservationSC.RentalDays.HasValue && reservationSC.RentalDays.Value > 0)
                    {
                        query = query.Where(r => r.RentalDays == reservationSC.RentalDays);
                    }

                    if (reservationSC.PickUpBranchIds?.Length > 0 && !reservationSC.PickUpBranchIds.Contains(null))
                    {
                        query = query.Where(r => reservationSC.PickUpBranchIds.Contains(r.PickUpBranchId.GetValueOrDefault()));
                    }
                    DateTime yesterday = DateTime.Now.AddDays(-1);
                    if (reservationSC.ActionStepId.HasValue && reservationSC.ActionStepId.Value > 0)
                    {
                        if (reservationSC.ActionStepId == 1) // need assignment 
                        {
                            query = query.Where(r => r.StepId == ((int)CacelledByCustomerStepEnum.ContactCenterAgentAssignment) && r.IsDone == false);
                        }

                        //else if (reservationSC.ActionStepId == 2) // notified but no asignment 
                        //{

                        //    query = query.Where(r => r.StepId == ((int)CacelledByCustomerStepEnum.ContactCenterAgentAssignment) && r.IsDone == false && r.DiffDateAssignSubmit >= 24); //
                        //}

                        //else if (reservationSC.ActionStepId == 3) // assigned but no form sumbitted 
                        //{
                        //    query = query.Where(r => r.StepId == ((int)CacelledByCustomerStepEnum.AgentFormSumbitted) && r.IsDone == false && r.DiffDateAssignSubmit >= 24);//
                        //}

                        else if (reservationSC.ActionStepId == 4) // form not sumbitted 
                        {
                            query = query.Where(r => r.StepId == ((int)CacelledByCustomerStepEnum.AgentFormSumbitted) && r.IsDone == false);
                        }

                        //else if (reservationSC.ActionStepId == 5) // Assigned ToMe 
                        //{
                        //    query = query.Where(r => r.ToUser ==
                        //    reservationSC.AssignedToId && r.StepId == ((int)CacelledByCustomerStepEnum.ContactCenterAgentAssignment) && r.IsDone == true);
                        //}
                    }
                    //if (  reservationSC.AssignedToMe == true)
                    //{
                    //    query = query.Where(r => r.ToUser == reservationSC.AssignedToId);
                    // }

                    var temp = query.OrderBy(s => s.StepId).ToList();
                    var Group = temp
                                    .GroupBy(b => b.ReservationNum)
                                    .Select(grp => new VReservationListModel
                                    {
                                        ReservationNo = grp.Key,
                                        ReservationList = grp.LastOrDefault(),
                                        NextStep = grp.LastOrDefault(s => s.IsDone == false) != null ?
                                                                    grp.LastOrDefault(s => s.IsDone == false).StepName :
                                                                    string.Empty,
                                        IsAssigned = grp.Any(s => s.StepId == ((int)CacelledByCustomerStepEnum.ContactCenterAgentAssignment) && s.IsDone == true),
                                        IsFormSubmitted = grp.Any(s => s.StepId == ((int)CacelledByCustomerStepEnum.AgentFormSumbitted) && s.IsDone == true),
                                        IsNeedToAssign = grp.Any(s => s.StepId == ((int)CacelledByCustomerStepEnum.ContactCenterAgentAssignment) && s.IsDone == false),
                                        DiffDateNotify = grp.LastOrDefault(s => s.StepId == ((int)CacelledByCustomerStepEnum.ContactCenterManagmentNotification))?.DiffDateAssignSubmit ?? 0,

                                        //Assignee = grp
                                        //               .LastOrDefault(s => s.StepId == ((int)CacelledByCustomerStepEnum.ContactCenterAgentAssignment)) != null ?
                                        //               grp.LastOrDefault(s => s.StepId == ((int)CacelledByCustomerStepEnum.ContactCenterAgentAssignment)).AssignToName :
                                        //               string.Empty,

                                        //AssignFrom = grp.LastOrDefault(s => s.StepId == ((int)CacelledByCustomerStepEnum.ContactCenterAgentAssignment)) != null ? grp.LastOrDefault(s => s.StepId == ((int)CacelledByCustomerStepEnum.ContactCenterAgentAssignment)).AssignFromName : "",
                                        //AssignCreationDate = grp.LastOrDefault(s => s.StepId == ((int)CacelledByCustomerStepEnum.ContactCenterAgentAssignment)) != null ? grp.LastOrDefault(s => s.StepId == ((int)CacelledByCustomerStepEnum.ContactCenterAgentAssignment)).AssignCreationDate.ToString() : ""
                                        //,
                                        AssignmentModel = grp.Any(s => s.StepId == ((int)CacelledByCustomerStepEnum.ContactCenterAgentAssignment) && s.IsDone == true) ?
                                        new AssignmentModel()
                                        {
                                            AssignID = grp.LastOrDefault(s => s.StepId == ((int)CacelledByCustomerStepEnum.ContactCenterAgentAssignment)).AssignId.ToString(),
                                            AssignFromName = grp.LastOrDefault(s => s.StepId == ((int)CacelledByCustomerStepEnum.ContactCenterAgentAssignment)).AssignFromName,
                                            AssignToName = grp.LastOrDefault(s => s.StepId == ((int)CacelledByCustomerStepEnum.ContactCenterAgentAssignment)).AssignToName,
                                            AssignmentDate = grp.LastOrDefault(s => s.StepId == ((int)CacelledByCustomerStepEnum.ContactCenterAgentAssignment)).AssignCreationDate.ToString(),
                                            AssignToId = grp.LastOrDefault(s => s.StepId == ((int)CacelledByCustomerStepEnum.ContactCenterAgentAssignment)).ToUser ?? 0,
                                            DiffDate = grp.LastOrDefault(s => s.StepId == ((int)CacelledByCustomerStepEnum.ContactCenterAgentAssignment))?.DiffDateAssignSubmit ?? 0,

                                        } : null,
                                        FormSumbitModel = grp.Any(s => s.StepId == ((int)CacelledByCustomerStepEnum.AgentFormSumbitted) && s.IsDone == true) ?
                                        new FormSumbitModel()
                                        {
                                            FormSubmitID = grp.LastOrDefault(s => s.StepId == ((int)CacelledByCustomerStepEnum.AgentFormSumbitted)).FormSubmitId.ToString(),
                                            FormSubmitedBy = grp.LastOrDefault(s => s.StepId == ((int)CacelledByCustomerStepEnum.AgentFormSumbitted)).FormSubmitCreatedByName.ToString(),
                                            ReasonStatus = grp.LastOrDefault(s => s.StepId == ((int)CacelledByCustomerStepEnum.AgentFormSumbitted)).ReasonStatus.ToString(),
                                            ReasonText = grp.LastOrDefault(s => s.StepId == ((int)CacelledByCustomerStepEnum.AgentFormSumbitted)).ReasonText.ToString(),
                                            FormSubmitionDate = grp.LastOrDefault(s => s.StepId == ((int)CacelledByCustomerStepEnum.AgentFormSumbitted)).FormSubmitCreationDate.ToString(),
                                            FormSubmitedComment = grp.LastOrDefault(s => s.StepId == ((int)CacelledByCustomerStepEnum.AgentFormSumbitted)).Comment ?? string.Empty,
                                        } : null
                                    });

                    if (reservationSC.ActionStepId.HasValue && reservationSC.ActionStepId.Value > 0)
                    {
                        if (reservationSC.ActionStepId == 2) // notified but no asignment 
                        {
                            //query = query.Where(r => r.StepId == ((int)CacelledByCustomerStepEnum.ContactCenterAgentAssignment) && r.IsDone == false && r.DiffDateAssignSubmit >= 24); //
                            Group = Group.Where(g => g.IsAssigned != true && g.IsNeedToAssign == true && g.DiffDateNotify >= 24);//
                        }
                        else if (reservationSC.ActionStepId == 3) // assigned but no form sumbitted 
                        {
                            Group = Group.Where(g => g.IsFormSubmitted != true && g.IsAssigned == true && g.AssignmentModel.DiffDate >= 24);//
                        }
                        else if (reservationSC.ActionStepId == 5) // Assigned ToMe 
                        {
                            Group = Group.Where(g => g.IsFormSubmitted != true && g.IsAssigned == true && g.AssignmentModel.AssignToId == reservationSC.AssignedToId);
                        }
                    }


                    return Group.ToList();

                }
                return null;


            }
            catch (Exception e)
            {

                return null;
            }



        }


        public List<VReservationListModel> GetAllReservationsCancelBySixt(ReservationSC reservationSC)
        {

            var result = new List<VReservationListModel>();
            try
            {
                var query = Context.VreservationList.AsQueryable();
                if (reservationSC != null)
                {
                    if (reservationSC.ReservationNum.HasValue && reservationSC.ReservationNum.Value > 0)
                    {
                        query = query.Where(r => r.ReservationNum == reservationSC.ReservationNum);
                    }
                    if (reservationSC.ReservationId.HasValue && reservationSC.ReservationId.Value > 0)
                    {
                        query = query.Where(r => r.Id == reservationSC.ReservationId);
                    }

                    if (reservationSC.ReservationStatusId.HasValue && reservationSC.ReservationStatusId.Value > 0)
                    {
                        query = query.Where(r => r.ReservationStatusId == reservationSC.ReservationStatusId);
                    }
                    if (reservationSC.BookingDateFrom.HasValue && reservationSC.BookingDateFrom.Value != DateTime.MinValue)
                    {
                        query = query.Where(r => r.BookingDate >= reservationSC.BookingDateFrom);
                    }
                    if (reservationSC.BookingDateTo.HasValue && reservationSC.BookingDateTo.Value != DateTime.MinValue)
                    {
                        query = query.Where(r => r.BookingDate <= reservationSC.BookingDateTo);
                    }
                    if (reservationSC.PickUpDateFrom.HasValue && reservationSC.PickUpDateFrom.Value != DateTime.MinValue)
                    {
                        query = query.Where(r => r.PickUpDate >= reservationSC.PickUpDateFrom);
                    }
                    if (reservationSC.PickUpDateTo.HasValue && reservationSC.PickUpDateTo.Value != DateTime.MinValue)
                    {
                        query = query.Where(r => r.PickUpDate <= reservationSC.PickUpDateTo);
                    }

                    if (reservationSC.CancelDateFrom.HasValue && reservationSC.CancelDateFrom.Value != DateTime.MinValue)
                    {
                        query = query.Where(r => r.CancelledDate >= reservationSC.CancelDateFrom);
                    }
                    if (reservationSC.CancelDateTo.HasValue && reservationSC.CancelDateTo.Value != DateTime.MinValue)
                    {
                        query = query.Where(r => r.CancelledDate <= reservationSC.CancelDateTo);
                    }

                    if (reservationSC.DropOffDate.HasValue && reservationSC.DropOffDate.Value != DateTime.MinValue)
                    {
                        query = query.Where(r => r.DropOffDate <= reservationSC.DropOffDate);
                    }

                    if (!string.IsNullOrWhiteSpace(reservationSC.CustomerName))
                    {
                        query = query.Where(r => r.CustomerName.Contains(reservationSC.CustomerName));
                    }
                    if (!string.IsNullOrWhiteSpace(reservationSC.VehicleAcriss))
                    {
                        query = query.Where(r => r.VehicleAcriss == reservationSC.VehicleAcriss);
                    }
                    if (reservationSC.CancelledAfterBookingDays != null)
                    {
                        query = query.Where(r => r.CancelledAfterBookingInDays == reservationSC.CancelledAfterBookingDays);
                    }
                    if (reservationSC.RentalDays.HasValue && reservationSC.RentalDays.Value > 0)
                    {
                        query = query.Where(r => r.RentalDays == reservationSC.RentalDays);
                    }

                    if (reservationSC.PickUpBranchIds?.Length > 0 && !reservationSC.PickUpBranchIds.Contains(null))
                    {
                        query = query.Where(r => reservationSC.PickUpBranchIds.Contains(r.PickUpBranchId.GetValueOrDefault()));
                    }
                    DateTime yesterday = DateTime.Now.AddDays(-1);
                    if (reservationSC.ActionStepId.HasValue && reservationSC.ActionStepId.Value > 0)
                    {
                        if (reservationSC.ActionStepId == 7) // need assignment 
                        {
                            query = query.Where(r => r.StepId == ((int)CacelledBySixtStepEnum.OperationReservationManagementAssignment) && r.IsDone == false);
                        }


                        else if (reservationSC.ActionStepId == 10) // form not sumbitted 
                        {
                            query = query.Where(r => r.StepId == ((int)CacelledBySixtStepEnum.OperationReservationManagementFormSumbitted) && r.IsDone == false);
                        }

                    }

                    var temp = query.OrderBy(s => s.StepId).ToList();
                    var Group = temp
                                    .GroupBy(b => b.ReservationNum)
                                    .Select(grp => new VReservationListModel
                                    {
                                        ReservationNo = grp.Key,
                                        ReservationList = grp.LastOrDefault(),
                                        NextStep = grp.LastOrDefault(s => s.IsDone == false) != null ?
                                                                    grp.LastOrDefault(s => s.IsDone == false).StepName :
                                                                    string.Empty,
                                        IsAssigned = grp.Any(s => s.StepId == ((int)CacelledBySixtStepEnum.OperationReservationManagementAssignment) && s.IsDone == true),
                                        IsFormSubmitted = grp.Any(s => s.StepId == ((int)CacelledBySixtStepEnum.OperationReservationManagementFormSumbitted) && s.IsDone == true),
                                        IsNeedToAssign = grp.Any(s => s.StepId == ((int)CacelledBySixtStepEnum.OperationReservationManagementAssignment) && s.IsDone == false),
                                        DiffDateNotify = grp.LastOrDefault(s => s.StepId == ((int)CacelledBySixtStepEnum.OperationReservationManagementNotification))?.DiffDateAssignSubmit ?? 0,

                                        //Assignee = grp
                                        //               .LastOrDefault(s => s.StepId == ((int)CacelledByCustomerStepEnum.ContactCenterAgentAssignment)) != null ?
                                        //               grp.LastOrDefault(s => s.StepId == ((int)CacelledByCustomerStepEnum.ContactCenterAgentAssignment)).AssignToName :
                                        //               string.Empty,

                                        //AssignFrom = grp.LastOrDefault(s => s.StepId == ((int)CacelledByCustomerStepEnum.ContactCenterAgentAssignment)) != null ? grp.LastOrDefault(s => s.StepId == ((int)CacelledByCustomerStepEnum.ContactCenterAgentAssignment)).AssignFromName : "",
                                        //AssignCreationDate = grp.LastOrDefault(s => s.StepId == ((int)CacelledByCustomerStepEnum.ContactCenterAgentAssignment)) != null ? grp.LastOrDefault(s => s.StepId == ((int)CacelledByCustomerStepEnum.ContactCenterAgentAssignment)).AssignCreationDate.ToString() : ""
                                        //,
                                        AssignmentModel = grp.Any(s => s.StepId == ((int)CacelledBySixtStepEnum.OperationReservationManagementAssignment) && s.IsDone == true) ?
                                        new AssignmentModel()
                                        {
                                            AssignID = grp.LastOrDefault(s => s.StepId == ((int)CacelledBySixtStepEnum.OperationReservationManagementAssignment)).AssignId.ToString(),
                                            AssignFromName = grp.LastOrDefault(s => s.StepId == ((int)CacelledBySixtStepEnum.OperationReservationManagementAssignment)).AssignFromName,
                                            AssignToName = grp.LastOrDefault(s => s.StepId == ((int)CacelledBySixtStepEnum.OperationReservationManagementAssignment)).AssignToName,
                                            AssignmentDate = grp.LastOrDefault(s => s.StepId == ((int)CacelledBySixtStepEnum.OperationReservationManagementAssignment)).AssignCreationDate.ToString(),
                                            AssignToId = grp.LastOrDefault(s => s.StepId == ((int)CacelledBySixtStepEnum.OperationReservationManagementAssignment)).ToUser ?? 0,
                                            DiffDate = grp.LastOrDefault(s => s.StepId == ((int)CacelledBySixtStepEnum.OperationReservationManagementAssignment))?.DiffDateAssignSubmit ?? 0,

                                        } : null,
                                        FormSumbitModel = grp.Any(s => s.StepId == ((int)CacelledBySixtStepEnum.OperationReservationManagementFormSumbitted) && s.IsDone == true) ?
                                        new FormSumbitModel()
                                        {
                                            FormSubmitID = grp.LastOrDefault(s => s.StepId == ((int)CacelledBySixtStepEnum.OperationReservationManagementFormSumbitted)).FormSubmitId.ToString(),
                                            FormSubmitedBy = grp.LastOrDefault(s => s.StepId == ((int)CacelledBySixtStepEnum.OperationReservationManagementFormSumbitted)).FormSubmitCreatedByName.ToString(),
                                            // ReasonStatus = grp.LastOrDefault(s => s.StepId == ((int)CacelledBySixtStepEnum.OperationReservationManagementFormSumbitted)).ReasonStatus.ToString(),
                                            ReasonText = grp.LastOrDefault(s => s.StepId == ((int)CacelledBySixtStepEnum.OperationReservationManagementFormSumbitted)).ReasonText.ToString(),
                                            FormSubmitionDate = grp.LastOrDefault(s => s.StepId == ((int)CacelledBySixtStepEnum.OperationReservationManagementFormSumbitted)).FormSubmitCreationDate.ToString(),
                                            FormSubmitedComment = grp.LastOrDefault(s => s.StepId == ((int)CacelledBySixtStepEnum.OperationReservationManagementFormSumbitted)).Comment ?? string.Empty,
                                        } : null
                                    });

                    if (reservationSC.ActionStepId.HasValue && reservationSC.ActionStepId.Value > 0)
                    {
                        if (reservationSC.ActionStepId == 8) // notified but no asignment 
                        {
                            //query = query.Where(r => r.StepId == ((int)CacelledByCustomerStepEnum.ContactCenterAgentAssignment) && r.IsDone == false && r.DiffDateAssignSubmit >= 24); //
                            Group = Group.Where(g => g.IsAssigned != true && g.IsNeedToAssign == true && g.DiffDateNotify >= 24);//
                        }
                        else if (reservationSC.ActionStepId == 9) // assigned but no form sumbitted 
                        {
                            Group = Group.Where(g => g.IsFormSubmitted != true && g.IsAssigned == true && g.AssignmentModel.DiffDate >= 24);//
                        }
                        else if (reservationSC.ActionStepId == 11) // Assigned ToMe 
                        {
                            Group = Group.Where(g => g.IsFormSubmitted != true && g.IsAssigned == true && g.AssignmentModel.AssignToId == reservationSC.AssignedToId);
                        }
                    }

                    return Group.ToList();

                }
                return null;


            }
            catch (Exception e)
            {

                return null;
            }



        }

        public PaginationResult<VReservationListModel> GetReservationsCancelBySixt(ReservationSC reservationSC, int? page = null, int? pageSize = null)
        {
            var result = new PaginationResult<VReservationListModel>
            {
                Page = page ?? 0,
                PageSize = pageSize ?? 0,
            };

            try
            {
                var query = Context.VreservationList.AsQueryable();
                if (reservationSC != null)
                {
                    if (reservationSC.ReservationNum.HasValue && reservationSC.ReservationNum.Value > 0)
                    {
                        query = query.Where(r => r.ReservationNum == reservationSC.ReservationNum);
                    }
                    if (reservationSC.ReservationId.HasValue && reservationSC.ReservationId.Value > 0)
                    {
                        query = query.Where(r => r.Id == reservationSC.ReservationId);
                    }

                    if (reservationSC.ReservationStatusId.HasValue && reservationSC.ReservationStatusId.Value > 0)
                    {
                        query = query.Where(r => r.ReservationStatusId == reservationSC.ReservationStatusId);
                    }
                    if (reservationSC.BookingDateFrom.HasValue && reservationSC.BookingDateFrom.Value != DateTime.MinValue)
                    {
                        query = query.Where(r => r.BookingDate >= reservationSC.BookingDateFrom);
                    }
                    if (reservationSC.BookingDateTo.HasValue && reservationSC.BookingDateTo.Value != DateTime.MinValue)
                    {
                        query = query.Where(r => r.BookingDate <= reservationSC.BookingDateTo);
                    }
                    if (reservationSC.PickUpDateFrom.HasValue && reservationSC.PickUpDateFrom.Value != DateTime.MinValue)
                    {
                        query = query.Where(r => r.PickUpDate >= reservationSC.PickUpDateFrom);
                    }
                    if (reservationSC.PickUpDateTo.HasValue && reservationSC.PickUpDateTo.Value != DateTime.MinValue)
                    {
                        query = query.Where(r => r.PickUpDate <= reservationSC.PickUpDateTo);
                    }

                    if (reservationSC.CancelDateFrom.HasValue && reservationSC.CancelDateFrom.Value != DateTime.MinValue)
                    {
                        query = query.Where(r => r.CancelledDate >= reservationSC.CancelDateFrom);
                    }
                    if (reservationSC.CancelDateTo.HasValue && reservationSC.CancelDateTo.Value != DateTime.MinValue)
                    {
                        query = query.Where(r => r.CancelledDate <= reservationSC.CancelDateTo);
                    }

                    if (reservationSC.DropOffDate.HasValue && reservationSC.DropOffDate.Value != DateTime.MinValue)
                    {
                        query = query.Where(r => r.DropOffDate <= reservationSC.DropOffDate);
                    }

                    if (!string.IsNullOrWhiteSpace(reservationSC.CustomerName))
                    {
                        query = query.Where(r => r.CustomerName.Contains(reservationSC.CustomerName));
                    }
                    if (!string.IsNullOrWhiteSpace(reservationSC.VehicleAcriss))
                    {
                        query = query.Where(r => r.VehicleAcriss == reservationSC.VehicleAcriss);
                    }
                    if (reservationSC.CancelledAfterBookingDays != null)
                    {
                        query = query.Where(r => r.CancelledAfterBookingInDays == reservationSC.CancelledAfterBookingDays);
                    }
                    if (reservationSC.RentalDays.HasValue && reservationSC.RentalDays.Value > 0)
                    {
                        query = query.Where(r => r.RentalDays == reservationSC.RentalDays);
                    }

                    if (reservationSC.PickUpBranchIds?.Length > 0 && !reservationSC.PickUpBranchIds.Contains(null))
                    {
                        query = query.Where(r => reservationSC.PickUpBranchIds.Contains(r.PickUpBranchId.GetValueOrDefault()));
                    }
                    DateTime yesterday = DateTime.Now.AddDays(-1);
                    if (reservationSC.ActionStepId.HasValue && reservationSC.ActionStepId.Value > 0)
                    {
                        if (reservationSC.ActionStepId == 7) // need assignment 
                        {
                            query = query.Where(r => r.StepId == ((int)CacelledBySixtStepEnum.OperationReservationManagementAssignment) && r.IsDone == false);
                        }


                        else if (reservationSC.ActionStepId == 10) // form not sumbitted 
                        {
                            query = query.Where(r => r.StepId == ((int)CacelledBySixtStepEnum.OperationReservationManagementFormSumbitted) && r.IsDone == false);
                        }

                    }

                    var temp = query.OrderBy(s => s.StepId).ToList();
                    var Group = temp
                                    .GroupBy(b => b.ReservationNum)
                                    .Select(grp => new VReservationListModel
                                    {
                                        ReservationNo = grp.Key,
                                        ReservationList = grp.LastOrDefault(),
                                        NextStep = grp.LastOrDefault(s => s.IsDone == false) != null ?
                                                                    grp.LastOrDefault(s => s.IsDone == false).StepName :
                                                                    string.Empty,
                                        IsAssigned = grp.Any(s => s.StepId == ((int)CacelledBySixtStepEnum.OperationReservationManagementAssignment) && s.IsDone == true),
                                        IsFormSubmitted = grp.Any(s => s.StepId == ((int)CacelledBySixtStepEnum.OperationReservationManagementFormSumbitted) && s.IsDone == true),
                                        IsNeedToAssign = grp.Any(s => s.StepId == ((int)CacelledBySixtStepEnum.OperationReservationManagementAssignment) && s.IsDone == false),
                                        DiffDateNotify = grp.LastOrDefault(s => s.StepId == ((int)CacelledBySixtStepEnum.OperationReservationManagementNotification))?.DiffDateAssignSubmit ?? 0,

                                        //Assignee = grp
                                        //               .LastOrDefault(s => s.StepId == ((int)CacelledByCustomerStepEnum.ContactCenterAgentAssignment)) != null ?
                                        //               grp.LastOrDefault(s => s.StepId == ((int)CacelledByCustomerStepEnum.ContactCenterAgentAssignment)).AssignToName :
                                        //               string.Empty,

                                        //AssignFrom = grp.LastOrDefault(s => s.StepId == ((int)CacelledByCustomerStepEnum.ContactCenterAgentAssignment)) != null ? grp.LastOrDefault(s => s.StepId == ((int)CacelledByCustomerStepEnum.ContactCenterAgentAssignment)).AssignFromName : "",
                                        //AssignCreationDate = grp.LastOrDefault(s => s.StepId == ((int)CacelledByCustomerStepEnum.ContactCenterAgentAssignment)) != null ? grp.LastOrDefault(s => s.StepId == ((int)CacelledByCustomerStepEnum.ContactCenterAgentAssignment)).AssignCreationDate.ToString() : ""
                                        //,
                                        AssignmentModel = grp.Any(s => s.StepId == ((int)CacelledBySixtStepEnum.OperationReservationManagementAssignment) && s.IsDone == true) ?
                                        new AssignmentModel()
                                        {
                                            AssignID = grp.LastOrDefault(s => s.StepId == ((int)CacelledBySixtStepEnum.OperationReservationManagementAssignment)).AssignId.ToString(),
                                            AssignFromName = grp.LastOrDefault(s => s.StepId == ((int)CacelledBySixtStepEnum.OperationReservationManagementAssignment)).AssignFromName,
                                            AssignToName = grp.LastOrDefault(s => s.StepId == ((int)CacelledBySixtStepEnum.OperationReservationManagementAssignment)).AssignToName,
                                            AssignmentDate = grp.LastOrDefault(s => s.StepId == ((int)CacelledBySixtStepEnum.OperationReservationManagementAssignment)).AssignCreationDate.ToString(),
                                            AssignToId = grp.LastOrDefault(s => s.StepId == ((int)CacelledBySixtStepEnum.OperationReservationManagementAssignment)).ToUser ?? 0,
                                            DiffDate = grp.LastOrDefault(s => s.StepId == ((int)CacelledBySixtStepEnum.OperationReservationManagementAssignment))?.DiffDateAssignSubmit ?? 0,

                                        } : null,
                                        FormSumbitModel = grp.Any(s => s.StepId == ((int)CacelledBySixtStepEnum.OperationReservationManagementFormSumbitted) && s.IsDone == true) ?
                                        new FormSumbitModel()
                                        {
                                            FormSubmitID = grp.LastOrDefault(s => s.StepId == ((int)CacelledBySixtStepEnum.OperationReservationManagementFormSumbitted)).FormSubmitId.ToString(),
                                            FormSubmitedBy = grp.LastOrDefault(s => s.StepId == ((int)CacelledBySixtStepEnum.OperationReservationManagementFormSumbitted)).FormSubmitCreatedByName.ToString(),
                                            // ReasonStatus = grp.LastOrDefault(s => s.StepId == ((int)CacelledBySixtStepEnum.OperationReservationManagementFormSumbitted)).ReasonStatus.ToString(),
                                            ReasonText = grp.LastOrDefault(s => s.StepId == ((int)CacelledBySixtStepEnum.OperationReservationManagementFormSumbitted)).ReasonText.ToString(),
                                            FormSubmitionDate = grp.LastOrDefault(s => s.StepId == ((int)CacelledBySixtStepEnum.OperationReservationManagementFormSumbitted)).FormSubmitCreationDate.ToString(),
                                            FormSubmitedComment = grp.LastOrDefault(s => s.StepId == ((int)CacelledBySixtStepEnum.OperationReservationManagementFormSumbitted)).Comment ?? string.Empty,
                                        } : null
                                    });

                    if (reservationSC.ActionStepId.HasValue && reservationSC.ActionStepId.Value > 0)
                    {
                        if (reservationSC.ActionStepId == 8) // notified but no asignment 
                        {
                            //query = query.Where(r => r.StepId == ((int)CacelledByCustomerStepEnum.ContactCenterAgentAssignment) && r.IsDone == false && r.DiffDateAssignSubmit >= 24); //
                            Group = Group.Where(g => g.IsAssigned != true && g.IsNeedToAssign == true && g.DiffDateNotify >= 24);//
                        }
                        else if (reservationSC.ActionStepId == 9) // assigned but no form sumbitted 
                        {
                            Group = Group.Where(g => g.IsFormSubmitted != true && g.IsAssigned == true && g.AssignmentModel.DiffDate >= 24);//
                        }
                        else if (reservationSC.ActionStepId == 11) // Assigned ToMe 
                        {
                            Group = Group.Where(g => g.IsFormSubmitted != true && g.IsAssigned == true && g.AssignmentModel.AssignToId == reservationSC.AssignedToId);
                        }
                    }

                    result.Total = Group.Count();
                    Group = Group.OrderByDescending(p => p.ReservationList.CreationDate);
                    if (page.GetValueOrDefault() > 0 && pageSize.GetValueOrDefault() > 0)
                    {
                        Group = Group
                                    .Skip((page.Value - 1) * pageSize.Value)
                                    .Take(pageSize.Value);
                    }
                    result.Items = Group.ToList();

                    return result;

                }
                return null;


            }
            catch (Exception e)
            {

                return null;
            }



        }


        public PaginationResult<VReservationListModel> GetAllReservationsNoShow(ReservationSC reservationSC, int? page = null, int? pageSize = null)
        {
            var result = new PaginationResult<VReservationListModel>
            {
                Page = page ?? 0,
                PageSize = pageSize ?? 0,
            };

            try
            {
                var query = Context.VreservationList.AsQueryable();
                if (reservationSC != null)
                {
                    if (reservationSC.ReservationNum.HasValue && reservationSC.ReservationNum.Value > 0)
                    {
                        query = query.Where(r => r.ReservationNum == reservationSC.ReservationNum);
                    }
                    if (reservationSC.ReservationId.HasValue && reservationSC.ReservationId.Value > 0)
                    {
                        query = query.Where(r => r.Id == reservationSC.ReservationId);
                    }

                    if (reservationSC.ReservationStatusId.HasValue && reservationSC.ReservationStatusId.Value > 0)
                    {
                        query = query.Where(r => r.ReservationStatusId == reservationSC.ReservationStatusId);
                    }
                    if (reservationSC.BookingDateFrom.HasValue && reservationSC.BookingDateFrom.Value != DateTime.MinValue)
                    {
                        query = query.Where(r => r.BookingDate >= reservationSC.BookingDateFrom);
                    }
                    if (reservationSC.BookingDateTo.HasValue && reservationSC.BookingDateTo.Value != DateTime.MinValue)
                    {
                        query = query.Where(r => r.BookingDate <= reservationSC.BookingDateTo);
                    }
                    if (reservationSC.PickUpDateFrom.HasValue && reservationSC.PickUpDateFrom.Value != DateTime.MinValue)
                    {
                        query = query.Where(r => r.PickUpDate >= reservationSC.PickUpDateFrom);
                    }
                    if (reservationSC.PickUpDateTo.HasValue && reservationSC.PickUpDateTo.Value != DateTime.MinValue)
                    {
                        query = query.Where(r => r.PickUpDate <= reservationSC.PickUpDateTo);
                    }

                    if (reservationSC.CancelDateFrom.HasValue && reservationSC.CancelDateFrom.Value != DateTime.MinValue)
                    {
                        query = query.Where(r => r.CancelledDate >= reservationSC.CancelDateFrom);
                    }
                    if (reservationSC.CancelDateTo.HasValue && reservationSC.CancelDateTo.Value != DateTime.MinValue)
                    {
                        query = query.Where(r => r.CancelledDate <= reservationSC.CancelDateTo);
                    }

                    if (reservationSC.DropOffDate.HasValue && reservationSC.DropOffDate.Value != DateTime.MinValue)
                    {
                        query = query.Where(r => r.DropOffDate <= reservationSC.DropOffDate);
                    }

                    if (!string.IsNullOrWhiteSpace(reservationSC.CustomerName))
                    {
                        query = query.Where(r => r.CustomerName.Contains(reservationSC.CustomerName));
                    }
                    if (!string.IsNullOrWhiteSpace(reservationSC.VehicleAcriss))
                    {
                        query = query.Where(r => r.VehicleAcriss == reservationSC.VehicleAcriss);
                    }
                    if (reservationSC.CancelledAfterBookingDays != null)
                    {
                        query = query.Where(r => r.CancelledAfterBookingInDays == reservationSC.CancelledAfterBookingDays);
                    }
                    if (reservationSC.RentalDays.HasValue && reservationSC.RentalDays.Value > 0)
                    {
                        query = query.Where(r => r.RentalDays == reservationSC.RentalDays);
                    }

                    if (reservationSC.PickUpBranchIds?.Length > 0 && !reservationSC.PickUpBranchIds.Contains(null))
                    {
                        query = query.Where(r => reservationSC.PickUpBranchIds.Contains(r.PickUpBranchId.GetValueOrDefault()));
                    }
                    DateTime yesterday = DateTime.Now.AddDays(-1);
                    if (reservationSC.ActionStepId.HasValue && reservationSC.ActionStepId.Value > 0)
                    {
                        if (reservationSC.ActionStepId == 13) // need assignment 
                        {
                            query = query.Where(r => r.StepId == ((int)NoShowStepEnum.ContactCenterAgentAssignment) && r.IsDone == false);
                        }

                        //else if (reservationSC.ActionStepId == 2) // notified but no asignment 
                        //{

                        //    query = query.Where(r => r.StepId == ((int)CacelledByCustomerStepEnum.ContactCenterAgentAssignment) && r.IsDone == false && r.DiffDateAssignSubmit >= 24); //
                        //}

                        //else if (reservationSC.ActionStepId == 3) // assigned but no form sumbitted 
                        //{
                        //    query = query.Where(r => r.StepId == ((int)CacelledByCustomerStepEnum.AgentFormSumbitted) && r.IsDone == false && r.DiffDateAssignSubmit >= 24);//
                        //}

                        else if (reservationSC.ActionStepId == 4) // form not sumbitted 
                        {
                            query = query.Where(r => r.StepId == ((int)NoShowStepEnum.AgentFormSumbitted) && r.IsDone == false);
                        }

                        //else if (reservationSC.ActionStepId == 5) // Assigned ToMe 
                        //{
                        //    query = query.Where(r => r.ToUser ==
                        //    reservationSC.AssignedToId && r.StepId == ((int)CacelledByCustomerStepEnum.ContactCenterAgentAssignment) && r.IsDone == true);
                        //}
                    }
                    //if (  reservationSC.AssignedToMe == true)
                    //{
                    //    query = query.Where(r => r.ToUser == reservationSC.AssignedToId);
                    // }

                    var temp = query.OrderBy(s => s.StepId).ToList();
                    var Group = temp
                                    .GroupBy(b => b.ReservationNum)
                                    .Select(grp => new VReservationListModel
                                    {
                                        ReservationNo = grp.Key,
                                        ReservationList = grp.LastOrDefault(),
                                        NextStep = grp.LastOrDefault(s => s.IsDone == false) != null ?
                                                                    grp.LastOrDefault(s => s.IsDone == false).StepName :
                                                                    string.Empty,
                                        IsAssigned = grp.Any(s => s.StepId == ((int)NoShowStepEnum.ContactCenterAgentAssignment) && s.IsDone == true),
                                        IsFormSubmitted = grp.Any(s => s.StepId == ((int)NoShowStepEnum.AgentFormSumbitted) && s.IsDone == true),
                                        IsNeedToAssign = grp.Any(s => s.StepId == ((int)NoShowStepEnum.ContactCenterAgentAssignment) && s.IsDone == false),
                                        DiffDateNotify = grp.LastOrDefault(s => s.StepId == ((int)NoShowStepEnum.ContactCenterManagmentNotification))?.DiffDateAssignSubmit ?? 0,

                                        //Assignee = grp
                                        //               .LastOrDefault(s => s.StepId == ((int)CacelledByCustomerStepEnum.ContactCenterAgentAssignment)) != null ?
                                        //               grp.LastOrDefault(s => s.StepId == ((int)CacelledByCustomerStepEnum.ContactCenterAgentAssignment)).AssignToName :
                                        //               string.Empty,

                                        //AssignFrom = grp.LastOrDefault(s => s.StepId == ((int)CacelledByCustomerStepEnum.ContactCenterAgentAssignment)) != null ? grp.LastOrDefault(s => s.StepId == ((int)CacelledByCustomerStepEnum.ContactCenterAgentAssignment)).AssignFromName : "",
                                        //AssignCreationDate = grp.LastOrDefault(s => s.StepId == ((int)CacelledByCustomerStepEnum.ContactCenterAgentAssignment)) != null ? grp.LastOrDefault(s => s.StepId == ((int)CacelledByCustomerStepEnum.ContactCenterAgentAssignment)).AssignCreationDate.ToString() : ""
                                        //,
                                        AssignmentModel = grp.Any(s => s.StepId == ((int)NoShowStepEnum.ContactCenterAgentAssignment) && s.IsDone == true) ?
                                        new AssignmentModel()
                                        {
                                            AssignID = grp.LastOrDefault(s => s.StepId == ((int)NoShowStepEnum.ContactCenterAgentAssignment)).AssignId.ToString(),
                                            AssignFromName = grp.LastOrDefault(s => s.StepId == ((int)NoShowStepEnum.ContactCenterAgentAssignment)).AssignFromName,
                                            AssignToName = grp.LastOrDefault(s => s.StepId == ((int)NoShowStepEnum.ContactCenterAgentAssignment)).AssignToName,
                                            AssignmentDate = grp.LastOrDefault(s => s.StepId == ((int)NoShowStepEnum.ContactCenterAgentAssignment)).AssignCreationDate.ToString(),
                                            AssignToId = grp.LastOrDefault(s => s.StepId == ((int)NoShowStepEnum.ContactCenterAgentAssignment)).ToUser ?? 0,
                                            DiffDate = grp.LastOrDefault(s => s.StepId == ((int)NoShowStepEnum.ContactCenterAgentAssignment))?.DiffDateAssignSubmit ?? 0,

                                        } : null,
                                        FormSumbitModel = grp.Any(s => s.StepId == ((int)NoShowStepEnum.AgentFormSumbitted) && s.IsDone == true) ?
                                        new FormSumbitModel()
                                        {
                                            FormSubmitID = grp.LastOrDefault(s => s.StepId == ((int)NoShowStepEnum.AgentFormSumbitted)).FormSubmitId.ToString(),
                                            FormSubmitedBy = grp.LastOrDefault(s => s.StepId == ((int)NoShowStepEnum.AgentFormSumbitted)).FormSubmitCreatedByName.ToString(),
                                            ReasonStatus = grp.LastOrDefault(s => s.StepId == ((int)NoShowStepEnum.AgentFormSumbitted)).ReasonStatus.ToString(),
                                            ReasonText = grp.LastOrDefault(s => s.StepId == ((int)NoShowStepEnum.AgentFormSumbitted)).ReasonText.ToString(),
                                            FormSubmitionDate = grp.LastOrDefault(s => s.StepId == ((int)NoShowStepEnum.AgentFormSumbitted)).FormSubmitCreationDate.ToString(),
                                            FormSubmitedComment = grp.LastOrDefault(s => s.StepId == ((int)NoShowStepEnum.AgentFormSumbitted)).Comment ?? string.Empty,
                                        } : null
                                    });

                    if (reservationSC.ActionStepId.HasValue && reservationSC.ActionStepId.Value > 0)
                    {
                        if (reservationSC.ActionStepId == 14) // notified but no asignment 
                        {
                            //query = query.Where(r => r.StepId == ((int)CacelledByCustomerStepEnum.ContactCenterAgentAssignment) && r.IsDone == false && r.DiffDateAssignSubmit >= 24); //
                            Group = Group.Where(g => g.IsAssigned != true && g.IsNeedToAssign == true && g.DiffDateNotify >= 24);//
                        }
                        else if (reservationSC.ActionStepId == 15) // assigned but no form sumbitted 
                        {
                            Group = Group.Where(g => g.IsFormSubmitted != true && g.IsAssigned == true && g.AssignmentModel.DiffDate >= 24);//
                        }
                        else if (reservationSC.ActionStepId == 17) // Assigned ToMe 
                        {
                            Group = Group.Where(g => g.IsFormSubmitted != true && g.IsAssigned == true && g.AssignmentModel.AssignToId == reservationSC.AssignedToId);
                        }
                    }
                    result.Total = Group.Count();
                    Group = Group.OrderByDescending(p => p.ReservationNo);
                    if (page.GetValueOrDefault() > 0 && pageSize.GetValueOrDefault() > 0)
                    {
                        Group = Group
                                    .Skip((page.Value - 1) * pageSize.Value)
                                    .Take(pageSize.Value);
                    }
                    result.Items = Group.ToList();

                    return result;

                }
                return null;


            }
            catch (Exception e)
            {

                return null;
            }



        }


        public List<VReservationListModel> GetReservationsNoShow(ReservationSC reservationSC)
        {
            var result = new List<VReservationListModel>();

            try
            {
                var query = Context.VreservationList.AsQueryable();
                if (reservationSC != null)
                {
                    if (reservationSC.ReservationNum.HasValue && reservationSC.ReservationNum.Value > 0)
                    {
                        query = query.Where(r => r.ReservationNum == reservationSC.ReservationNum);
                    }
                    if (reservationSC.ReservationId.HasValue && reservationSC.ReservationId.Value > 0)
                    {
                        query = query.Where(r => r.Id == reservationSC.ReservationId);
                    }

                    if (reservationSC.ReservationStatusId.HasValue && reservationSC.ReservationStatusId.Value > 0)
                    {
                        query = query.Where(r => r.ReservationStatusId == reservationSC.ReservationStatusId);
                    }
                    if (reservationSC.BookingDateFrom.HasValue && reservationSC.BookingDateFrom.Value != DateTime.MinValue)
                    {
                        query = query.Where(r => r.BookingDate >= reservationSC.BookingDateFrom);
                    }
                    if (reservationSC.BookingDateTo.HasValue && reservationSC.BookingDateTo.Value != DateTime.MinValue)
                    {
                        query = query.Where(r => r.BookingDate <= reservationSC.BookingDateTo);
                    }
                    if (reservationSC.PickUpDateFrom.HasValue && reservationSC.PickUpDateFrom.Value != DateTime.MinValue)
                    {
                        query = query.Where(r => r.PickUpDate >= reservationSC.PickUpDateFrom);
                    }
                    if (reservationSC.PickUpDateTo.HasValue && reservationSC.PickUpDateTo.Value != DateTime.MinValue)
                    {
                        query = query.Where(r => r.PickUpDate <= reservationSC.PickUpDateTo);
                    }

                    if (reservationSC.CancelDateFrom.HasValue && reservationSC.CancelDateFrom.Value != DateTime.MinValue)
                    {
                        query = query.Where(r => r.CancelledDate >= reservationSC.CancelDateFrom);
                    }
                    if (reservationSC.CancelDateTo.HasValue && reservationSC.CancelDateTo.Value != DateTime.MinValue)
                    {
                        query = query.Where(r => r.CancelledDate <= reservationSC.CancelDateTo);
                    }

                    if (reservationSC.DropOffDate.HasValue && reservationSC.DropOffDate.Value != DateTime.MinValue)
                    {
                        query = query.Where(r => r.DropOffDate <= reservationSC.DropOffDate);
                    }

                    if (!string.IsNullOrWhiteSpace(reservationSC.CustomerName))
                    {
                        query = query.Where(r => r.CustomerName.Contains(reservationSC.CustomerName));
                    }
                    if (!string.IsNullOrWhiteSpace(reservationSC.VehicleAcriss))
                    {
                        query = query.Where(r => r.VehicleAcriss == reservationSC.VehicleAcriss);
                    }
                    if (reservationSC.CancelledAfterBookingDays != null)
                    {
                        query = query.Where(r => r.CancelledAfterBookingInDays == reservationSC.CancelledAfterBookingDays);
                    }
                    if (reservationSC.RentalDays.HasValue && reservationSC.RentalDays.Value > 0)
                    {
                        query = query.Where(r => r.RentalDays == reservationSC.RentalDays);
                    }

                    if (reservationSC.PickUpBranchIds?.Length > 0 && !reservationSC.PickUpBranchIds.Contains(null))
                    {
                        query = query.Where(r => reservationSC.PickUpBranchIds.Contains(r.PickUpBranchId.GetValueOrDefault()));
                    }
                    DateTime yesterday = DateTime.Now.AddDays(-1);
                    if (reservationSC.ActionStepId.HasValue && reservationSC.ActionStepId.Value > 0)
                    {
                        if (reservationSC.ActionStepId == 13) // need assignment 
                        {
                            query = query.Where(r => r.StepId == ((int)NoShowStepEnum.ContactCenterAgentAssignment) && r.IsDone == false);
                        }

                        //else if (reservationSC.ActionStepId == 2) // notified but no asignment 
                        //{

                        //    query = query.Where(r => r.StepId == ((int)CacelledByCustomerStepEnum.ContactCenterAgentAssignment) && r.IsDone == false && r.DiffDateAssignSubmit >= 24); //
                        //}

                        //else if (reservationSC.ActionStepId == 3) // assigned but no form sumbitted 
                        //{
                        //    query = query.Where(r => r.StepId == ((int)CacelledByCustomerStepEnum.AgentFormSumbitted) && r.IsDone == false && r.DiffDateAssignSubmit >= 24);//
                        //}

                        else if (reservationSC.ActionStepId == 1) // form not sumbitted 
                        {
                            query = query.Where(r => r.StepId == ((int)NoShowStepEnum.AgentFormSumbitted) && r.IsDone == false);
                        }

                        //else if (reservationSC.ActionStepId == 5) // Assigned ToMe 
                        //{
                        //    query = query.Where(r => r.ToUser ==
                        //    reservationSC.AssignedToId && r.StepId == ((int)CacelledByCustomerStepEnum.ContactCenterAgentAssignment) && r.IsDone == true);
                        //}
                    }
                    //if (  reservationSC.AssignedToMe == true)
                    //{
                    //    query = query.Where(r => r.ToUser == reservationSC.AssignedToId);
                    // }

                    var temp = query.OrderBy(s => s.StepId).ToList();
                    var Group = temp
                                    .GroupBy(b => b.ReservationNum)
                                    .Select(grp => new VReservationListModel
                                    {
                                        ReservationNo = grp.Key,
                                        ReservationList = grp.LastOrDefault(),
                                        NextStep = grp.LastOrDefault(s => s.IsDone == false) != null ?
                                                                    grp.LastOrDefault(s => s.IsDone == false).StepName :
                                                                    string.Empty,
                                        IsAssigned = grp.Any(s => s.StepId == ((int)NoShowStepEnum.ContactCenterAgentAssignment) && s.IsDone == true),
                                        IsFormSubmitted = grp.Any(s => s.StepId == ((int)NoShowStepEnum.AgentFormSumbitted) && s.IsDone == true),
                                        IsNeedToAssign = grp.Any(s => s.StepId == ((int)NoShowStepEnum.ContactCenterAgentAssignment) && s.IsDone == false),
                                        DiffDateNotify = grp.LastOrDefault(s => s.StepId == ((int)NoShowStepEnum.ContactCenterManagmentNotification))?.DiffDateAssignSubmit ?? 0,

                                        //Assignee = grp
                                        //               .LastOrDefault(s => s.StepId == ((int)CacelledByCustomerStepEnum.ContactCenterAgentAssignment)) != null ?
                                        //               grp.LastOrDefault(s => s.StepId == ((int)CacelledByCustomerStepEnum.ContactCenterAgentAssignment)).AssignToName :
                                        //               string.Empty,

                                        //AssignFrom = grp.LastOrDefault(s => s.StepId == ((int)CacelledByCustomerStepEnum.ContactCenterAgentAssignment)) != null ? grp.LastOrDefault(s => s.StepId == ((int)CacelledByCustomerStepEnum.ContactCenterAgentAssignment)).AssignFromName : "",
                                        //AssignCreationDate = grp.LastOrDefault(s => s.StepId == ((int)CacelledByCustomerStepEnum.ContactCenterAgentAssignment)) != null ? grp.LastOrDefault(s => s.StepId == ((int)CacelledByCustomerStepEnum.ContactCenterAgentAssignment)).AssignCreationDate.ToString() : ""
                                        //,
                                        AssignmentModel = grp.Any(s => s.StepId == ((int)NoShowStepEnum.ContactCenterAgentAssignment) && s.IsDone == true) ?
                                        new AssignmentModel()
                                        {
                                            AssignID = grp.LastOrDefault(s => s.StepId == ((int)NoShowStepEnum.ContactCenterAgentAssignment)).AssignId.ToString(),
                                            AssignFromName = grp.LastOrDefault(s => s.StepId == ((int)NoShowStepEnum.ContactCenterAgentAssignment)).AssignFromName,
                                            AssignToName = grp.LastOrDefault(s => s.StepId == ((int)NoShowStepEnum.ContactCenterAgentAssignment)).AssignToName,
                                            AssignmentDate = grp.LastOrDefault(s => s.StepId == ((int)NoShowStepEnum.ContactCenterAgentAssignment)).AssignCreationDate.ToString(),
                                            AssignToId = grp.LastOrDefault(s => s.StepId == ((int)NoShowStepEnum.ContactCenterAgentAssignment)).ToUser ?? 0,
                                            DiffDate = grp.LastOrDefault(s => s.StepId == ((int)NoShowStepEnum.ContactCenterAgentAssignment))?.DiffDateAssignSubmit ?? 0,

                                        } : null,
                                        FormSumbitModel = grp.Any(s => s.StepId == ((int)NoShowStepEnum.AgentFormSumbitted) && s.IsDone == true) ?
                                        new FormSumbitModel()
                                        {
                                            FormSubmitID = grp.LastOrDefault(s => s.StepId == ((int)NoShowStepEnum.AgentFormSumbitted)).FormSubmitId.ToString(),
                                            FormSubmitedBy = grp.LastOrDefault(s => s.StepId == ((int)NoShowStepEnum.AgentFormSumbitted)).FormSubmitCreatedByName.ToString(),
                                            ReasonStatus = grp.LastOrDefault(s => s.StepId == ((int)NoShowStepEnum.AgentFormSumbitted)).ReasonStatus.ToString(),
                                            ReasonText = grp.LastOrDefault(s => s.StepId == ((int)NoShowStepEnum.AgentFormSumbitted)).ReasonText.ToString(),
                                            FormSubmitionDate = grp.LastOrDefault(s => s.StepId == ((int)NoShowStepEnum.AgentFormSumbitted)).FormSubmitCreationDate.ToString(),
                                            FormSubmitedComment = grp.LastOrDefault(s => s.StepId == ((int)NoShowStepEnum.AgentFormSumbitted)).Comment ?? string.Empty,
                                        } : null
                                    });

                    if (reservationSC.ActionStepId.HasValue && reservationSC.ActionStepId.Value > 0)
                    {
                        if (reservationSC.ActionStepId == 14) // notified but no asignment 
                        {
                            //query = query.Where(r => r.StepId == ((int)CacelledByCustomerStepEnum.ContactCenterAgentAssignment) && r.IsDone == false && r.DiffDateAssignSubmit >= 24); //
                            Group = Group.Where(g => g.IsAssigned != true && g.IsNeedToAssign == true && g.DiffDateNotify >= 24);//
                        }
                        else if (reservationSC.ActionStepId == 15) // assigned but no form sumbitted 
                        {
                            Group = Group.Where(g => g.IsFormSubmitted != true && g.IsAssigned == true && g.AssignmentModel.DiffDate >= 24);//
                        }
                        else if (reservationSC.ActionStepId == 17) // Assigned ToMe 
                        {
                            Group = Group.Where(g => g.IsFormSubmitted != true && g.IsAssigned == true && g.AssignmentModel.AssignToId == reservationSC.AssignedToId);
                        }
                    }


                    return Group.ToList();

                }
                return null;


            }
            catch (Exception e)
            {

                return null;
            }



        }


        public PaginationResult<VReservationListModel> GetAllReservationsOpen(ReservationSC reservationSC, int? page = null, int? pageSize = null)
        {
            var result = new PaginationResult<VReservationListModel>
            {
                Page = page ?? 0,
                PageSize = pageSize ?? 0,
            };

            try
            {
                var query = Context.VreservationList.AsQueryable();
                if (reservationSC != null)
                {
                    if (reservationSC.ReservationNum.HasValue && reservationSC.ReservationNum.Value > 0)
                    {
                        query = query.Where(r => r.ReservationNum == reservationSC.ReservationNum);
                    }
                    if (reservationSC.ReservationId.HasValue && reservationSC.ReservationId.Value > 0)
                    {
                        query = query.Where(r => r.Id == reservationSC.ReservationId);
                    }

                    if (reservationSC.ReservationStatusId.HasValue && reservationSC.ReservationStatusId.Value > 0)
                    {
                        query = query.Where(r => r.ReservationStatusId == reservationSC.ReservationStatusId);
                    }
                    if (reservationSC.BookingDateFrom.HasValue && reservationSC.BookingDateFrom.Value != DateTime.MinValue)
                    {
                        query = query.Where(r => r.BookingDate >= reservationSC.BookingDateFrom);
                    }
                    if (reservationSC.BookingDateTo.HasValue && reservationSC.BookingDateTo.Value != DateTime.MinValue)
                    {
                        query = query.Where(r => r.BookingDate <= reservationSC.BookingDateTo);
                    }
                    if (reservationSC.PickUpDateFrom.HasValue && reservationSC.PickUpDateFrom.Value != DateTime.MinValue)
                    {
                        query = query.Where(r => r.PickUpDate >= reservationSC.PickUpDateFrom);
                    }
                    if (reservationSC.PickUpDateTo.HasValue && reservationSC.PickUpDateTo.Value != DateTime.MinValue)
                    {
                        query = query.Where(r => r.PickUpDate <= reservationSC.PickUpDateTo);
                    }

                    if (reservationSC.CancelDateFrom.HasValue && reservationSC.CancelDateFrom.Value != DateTime.MinValue)
                    {
                        query = query.Where(r => r.CancelledDate >= reservationSC.CancelDateFrom);
                    }
                    if (reservationSC.CancelDateTo.HasValue && reservationSC.CancelDateTo.Value != DateTime.MinValue)
                    {
                        query = query.Where(r => r.CancelledDate <= reservationSC.CancelDateTo);
                    }

                    if (reservationSC.DropOffDate.HasValue && reservationSC.DropOffDate.Value != DateTime.MinValue)
                    {
                        query = query.Where(r => r.DropOffDate <= reservationSC.DropOffDate);
                    }

                    if (!string.IsNullOrWhiteSpace(reservationSC.CustomerName))
                    {
                        query = query.Where(r => r.CustomerName.Contains(reservationSC.CustomerName));
                    }
                    if (!string.IsNullOrWhiteSpace(reservationSC.VehicleAcriss))
                    {
                        query = query.Where(r => r.VehicleAcriss == reservationSC.VehicleAcriss);
                    }
                    if (reservationSC.CancelledAfterBookingDays != null)
                    {
                        query = query.Where(r => r.CancelledAfterBookingInDays == reservationSC.CancelledAfterBookingDays);
                    }
                    if (reservationSC.RentalDays.HasValue && reservationSC.RentalDays.Value > 0)
                    {
                        query = query.Where(r => r.RentalDays == reservationSC.RentalDays);
                    }

                    if (reservationSC.PickUpBranchIds?.Length > 0 && !reservationSC.PickUpBranchIds.Contains(null))
                    {
                        query = query.Where(r => reservationSC.PickUpBranchIds.Contains(r.PickUpBranchId.GetValueOrDefault()));
                    }
                    DateTime yesterday = DateTime.Now.AddDays(-1);
                    query = query.Where(r => DateTime.Now > r.PickUpDate.Value.AddHours(-24) && r.PickUpDate > yesterday || r.PickUpDate > DateTime.Now);
                    if (reservationSC.ActionStepId.HasValue && reservationSC.ActionStepId.Value > 0)
                    {
                        if (reservationSC.ActionStepId == (int)OpenStepEnum.BranchManagmentNotification) // need assignment  BM open
                        {
                            query = query.Where(r => r.StepId == ((int)OpenStepEnum.BranchAgentAssignment) && r.IsDone == false);
                        }

                        else if (reservationSC.ActionStepId == (int)OpenStepEnum.NoFormsumbittedNotification) // form not sumbitted  BM open
                        {
                            query = query.Where(r => r.StepId == ((int)OpenStepEnum.AgentFormSumbitted) && r.IsDone == false);
                        }
                        if (reservationSC.ActionStepId == (int)OpenStepEnum.ContactManagmentNotification) // need assignment  CC
                        {
                            query = query.Where(r => r.StepId == ((int)OpenStepEnum.ContactManagmentAssignment) && r.IsDone == false);
                        }
                        else if (reservationSC.ActionStepId == (int)OpenStepEnum.ContactManagmentNoFormsumbittedNotification) //form not sumbitted 
                        {
                            query = query.Where(r => r.StepId == ((int)OpenStepEnum.ContactManagmentFormSumbitted) && r.IsDone == false);
                        }

                        if (reservationSC.ActionStepId == (int)OpenStepEnum.BranchAgentNotification) // need assignment  BM confirmed
                        {
                            query = query.Where(r => r.StepId == ((int)OpenStepEnum.BranchAgentAssignmentConfirmed) && r.IsDone == false);
                        }
                        else if (reservationSC.ActionStepId == (int)OpenStepEnum.BranchAgentNoFormsumbittedNotification) //form not sumbitted  BM confirmed
                        {
                            query = query.Where(r => r.StepId == ((int)OpenStepEnum.BranchAgentFormSumbitted) && r.IsDone == false);
                        }

                    }
                    //if (  reservationSC.AssignedToMe == true)
                    //{
                    //    query = query.Where(r => r.ToUser == reservationSC.AssignedToId);
                    // }

                    var temp = query.OrderBy(s => s.StepId).ToList();
                    var Group = temp
                                    .GroupBy(b => b.ReservationNum)
                                    .Select(grp => new VReservationListModel
                                    {
                                        ReservationNo = grp.Key,
                                        ReservationList = grp.LastOrDefault(),
                                        NextStep = grp.LastOrDefault(s => s.IsDone == false) != null ?
                                                                    grp.LastOrDefault(s => s.IsDone == false).StepName :
                                                                    string.Empty,
                                        IsAssigned = grp.Any(s => s.StepId == ((int)OpenStepEnum.BranchAgentAssignment) && s.IsDone == true),
                                        IsAssignedToCCAgent = grp.Any(s => s.StepId == ((int)OpenStepEnum.ContactManagmentAssignment) && s.IsDone == true),
                                        IsAssignedToBM = grp.Any(s => s.StepId == ((int)OpenStepEnum.BranchAgentAssignmentConfirmed) && s.IsDone == true),
                                        IsFormSubmitted = grp.Any(s => s.StepId == ((int)OpenStepEnum.AgentFormSumbitted) && s.IsDone == true),
                                        IsFormSubmittedToCCAgent = grp.Any(s => s.StepId == ((int)OpenStepEnum.ContactManagmentFormSumbitted) && s.IsDone == true),
                                        IsFormSubmittedToBM = grp.Any(s => s.StepId == ((int)OpenStepEnum.BranchAgentFormSumbitted) && s.IsDone == true),
                                        IsNeedToAssign = grp.Any(s => s.StepId == ((int)OpenStepEnum.BranchAgentAssignment) && s.IsDone == false),
                                        IsNeedToAssignToCCAgent = grp.Any(s => s.StepId == ((int)OpenStepEnum.ContactManagmentAssignment) && s.IsDone == false),
                                        IsNeedToAssignToBM = grp.Any(s => s.StepId == ((int)OpenStepEnum.BranchAgentAssignmentConfirmed) && s.IsDone == false),
                                        DiffDateNotify = grp.LastOrDefault(s => s.StepId == ((int)OpenStepEnum.BranchManagmentNotification))?.DiffDateAssignSubmit ?? 0,
                                        DiffDateNotifyToCCAgent = grp.LastOrDefault(s => s.StepId == ((int)OpenStepEnum.ContactManagmentNotification))?.DiffDateAssignSubmit ?? 0,
                                        DiffDateNotifyToBM = grp.LastOrDefault(s => s.StepId == ((int)OpenStepEnum.BranchAgentNotification))?.DiffDateAssignSubmit ?? 0,


                                        AssignmentModel = grp.Any(s => s.StepId == ((int)OpenStepEnum.BranchAgentAssignment) && s.IsDone == true) ?
                                        new AssignmentModel()
                                        {
                                            AssignID = grp.LastOrDefault(s => s.StepId == ((int)OpenStepEnum.BranchAgentAssignment)).AssignId.ToString(),
                                            AssignFromName = grp.LastOrDefault(s => s.StepId == ((int)OpenStepEnum.BranchAgentAssignment)).AssignFromName,
                                            AssignToName = grp.LastOrDefault(s => s.StepId == ((int)OpenStepEnum.BranchAgentAssignment)).AssignToName,
                                            AssignmentDate = grp.LastOrDefault(s => s.StepId == ((int)OpenStepEnum.BranchAgentAssignment)).AssignCreationDate.ToString(),
                                            AssignToId = grp.LastOrDefault(s => s.StepId == ((int)OpenStepEnum.BranchAgentAssignment)).ToUser ?? 0,
                                            DiffDate = grp.LastOrDefault(s => s.StepId == ((int)OpenStepEnum.BranchAgentAssignment))?.DiffDateAssignSubmit ?? 0,

                                        } : null,
                                        CCAgentAssignmentModel = grp.Any(s => s.StepId == ((int)OpenStepEnum.ContactManagmentAssignment) && s.IsDone == true) ?
                                        new AssignmentModel()
                                        {
                                            AssignID = grp.LastOrDefault(s => s.StepId == ((int)OpenStepEnum.ContactManagmentAssignment)).AssignId.ToString(),
                                            AssignFromName = grp.LastOrDefault(s => s.StepId == ((int)OpenStepEnum.ContactManagmentAssignment)).AssignFromName,
                                            AssignToName = grp.LastOrDefault(s => s.StepId == ((int)OpenStepEnum.ContactManagmentAssignment)).AssignToName,
                                            AssignmentDate = grp.LastOrDefault(s => s.StepId == ((int)OpenStepEnum.ContactManagmentAssignment)).AssignCreationDate.ToString(),
                                            AssignToId = grp.LastOrDefault(s => s.StepId == ((int)OpenStepEnum.ContactManagmentAssignment)).ToUser ?? 0,
                                            DiffDate = grp.LastOrDefault(s => s.StepId == ((int)OpenStepEnum.ContactManagmentAssignment))?.DiffDateAssignSubmit ?? 0,

                                        } : null,
                                        BMAssignmentModel = grp.Any(s => s.StepId == ((int)OpenStepEnum.BranchAgentAssignmentConfirmed) && s.IsDone == true) ?
                                        new AssignmentModel()
                                        {
                                            AssignID = grp.LastOrDefault(s => s.StepId == ((int)OpenStepEnum.BranchAgentAssignmentConfirmed)).AssignId.ToString(),
                                            AssignFromName = grp.LastOrDefault(s => s.StepId == ((int)OpenStepEnum.BranchAgentAssignmentConfirmed)).AssignFromName,
                                            AssignToName = grp.LastOrDefault(s => s.StepId == ((int)OpenStepEnum.BranchAgentAssignmentConfirmed)).AssignToName,
                                            AssignmentDate = grp.LastOrDefault(s => s.StepId == ((int)OpenStepEnum.BranchAgentAssignmentConfirmed)).AssignCreationDate.ToString(),
                                            AssignToId = grp.LastOrDefault(s => s.StepId == ((int)OpenStepEnum.BranchAgentAssignmentConfirmed)).ToUser ?? 0,
                                            DiffDate = grp.LastOrDefault(s => s.StepId == ((int)OpenStepEnum.BranchAgentAssignmentConfirmed))?.DiffDateAssignSubmit ?? 0,

                                        } : null,
                                        FormSumbitModel = grp.Any(s => s.StepId == ((int)OpenStepEnum.AgentFormSumbitted) && s.IsDone == true) ?
                                        new FormSumbitModel()
                                        {
                                            FormSubmitID = grp.LastOrDefault(s => s.StepId == ((int)OpenStepEnum.AgentFormSumbitted)).FormSubmitId.ToString() ?? string.Empty,
                                            FormSubmitedBy = grp.LastOrDefault(s => s.StepId == ((int)OpenStepEnum.AgentFormSumbitted)).FormSubmitCreatedByName ?? string.Empty,
                                            //ReasonStatus = grp.LastOrDefault(s => s.StepId == ((int)OpenStepEnum.AgentFormSumbitted)).ReasonStatus.ToString()??String.Empty,
                                            //ReasonText = grp.LastOrDefault(s => s.StepId == ((int)OpenStepEnum.AgentFormSumbitted)).ReasonText.ToString()??string.Empty,
                                            FormSubmitionDate = grp.LastOrDefault(s => s.StepId == ((int)OpenStepEnum.AgentFormSumbitted)).FormSubmitCreationDate.ToString() ?? string.Empty,
                                            //FormSubmitedComment = grp.LastOrDefault(s => s.StepId == ((int)OpenStepEnum.AgentFormSumbitted)).Comment ?? string.Empty,
                                        } : null,
                                        CCAgentFormSumbitModel = grp.Any(s => s.StepId == ((int)OpenStepEnum.ContactManagmentFormSumbitted) && s.IsDone == true) ?
                                        new FormSumbitModel()
                                        {
                                            FormSubmitID = grp.LastOrDefault(s => s.StepId == ((int)OpenStepEnum.ContactManagmentFormSumbitted)).FormSubmitId.ToString() ?? string.Empty,
                                            FormSubmitedBy = grp.LastOrDefault(s => s.StepId == ((int)OpenStepEnum.ContactManagmentFormSumbitted)).FormSubmitCreatedByName ?? string.Empty,
                                            //ReasonStatus = grp.LastOrDefault(s => s.StepId == ((int)OpenStepEnum.AgentFormSumbitted)).ReasonStatus.ToString()??String.Empty,
                                            //ReasonText = grp.LastOrDefault(s => s.StepId == ((int)OpenStepEnum.AgentFormSumbitted)).ReasonText.ToString()??string.Empty,
                                            FormSubmitionDate = grp.LastOrDefault(s => s.StepId == ((int)OpenStepEnum.ContactManagmentFormSumbitted)).FormSubmitCreationDate.ToString() ?? string.Empty,
                                            //FormSubmitedComment = grp.LastOrDefault(s => s.StepId == ((int)OpenStepEnum.AgentFormSumbitted)).Comment ?? string.Empty,
                                        } : null,
                                        BMFormSumbitModel = grp.Any(s => s.StepId == ((int)OpenStepEnum.BranchAgentFormSumbitted) && s.IsDone == true) ?
                                        new FormSumbitModel()
                                        {
                                            FormSubmitID = grp.LastOrDefault(s => s.StepId == ((int)OpenStepEnum.BranchAgentFormSumbitted)).FormSubmitId.ToString() ?? string.Empty,
                                            FormSubmitedBy = grp.LastOrDefault(s => s.StepId == ((int)OpenStepEnum.BranchAgentFormSumbitted)).FormSubmitCreatedByName ?? string.Empty,
                                            //ReasonStatus = grp.LastOrDefault(s => s.StepId == ((int)OpenStepEnum.AgentFormSumbitted)).ReasonStatus.ToString()??String.Empty,
                                            //ReasonText = grp.LastOrDefault(s => s.StepId == ((int)OpenStepEnum.AgentFormSumbitted)).ReasonText.ToString()??string.Empty,
                                            FormSubmitionDate = grp.LastOrDefault(s => s.StepId == ((int)OpenStepEnum.BranchAgentFormSumbitted)).FormSubmitCreationDate.ToString() ?? string.Empty,
                                            //FormSubmitedComment = grp.LastOrDefault(s => s.StepId == ((int)OpenStepEnum.AgentFormSumbitted)).Comment ?? string.Empty,
                                        } : null
                                    });

                    if (reservationSC.ActionStepId.HasValue && reservationSC.ActionStepId.Value > 0)
                    {
                        if (reservationSC.ActionStepId == (int)OpenStepEnum.NoAssignmentNotification) // notified but no asignment 
                        {
                            //query = query.Where(r => r.StepId == ((int)CacelledByCustomerStepEnum.ContactCenterAgentAssignment) && r.IsDone == false && r.DiffDateAssignSubmit >= 24); //
                            Group = Group.Where(g => g.IsAssigned != true && g.IsNeedToAssign == true && g.DiffDateNotify >= 24);//
                        }
                        else if (reservationSC.ActionStepId == (int)OpenStepEnum.BranchAgentAssignment) // assigned but no form sumbitted 
                        {
                            Group = Group.Where(g => g.IsFormSubmitted != true && g.IsAssigned == true && g.AssignmentModel.DiffDate >= 24);//
                        }
                        else if (reservationSC.ActionStepId == (int)OpenStepEnum.AgentFormSumbitted) // Assigned ToMe 
                        {
                            Group = Group.Where(g => g.IsFormSubmitted != true && g.IsAssigned == true && g.AssignmentModel.AssignToId == reservationSC.AssignedToId);
                        }
                        if (reservationSC.ActionStepId == (int)OpenStepEnum.ContactManagmentNoAssignmentNotification) // notified but no asignment 
                        {
                            Group = Group.Where(g => g.IsAssignedToCCAgent != true && g.IsNeedToAssignToCCAgent == true && g.DiffDateNotifyToCCAgent >= 24);//
                        }
                        else if (reservationSC.ActionStepId == (int)OpenStepEnum.ContactManagmentAssignment) // assigned but no form sumbitted 
                        {
                            Group = Group.Where(g => g.IsFormSubmittedToCCAgent != true && g.IsAssignedToCCAgent == true && g.CCAgentAssignmentModel.DiffDate >= 24);//
                        }
                        else if (reservationSC.ActionStepId == (int)OpenStepEnum.ContactManagmentFormSumbitted) // Assigned ToMe 
                        {
                            Group = Group.Where(g => g.IsFormSubmittedToCCAgent != true && g.IsAssignedToCCAgent == true && g.CCAgentAssignmentModel.AssignToId == reservationSC.AssignedToId);
                        }

                        if (reservationSC.ActionStepId == (int)OpenStepEnum.BranchAgentNoAssignmentNotification) // notified but no asignment  BM confirmed
                        {
                            Group = Group.Where(g => g.IsAssignedToBM != true && g.IsNeedToAssignToBM == true && g.DiffDateNotifyToBM >= 24);//
                        }
                        else if (reservationSC.ActionStepId == (int)OpenStepEnum.BranchAgentAssignmentConfirmed) // assigned but no form sumbitted BM confirmed
                        {
                            Group = Group.Where(g => g.IsFormSubmittedToBM != true && g.IsAssignedToBM == true && g.BMAssignmentModel.DiffDate >= 24);//
                        }
                        else if (reservationSC.ActionStepId == (int)OpenStepEnum.BranchAgentFormSumbitted) // Assigned ToMe  BM confirmed
                        {
                            Group = Group.Where(g => g.IsFormSubmittedToBM != true && g.IsAssignedToBM == true && g.BMAssignmentModel.AssignToId == reservationSC.AssignedToId);
                        }
                    }
                    result.Total = Group.Count();
                    Group = Group.OrderByDescending(p => p.ReservationNo);
                    if (page.GetValueOrDefault() > 0 && pageSize.GetValueOrDefault() > 0)
                    {
                        Group = Group
                                    .Skip((page.Value - 1) * pageSize.Value)
                                    .Take(pageSize.Value);
                    }
                    result.Items = Group.ToList();

                    return result;

                }
                return null;


            }
            catch (Exception e)
            {

                return null;
            }



        }

        public List<VReservationListModel> GetReservationsOpen(ReservationSC reservationSC)
        {
            var result = new List<VReservationListModel>();
            try
            {
                var query = Context.VreservationList.AsQueryable();
                if (reservationSC != null)
                {
                    if (reservationSC.ReservationNum.HasValue && reservationSC.ReservationNum.Value > 0)
                    {
                        query = query.Where(r => r.ReservationNum == reservationSC.ReservationNum);
                    }
                    if (reservationSC.ReservationId.HasValue && reservationSC.ReservationId.Value > 0)
                    {
                        query = query.Where(r => r.Id == reservationSC.ReservationId);
                    }

                    if (reservationSC.ReservationStatusId.HasValue && reservationSC.ReservationStatusId.Value > 0)
                    {
                        query = query.Where(r => r.ReservationStatusId == reservationSC.ReservationStatusId);
                    }
                    if (reservationSC.BookingDateFrom.HasValue && reservationSC.BookingDateFrom.Value != DateTime.MinValue)
                    {
                        query = query.Where(r => r.BookingDate >= reservationSC.BookingDateFrom);
                    }
                    if (reservationSC.BookingDateTo.HasValue && reservationSC.BookingDateTo.Value != DateTime.MinValue)
                    {
                        query = query.Where(r => r.BookingDate <= reservationSC.BookingDateTo);
                    }
                    if (reservationSC.PickUpDateFrom.HasValue && reservationSC.PickUpDateFrom.Value != DateTime.MinValue)
                    {
                        query = query.Where(r => r.PickUpDate >= reservationSC.PickUpDateFrom);
                    }
                    if (reservationSC.PickUpDateTo.HasValue && reservationSC.PickUpDateTo.Value != DateTime.MinValue)
                    {
                        query = query.Where(r => r.PickUpDate <= reservationSC.PickUpDateTo);
                    }

                    if (reservationSC.CancelDateFrom.HasValue && reservationSC.CancelDateFrom.Value != DateTime.MinValue)
                    {
                        query = query.Where(r => r.CancelledDate >= reservationSC.CancelDateFrom);
                    }
                    if (reservationSC.CancelDateTo.HasValue && reservationSC.CancelDateTo.Value != DateTime.MinValue)
                    {
                        query = query.Where(r => r.CancelledDate <= reservationSC.CancelDateTo);
                    }

                    if (reservationSC.DropOffDate.HasValue && reservationSC.DropOffDate.Value != DateTime.MinValue)
                    {
                        query = query.Where(r => r.DropOffDate <= reservationSC.DropOffDate);
                    }

                    if (!string.IsNullOrWhiteSpace(reservationSC.CustomerName))
                    {
                        query = query.Where(r => r.CustomerName.Contains(reservationSC.CustomerName));
                    }
                    if (!string.IsNullOrWhiteSpace(reservationSC.VehicleAcriss))
                    {
                        query = query.Where(r => r.VehicleAcriss == reservationSC.VehicleAcriss);
                    }
                    if (reservationSC.CancelledAfterBookingDays != null)
                    {
                        query = query.Where(r => r.CancelledAfterBookingInDays == reservationSC.CancelledAfterBookingDays);
                    }
                    if (reservationSC.RentalDays.HasValue && reservationSC.RentalDays.Value > 0)
                    {
                        query = query.Where(r => r.RentalDays == reservationSC.RentalDays);
                    }

                    if (reservationSC.PickUpBranchIds?.Length > 0 && !reservationSC.PickUpBranchIds.Contains(null))
                    {
                        query = query.Where(r => reservationSC.PickUpBranchIds.Contains(r.PickUpBranchId.GetValueOrDefault()));
                    }
                    DateTime yesterday = DateTime.Now.AddDays(-1);
                    if (reservationSC.ActionStepId.HasValue && reservationSC.ActionStepId.Value > 0)
                    {
                        if (reservationSC.ActionStepId == (int)OpenStepEnum.BranchManagmentNotification) // need assignment 
                        {
                            query = query.Where(r => r.StepId == ((int)OpenStepEnum.BranchAgentAssignment) && r.IsDone == false);
                        }

                        else if (reservationSC.ActionStepId == (int)OpenStepEnum.NoFormsumbittedNotification) // form not sumbitted 
                        {
                            query = query.Where(r => r.StepId == ((int)OpenStepEnum.AgentFormSumbitted) && r.IsDone == false);
                        }
                        if (reservationSC.ActionStepId == (int)OpenStepEnum.ContactManagmentNotification) // need assignment  CC
                        {
                            query = query.Where(r => r.StepId == ((int)OpenStepEnum.ContactManagmentAssignment) && r.IsDone == false);
                        }

                    }
                    //if (  reservationSC.AssignedToMe == true)
                    //{
                    //    query = query.Where(r => r.ToUser == reservationSC.AssignedToId);
                    // }

                    var temp = query.OrderBy(s => s.StepId).ToList();
                    var Group = temp
                                     .GroupBy(b => b.ReservationNum)
                                     .Select(grp => new VReservationListModel
                                     {
                                         ReservationNo = grp.Key,
                                         ReservationList = grp.LastOrDefault(),
                                         NextStep = grp.LastOrDefault(s => s.IsDone == false) != null ?
                                                                     grp.LastOrDefault(s => s.IsDone == false).StepName :
                                                                     string.Empty,
                                         IsAssigned = grp.Any(s => s.StepId == ((int)OpenStepEnum.BranchAgentAssignment) && s.IsDone == true),
                                         IsAssignedToCCAgent = grp.Any(s => s.StepId == ((int)OpenStepEnum.ContactManagmentAssignment) && s.IsDone == true),
                                         IsFormSubmitted = grp.Any(s => s.StepId == ((int)OpenStepEnum.AgentFormSumbitted) && s.IsDone == true),
                                         IsFormSubmittedToCCAgent = grp.Any(s => s.StepId == ((int)OpenStepEnum.ContactManagmentFormSumbitted) && s.IsDone == true),
                                         IsNeedToAssign = grp.Any(s => s.StepId == ((int)OpenStepEnum.BranchAgentAssignment) && s.IsDone == false),
                                         IsNeedToAssignToCCAgent = grp.Any(s => s.StepId == ((int)OpenStepEnum.ContactManagmentAssignment) && s.IsDone == false),
                                         DiffDateNotify = grp.LastOrDefault(s => s.StepId == ((int)OpenStepEnum.BranchManagmentNotification))?.DiffDateAssignSubmit ?? 0,
                                         DiffDateNotifyToCCAgent = grp.LastOrDefault(s => s.StepId == ((int)OpenStepEnum.ContactManagmentNotification))?.DiffDateAssignSubmit ?? 0,

                                         //Assignee = grp
                                         //               .LastOrDefault(s => s.StepId == ((int)CacelledByCustomerStepEnum.ContactCenterAgentAssignment)) != null ?
                                         //               grp.LastOrDefault(s => s.StepId == ((int)CacelledByCustomerStepEnum.ContactCenterAgentAssignment)).AssignToName :
                                         //               string.Empty,

                                         //AssignFrom = grp.LastOrDefault(s => s.StepId == ((int)CacelledByCustomerStepEnum.ContactCenterAgentAssignment)) != null ? grp.LastOrDefault(s => s.StepId == ((int)CacelledByCustomerStepEnum.ContactCenterAgentAssignment)).AssignFromName : "",
                                         //AssignCreationDate = grp.LastOrDefault(s => s.StepId == ((int)CacelledByCustomerStepEnum.ContactCenterAgentAssignment)) != null ? grp.LastOrDefault(s => s.StepId == ((int)CacelledByCustomerStepEnum.ContactCenterAgentAssignment)).AssignCreationDate.ToString() : ""
                                         //,
                                         AssignmentModel = grp.Any(s => s.StepId == ((int)OpenStepEnum.BranchAgentAssignment) && s.IsDone == true) ?
                                         new AssignmentModel()
                                         {
                                             AssignID = grp.LastOrDefault(s => s.StepId == ((int)OpenStepEnum.BranchAgentAssignment)).AssignId.ToString(),
                                             AssignFromName = grp.LastOrDefault(s => s.StepId == ((int)OpenStepEnum.BranchAgentAssignment)).AssignFromName,
                                             AssignToName = grp.LastOrDefault(s => s.StepId == ((int)OpenStepEnum.BranchAgentAssignment)).AssignToName,
                                             AssignmentDate = grp.LastOrDefault(s => s.StepId == ((int)OpenStepEnum.BranchAgentAssignment)).AssignCreationDate.ToString(),
                                             AssignToId = grp.LastOrDefault(s => s.StepId == ((int)OpenStepEnum.BranchAgentAssignment)).ToUser ?? 0,
                                             DiffDate = grp.LastOrDefault(s => s.StepId == ((int)OpenStepEnum.BranchAgentAssignment))?.DiffDateAssignSubmit ?? 0,

                                         } : null,
                                         CCAgentAssignmentModel = grp.Any(s => s.StepId == ((int)OpenStepEnum.ContactManagmentAssignment) && s.IsDone == true) ?
                                         new AssignmentModel()
                                         {
                                             AssignID = grp.LastOrDefault(s => s.StepId == ((int)OpenStepEnum.ContactManagmentAssignment)).AssignId.ToString(),
                                             AssignFromName = grp.LastOrDefault(s => s.StepId == ((int)OpenStepEnum.ContactManagmentAssignment)).AssignFromName,
                                             AssignToName = grp.LastOrDefault(s => s.StepId == ((int)OpenStepEnum.ContactManagmentAssignment)).AssignToName,
                                             AssignmentDate = grp.LastOrDefault(s => s.StepId == ((int)OpenStepEnum.ContactManagmentAssignment)).AssignCreationDate.ToString(),
                                             AssignToId = grp.LastOrDefault(s => s.StepId == ((int)OpenStepEnum.ContactManagmentAssignment)).ToUser ?? 0,
                                             DiffDate = grp.LastOrDefault(s => s.StepId == ((int)OpenStepEnum.ContactManagmentAssignment))?.DiffDateAssignSubmit ?? 0,

                                         } : null,
                                         FormSumbitModel = grp.Any(s => s.StepId == ((int)OpenStepEnum.AgentFormSumbitted) && s.IsDone == true) ?
                                         new FormSumbitModel()
                                         {
                                             FormSubmitID = grp.LastOrDefault(s => s.StepId == ((int)OpenStepEnum.AgentFormSumbitted)).FormSubmitId.ToString() ?? string.Empty,
                                             FormSubmitedBy = grp.LastOrDefault(s => s.StepId == ((int)OpenStepEnum.AgentFormSumbitted)).FormSubmitCreatedByName.ToString() ?? string.Empty,
                                             //ReasonStatus = grp.LastOrDefault(s => s.StepId == ((int)OpenStepEnum.AgentFormSumbitted)).ReasonStatus.ToString()??String.Empty,
                                             //ReasonText = grp.LastOrDefault(s => s.StepId == ((int)OpenStepEnum.AgentFormSumbitted)).ReasonText.ToString()??string.Empty,
                                             FormSubmitionDate = grp.LastOrDefault(s => s.StepId == ((int)OpenStepEnum.AgentFormSumbitted)).FormSubmitCreationDate.ToString() ?? string.Empty,
                                             //FormSubmitedComment = grp.LastOrDefault(s => s.StepId == ((int)OpenStepEnum.AgentFormSumbitted)).Comment ?? string.Empty,
                                         } : null,
                                         CCAgentFormSumbitModel = grp.Any(s => s.StepId == ((int)OpenStepEnum.ContactManagmentFormSumbitted) && s.IsDone == true) ?
                                         new FormSumbitModel()
                                         {
                                             FormSubmitID = grp.LastOrDefault(s => s.StepId == ((int)OpenStepEnum.ContactManagmentFormSumbitted)).FormSubmitId.ToString() ?? string.Empty,
                                             FormSubmitedBy = grp.LastOrDefault(s => s.StepId == ((int)OpenStepEnum.ContactManagmentFormSumbitted)).FormSubmitCreatedByName.ToString() ?? string.Empty,
                                             //ReasonStatus = grp.LastOrDefault(s => s.StepId == ((int)OpenStepEnum.AgentFormSumbitted)).ReasonStatus.ToString()??String.Empty,
                                             //ReasonText = grp.LastOrDefault(s => s.StepId == ((int)OpenStepEnum.AgentFormSumbitted)).ReasonText.ToString()??string.Empty,
                                             FormSubmitionDate = grp.LastOrDefault(s => s.StepId == ((int)OpenStepEnum.ContactManagmentFormSumbitted)).FormSubmitCreationDate.ToString() ?? string.Empty,
                                             //FormSubmitedComment = grp.LastOrDefault(s => s.StepId == ((int)OpenStepEnum.AgentFormSumbitted)).Comment ?? string.Empty,
                                         } : null
                                     });

                    if (reservationSC.ActionStepId.HasValue && reservationSC.ActionStepId.Value > 0)
                    {
                        if (reservationSC.ActionStepId == (int)OpenStepEnum.NoAssignmentNotification) // notified but no asignment 
                        {
                            //query = query.Where(r => r.StepId == ((int)CacelledByCustomerStepEnum.ContactCenterAgentAssignment) && r.IsDone == false && r.DiffDateAssignSubmit >= 24); //
                            Group = Group.Where(g => g.IsAssigned != true && g.IsNeedToAssign == true && g.DiffDateNotify >= 24);//
                        }
                        else if (reservationSC.ActionStepId == (int)OpenStepEnum.BranchAgentAssignment) // assigned but no form sumbitted 
                        {
                            Group = Group.Where(g => g.IsFormSubmitted != true && g.IsAssigned == true && g.AssignmentModel.DiffDate >= 24);//
                        }
                        else if (reservationSC.ActionStepId == (int)OpenStepEnum.AgentFormSumbitted) // Assigned ToMe 
                        {
                            Group = Group.Where(g => g.IsFormSubmitted != true && g.IsAssigned == true && g.AssignmentModel.AssignToId == reservationSC.AssignedToId);
                        }
                        if (reservationSC.ActionStepId == (int)OpenStepEnum.ContactManagmentNoAssignmentNotification) // notified but no asignment 
                        {
                            Group = Group.Where(g => g.IsAssignedToCCAgent != true && g.IsNeedToAssignToCCAgent == true && g.DiffDateNotifyToCCAgent >= 24);//
                        }
                        else if (reservationSC.ActionStepId == (int)OpenStepEnum.ContactManagmentAssignment) // assigned but no form sumbitted 
                        {
                            Group = Group.Where(g => g.IsFormSubmittedToCCAgent != true && g.IsAssignedToCCAgent == true && g.CCAgentAssignmentModel.DiffDate >= 24);//
                        }
                        else if (reservationSC.ActionStepId == (int)OpenStepEnum.ContactManagmentFormSumbitted) // Assigned ToMe 
                        {
                            Group = Group.Where(g => g.IsFormSubmittedToCCAgent != true && g.IsAssignedToCCAgent == true && g.CCAgentAssignmentModel.AssignToId == reservationSC.AssignedToId);
                        }
                    }
                    result = Group.ToList();
                    return result;

                }
                return null;


            }
            catch (Exception e)
            {

                return null;
            }



        }



        public PaginationResult<VReservationListModel> GetAllReservationsNeedToAssign(ReservationSC reservationSC, int? page = null, int? pageSize = null)
        {
            var result = new PaginationResult<VReservationListModel>
            {
                Page = page ?? 0,
                PageSize = pageSize ?? 0,
            };

            try
            {
                var query = Context.VreservationList.AsQueryable();
                if (reservationSC != null)
                {
                    if (reservationSC.ReservationNum.HasValue && reservationSC.ReservationNum.Value > 0)
                    {
                        query = query.Where(r => r.ReservationNum == reservationSC.ReservationNum);
                    }
                    if (reservationSC.ReservationId.HasValue && reservationSC.ReservationId.Value > 0)
                    {
                        query = query.Where(r => r.Id == reservationSC.ReservationId);
                    }

                    if (reservationSC.ReservationStatusId.HasValue && reservationSC.ReservationStatusId.Value > 0)
                    {
                        query = query.Where(r => r.ReservationStatusId == reservationSC.ReservationStatusId);
                    }
                    if (reservationSC.BookingDateFrom.HasValue && reservationSC.BookingDateFrom.Value != DateTime.MinValue)
                    {
                        query = query.Where(r => r.BookingDate >= reservationSC.BookingDateFrom);
                    }
                    if (reservationSC.BookingDateTo.HasValue && reservationSC.BookingDateTo.Value != DateTime.MinValue)
                    {
                        query = query.Where(r => r.BookingDate <= reservationSC.BookingDateTo);
                    }
                    if (reservationSC.PickUpDateFrom.HasValue && reservationSC.PickUpDateFrom.Value != DateTime.MinValue)
                    {
                        query = query.Where(r => r.PickUpDate >= reservationSC.PickUpDateFrom);
                    }
                    if (reservationSC.PickUpDateTo.HasValue && reservationSC.PickUpDateTo.Value != DateTime.MinValue)
                    {
                        query = query.Where(r => r.PickUpDate <= reservationSC.PickUpDateTo);
                    }

                    if (reservationSC.CancelDateFrom.HasValue && reservationSC.CancelDateFrom.Value != DateTime.MinValue)
                    {
                        query = query.Where(r => r.CancelledDate >= reservationSC.CancelDateFrom);
                    }
                    if (reservationSC.CancelDateTo.HasValue && reservationSC.CancelDateTo.Value != DateTime.MinValue)
                    {
                        query = query.Where(r => r.CancelledDate <= reservationSC.CancelDateTo);
                    }

                    if (reservationSC.DropOffDate.HasValue && reservationSC.DropOffDate.Value != DateTime.MinValue)
                    {
                        query = query.Where(r => r.DropOffDate <= reservationSC.DropOffDate);
                    }

                    if (!string.IsNullOrWhiteSpace(reservationSC.CustomerName))
                    {
                        query = query.Where(r => r.CustomerName.Contains(reservationSC.CustomerName));
                    }
                    if (!string.IsNullOrWhiteSpace(reservationSC.VehicleAcriss))
                    {
                        query = query.Where(r => r.VehicleAcriss == reservationSC.VehicleAcriss);
                    }
                    if (reservationSC.CancelledAfterBookingDays != null)
                    {
                        query = query.Where(r => r.CancelledAfterBookingInDays == reservationSC.CancelledAfterBookingDays);
                    }
                    if (reservationSC.RentalDays.HasValue && reservationSC.RentalDays.Value > 0)
                    {
                        query = query.Where(r => r.RentalDays == reservationSC.RentalDays);
                    }

                    if (reservationSC.PickUpBranchIds?.Length > 0 && !reservationSC.PickUpBranchIds.Contains(null))
                    {
                        query = query.Where(r => reservationSC.PickUpBranchIds.Contains(r.PickUpBranchId.GetValueOrDefault()));
                    }
                    query = query.Where(r => r.StepId == ((int)OpenStepEnum.ChangeReservationStatus) && r.IsDone == false);
                    DateTime yesterday = DateTime.Now.AddDays(-1);

                    var temp = query.OrderBy(s => s.StepId).ToList();
                    var Group = temp
                                    .GroupBy(b => b.ReservationNum)
                                    .Select(grp => new VReservationListModel
                                    {
                                        ReservationNo = grp.Key,
                                        ReservationList = grp.LastOrDefault(),
                                       
                                    });

                   

                    result.Total = Group.Count();
                    Group = Group.OrderByDescending(p => p.ReservationList.CreationDate);
                    if (page.GetValueOrDefault() > 0 && pageSize.GetValueOrDefault() > 0)
                    {
                        Group = Group
                                    .Skip((page.Value - 1) * pageSize.Value)
                                    .Take(pageSize.Value);
                    }
                    result.Items = Group.ToList();

                    return result;

                }
                return null;


            }
            catch (Exception e)
            {

                return null;
            }



        }


    }
}