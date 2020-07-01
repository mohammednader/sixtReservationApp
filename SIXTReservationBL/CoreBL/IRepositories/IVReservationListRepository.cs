using SIXTReservationBL.Models.Domain;
using SIXTReservationBL.Models.ViewModels;
using SIXTReservationBL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace SIXTReservationBL.CoreBL.IRepositories
{ 
    public interface IVReservationListRepository : IGenericRepository<VreservationList>
    {
        PaginationResult<VReservationListModel> GetAllReservations(ReservationSC reservationSC, int? page = null, int? pageSize = null);
        List<VReservationListModel> GetReservations(ReservationSC reservationSC);
        List<VReservationListModel> GetAllReservationsCancelBySixt(ReservationSC reservationSC);
        PaginationResult<VReservationListModel> GetReservationsCancelBySixt(ReservationSC reservationSC, int? page = null, int? pageSize = null);
        PaginationResult<VReservationListModel> GetAllReservationsNoShow(ReservationSC reservationSC, int? page = null, int? pageSize = null);
        List<VReservationListModel> GetReservationsNoShow(ReservationSC reservationSC);
        PaginationResult<VReservationListModel> GetAllReservationsOpen(ReservationSC reservationSC, int? page = null, int? pageSize = null);
        List<VReservationListModel> GetReservationsOpen(ReservationSC reservationSC);
        PaginationResult<VReservationListModel> GetAllReservationsNeedToAssign(ReservationSC reservationSC, int? page = null, int? pageSize = null);
    }
}
