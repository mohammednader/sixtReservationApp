using SIXTReservationBL.Models.ViewModels;
using SIXTReservationBL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIXTReservationApp.Models.CByCustomerReservation
{
    public class CByCustomerSearchVM<T>
    {
        public CByCustomerSearchVM()
        {
            SearchInfo = new ReservationSC();
            PageData = new PagedData<T>();
        }

        public ReservationSC SearchInfo { set; get; }

        public PagedData<T> PageData { set; get; }

    }
}
