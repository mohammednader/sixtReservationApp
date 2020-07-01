using SIXTReservationBL.CoreBL.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIXTReservationBL.CoreBL
{
    public interface IUnitOfWork : IDisposable
    {
        //Define Interfaces
        IBranchRepository BranchBL { get; }
        IUserRepository UserBL { get; }
        IRoleRepository RoleBL { get; }
        IJobTitleRepository JobTitleBL { get; }
        IReasonRepository ReasonBL { get; }
        IQuestionRepository QuestionBL { get; }
        IReservationStatusRepository ReservationStatusBL { get; }
        IStatusStepRepository StatusStepBL { get; }
        IRateSegmentRepository RateSegmentBL { get; }
        IRateSegmentCategoryRepository RateSegmentCategoryBL { get; }
        INotificationSettingRepository NotificationSettingBL { get; }
        IPermissionRepository PermissionBL { get; }
        ICustomerRepository CustomerBL { get; }
        IUploadLogRepository UploadLogBL { get; }
        IReservationRepository ReservationBL { get; }
        IReservationHistoryRepository ReservationHistoryBL { get; }
        IActionSettingRepository ActionSettingBL { get; }
        IWeekDayRepository WeekDayBL { get; }
        INotificationRepository NotificationBL { get; }
        IReservationStepLogRepository ReservationStepLogBL { get; }
        IVReservationLogRepository  VReservationLogBL { get; }
        IReservationAssignmentRepository ReservationAssignmentBL{ get; }
        IFormSubmittedRepository FormSubmittedBL { get; }
        IVReservationListRepository VReservationListBL { get; }
        IEmailSettingRepository EmailSettingBL { get; }
        IVReservationHistoryRepository VReservationHistoryBL { get; }
        int Complete();
    }
}
