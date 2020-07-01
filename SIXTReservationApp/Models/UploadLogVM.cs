using SIXTReservationBL.Models;
using SIXTReservationBL.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIXTReservationApp.Models
{
    public class UploadLogVM
    {
        public int Id { get; set; }
        public string FilePath { get; set; }
        public int NumberOfEntries { get; set; }
        public string UploadedTime { get; set; }
        public int? UserId { get; set; }
        public string UploadedBy { get; set; }
        public int SavedRecordNum { get; set; }
        public int FailedRecordNum { get; set; }
        public string UploadStatus { get; set; }
        public string ErrorLogPath { get; set; }
        public string FailureMsg { get; set; }
        public string FileName { get; set; }
        public UploadLogVM(UploadLog log)
        {
            if (log != null)
            {
                Id = log.Id;
                UploadedBy = log.User?.FullName;
                UploadedTime = log.CreationTime?.ToString();
                UploadStatus = GetUploadStatus(log.UploadStatus.GetValueOrDefault());
                SavedRecordNum = log.SavedRecordNum.GetValueOrDefault();
                FailedRecordNum = log.FailedRecordNum.GetValueOrDefault();
                NumberOfEntries = log.NumberOfEntries.GetValueOrDefault();
                ErrorLogPath = log.ErrorLogPath;
                FailureMsg = log.FailureMsg;
                FileName = log.OriginalFileName;
            }

        }

        public string GetUploadStatus(int status)
        {
            return Enum.GetName(typeof(UploadStatusEnum), status).ToSentenceCase();
        }

    }
}
