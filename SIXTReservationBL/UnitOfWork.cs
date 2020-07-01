using SIXTReservationBL.CoreBL;
using SIXTReservationBL.CoreBL.IRepositories;
using SIXTReservationBL.Models.Domain;
using System;

namespace SIXTReservationBL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SixtReservationContext _context;
        public IBranchRepository BranchBL { get; }
        public IUserRepository UserBL { get; }
        public IRoleRepository RoleBL { get; }
        public IJobTitleRepository JobTitleBL { get; }
        public IReasonRepository ReasonBL { get; }
        public IQuestionRepository QuestionBL { get; }
        public IReservationStatusRepository ReservationStatusBL { get; }
        public IStatusStepRepository StatusStepBL { get; }
        public IRateSegmentRepository RateSegmentBL { get; }
        public IRateSegmentCategoryRepository RateSegmentCategoryBL { get; }
        public INotificationSettingRepository NotificationSettingBL { get; }
        public IPermissionRepository PermissionBL { get; }
        public ICustomerRepository CustomerBL { get; }
        public IUploadLogRepository UploadLogBL { get; }
        public IReservationRepository ReservationBL { get; }
        public IReservationHistoryRepository ReservationHistoryBL { get; }
        public IActionSettingRepository ActionSettingBL { get; }
        public IWeekDayRepository WeekDayBL { get; }
        public INotificationRepository NotificationBL { get; }
        public IReservationStepLogRepository ReservationStepLogBL { get; }
        public IReservationAssignmentRepository ReservationAssignmentBL { get; }
        public IVReservationLogRepository VReservationLogBL { get; }
        public IFormSubmittedRepository FormSubmittedBL { get; }
        public IVReservationListRepository VReservationListBL { get; }
        public IEmailSettingRepository EmailSettingBL { get; set; }

        public IVReservationHistoryRepository VReservationHistoryBL { get; set; }

        public UnitOfWork(SixtReservationContext context,
            IBranchRepository branchRepository,
            IUserRepository userRepository,
            IRoleRepository roleRepository,
            IJobTitleRepository jobTitleRepository,
            IReasonRepository reasonRepository,
            IQuestionRepository questionRepository,
            IReservationStatusRepository reservationStatusRepository,
            IStatusStepRepository statusStepRepository,
            IRateSegmentRepository rateSegmentRepository,
            IRateSegmentCategoryRepository RateSegmentCategoryRepository,
            INotificationSettingRepository notificationSettingRepository,
            IPermissionRepository permissionRepository,
            ICustomerRepository customerRepository,
            IUploadLogRepository uploadLogRepository,
            IReservationRepository reservationRepository,
            IReservationHistoryRepository reservationHistoryRepository,
            IActionSettingRepository actionSettingRepository,
            IWeekDayRepository weekDayRepository,
            INotificationRepository notificationRepository,
            IReservationStepLogRepository reservationStepLogRepository,
            IVReservationLogRepository vReservationLogRepository,
            IReservationAssignmentRepository reservationAssinmentRepository,
            IFormSubmittedRepository formSubmittedRepository,
            IVReservationListRepository vReservationListRepository,
            IEmailSettingRepository emailSettingRepository,
            IVReservationHistoryRepository vReservationHistoryRepository
            )
        {
            _context = context;
            BranchBL = branchRepository;
            UserBL = userRepository;
            RoleBL = roleRepository;
            JobTitleBL = jobTitleRepository;
            ReasonBL = reasonRepository;
            QuestionBL = questionRepository;
            ReservationStatusBL = reservationStatusRepository;
            StatusStepBL = statusStepRepository;
            NotificationSettingBL = notificationSettingRepository;
            RateSegmentBL = rateSegmentRepository;
            RateSegmentCategoryBL = RateSegmentCategoryRepository;
            PermissionBL = permissionRepository;
            CustomerBL = customerRepository;
            UploadLogBL = uploadLogRepository;
            ReservationBL = reservationRepository;
            ReservationHistoryBL = reservationHistoryRepository;
            ActionSettingBL = actionSettingRepository;
            WeekDayBL = weekDayRepository;
            NotificationBL = notificationRepository;
            ReservationStepLogBL = reservationStepLogRepository;
            VReservationLogBL = vReservationLogRepository;
            ReservationAssignmentBL = reservationAssinmentRepository;
            FormSubmittedBL = formSubmittedRepository;
            VReservationListBL = vReservationListRepository;
            EmailSettingBL = emailSettingRepository;
            VReservationHistoryBL = vReservationHistoryRepository;
        }
        public int Complete()
        {
            try
            {
                return _context.SaveChanges();
            }
            catch (Exception e)
            {

                return -1;
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
