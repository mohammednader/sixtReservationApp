using System;
using System.Collections.Generic;
using System.Text;

namespace SIXTReservationBL.Models.ViewModels
{
    public class UploadSC
    {
        public long UploadId { get; set; }
        public int?[] UploadStatusId { get; set; }
        public DateTime? UploadDateFrom { get; set; }
        public DateTime? UploadDateTo { get; set; }
    }
}
