using System;
using System.Collections.Generic;
using System.Text;

namespace SIXTReservationBL.Repositories
{
    public class PagedData<T>
    {
        public List<T> PagedList { get; set; }

        public int PageCount { get; set; }

        public int RowCount { get; set; }

        public int Start { get; set; }

        public int End { get; set; }
    }
}
