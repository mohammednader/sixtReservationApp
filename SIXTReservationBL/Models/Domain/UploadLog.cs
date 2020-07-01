using System;
using System.Collections.Generic;

namespace SIXTReservationBL.Models.Domain
{
    public partial class UploadLog
    {
        public int Id { get; set; }
        public string FilePath { get; set; }
        public int? NumberOfEntries { get; set; }
        public DateTime? CreationTime { get; set; }
        public int? UserId { get; set; }
        public DateTime? ParsingStartTime { get; set; }
        public DateTime? ParsingCompleteTime { get; set; }
        public int? SavedRecordNum { get; set; }
        public int? UploadStatus { get; set; }
        public string ErrorLogPath { get; set; }
        public string FailureMsg { get; set; }
        public int? FailedRecordNum { get; set; }
        public string OriginalFileName { get; set; }

        public virtual AppUser User { get; set; }
    }
}
