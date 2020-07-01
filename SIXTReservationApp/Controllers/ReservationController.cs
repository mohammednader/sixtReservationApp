using SIXTReservationApp.Auth;
using SIXTReservationApp.Hubs;
using SIXTReservationBL.CoreBL;

namespace SIXTReservationApp.Controllers
{
    [AppAuthorize]
    public partial class ReservationController : BaseController
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly Notify Notify;
        public ReservationController(IUnitOfWork _UnitOfWork, Notify notify)
        {
            unitOfWork = _UnitOfWork;
            Notify = notify;
        }         

    }
   
}