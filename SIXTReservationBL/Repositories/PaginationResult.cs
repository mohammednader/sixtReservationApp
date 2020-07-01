using System;
using System.Collections.Generic;
using System.Text;

namespace SIXTReservationBL.Repositories
{
    public class PaginationResult<T>
    {
        public List<T> Items { get; set; }
        public int Total { get; set; }
        public int Page { get; set; }
        public bool CanExport { get; set; }
        public int PageSize { get; set; }
        public PaginationResult()
        {
            Items = new List<T>();
        }
    }
}
