using System;
using System.Collections.Generic;
using System.Text;

namespace SIXTReservationBL.Models.ViewModels
{
   public class RoleSC
    {
        public string Name { get; set; }
        public List<int> CreatedBy { get; set; }
        public DateTime? CreationDateFrom { get; set; }
        public DateTime? CreationDateTo { get; set; }

        public RoleSC()
        {
            CreatedBy = new List<int>();
        }
    }
}
