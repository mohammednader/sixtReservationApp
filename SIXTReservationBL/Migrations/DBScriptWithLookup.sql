  ---- Scaffold-DbContext "Server=.;Database=SixtReservation;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models/Domain -force -project SIXTReservationBL
 
   

USE [SixtReservation]
GO
/****** Object:  Table [dbo].[AppUser]    Script Date: 4/8/2020 11:33:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AppUser](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PasswordHash] [nvarchar](256) NULL,
	[FullName] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](50) NULL,
	[Email] [nvarchar](100) NULL,
	[IsActive] [bit] NULL,
	[IsSysAdmin] [bit] NULL,
	[CreatedBy] [int] NULL,
	[CreationDate] [datetime] NULL,
	[LastModifiedBy] [int] NULL,
	[LastModificationDate] [datetime] NULL,
	[IsChangedPassword] [bit] NULL,
	[IsEmailConfirmed] [bit] NULL,
	[IsPhoneNumberConfirmed] [bit] NULL,
	[IsTwoFactorEnabled] [bit] NULL,
	[IsDeleted] [bit] NULL,
	[JobTitleID] [int] NULL,
	[BranchID] [int] NULL,
 CONSTRAINT [PK_AppUser] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Branch]    Script Date: 4/8/2020 11:33:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Branch](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](200) NULL,
	[Code] [nvarchar](50) NULL,
	[Email] [nvarchar](50) NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_Branch] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Customer]    Script Date: 4/8/2020 11:33:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NULL,
	[Email] [nvarchar](50) NULL,
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[JobTitle]    Script Date: 4/8/2020 11:33:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[JobTitle](
	[JobTitleId] [int] NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Type] [int] NOT NULL CONSTRAINT [DF_JobTitle_Type]  DEFAULT ((0)),
 CONSTRAINT [PK_dbo.JobTitles] PRIMARY KEY CLUSTERED 
(
	[JobTitleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[LNK_RolePermission]    Script Date: 4/8/2020 11:33:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LNK_RolePermission](
	[RoleID] [int] NOT NULL,
	[PermissionID] [int] NOT NULL,
 CONSTRAINT [PK_LNK_RolePermission] PRIMARY KEY CLUSTERED 
(
	[RoleID] ASC,
	[PermissionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[LNK_UserRole]    Script Date: 4/8/2020 11:33:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LNK_UserRole](
	[UserID] [int] NOT NULL,
	[RoleID] [int] NOT NULL,
 CONSTRAINT [PK_LNK_UserRole] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC,
	[RoleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[NotificationSetting]    Script Date: 4/8/2020 11:33:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NotificationSetting](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ActionStep] [int] NULL,
	[IsDisabled] [bit] NULL,
 CONSTRAINT [PK_NotificationSetting] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Permission]    Script Date: 4/8/2020 11:33:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Permission](
	[ID] [int] NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[DisplayName] [nvarchar](500) NOT NULL,
	[Description] [nvarchar](500) NULL,
	[ParentID] [int] NULL,
	[Order] [int] NULL,
	[PermissionType] [int] NULL,
	[PermissionURL] [nvarchar](max) NULL,
	[StyleClass] [nvarchar](100) NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_Permission] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Question]    Script Date: 4/8/2020 11:33:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Question](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ReservationStatus] [int] NULL,
	[ActionStep] [int] NULL,
	[QuestionText] [nvarchar](max) NULL,
 CONSTRAINT [PK_Question] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[RateSegment]    Script Date: 4/8/2020 11:33:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RateSegment](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[RateSegment] [nvarchar](50) NULL,
 CONSTRAINT [PK_RateSegment] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Reason]    Script Date: 4/8/2020 11:33:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Reason](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ReasonText] [nvarchar](200) NULL,
	[IsAnswer] [bit] NULL,
	[IsActive] [bit] NULL,
	[ReservationStatusId] [int] NULL,
 CONSTRAINT [PK_Reason] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Reservation]    Script Date: 4/8/2020 11:33:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Reservation](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ReservationNum] [bigint] NOT NULL,
	[NumberOfReservations] [int] NULL,
	[BookingDate] [datetime] NULL,
	[ReservationHour] [int] NULL,
	[PickUpDate] [datetime] NULL,
	[PickUpHour] [int] NULL,
	[PickUpweekDay] [nvarchar](50) NULL,
	[Revenue(EUR)] [decimal](18, 3) NULL,
	[RevenuePerDay(EUR)] [decimal](18, 3) NULL,
	[RentalDays] [int] NULL,
	[LeadTimeInDays] [int] NULL,
	[CancelledAfterBookingInDays] [int] NULL,
	[CancelledBeforeBickUpInDays] [int] NULL,
	[PickUpBranchId] [int] NULL,
	[DropOffDate] [datetime] NULL,
	[DropOffHour] [int] NULL,
	[DropOffweekDay] [nvarchar](50) NULL,
	[DropOffBranchId] [int] NULL,
	[VehicleACRISS] [nvarchar](50) NULL,
	[ReservationSourceChannel1] [nvarchar](50) NULL,
	[ReservationSourceChannel2] [nvarchar](50) NULL,
	[ReservationSourceChannel3] [nvarchar](50) NULL,
	[BusinessSegmentId] [int] NULL,
	[RateSegmentCategory] [int] NULL,
	[RateSegmentSubCategory] [nvarchar](50) NULL,
	[ReservationPointOfSale] [nvarchar](50) NULL,
	[OneWayReservation] [bit] NULL,
	[OnRequest] [bit] NULL,
	[RequestLevel] [nvarchar](50) NULL,
	[RequestLevelId] [int] NULL,
	[ReservationAgent] [nvarchar](200) NULL,
	[ReservationStatusId] [int] NULL,
	[CancelledDate] [datetime] NULL,
	[RentalAgreementNumber] [bigint] NULL,
	[Prepaid] [bit] NULL,
	[ConvertedToRental] [bit] NULL,
	[NoShowDate] [datetime] NULL,
	[CDNumber] [bigint] NULL,
	[CustomerId] [int] NULL,
	[DriverName] [nvarchar](100) NULL,
	[DriverCountry] [nvarchar](50) NULL,
	[CustomerCardIndicator] [nvarchar](200) NULL,
	[AgencyCountry] [nvarchar](200) NULL,
	[AgencySubsidiaryName] [nvarchar](200) NULL,
	[AgencyParentName] [nvarchar](200) NULL,
 CONSTRAINT [PK_Reservation] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ReservationStatus]    Script Date: 4/8/2020 11:33:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ReservationStatus](
	[ID] [int] NOT NULL,
	[Status] [nvarchar](100) NULL,
 CONSTRAINT [PK_ReservationStatus] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ReservationStepsLog]    Script Date: 4/8/2020 11:33:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ReservationStepsLog](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ReservationNo] [bigint] NULL,
	[StepId] [int] NULL,
	[IsDone] [bit] NULL,
	[TableName] [nvarchar](200) NULL,
	[TableId] [int] NULL,
	[ReservationStatus] [int] NULL,
	[CreationDate] [datetime] NULL,
 CONSTRAINT [PK_ReservationLog] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Role]    Script Date: 4/8/2020 11:33:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NULL,
	[Description] [nvarchar](500) NULL,
	[IsSysRole] [bit] NULL,
	[DefaultPageID] [int] NULL,
	[CreatedBy] [int] NULL,
	[CreationDate] [datetime] NULL,
	[LastModifiedBy] [int] NULL,
	[LastModificationDate] [datetime] NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[StatusStep]    Script Date: 4/8/2020 11:33:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StatusStep](
	[StepId] [int] NOT NULL,
	[StepName] [nvarchar](200) NULL,
	[ReseravationStatus] [int] NULL,
	[StepOrder] [int] NULL,
 CONSTRAINT [PK_ActionType] PRIMARY KEY CLUSTERED 
(
	[StepId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UploadLog]    Script Date: 4/8/2020 11:33:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UploadLog](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FilePath] [nvarchar](250) NULL,
	[NumberOfEntries] [int] NULL,
	[CreationTime] [datetime2](7) NULL,
	[UserId] [int] NULL,
 CONSTRAINT [PK_UploadLogs] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[AppUser]  WITH CHECK ADD  CONSTRAINT [FK_User_Branch] FOREIGN KEY([BranchID])
REFERENCES [dbo].[Branch] ([ID])
GO
ALTER TABLE [dbo].[AppUser] CHECK CONSTRAINT [FK_User_Branch]
GO
ALTER TABLE [dbo].[AppUser]  WITH CHECK ADD  CONSTRAINT [FK_User_JobTitle] FOREIGN KEY([JobTitleID])
REFERENCES [dbo].[JobTitle] ([JobTitleId])
GO
ALTER TABLE [dbo].[AppUser] CHECK CONSTRAINT [FK_User_JobTitle]
GO
ALTER TABLE [dbo].[LNK_RolePermission]  WITH CHECK ADD  CONSTRAINT [FK_LNK_RolePermission_Permission] FOREIGN KEY([PermissionID])
REFERENCES [dbo].[Permission] ([ID])
GO
ALTER TABLE [dbo].[LNK_RolePermission] CHECK CONSTRAINT [FK_LNK_RolePermission_Permission]
GO
ALTER TABLE [dbo].[LNK_RolePermission]  WITH CHECK ADD  CONSTRAINT [FK_LNK_RolePermission_Role] FOREIGN KEY([RoleID])
REFERENCES [dbo].[Role] ([ID])
GO
ALTER TABLE [dbo].[LNK_RolePermission] CHECK CONSTRAINT [FK_LNK_RolePermission_Role]
GO
ALTER TABLE [dbo].[LNK_UserRole]  WITH CHECK ADD  CONSTRAINT [FK_LNK_UserRole_Role] FOREIGN KEY([RoleID])
REFERENCES [dbo].[Role] ([ID])
GO
ALTER TABLE [dbo].[LNK_UserRole] CHECK CONSTRAINT [FK_LNK_UserRole_Role]
GO
ALTER TABLE [dbo].[LNK_UserRole]  WITH CHECK ADD  CONSTRAINT [FK_LNK_UserRole_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[AppUser] ([ID])
GO
ALTER TABLE [dbo].[LNK_UserRole] CHECK CONSTRAINT [FK_LNK_UserRole_User]
GO
ALTER TABLE [dbo].[Question]  WITH CHECK ADD  CONSTRAINT [FK_Question_ReservationStatus] FOREIGN KEY([ReservationStatus])
REFERENCES [dbo].[ReservationStatus] ([ID])
GO
ALTER TABLE [dbo].[Question] CHECK CONSTRAINT [FK_Question_ReservationStatus]
GO
ALTER TABLE [dbo].[Reason]  WITH CHECK ADD  CONSTRAINT [FK_Reason_ReservationStatusId] FOREIGN KEY([ReservationStatusId])
REFERENCES [dbo].[ReservationStatus] ([ID])
GO
ALTER TABLE [dbo].[Reason] CHECK CONSTRAINT [FK_Reason_ReservationStatusId]
GO
ALTER TABLE [dbo].[Reservation]  WITH CHECK ADD  CONSTRAINT [FK_Reservation_BusinessSegment] FOREIGN KEY([BusinessSegmentId])
REFERENCES [dbo].[RateSegment] ([ID])
GO
ALTER TABLE [dbo].[Reservation] CHECK CONSTRAINT [FK_Reservation_BusinessSegment]
GO
ALTER TABLE [dbo].[Reservation]  WITH CHECK ADD  CONSTRAINT [FK_Reservation_Customer] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([ID])
GO
ALTER TABLE [dbo].[Reservation] CHECK CONSTRAINT [FK_Reservation_Customer]
GO
ALTER TABLE [dbo].[Reservation]  WITH CHECK ADD  CONSTRAINT [FK_Reservation_ReserationStatus] FOREIGN KEY([ReservationStatusId])
REFERENCES [dbo].[ReservationStatus] ([ID])
GO
ALTER TABLE [dbo].[Reservation] CHECK CONSTRAINT [FK_Reservation_ReserationStatus]
GO
ALTER TABLE [dbo].[Role]  WITH CHECK ADD  CONSTRAINT [FK_Role_DefaultPage] FOREIGN KEY([DefaultPageID])
REFERENCES [dbo].[Permission] ([ID])
GO
ALTER TABLE [dbo].[Role] CHECK CONSTRAINT [FK_Role_DefaultPage]
GO
ALTER TABLE [dbo].[StatusStep]  WITH CHECK ADD  CONSTRAINT [FK_Step_ReserationStatus] FOREIGN KEY([ReseravationStatus])
REFERENCES [dbo].[ReservationStatus] ([ID])
GO
ALTER TABLE [dbo].[StatusStep] CHECK CONSTRAINT [FK_Step_ReserationStatus]
GO
USE [master]
GO
ALTER DATABASE [SixtReservation] SET  READ_WRITE 
GO


---------- do db chages here 


/*Alter table Question by nader*/
ALTER TABLE [dbo].[Question]  WITH CHECK ADD  CONSTRAINT [FK_ActionStep_StatusStep] FOREIGN KEY([ActionStep])
REFERENCES [dbo].[StatusStep] ([StepId])

ALTER TABLE NotificationSetting
ADD JobTitleId [int] null

ALTER TABLE [dbo].[NotificationSetting]  WITH CHECK ADD  CONSTRAINT [FK_Notification_JobTitle] FOREIGN KEY([JobTitleId])
REFERENCES [dbo].[JobTitle] ([JobTitleId])
GO
ALTER TABLE [dbo].[NotificationSetting]  WITH CHECK ADD  CONSTRAINT [FK_Notification_StatusStep] FOREIGN KEY([ActionStep])
REFERENCES [dbo].[StatusStep] ([StepId])

ALTER TABLE NotificationSetting
ADD ReservationStatusId [int] null

ALTER TABLE [dbo].[NotificationSetting]  WITH CHECK ADD  CONSTRAINT [FK_Notification_ReservationStatus] FOREIGN KEY([ReservationStatusId])
REFERENCES [dbo].[ReservationStatus] ([ID])
	GO
 


-- rename rate segemnt column  yasser 8/4/2020 

EXEC sp_RENAME 'RateSegment.RateSegment' , 'RateSegmentName', 'COLUMN'


CREATE TABLE [dbo].[RateSegmentCategory](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[RateSegmentCategoryName] [nvarchar](50) NULL,
 CONSTRAINT [PK_RateSegmentCategoryName] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

--- 

/* Alter table Notification */
CREATE TABLE [dbo].[LNK_Notification_JobTitle](
	[NotificationID] [int] NOT NULL,
	[JobTitleID] [int] NOT NULL,
 CONSTRAINT [PK_LNK_Notification_JobTitle] PRIMARY KEY CLUSTERED 
(
	[NotificationID] ASC,
	[JobTitleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]



ALTER TABLE [dbo].[LNK_Notification_JobTitle]  WITH CHECK ADD  CONSTRAINT [FK_LNK_Notification] FOREIGN KEY([NotificationID])
REFERENCES [dbo].[NotificationSetting] ([ID])

ALTER TABLE [dbo].[LNK_Notification_JobTitle]  WITH CHECK ADD  CONSTRAINT [FK_LNK_JobTitle] FOREIGN KEY([JobTitleID])
REFERENCES [dbo].[JobTitle] ([JobTitleId])

Alter Table [dbo].[StatusStep] 
Add  IsNotification bit default 0

Alter Table [dbo].[NotificationSetting] 
drop constraint [FK_Notification_JobTitle] 

Alter Table [dbo].[NotificationSetting] 
drop column JobTitleId 

ALTER TABLE [dbo].[LNK_Notification_JobTitle]  WITH CHECK ADD  CONSTRAINT [FK_LNK_Notification] FOREIGN KEY([NotificationID])
REFERENCES [dbo].[NotificationSetting] ([ID])
ON DELETE CASCADE
/* end region */
---  yasser 9/4/2020 


 alter table  [SixtReservation].[dbo].[Reservation]
  add UploadId int null 


   alter table  [SixtReservation].[dbo].[Reservation]
  add BusinessSegmentText  nvarchar(200)   null 

  --- 
  -- reservation history 12/04/2020  yasser
  
CREATE TABLE [dbo].[ReservationHistory](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ReservationID] [bigint] NOT NULL,
	[ReservationNum] [bigint] NOT NULL,
	[NumberOfReservations] [int] NULL,
	[BookingDate] [datetime] NULL,
	[ReservationHour] [int] NULL,
	[PickUpDate] [datetime] NULL,
	[PickUpHour] [int] NULL,
	[PickUpweekDay] [nvarchar](50) NULL,
	[Revenue(EUR)] [decimal](18, 3) NULL,
	[RevenuePerDay(EUR)] [decimal](18, 3) NULL,
	[RentalDays] [int] NULL,
	[LeadTimeInDays] [int] NULL,
	[CancelledAfterBookingInDays] [int] NULL,
	[CancelledBeforeBickUpInDays] [int] NULL,
	[PickUpBranchId] [int] NULL,
	[DropOffDate] [datetime] NULL,
	[DropOffHour] [int] NULL,
	[DropOffweekDay] [nvarchar](50) NULL,
	[DropOffBranchId] [int] NULL,
	[VehicleACRISS] [nvarchar](50) NULL,
	[ReservationSourceChannel1] [nvarchar](50) NULL,
	[ReservationSourceChannel2] [nvarchar](50) NULL,
	[ReservationSourceChannel3] [nvarchar](50) NULL,
	[BusinessSegmentId] [int] NULL,
	[RateSegmentCategory] [int] NULL,
	[RateSegmentSubCategory] [nvarchar](50) NULL,
	[ReservationPointOfSale] [nvarchar](50) NULL,
	[OneWayReservation] [bit] NULL,
	[OnRequest] [bit] NULL,
	[RequestLevel] [nvarchar](50) NULL,
	[RequestLevelId] [int] NULL,
	[ReservationAgent] [nvarchar](200) NULL,
	[ReservationStatusId] [int] NULL,
	[CancelledDate] [datetime] NULL,
	[RentalAgreementNumber] [bigint] NULL,
	[Prepaid] [bit] NULL,
	[ConvertedToRental] [bit] NULL,
	[NoShowDate] [datetime] NULL,
	[CDNumber] [bigint] NULL,
	[CustomerId] [int] NULL,
	[DriverName] [nvarchar](100) NULL,
	[DriverCountry] [nvarchar](50) NULL,
	[CustomerCardIndicator] [nvarchar](200) NULL,
	[AgencyCountry] [nvarchar](200) NULL,
	[AgencySubsidiaryName] [nvarchar](200) NULL,
	[AgencyParentName] [nvarchar](200) NULL,
	[UploadId] [int] NULL,
	[BusinessSegmentText] [nvarchar](200) NULL,
	[CreatedDate] [datetime] NULL,
	[IsCurrent] [bit] NULL,
	[CreatedByUserID] [int] NULL,
	[DateFrom] [datetime] NULL,
	[DateTo] [datetime] NULL,
 CONSTRAINT [PK_Reservation_history] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

-------
  /* Adding Action setting  nader */
  GO
CREATE TABLE [dbo].[Weekdays](
	[ID] [int] NOT NULL,
	[WeekDayName] [nvarchar](50) NULL,
 CONSTRAINT [PK_Weekday] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


GO
CREATE TABLE [dbo].[ActionSetting](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ReservationStatusId] [int] NULL,
	[RateSegmentCategoryId] [int] NULL,
	[BranchId] [int] NULL,
	[ActionStepId] [int] NULL,
	[WeekDayId] [int] NULL,
	[IsEnable] [bit] NULL,
 CONSTRAINT [PK_ActionSetting] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

GO
ALTER TABLE [dbo].[ActionSetting]  WITH CHECK ADD  CONSTRAINT [FK_LNK_ActionStep] FOREIGN KEY([ActionStepId])
REFERENCES [dbo].[StatusStep] ([StepId])
GO
ALTER TABLE [dbo].[ActionSetting] CHECK CONSTRAINT [FK_LNK_ActionStep]
GO
ALTER TABLE [dbo].[ActionSetting]  WITH CHECK ADD  CONSTRAINT [FK_LNK_Branch] FOREIGN KEY([BranchId])
REFERENCES [dbo].[Branch] ([ID])
GO
ALTER TABLE [dbo].[ActionSetting] CHECK CONSTRAINT [FK_LNK_Branch]
GO
ALTER TABLE [dbo].[ActionSetting]  WITH CHECK ADD  CONSTRAINT [FK_LNK_RateSegmentCategoryId] FOREIGN KEY([RateSegmentCategoryId])
REFERENCES [dbo].[RateSegmentCategory] ([ID])
GO
ALTER TABLE [dbo].[ActionSetting] CHECK CONSTRAINT [FK_LNK_RateSegmentCategoryId]
GO
ALTER TABLE [dbo].[ActionSetting]  WITH CHECK ADD  CONSTRAINT [FK_LNK_ReservationStatus] FOREIGN KEY([ReservationStatusId])
REFERENCES [dbo].[ReservationStatus] ([ID])
GO
ALTER TABLE [dbo].[ActionSetting] CHECK CONSTRAINT [FK_LNK_ReservationStatus]
GO
ALTER TABLE [dbo].[ActionSetting]  WITH CHECK ADD  CONSTRAINT [FK_LNK_WeekDay] FOREIGN KEY([WeekDayId])
REFERENCES [dbo].[Weekdays] ([ID])
GO
ALTER TABLE [dbo].[ActionSetting] CHECK CONSTRAINT [FK_LNK_WeekDay]


/* end  */



alter table ReservationStepsLog
  add ReservationId int null

    alter table Reservation
  add CreationDate datetime null 



  ----- yasser 16/4/2020


   alter table ReservationStepsLog
 add ReservationStatusId int null 

  alter table [Notification]
 add ReservationStatusId int null 


 ALTER TABLE [dbo].[Notification]  WITH CHECK ADD  CONSTRAINT [FK_Notification_ReservationLogId] FOREIGN KEY([ReservationLogId])
REFERENCES [dbo].[ReservationStepsLog] ([ID])
GO


ALTER TABLE [dbo].[Reservation]  WITH CHECK ADD  CONSTRAINT [FK_Reservation_PickUpBranch] FOREIGN KEY([PickUpBranchId])
REFERENCES [dbo].[Branch] ([ID])
GO

ALTER TABLE [dbo].[Reservation]  WITH CHECK ADD  CONSTRAINT [FK_Reservation_DropOffBranch] FOREIGN KEY([DropOffBranchId])
REFERENCES [dbo].[Branch] ([ID])
GO

CREATE TABLE [dbo].[ReservationAssignement](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[FromUser] [int] NULL,
	[ToUser] [int] NULL,
	[CreationDate] [datetime] NULL,
	[ReservationNo] [bigint] NULL,
	[ReservationLogId] [int] NULL,

 CONSTRAINT [PK_Assignement] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

 ALTER TABLE [dbo].[ReservationAssignement]  WITH CHECK ADD  CONSTRAINT [FK_ReservationAssignement_ReservationLogId] FOREIGN KEY([ReservationLogId])
REFERENCES [dbo].[ReservationStepsLog] ([ID])
GO



ALTER TABLE [dbo].[ReservationStepsLog]  WITH CHECK ADD  CONSTRAINT [FK_ReservationStepsLog_StepId] FOREIGN KEY([StepId])
REFERENCES [dbo].[StatusStep] ([StepId])
GO

alter table ReservationStepsLog 
add Completedate datetime null 

alter table StatusStep 
add IsLast bit null 
-------- end yasser 



alter table UploadLog
add ParsingStartTime datetime null
 
alter table UploadLog
add ParsingCompleteTime datetime null
 
alter table UploadLog
add NumberOfSavedRecord int null

alter table UploadLog
add UploadStatus int null

alter table UploadLog
add	[ErrorLogPath] [nvarchar](250) NULL 

alter table UploadLog
add [FailureMsg] [nvarchar](500) NULL 

--------
-- Alter table notification ---
alter table Notification
add  [CreateDate] dateTime Null 

ALTER TABLE [dbo].[Notification]  WITH CHECK ADD  CONSTRAINT [FK_Notification_AppUser] FOREIGN KEY([ToUser])
REFERENCES [dbo].[AppUser] ([ID])
GO
--End  --


-- yasser 20-4-2020

EXEC sp_RENAME 'UploadLog.NumberOfSavedRecord' , 'SavedRecordNum', 'COLUMN'

alter table UploadLog
add	FailedRecordNum int NULL 

  
ALTER TABLE [dbo].[UploadLog]  WITH CHECK ADD  CONSTRAINT [FK_UploadLog_AppUser] FOREIGN KEY([UserId])
REFERENCES [dbo].[AppUser] ([ID])
GO


  alter table [Notification] 
  alter column ReservationNo bigint null

  --------
--  CREATE view [dbo].[VReservationLog]
-- 	as
--select NEWID() as UniqueId
--	,Res.ReservationNum
--	,Res.ReservationStatusId  StatusId 
--	,Res.BookingDate
--	,Res.PickUpDate
--	,Res.DropOffDate
--	,Res.CancelledDate
--	,Reslg.[ID] as LogId
--	,Reslg.[ReservationNo]
--	,Reslg.[StepId]
--	,Reslg.[IsDone]
--	--,Reslg.[TableName]
--	--,Reslg.[TableId]
--	,Reslg.[ReservationStatus]
--	,Reslg.[CreationDate]
--	,Reslg.[ReservationId]
--	,Reslg.[ReservationStatusId]
--	,Reslg.[CompleteDate]
--	,ResStatus.Status as  StatusTitle 
--	,Notify.[ID] as NotificationId
--    ,Notify.[NotificationText]
--    ,Notify.[ToUser] as NotifyToUser 
--    ,Notify.[IsSeen]
--    ,Notify.[SeenDate]
--    ,Notify.[ReservationNo] as NotifyReservationNo
--    ,Notify.[ReservationLogId] as NotifyLogId
--    ,Notify.[Status]
--   -- ,Notify.[ReservationStatusId] as NotifyReservationStatus
--    ,Notify.[CreateDate] as NotificationCreationDate
--    ,Assign.[ID] as AssignmentId
--    ,Assign.[FromUser] as AssignFromUser
--    ,Assign.[ToUser] as AssignToUser
--    ,Assign.[CreationDate] as AssignCreationDate
--    ,Assign.[ReservationNo] as AssignReservationNo
--    ,Assign.[ReservationLogId] as AssignLogId
   
--from Reservation Res
--left join ReservationStepsLog  Reslg on Reslg.ReservationId=Res.ID and Reslg.ReservationStatusId = Res.ReservationStatusId and Reslg.ReservationNo =Res.ReservationNum
--inner join  ReservationStatus ResStatus  on ResStatus.ID = Res.ReservationStatusId
--left join [Notification] Notify on Notify.ReservationLogId=Reslg.ID
--left join ReservationAssignement Assign on assign.ReservationLogId =Reslg.ID
---- left join Actions   
----inner join AppUser NotifyToUser on NotifyToUser.ID = Notify.[ToUser] 
----inner join AppUser AssignToUser on AssignToUser.ID = Assign.[ToUser] 
----inner join AppUser AssignFromUser  on  AssignFromUser.ID = Assign.[FromUser] 

--GO

-----
--End  --

-- create Form submit table ----------
CREATE TABLE [dbo].[ReservationFormSubmit-Reason](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[FromUser] [int] NULL,
	[ReasonId] [int] NULL,
	[ReasonStatus] [bit] Null,
	[Comment] [nvarchar](100) NULL,
	[CreationDate] [datetime] NULL,
	[ReservationNo] [bigint] NULL,
	[ReservationLogId] [int] NULL,

 CONSTRAINT [PK_ReservationFormSubmit] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

 ALTER TABLE [dbo].[ReservationFormSubmit-Reason]  WITH CHECK ADD  CONSTRAINT [FK_ReservationFormSubmit-Reason_Reason] FOREIGN KEY([ReasonId])
REFERENCES [dbo].[Reason] ([ID])
GO

---------end------------------

--------Alter table ReservationAssignment-   -----
ALTER TABLE [dbo].[ReservationAssignement]  WITH CHECK ADD  CONSTRAINT [FK_reservationAssignmentFromUser_Appuser] FOREIGN KEY([FromUser])
REFERENCES [dbo].[AppUser] ([ID])
GO
ALTER TABLE [dbo].[ReservationAssignement]  WITH CHECK ADD  CONSTRAINT [FK_reservationAssignmentToUser_Appuser] FOREIGN KEY([ToUser])
REFERENCES [dbo].[AppUser] ([ID])
GO
---------end------

----------Alter Table ReservationFormSubmit-------------
EXEC sp_rename 'ReservationFormSubmit-Reason', 'ReservationFormSubmit';  
EXEC sp_RENAME 'ReservationFormSubmit.FromUser' , 'CreatedUser', 'COLUMN';
ALTER TABLE [dbo].[ReservationFormSubmit]  WITH CHECK ADD  CONSTRAINT [FK_ReservationFormSubmitCreatedUser_AppUser] FOREIGN KEY([CreatedUser])
REFERENCES [dbo].[AppUser] ([ID])
GO

----- Alter Notification -------------
  alter table [Notification] 
  Add [UrlNotification] nvarchar(100) null


  alter table [Notification] 
  Add [NotificationType] int null
-------------End----------------

-------------yasser  22-4-2020---------------

alter table UploadLog
alter column SavedRecordNum int null
-------
 

----end----
----yasser 27-4-2020
 alter table UploadLog  
  add OriginalFileName nvarchar (500) null 
 

--GO

------end yasser 

------------------Create  table  [LNK_UserBranch] Alter [AppUser]--------------------------------------
ALTER TABLE [dbo].[AppUser]   drop  CONSTRAINT [FK_User_Branch] 
ALTER TABLE [dbo].[AppUser]   drop  Column [BranchID] 


CREATE TABLE [dbo].[LNK_UserBranch](
	[UserID] [int] NOT NULL,
	[BranchID] [int] NOT NULL,
 CONSTRAINT [PK_LNK_UserBranch] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC,
	[BranchID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[LNK_UserBranch]  WITH CHECK ADD  CONSTRAINT [FK_LNK_UserBranch_Branch] FOREIGN KEY([BranchID])
REFERENCES [dbo].[Branch] ([ID])
GO

ALTER TABLE [dbo].[LNK_UserBranch]  WITH CHECK ADD  CONSTRAINT [FK_LNK_UserBranch_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[AppUser] ([ID])
GO

----------End----------------------

----------------------create NotificationGroup table ----------------------------
CREATE TABLE [dbo].[NotificationGroup](
	[ID] [int]  Not NULL,
	[GroupName][nvarchar](50) Null,
	[Order] [int]  NULL,
	[ActionStep][int]  Null,
	
 CONSTRAINT [PK_Notification_group] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] 

GO
Alter Table [Notification] add [GroupId] [int] null 
Alter Table [Notification] add [IsDeleted] [bit] null 

ALTER TABLE [dbo].[Notification]  WITH CHECK ADD  CONSTRAINT [FK_LNK_Notification_NotificationGroup] FOREIGN KEY([GroupId])
REFERENCES [dbo].[NotificationGroup] ([ID])
GO

ALTER TABLE [dbo].[NotificationGroup]  WITH CHECK ADD  CONSTRAINT [FK_LNK_NotificationGroup_ActionStep] FOREIGN KEY([ActionStep])
REFERENCES [dbo].[StatusStep] ([StepId])
GO
------------------------End----------------------

---------------------Alter Reservation View   29/4 ----------------------------

/****** Object:  View [dbo].[VReservationList]    Script Date: 04/29/2020 2:17:00 PM ******/
 
------yasser new view 2-5-2020
 ---------------

 

--------
ALTER TABLE [dbo].[NotificationGroup] DROP CONSTRAINT [FK_LNK_NotificationGroup_ActionStep]
GO
 
   update [NotificationGroup] set ActionStep =1 where ID in (1 )
 
 update [NotificationGroup] set ActionStep =2 where ID in ( 2)

 update [NotificationGroup] set ActionStep =3 where ID in (3)
  
 update [NotificationGroup] set ActionStep =3 where ID in (4)

------------- end yasser

----last view 4-5-2020


 ALTER view [dbo].[VReservationLog]
 	as
select NEWID() as UniqueId
	,Res.ReservationNum
	,Res.ReservationStatusId  StatusId 
	,Res.BookingDate
	,Res.PickUpDate
	,Res.DropOffDate
	,Res.CancelledDate
	,Reslg.[ID] as LogId
	,Reslg.[ReservationNo]
	,Reslg.[StepId]
	,Reslg.[IsDone]
	--,Reslg.[TableName]
	--,Reslg.[TableId]
	,Reslg.[ReservationStatus]
	,Reslg.[CreationDate]
	,Reslg.[ReservationId]
	,Reslg.[ReservationStatusId]
	,Reslg.[CompleteDate]
	,ResStatus.Status as  StatusTitle 
	,Notify.[ID] as NotificationId
    ,Notify.[NotificationText]
    ,Notify.[ToUser] as NotifyToUser 
    ,Notify.[IsSeen]
    ,Notify.[SeenDate]
    ,Notify.[ReservationNo] as NotifyReservationNo
    ,Notify.[ReservationLogId] as NotifyLogId
    ,Notify.[Status]
   -- ,Notify.[ReservationStatusId] as NotifyReservationStatus
    ,Notify.[CreateDate] as NotificationCreationDate
    ,Assign.[ID] as AssignmentId
    ,Assign.[FromUser] as AssignFromUser
    ,Assign.[ToUser] as AssignToUser
    ,Assign.[CreationDate] as AssignCreationDate
    ,Assign.[ReservationNo] as AssignReservationNo
    ,Assign.[ReservationLogId] as AssignLogId
	,DATEDIFF (HH,Reslg.[CreationDate] , CURRENT_TIMESTAMP) as DiffDateNotifyAssign
	,DATEDIFF(HH,Reslg.[CreationDate],CURRENT_TIMESTAMP) as DiffDateAssignSubmit
 
from Reservation Res
inner join ReservationStepsLog  Reslg on  Res.ReservationStatusId=Reslg.ReservationStatus  and Res.ReservationNum= Reslg.ReservationNo 
inner join  ReservationStatus ResStatus  on ResStatus.ID = Res.ReservationStatusId
left join [Notification] Notify on Reslg.ReservationNo= Notify.ReservationNo
left join ReservationAssignement Assign on assign.ReservationLogId =Reslg.ID
GO


 ALTER   view [dbo].[VReservationList]
 	as
  
with Stepper as 
( 
select Reslg.StepId , Reslg.IsDone ,  step.StepName , Reslg.ID LogId ,Reslg.ReservationNo  LogReservationNo, Reslg.ReservationStatus LogStatusId ,  
   Reslg.CreationDate LogCreationDate   ,  Reslg.CompleteDate ,
 ResStatus.Status LogStatusName,

Assign.ID AssignID, Assign.[FromUser], Assign.[ToUser], Assign.[CreationDate] AssignCreationDate,UsrAssignTo.FullName AssignToName , UsrAssignFrom.FullName AssignFromName,
 DATEDIFF(HH,Reslg.[CreationDate],CURRENT_TIMESTAMP) as DiffDateAssignSubmit

,FormSubmit.ID FormSubmitID,  FormSubmit.[CreatedUser] ,UsrSumbitForm.FullName  FormSubmitCreatedByName , FormSubmit.[ReasonId] ,Reason.ReasonText ,FormSubmit.[Comment],FormSubmit.[CreationDate] FormSubmitCreationDate , 
 CASE WHEN FormSubmit.[ReasonStatus] is not null and  FormSubmit.[ReasonStatus] = 1 THEN 'Answer' 
 ELSE (case when FormSubmit.[ReasonStatus] is not null and  FormSubmit.[ReasonStatus] = 0 then  'NoAnswer' else null end  ) END  as ReasonStatus
 ,DATEDIFF(HH,Reslg.[CreationDate],CURRENT_TIMESTAMP) as DiffDateFormSubmit

 from ReservationStepsLog  Reslg 
inner join  ReservationStatus ResStatus  on ResStatus.ID = Reslg.ReservationStatus 
left join ReservationAssignement Assign on assign.ReservationLogId =Reslg.ID
left join ReservationFormSubmit FormSubmit on FormSubmit.ReservationLogId =Reslg.ID
left join Reason  on Reason.ID =FormSubmit.ReasonId
left join StatusStep step on step.StepId =Reslg.stepId 
left join AppUser UsrAssignTo on UsrAssignTo.ID =Assign.ToUser 
left join AppUser UsrAssignFrom on UsrAssignFrom.ID =Assign.FromUser 
left join AppUser UsrSumbitForm on UsrSumbitForm.ID =FormSubmit.CreatedUser  
--left join Notification notify on notify.ReservationLogId =Reslg.ID 
 --where step.IsNotification <>1
 --order by Reslg.ReservationNo , Reslg.StepId
)  
 
select NEWID() as UniqueId, Res.*     --ReservationNum,res.ReservationStatusId,BookingDate
,ResStatus.Status ReservationStatus
,Branch.Name   PickUpBranchName  ,Customer.Name CustomerName , SegmentCategory.RateSegmentCategoryName ,Segment.RateSegmentName

,Stepper.* from    Reservation Res
left join Stepper on Res.ReservationStatusId=Stepper.LogStatusId  and Res.ReservationNum= Stepper.LogReservationNo
inner join  ReservationStatus ResStatus  on ResStatus.ID = Res.ReservationStatusId 
 
left join Customer    on Customer.ID =RES.CustomerId 
left join RateSegmentCategory  SegmentCategory     on  SegmentCategory.ID =RES.RateSegmentCategory 
left join RateSegment  Segment     on  Segment.ID =RES.BusinessSegmentId 
left join Branch   on Branch.ID =Res.PickUpBranchId 
--where Res.ReservationStatusId=1
GO

------------------------------------------------------
----- Add notification url to Notification group 06/05/2020 -------------
Alter table NotificationGroup
 Add NotificationUrl nvarchar(50) null
 -----------------------------------------------------
 ----------Alter table form submit --------------------------------
 
alter table ReservationformSubmit add IsOpenConfirm bit Null
------------------------------------------------------------------

------------ Alter table Customer----------- 31/5/2020 ------------------------
Alter Table Customer Add  Phone nvarchar(50) Null , CreatedDate dateTime Null, ModifiedDate dateTime Null ,CreatedBy int Null,ModifiedBy int null,StepLogId int Null
,ReservationNo bigint Null, Comment nvarchar(100) Null 
-------------------------------------------------------------------------------
------------ Alter view ---------------
USE [SixtReservation]
GO

/****** Object:  View [dbo].[VReservationLog]    Script Date: 06/02/2020 11:32:02 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




 Alter view [dbo].[VReservationLog]
 	as
select NEWID() as UniqueId
	,Res.ReservationNum
	,Res.ReservationStatusId  StatusId 
	,Res.BookingDate
	,Res.PickUpDate
	,Res.DropOffDate
	,Res.CancelledDate
	,Reslg.[ID] as LogId
	,Reslg.[ReservationNo]
	,Reslg.[StepId]
	,Reslg.[IsDone]
	--,Reslg.[TableName]
	--,Reslg.[TableId]
	,Reslg.[ReservationStatus]
	,Reslg.[CreationDate]
	,Reslg.[ReservationId]
	,Reslg.[ReservationStatusId]
	,Reslg.[CompleteDate]
	,ResStatus.Status as  StatusTitle 
	,Notify.[ID] as NotificationId
    ,Notify.[NotificationText]
    ,Notify.[ToUser] as NotifyToUser 
    ,Notify.[IsSeen]
    ,Notify.[SeenDate]
    ,Notify.[ReservationNo] as NotifyReservationNo
    ,Notify.[ReservationLogId] as NotifyLogId
    ,Notify.[Status]
   -- ,Notify.[ReservationStatusId] as NotifyReservationStatus
    ,Notify.[CreateDate] as NotificationCreationDate
    ,Assign.[ID] as AssignmentId
    ,Assign.[FromUser] as AssignFromUser
    ,Assign.[ToUser] as AssignToUser
    ,Assign.[CreationDate] as AssignCreationDate
    ,Assign.[ReservationNo] as AssignReservationNo
    ,Assign.[ReservationLogId] as AssignLogId
	,DATEDIFF (HH,Reslg.[CreationDate] , CURRENT_TIMESTAMP) as DiffDateNotifyAssign
	,DATEDIFF(HH,Reslg.[CreationDate],CURRENT_TIMESTAMP) as DiffDateAssignSubmit
	,DATEDIFF(HH,CURRENT_TIMESTAMP,Res.PickUpDate) as DiffCurrPickUpDate
 
from Reservation Res
inner join ReservationStepsLog  Reslg on  Res.ReservationStatusId=Reslg.ReservationStatus  and Res.ReservationNum= Reslg.ReservationNo 
inner join  ReservationStatus ResStatus  on ResStatus.ID = Res.ReservationStatusId
left join [Notification] Notify on Reslg.ReservationNo= Notify.ReservationNo
left join ReservationAssignement Assign on assign.ReservationLogId =Reslg.ID
GO


------------------------------------------

------------------------------Adding Email Setting table- 06/03/2020---------------------------------------
CREATE TABLE [dbo].[EmailSetting](
	[Id] [int] NOT NULL identity(1,1),
	[ReseravationStatus] [int] NULL,
	[EmailText] [nvarchar](max) NULL,
	
 CONSTRAINT [PK_EmailSetting] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
---------------------End------------------

-------------------------------Alter Reservation Table-- 07/06/2020-----------------------------------------
ALTER TABLE Reservation
ADD IsCompleted bit Null
------------------------------------------------------------------------------
      
ALTER view [dbo].[VReservationHistory]
 	as
select Reslg.* ,ResStatus.Status,Assign.FromUser as AssignFromUser ,Assign.ToUser as AssignToUser,Assign.CreationDate as AssignCreationDate,step.StepName,FormSubmit.CreatedUser as FormSubmitUserId,FormSubmit.ReasonId as FormSubmitReasonId,FormSubmit.ReasonStatus,FormSubmit.Comment,FormSubmit.CreationDate as FormSubmitCreationDate,r.ReasonText as FormSubmitReason,UsrAssignTo.FullName as AssignTo,UsrAssignFrom.FullName as AssignFrom,UsrSumbitForm.FullName as UserSubmit ,CustomerForm.ModifiedBy as CustomerFormModifiedBy,CustomerForm.ModifiedDate as CustomerFormModifiedDate,CustomerForm.Email as CustomerFormEmail,CustomerForm.Phone as CustomerFormPhone,CustomerForm.Comment as CustomerFormComment,UsrCustomerForm.FullName as UsrCustomerForm 
from ReservationStepsLog  Reslg
inner join  ReservationStatus ResStatus  on ResStatus.ID = Reslg.ReservationStatus
left join ReservationAssignement Assign on assign.ReservationLogId =Reslg.ID
left join ReservationFormSubmit FormSubmit on FormSubmit.ReservationLogId =Reslg.ID
left join Reason r on r.ID =FormSubmit.ReasonId
left join Customer CustomerForm on CustomerForm.StepLogId=Reslg.ID
left join StatusStep step on step.StepId =Reslg.stepId
left join AppUser UsrAssignTo on UsrAssignTo.ID =Assign.ToUser
left join AppUser UsrAssignFrom on UsrAssignFrom.ID =Assign.FromUser
left join AppUser UsrSumbitForm on UsrSumbitForm.ID =FormSubmit.CreatedUser
left join AppUser UsrCustomerForm on UsrCustomerForm.ID =CustomerForm.ModifiedBy
where  step.StepOrder !=0 and Reslg.IsDone=1
---------------------------------------



