using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SIXTReservationBL.Models.Domain
{
    public partial class SixtReservationContext : DbContext
    {
        public SixtReservationContext(DbContextOptions<SixtReservationContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ActionSetting> ActionSetting { get; set; }
        public virtual DbSet<AppUser> AppUser { get; set; }
        public virtual DbSet<Branch> Branch { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<EmailSetting> EmailSetting { get; set; }
        public virtual DbSet<ExchangeRates> ExchangeRates { get; set; }
        public virtual DbSet<JobTitle> JobTitle { get; set; }
        public virtual DbSet<LnkNotificationJobTitle> LnkNotificationJobTitle { get; set; }
        public virtual DbSet<LnkRolePermission> LnkRolePermission { get; set; }
        public virtual DbSet<LnkUserBranch> LnkUserBranch { get; set; }
        public virtual DbSet<LnkUserRole> LnkUserRole { get; set; }
        public virtual DbSet<Notification> Notification { get; set; }
        public virtual DbSet<NotificationGroup> NotificationGroup { get; set; }
        public virtual DbSet<NotificationSetting> NotificationSetting { get; set; }
        public virtual DbSet<Permission> Permission { get; set; }
        public virtual DbSet<Question> Question { get; set; }
        public virtual DbSet<RateSegment> RateSegment { get; set; }
        public virtual DbSet<RateSegmentCategory> RateSegmentCategory { get; set; }
        public virtual DbSet<Reason> Reason { get; set; }
        public virtual DbSet<Reservation> Reservation { get; set; }
        public virtual DbSet<ReservationAssignement> ReservationAssignement { get; set; }
        public virtual DbSet<ReservationFormSubmit> ReservationFormSubmit { get; set; }
        public virtual DbSet<ReservationHistory> ReservationHistory { get; set; }
        public virtual DbSet<ReservationStatus> ReservationStatus { get; set; }
        public virtual DbSet<ReservationStepsLog> ReservationStepsLog { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<StatusStep> StatusStep { get; set; }
        public virtual DbSet<UploadLog> UploadLog { get; set; }
        public virtual DbSet<VreservationHistory> VreservationHistory { get; set; }
        public virtual DbSet<VreservationList> VreservationList { get; set; }
        public virtual DbSet<VreservationLog> VreservationLog { get; set; }
        public virtual DbSet<Weekdays> Weekdays { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.;Database=SixtReservation;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ActionSetting>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.HasOne(d => d.ActionStep)
                    .WithMany(p => p.ActionSetting)
                    .HasForeignKey(d => d.ActionStepId)
                    .HasConstraintName("FK_LNK_ActionStep");

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.ActionSetting)
                    .HasForeignKey(d => d.BranchId)
                    .HasConstraintName("FK_LNK_Branch");

                entity.HasOne(d => d.RateSegmentCategory)
                    .WithMany(p => p.ActionSetting)
                    .HasForeignKey(d => d.RateSegmentCategoryId)
                    .HasConstraintName("FK_LNK_RateSegmentCategoryId");

                entity.HasOne(d => d.ReservationStatus)
                    .WithMany(p => p.ActionSetting)
                    .HasForeignKey(d => d.ReservationStatusId)
                    .HasConstraintName("FK_LNK_ReservationStatus");

                entity.HasOne(d => d.WeekDay)
                    .WithMany(p => p.ActionSetting)
                    .HasForeignKey(d => d.WeekDayId)
                    .HasConstraintName("FK_LNK_WeekDay");
            });

            modelBuilder.Entity<AppUser>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.JobTitleId).HasColumnName("JobTitleID");

                entity.Property(e => e.LastModificationDate).HasColumnType("datetime");

                entity.Property(e => e.PasswordHash).HasMaxLength(256);

                entity.Property(e => e.PhoneNumber).HasMaxLength(50);

                entity.HasOne(d => d.JobTitle)
                    .WithMany(p => p.AppUser)
                    .HasForeignKey(d => d.JobTitleId)
                    .HasConstraintName("FK_User_JobTitle");
            });

            modelBuilder.Entity<Branch>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(200);
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Comment).HasMaxLength(100);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.Phone).HasMaxLength(50);
            });

            modelBuilder.Entity<ExchangeRates>(entity =>
            {
                entity.Property(e => e.Rate).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<JobTitle>(entity =>
            {
                entity.Property(e => e.JobTitleId).ValueGeneratedNever();
            });

            modelBuilder.Entity<LnkNotificationJobTitle>(entity =>
            {
                entity.HasKey(e => new { e.NotificationId, e.JobTitleId });

                entity.ToTable("LNK_Notification_JobTitle");

                entity.Property(e => e.NotificationId).HasColumnName("NotificationID");

                entity.Property(e => e.JobTitleId).HasColumnName("JobTitleID");

                entity.HasOne(d => d.JobTitle)
                    .WithMany(p => p.LnkNotificationJobTitle)
                    .HasForeignKey(d => d.JobTitleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LNK_JobTitle");

                entity.HasOne(d => d.Notification)
                    .WithMany(p => p.LnkNotificationJobTitle)
                    .HasForeignKey(d => d.NotificationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LNK_Notification");
            });

            modelBuilder.Entity<LnkRolePermission>(entity =>
            {
                entity.HasKey(e => new { e.RoleId, e.PermissionId });

                entity.ToTable("LNK_RolePermission");

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.PermissionId).HasColumnName("PermissionID");

                entity.HasOne(d => d.Permission)
                    .WithMany(p => p.LnkRolePermission)
                    .HasForeignKey(d => d.PermissionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LNK_RolePermission_Permission");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.LnkRolePermission)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LNK_RolePermission_Role");
            });

            modelBuilder.Entity<LnkUserBranch>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.BranchId });

                entity.ToTable("LNK_UserBranch");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.BranchId).HasColumnName("BranchID");

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.LnkUserBranch)
                    .HasForeignKey(d => d.BranchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LNK_UserBranch_Branch");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.LnkUserBranch)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LNK_UserBranch_User");
            });

            modelBuilder.Entity<LnkUserRole>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.ToTable("LNK_UserRole");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.LnkUserRole)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LNK_UserRole_Role");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.LnkUserRole)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LNK_UserRole_User");
            });

            modelBuilder.Entity<Notification>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.NotificationText).HasMaxLength(250);

                entity.Property(e => e.SeenDate).HasColumnType("datetime");

                entity.Property(e => e.UrlNotification).HasMaxLength(100);

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.Notification)
                    .HasForeignKey(d => d.GroupId)
                    .HasConstraintName("FK_LNK_Notification_NotificationGroup");

                entity.HasOne(d => d.ReservationLog)
                    .WithMany(p => p.Notification)
                    .HasForeignKey(d => d.ReservationLogId)
                    .HasConstraintName("FK_Notification_ReservationLogId");

                entity.HasOne(d => d.ToUserNavigation)
                    .WithMany(p => p.Notification)
                    .HasForeignKey(d => d.ToUser)
                    .HasConstraintName("FK_Notification_AppUser");
            });

            modelBuilder.Entity<NotificationGroup>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.GroupName).HasMaxLength(50);

                entity.Property(e => e.NotificationUrl).HasMaxLength(50);
            });

            modelBuilder.Entity<NotificationSetting>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.HasOne(d => d.ActionStepNavigation)
                    .WithMany(p => p.NotificationSetting)
                    .HasForeignKey(d => d.ActionStep)
                    .HasConstraintName("FK_Notification_StatusStep");

                entity.HasOne(d => d.ReservationStatus)
                    .WithMany(p => p.NotificationSetting)
                    .HasForeignKey(d => d.ReservationStatusId)
                    .HasConstraintName("FK_Notification_ReservationStatus");
            });

            modelBuilder.Entity<Permission>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.DisplayName)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.ParentId).HasColumnName("ParentID");

                entity.Property(e => e.PermissionUrl).HasColumnName("PermissionURL");

                entity.Property(e => e.StyleClass).HasMaxLength(100);
            });

            modelBuilder.Entity<Question>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.HasOne(d => d.ActionStepNavigation)
                    .WithMany(p => p.Question)
                    .HasForeignKey(d => d.ActionStep)
                    .HasConstraintName("FK_ActionStep_StatusStep");

                entity.HasOne(d => d.ReservationStatusNavigation)
                    .WithMany(p => p.Question)
                    .HasForeignKey(d => d.ReservationStatus)
                    .HasConstraintName("FK_Question_ReservationStatus");
            });

            modelBuilder.Entity<RateSegment>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.RateSegmentName).HasMaxLength(50);
            });

            modelBuilder.Entity<RateSegmentCategory>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.RateSegmentCategoryName).HasMaxLength(50);
            });

            modelBuilder.Entity<Reason>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ReasonText).HasMaxLength(200);

                entity.HasOne(d => d.ReservationStatus)
                    .WithMany(p => p.Reason)
                    .HasForeignKey(d => d.ReservationStatusId)
                    .HasConstraintName("FK_Reason_ReservationStatusId");
            });

            modelBuilder.Entity<Reservation>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AgencyCountry).HasMaxLength(200);

                entity.Property(e => e.AgencyParentName).HasMaxLength(200);

                entity.Property(e => e.AgencySubsidiaryName).HasMaxLength(200);

                entity.Property(e => e.BookingDate).HasColumnType("datetime");

                entity.Property(e => e.BusinessSegmentText).HasMaxLength(200);

                entity.Property(e => e.CancelledDate).HasColumnType("datetime");

                entity.Property(e => e.Cdnumber).HasColumnName("CDNumber");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.CustomerCardIndicator).HasMaxLength(200);

                entity.Property(e => e.DriverCountry).HasMaxLength(50);

                entity.Property(e => e.DriverName).HasMaxLength(100);

                entity.Property(e => e.DropOffDate).HasColumnType("datetime");

                entity.Property(e => e.DropOffweekDay).HasMaxLength(50);

                entity.Property(e => e.NoShowDate).HasColumnType("datetime");

                entity.Property(e => e.PickUpDate).HasColumnType("datetime");

                entity.Property(e => e.PickUpweekDay).HasMaxLength(50);

                entity.Property(e => e.RateSegmentSubCategory).HasMaxLength(50);

                entity.Property(e => e.RequestLevel).HasMaxLength(50);

                entity.Property(e => e.ReservationAgent).HasMaxLength(200);

                entity.Property(e => e.ReservationPointOfSale).HasMaxLength(50);

                entity.Property(e => e.ReservationSourceChannel1).HasMaxLength(50);

                entity.Property(e => e.ReservationSourceChannel2).HasMaxLength(50);

                entity.Property(e => e.ReservationSourceChannel3).HasMaxLength(50);

                entity.Property(e => e.RevenueEur)
                    .HasColumnName("Revenue(EUR)")
                    .HasColumnType("decimal(18, 3)");

                entity.Property(e => e.RevenuePerDayEur)
                    .HasColumnName("RevenuePerDay(EUR)")
                    .HasColumnType("decimal(18, 3)");

                entity.Property(e => e.VehicleAcriss)
                    .HasColumnName("VehicleACRISS")
                    .HasMaxLength(50);

                entity.HasOne(d => d.BusinessSegment)
                    .WithMany(p => p.Reservation)
                    .HasForeignKey(d => d.BusinessSegmentId)
                    .HasConstraintName("FK_Reservation_BusinessSegment");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Reservation)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_Reservation_Customer");

                entity.HasOne(d => d.DropOffBranch)
                    .WithMany(p => p.ReservationDropOffBranch)
                    .HasForeignKey(d => d.DropOffBranchId)
                    .HasConstraintName("FK_Reservation_DropOffBranch");

                entity.HasOne(d => d.PickUpBranch)
                    .WithMany(p => p.ReservationPickUpBranch)
                    .HasForeignKey(d => d.PickUpBranchId)
                    .HasConstraintName("FK_Reservation_PickUpBranch");

                entity.HasOne(d => d.ReservationStatus)
                    .WithMany(p => p.Reservation)
                    .HasForeignKey(d => d.ReservationStatusId)
                    .HasConstraintName("FK_Reservation_ReserationStatus");
            });

            modelBuilder.Entity<ReservationAssignement>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.HasOne(d => d.FromUserNavigation)
                    .WithMany(p => p.ReservationAssignementFromUserNavigation)
                    .HasForeignKey(d => d.FromUser)
                    .HasConstraintName("FK_reservationAssignmentFromUser_Appuser");

                entity.HasOne(d => d.ReservationLog)
                    .WithMany(p => p.ReservationAssignement)
                    .HasForeignKey(d => d.ReservationLogId)
                    .HasConstraintName("FK_ReservationAssignement_ReservationLogId");

                entity.HasOne(d => d.ToUserNavigation)
                    .WithMany(p => p.ReservationAssignementToUserNavigation)
                    .HasForeignKey(d => d.ToUser)
                    .HasConstraintName("FK_reservationAssignmentToUser_Appuser");
            });

            modelBuilder.Entity<ReservationFormSubmit>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Comment).HasMaxLength(100);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.HasOne(d => d.CreatedUserNavigation)
                    .WithMany(p => p.ReservationFormSubmit)
                    .HasForeignKey(d => d.CreatedUser)
                    .HasConstraintName("FK_ReservationFormSubmitCreatedUser_AppUser");

                entity.HasOne(d => d.Reason)
                    .WithMany(p => p.ReservationFormSubmit)
                    .HasForeignKey(d => d.ReasonId)
                    .HasConstraintName("FK_ReservationFormSubmit-Reason_Reason");
            });

            modelBuilder.Entity<ReservationHistory>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AgencyCountry).HasMaxLength(200);

                entity.Property(e => e.AgencyParentName).HasMaxLength(200);

                entity.Property(e => e.AgencySubsidiaryName).HasMaxLength(200);

                entity.Property(e => e.BookingDate).HasColumnType("datetime");

                entity.Property(e => e.BusinessSegmentText).HasMaxLength(200);

                entity.Property(e => e.CancelledDate).HasColumnType("datetime");

                entity.Property(e => e.Cdnumber).HasColumnName("CDNumber");

                entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.CustomerCardIndicator).HasMaxLength(200);

                entity.Property(e => e.DateFrom).HasColumnType("datetime");

                entity.Property(e => e.DateTo).HasColumnType("datetime");

                entity.Property(e => e.DriverCountry).HasMaxLength(50);

                entity.Property(e => e.DriverName).HasMaxLength(100);

                entity.Property(e => e.DropOffDate).HasColumnType("datetime");

                entity.Property(e => e.DropOffweekDay).HasMaxLength(50);

                entity.Property(e => e.NoShowDate).HasColumnType("datetime");

                entity.Property(e => e.PickUpDate).HasColumnType("datetime");

                entity.Property(e => e.PickUpweekDay).HasMaxLength(50);

                entity.Property(e => e.RateSegmentSubCategory).HasMaxLength(50);

                entity.Property(e => e.RequestLevel).HasMaxLength(50);

                entity.Property(e => e.ReservationAgent).HasMaxLength(200);

                entity.Property(e => e.ReservationId).HasColumnName("ReservationID");

                entity.Property(e => e.ReservationPointOfSale).HasMaxLength(50);

                entity.Property(e => e.ReservationSourceChannel1).HasMaxLength(50);

                entity.Property(e => e.ReservationSourceChannel2).HasMaxLength(50);

                entity.Property(e => e.ReservationSourceChannel3).HasMaxLength(50);

                entity.Property(e => e.RevenueEur)
                    .HasColumnName("Revenue(EUR)")
                    .HasColumnType("decimal(18, 3)");

                entity.Property(e => e.RevenuePerDayEur)
                    .HasColumnName("RevenuePerDay(EUR)")
                    .HasColumnType("decimal(18, 3)");

                entity.Property(e => e.VehicleAcriss)
                    .HasColumnName("VehicleACRISS")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<ReservationStatus>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Status).HasMaxLength(100);
            });

            modelBuilder.Entity<ReservationStepsLog>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Completedate).HasColumnType("datetime");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.TableName).HasMaxLength(200);

                entity.HasOne(d => d.Step)
                    .WithMany(p => p.ReservationStepsLog)
                    .HasForeignKey(d => d.StepId)
                    .HasConstraintName("FK_ReservationStepsLog_StepId");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.DefaultPageId).HasColumnName("DefaultPageID");

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.LastModificationDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.HasOne(d => d.DefaultPage)
                    .WithMany(p => p.Role)
                    .HasForeignKey(d => d.DefaultPageId)
                    .HasConstraintName("FK_Role_DefaultPage");
            });

            modelBuilder.Entity<StatusStep>(entity =>
            {
                entity.HasKey(e => e.StepId)
                    .HasName("PK_ActionType");

                entity.Property(e => e.StepId).ValueGeneratedNever();

                entity.Property(e => e.IsNotification).HasDefaultValueSql("((0))");

                entity.Property(e => e.StepName).HasMaxLength(200);

                entity.HasOne(d => d.ReseravationStatusNavigation)
                    .WithMany(p => p.StatusStep)
                    .HasForeignKey(d => d.ReseravationStatus)
                    .HasConstraintName("FK_Step_ReserationStatus");
            });

            modelBuilder.Entity<UploadLog>(entity =>
            {
                entity.Property(e => e.ErrorLogPath).HasMaxLength(250);

                entity.Property(e => e.FailureMsg).HasMaxLength(500);

                entity.Property(e => e.FilePath).HasMaxLength(250);

                entity.Property(e => e.OriginalFileName).HasMaxLength(500);

                entity.Property(e => e.ParsingCompleteTime).HasColumnType("datetime");

                entity.Property(e => e.ParsingStartTime).HasColumnType("datetime");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UploadLog)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_UploadLog_AppUser");
            });

            modelBuilder.Entity<VreservationHistory>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VReservationHistory");

                entity.Property(e => e.AssignCreationDate).HasColumnType("datetime");

                entity.Property(e => e.Comment).HasMaxLength(100);

                entity.Property(e => e.Completedate).HasColumnType("datetime");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.CustomerFormComment).HasMaxLength(100);

                entity.Property(e => e.CustomerFormEmail).HasMaxLength(50);

                entity.Property(e => e.CustomerFormModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.CustomerFormPhone).HasMaxLength(50);

                entity.Property(e => e.FormSubmitCreationDate).HasColumnType("datetime");

                entity.Property(e => e.FormSubmitReason).HasMaxLength(200);

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Status).HasMaxLength(100);

                entity.Property(e => e.StepName).HasMaxLength(200);

                entity.Property(e => e.TableName).HasMaxLength(200);
            });

            modelBuilder.Entity<VreservationList>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VReservationList");

                entity.Property(e => e.AgencyCountry).HasMaxLength(200);

                entity.Property(e => e.AgencyParentName).HasMaxLength(200);

                entity.Property(e => e.AgencySubsidiaryName).HasMaxLength(200);

                entity.Property(e => e.AssignCreationDate).HasColumnType("datetime");

                entity.Property(e => e.AssignId).HasColumnName("AssignID");

                entity.Property(e => e.BookingDate).HasColumnType("datetime");

                entity.Property(e => e.BusinessSegmentText).HasMaxLength(200);

                entity.Property(e => e.CancelledDate).HasColumnType("datetime");

                entity.Property(e => e.Cdnumber).HasColumnName("CDNumber");

                entity.Property(e => e.Comment).HasMaxLength(100);

                entity.Property(e => e.CompleteDate).HasColumnType("datetime");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.CustomerCardIndicator).HasMaxLength(200);

                entity.Property(e => e.CustomerName).HasMaxLength(100);

                entity.Property(e => e.DriverCountry).HasMaxLength(50);

                entity.Property(e => e.DriverName).HasMaxLength(100);

                entity.Property(e => e.DropOffDate).HasColumnType("datetime");

                entity.Property(e => e.DropOffweekDay).HasMaxLength(50);

                entity.Property(e => e.FormSubmitCreationDate).HasColumnType("datetime");

                entity.Property(e => e.FormSubmitId).HasColumnName("FormSubmitID");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.LogCreationDate).HasColumnType("datetime");

                entity.Property(e => e.LogStatusName).HasMaxLength(100);

                entity.Property(e => e.NoShowDate).HasColumnType("datetime");

                entity.Property(e => e.PickUpBranchName).HasMaxLength(200);

                entity.Property(e => e.PickUpDate).HasColumnType("datetime");

                entity.Property(e => e.PickUpweekDay).HasMaxLength(50);

                entity.Property(e => e.RateSegmentCategoryName).HasMaxLength(50);

                entity.Property(e => e.RateSegmentName).HasMaxLength(50);

                entity.Property(e => e.RateSegmentSubCategory).HasMaxLength(50);

                entity.Property(e => e.ReasonStatus)
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.ReasonText).HasMaxLength(200);

                entity.Property(e => e.RequestLevel).HasMaxLength(50);

                entity.Property(e => e.ReservationAgent).HasMaxLength(200);

                entity.Property(e => e.ReservationPointOfSale).HasMaxLength(50);

                entity.Property(e => e.ReservationSourceChannel1).HasMaxLength(50);

                entity.Property(e => e.ReservationSourceChannel2).HasMaxLength(50);

                entity.Property(e => e.ReservationSourceChannel3).HasMaxLength(50);

                entity.Property(e => e.ReservationStatus).HasMaxLength(100);

                entity.Property(e => e.RevenueEur)
                    .HasColumnName("Revenue(EUR)")
                    .HasColumnType("decimal(18, 3)");

                entity.Property(e => e.RevenuePerDayEur)
                    .HasColumnName("RevenuePerDay(EUR)")
                    .HasColumnType("decimal(18, 3)");

                entity.Property(e => e.StepName).HasMaxLength(200);

                entity.Property(e => e.VehicleAcriss)
                    .HasColumnName("VehicleACRISS")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<VreservationLog>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VReservationLog");

                entity.Property(e => e.AssignCreationDate).HasColumnType("datetime");

                entity.Property(e => e.BookingDate).HasColumnType("datetime");

                entity.Property(e => e.CancelledDate).HasColumnType("datetime");

                entity.Property(e => e.CompleteDate).HasColumnType("datetime");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.DropOffDate).HasColumnType("datetime");

                entity.Property(e => e.PickUpDate).HasColumnType("datetime");

                entity.Property(e => e.StatusTitle).HasMaxLength(100);
            });

            modelBuilder.Entity<Weekdays>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.WeekDayName).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
