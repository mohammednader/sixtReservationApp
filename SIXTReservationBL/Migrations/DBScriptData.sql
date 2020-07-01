INSERT [dbo].[JobTitle] ([JobTitleId], [Name]) VALUES (1, N'Contact Center Agent')
GO
INSERT [dbo].[JobTitle] ([JobTitleId], [Name]) VALUES (2, N'Contact Center Manager')
GO
INSERT [dbo].[JobTitle] ([JobTitleId], [Name]) VALUES (3, N'Branch Agent')
GO
INSERT [dbo].[JobTitle] ([JobTitleId], [Name]) VALUES (4, N'Branch Manager')
GO
INSERT [dbo].[JobTitle] ([JobTitleId], [Name]) VALUES (5, N'Operations Manager')
GO
INSERT [dbo].[JobTitle] ([JobTitleId], [Name]) VALUES (6, N'Reservation Manager')
GO
INSERT [dbo].[JobTitle] ([JobTitleId], [Name]) VALUES (7, N'Management')
GO
INSERT [dbo].[JobTitle] ([JobTitleId], [Name]) VALUES (8, N'Admin')
GO

SET IDENTITY_INSERT [dbo].[Role] ON 
GO
INSERT [dbo].[Role] ([ID], [Name], [Description], [IsSysRole], [DefaultPageID], [CreatedBy], [CreationDate], [LastModifiedBy], [LastModificationDate], [IsDeleted]) VALUES (1, N'BranchAgent', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Role] ([ID], [Name], [Description], [IsSysRole], [DefaultPageID], [CreatedBy], [CreationDate], [LastModifiedBy], [LastModificationDate], [IsDeleted]) VALUES (2, N'BranchManger', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
/*hardcoded Reservation status*/
GO
INSERT [dbo].[ReservationStatus] ([ID], [Status]) VALUES (1, N'CancelledByCustomer')
GO
INSERT [dbo].[ReservationStatus] ([ID], [Status]) VALUES (2, N'CancelledBySixt')
GO
INSERT [dbo].[ReservationStatus] ([ID], [Status]) VALUES (3, N'NoShow')
GO
INSERT [dbo].[ReservationStatus] ([ID], [Status]) VALUES (4, N'Open')
GO
INSERT [dbo].[ReservationStatus] ([ID], [Status]) VALUES (5, N'Invoiced')
GO

USE [SixtReservation]
GO
INSERT [dbo].[StatusStep] ([StepId], [StepName], [ReseravationStatus], [StepOrder]) VALUES (1, N'Contact Center Managment  Notification', 1, 1)
GO
INSERT [dbo].[StatusStep] ([StepId], [StepName], [ReseravationStatus], [StepOrder]) VALUES (2, N'No assignment Notification', 1, 0)
GO
INSERT [dbo].[StatusStep] ([StepId], [StepName], [ReseravationStatus], [StepOrder]) VALUES (3, N'Contact Center Agent Assignment ', 1, 2)
GO
INSERT [dbo].[StatusStep] ([StepId], [StepName], [ReseravationStatus], [StepOrder]) VALUES (4, N'No Form sumbitted Notification', 1, 0)
GO
INSERT [dbo].[StatusStep] ([StepId], [StepName], [ReseravationStatus], [StepOrder]) VALUES (5, N'Agent Form Sumbitted', 1, 3)
GO
INSERT [dbo].[StatusStep] ([StepId], [StepName], [ReseravationStatus], [StepOrder]) VALUES (6, N'Finished', 1, 0)
GO
INSERT [dbo].[StatusStep] ([StepId], [StepName], [ReseravationStatus], [StepOrder]) VALUES (7, N'Operations and Reservation Manager  Notification', 2, 1)
GO
INSERT [dbo].[StatusStep] ([StepId], [StepName], [ReseravationStatus], [StepOrder]) VALUES (8, N'No assignment Notification', 2, 2)
GO
INSERT [dbo].[StatusStep] ([StepId], [StepName], [ReseravationStatus], [StepOrder]) VALUES (9, N' Agent Assignment - all users', 2, NULL)
GO
INSERT [dbo].[StatusStep] ([StepId], [StepName], [ReseravationStatus], [StepOrder]) VALUES (10, N'No Form sumbitted Notification', 2, NULL)
GO
INSERT [dbo].[StatusStep] ([StepId], [StepName], [ReseravationStatus], [StepOrder]) VALUES (11, N'Agent Form Sumbitted', 2, NULL)
GO
INSERT [dbo].[StatusStep] ([StepId], [StepName], [ReseravationStatus], [StepOrder]) VALUES (12, N'Finished', 2, NULL)
GO
INSERT [dbo].[StatusStep] ([StepId], [StepName], [ReseravationStatus], [StepOrder]) VALUES (13, N'Contact Center Managment  Notification', 3, NULL)
GO
INSERT [dbo].[StatusStep] ([StepId], [StepName], [ReseravationStatus], [StepOrder]) VALUES (14, N'No assignment Notification', 3, NULL)
GO
INSERT [dbo].[StatusStep] ([StepId], [StepName], [ReseravationStatus], [StepOrder]) VALUES (15, N'Contact Center Agent Assignment ', 3, NULL)
GO
INSERT [dbo].[StatusStep] ([StepId], [StepName], [ReseravationStatus], [StepOrder]) VALUES (16, N'No Form sumbitted Notification', 3, NULL)
GO
INSERT [dbo].[StatusStep] ([StepId], [StepName], [ReseravationStatus], [StepOrder]) VALUES (17, N'Agent Form Sumbitted', 3, NULL)
GO
INSERT [dbo].[StatusStep] ([StepId], [StepName], [ReseravationStatus], [StepOrder]) VALUES (18, N'Finished', 3, NULL)
GO



USE [SixtReservation]
GO
INSERT [dbo].[RateSegment] ([ID], [RateSegmentName]) VALUES (1, N'Touristic')
GO
INSERT [dbo].[RateSegment] ([ID], [RateSegmentName]) VALUES (2, N'Corporate')
GO
INSERT [dbo].[RateSegment] ([ID], [RateSegmentName]) VALUES (3, N'Retail')
GO
INSERT [dbo].[RateSegment] ([ID], [RateSegmentName]) VALUES (4, N'Other')
GO
INSERT [dbo].[RateSegmentCategory] ([ID], [RateSegmentCategoryName]) VALUES (1, N'Domestic')
GO
INSERT [dbo].[RateSegmentCategory] ([ID], [RateSegmentCategoryName]) VALUES (2, N'Inbound EU')
GO
INSERT [dbo].[RateSegmentCategory] ([ID], [RateSegmentCategoryName]) VALUES (3, N'Inbound US')
GO
INSERT [dbo].[RateSegmentCategory] ([ID], [RateSegmentCategoryName]) VALUES (4, N'Touristic')
GO
INSERT [dbo].[RateSegmentCategory] ([ID], [RateSegmentCategoryName]) VALUES (5, N'Corporate')
GO
INSERT [dbo].[RateSegmentCategory] ([ID], [RateSegmentCategoryName]) VALUES (6, N'Other')
GO


-- reservation status open
INSERT [dbo].[StatusStep] ([StepId], [StepName], [ReseravationStatus], [StepOrder], [IsNotification]) VALUES (23, N'Contact Center Managment  Notification', 4, 1, 1)
GO
INSERT [dbo].[StatusStep] ([StepId], [StepName], [ReseravationStatus], [StepOrder], [IsNotification]) VALUES (24, N'No Assign Call to Agent', 4, 2, 0)
GO
INSERT [dbo].[StatusStep] ([StepId], [StepName], [ReseravationStatus], [StepOrder], [IsNotification]) VALUES (25, N'Contact Center Agent Assignment ', 4, 3, 0)
GO
INSERT [dbo].[StatusStep] ([StepId], [StepName], [ReseravationStatus], [StepOrder], [IsNotification]) VALUES (26, N'No Form sumbitted Notification', 4, 4, 1)
GO
INSERT [dbo].[StatusStep] ([StepId], [StepName], [ReseravationStatus], [StepOrder], [IsNotification]) VALUES (27, N'Agent Form Sumbitted', 4, 5, 0)
GO
INSERT [dbo].[StatusStep] ([StepId], [StepName], [ReseravationStatus], [StepOrder], [IsNotification]) VALUES (28, N'Finished', 4, 6, 0)
GO

-- week day 
INSERT [dbo].[Weekdays] ([ID], [WeekDayName]) VALUES (1, N'Sunday')
GO
INSERT [dbo].[Weekdays] ([ID], [WeekDayName]) VALUES (2, N'Monday')
GO
INSERT [dbo].[Weekdays] ([ID], [WeekDayName]) VALUES (3, N'Tuesday')
GO
INSERT [dbo].[Weekdays] ([ID], [WeekDayName]) VALUES (4, N'wednesday')
GO
INSERT [dbo].[Weekdays] ([ID], [WeekDayName]) VALUES (5, N'Thursday')
GO
INSERT [dbo].[Weekdays] ([ID], [WeekDayName]) VALUES (6, N'Friday')
GO
INSERT [dbo].[Weekdays] ([ID], [WeekDayName]) VALUES (7, N'Saturday')

-------yasser 30-4 

USE [SixtReservation]
GO
INSERT [dbo].[NotificationGroup] ([ID], [GroupName], [Order], [ActionStep]) VALUES (1, N'Reservation Need Assignment', 1, 1)
GO
INSERT [dbo].[NotificationGroup] ([ID], [GroupName], [Order], [ActionStep]) VALUES (2, N'No Assignment Notification', 2, 2)
GO
INSERT [dbo].[NotificationGroup] ([ID], [GroupName], [Order], [ActionStep]) VALUES (3, N'No Form sumbitted Notification', 3, 4)
GO
INSERT [dbo].[NotificationGroup] ([ID], [GroupName], [Order], [ActionStep]) VALUES (4, N'Assigned To Me ', 4, NULL)
GO
-----------


----------------Notification Group------------------
GO
INSERT [dbo].[NotificationGroup] ([ID], [GroupName], [Order], [ActionStep], [NotificationUrl]) VALUES (1, N'Reservation Need Assignment', 1, 1, N'/Reservation/CByCustomerIndex')
GO
INSERT [dbo].[NotificationGroup] ([ID], [GroupName], [Order], [ActionStep], [NotificationUrl]) VALUES (2, N'No Assignment Notification', 2, 2, N'/Reservation/CByCustomerIndex')
GO
INSERT [dbo].[NotificationGroup] ([ID], [GroupName], [Order], [ActionStep], [NotificationUrl]) VALUES (3, N'No Form sumbitted Notification', 3, 3, N'/Reservation/CByCustomerIndex')
GO
INSERT [dbo].[NotificationGroup] ([ID], [GroupName], [Order], [ActionStep], [NotificationUrl]) VALUES (4, N'Assigned To Me ', 4, 5, N'/Reservation/CByCustomerIndex')
GO
INSERT [dbo].[NotificationGroup] ([ID], [GroupName], [Order], [ActionStep], [NotificationUrl]) VALUES (5, N'Reservation Need Assignment', 5, 7, N'/Reservation/CBySixtIndex')
GO
INSERT [dbo].[NotificationGroup] ([ID], [GroupName], [Order], [ActionStep], [NotificationUrl]) VALUES (6, N'No Assignment Notification', 6, 8, N'/Reservation/CBySixtIndex')
GO
INSERT [dbo].[NotificationGroup] ([ID], [GroupName], [Order], [ActionStep], [NotificationUrl]) VALUES (7, N'No Form sumbitted Notification', 7, 9, N'/Reservation/CBySixtIndex')
GO
INSERT [dbo].[NotificationGroup] ([ID], [GroupName], [Order], [ActionStep], [NotificationUrl]) VALUES (8, N'Assigned To Me ', 8, 11, N'/Reservation/CBySixtIndex')
GO
------------------------------------------------No Show
INSERT [dbo].[NotificationGroup] ([ID], [GroupName], [Order], [ActionStep], [NotificationUrl]) VALUES (9, N'Reservation Need Assignment', 9, 13, N'/Reservation/NoShowIndex')
GO
INSERT [dbo].[NotificationGroup] ([ID], [GroupName], [Order], [ActionStep], [NotificationUrl]) VALUES (10, N'No Assignment Notification', 10, 14, N'/Reservation/NoShowIndex')
GO
INSERT [dbo].[NotificationGroup] ([ID], [GroupName], [Order], [ActionStep], [NotificationUrl]) VALUES (11, N'No Form sumbitted Notification', 11, 15, N'/Reservation/NoShowIndex')
GO
INSERT [dbo].[NotificationGroup] ([ID], [GroupName], [Order], [ActionStep], [NotificationUrl]) VALUES (12, N'Assigned To Me ', 12, 17, N'/Reservation/NoShowIndex')

-----------------------------------------------Open
GO
INSERT [dbo].[NotificationGroup] ([ID], [GroupName], [Order], [ActionStep], [NotificationUrl]) VALUES (13, N'Reservation Need Assignment', 13, 19, N'/Reservation/OpenIndex')
GO
INSERT [dbo].[NotificationGroup] ([ID], [GroupName], [Order], [ActionStep], [NotificationUrl]) VALUES (14, N'No Assignment Notification', 14, 20, N'/Reservation/OpenIndex')
GO
INSERT [dbo].[NotificationGroup] ([ID], [GroupName], [Order], [ActionStep], [NotificationUrl]) VALUES (15, N'No Form sumbitted Notification', 15, 21, N'/Reservation/OpenIndex')
GO
INSERT [dbo].[NotificationGroup] ([ID], [GroupName], [Order], [ActionStep], [NotificationUrl]) VALUES (16, N'Assigned To Me ', 16, 23, N'/Reservation/OpenIndex')
GO

-----------------------------------------------------------------------------
-------------------- Open Status Step -----------------

INSERT [dbo].[StatusStep] ([StepId], [StepName], [ReseravationStatus], [StepOrder], [IsNotification], [IsLast]) VALUES (19, N'Branch Managment  Notification', 4, 1, 1, NULL)
GO
INSERT [dbo].[StatusStep] ([StepId], [StepName], [ReseravationStatus], [StepOrder], [IsNotification], [IsLast]) VALUES (20, N'No assignment Notification', 4, 0, 1, NULL)
GO
INSERT [dbo].[StatusStep] ([StepId], [StepName], [ReseravationStatus], [StepOrder], [IsNotification], [IsLast]) VALUES (21, N'Branch Agent Assignment ', 4, 2, 0, NULL)
GO
INSERT [dbo].[StatusStep] ([StepId], [StepName], [ReseravationStatus], [StepOrder], [IsNotification], [IsLast]) VALUES (22, N'Branch Agent Confirmation_No FormSubmit', 4, 0, 1, NULL)
GO
INSERT [dbo].[StatusStep] ([StepId], [StepName], [ReseravationStatus], [StepOrder], [IsNotification], [IsLast]) VALUES (23, N'Branch Agent Confirmation_FormSubmit', 4, 3, 0, 1)
GO
INSERT [dbo].[StatusStep] ([StepId], [StepName], [ReseravationStatus], [StepOrder], [IsNotification], [IsLast]) VALUES (24, N'Contact Center Managment  Notification', 4, 4, 1, NULL)
GO
INSERT [dbo].[StatusStep] ([StepId], [StepName], [ReseravationStatus], [StepOrder], [IsNotification], [IsLast]) VALUES (25, N'No assignment Notification', 4, 0, 1, NULL)
GO
INSERT [dbo].[StatusStep] ([StepId], [StepName], [ReseravationStatus], [StepOrder], [IsNotification], [IsLast]) VALUES (26, N'Contact Center Agent Assignment ', 4, 5, 0, NULL)
GO
INSERT [dbo].[StatusStep] ([StepId], [StepName], [ReseravationStatus], [StepOrder], [IsNotification], [IsLast]) VALUES (27, N'Contact Center Agent_No Form Submit', 4, 0, 1, NULL)
GO
INSERT [dbo].[StatusStep] ([StepId], [StepName], [ReseravationStatus], [StepOrder], [IsNotification], [IsLast]) VALUES (28, N'Contact Center Agent_Form Submit', 4, 6, 0, 1)
----------------------------End  -------------------------------

----------------- open Notitification Group  ------------
INSERT [dbo].[NotificationGroup] ([ID], [GroupName], [Order], [ActionStep], [NotificationUrl]) VALUES (13, N'Reservation Need Assignment (BM)', 13, 19, N'/Reservation/OpenIndex')
GO
INSERT [dbo].[NotificationGroup] ([ID], [GroupName], [Order], [ActionStep], [NotificationUrl]) VALUES (14, N'No Assignment Notification (BM)', 14, 20, N'/Reservation/OpenIndex')
GO
INSERT [dbo].[NotificationGroup] ([ID], [GroupName], [Order], [ActionStep], [NotificationUrl]) VALUES (15, N'No Form sumbitted Notification (BM)', 15, 21, N'/Reservation/OpenIndex')
GO
INSERT [dbo].[NotificationGroup] ([ID], [GroupName], [Order], [ActionStep], [NotificationUrl]) VALUES (16, N'Assigned To Me (BM)', 16, 23, N'/Reservation/OpenIndex')
GO
INSERT [dbo].[NotificationGroup] ([ID], [GroupName], [Order], [ActionStep], [NotificationUrl]) VALUES (17, N'Reservation Need Assignment (CCM)', 17, 24, N'/Reservation/OpenIndex')
GO
INSERT [dbo].[NotificationGroup] ([ID], [GroupName], [Order], [ActionStep], [NotificationUrl]) VALUES (18, N'No Assignment Notification (CCM)', 18, 25, N'/Reservation/OpenIndex')
GO
INSERT [dbo].[NotificationGroup] ([ID], [GroupName], [Order], [ActionStep], [NotificationUrl]) VALUES (19, N'No Form sumbitted Notification (CCM)', 19, 26, N'/Reservation/OpenIndex')
GO
INSERT [dbo].[NotificationGroup] ([ID], [GroupName], [Order], [ActionStep], [NotificationUrl]) VALUES (20, N'Assigned To Me (CCM)', 20, 28, N'/Reservation/OpenIndex')
------------------------------------------
------------------------------ Adding open Steps confirmed-  06/02/2020----------------------------------------
GO
INSERT [dbo].[StatusStep] ([StepId], [StepName], [ReseravationStatus], [StepOrder], [IsNotification], [IsLast]) VALUES (29, N'Fill Customer Form_Email&Phone', 4, 7, 0, NULL)
GO
INSERT [dbo].[StatusStep] ([StepId], [StepName], [ReseravationStatus], [StepOrder], [IsNotification], [IsLast]) VALUES (30, N'Customer Notification Email', 4, 0, 0, NULL)
GO
INSERT [dbo].[StatusStep] ([StepId], [StepName], [ReseravationStatus], [StepOrder], [IsNotification], [IsLast]) VALUES (31, N'Branch Managment  Notification', 4, 0, 1, NULL)
GO
INSERT [dbo].[StatusStep] ([StepId], [StepName], [ReseravationStatus], [StepOrder], [IsNotification], [IsLast]) VALUES (32, N'No assignment Notification', 4, 0, 1, NULL)
GO
INSERT [dbo].[StatusStep] ([StepId], [StepName], [ReseravationStatus], [StepOrder], [IsNotification], [IsLast]) VALUES (33, N'Branch Agent Assignment ', 4, 8, 0, NULL)
GO
INSERT [dbo].[StatusStep] ([StepId], [StepName], [ReseravationStatus], [StepOrder], [IsNotification], [IsLast]) VALUES (34, N'Branch Agent Confirmation_No FormSubmit', 4, 0, 1, NULL)
GO
INSERT [dbo].[StatusStep] ([StepId], [StepName], [ReseravationStatus], [StepOrder], [IsNotification], [IsLast]) VALUES (35, N'Branch Agent Confirmation_FormSubmit', 4, 9, 0, 1)
-----------------------------

GO
INSERT [dbo].[NotificationGroup] ([ID], [GroupName], [Order], [ActionStep], [NotificationUrl]) VALUES (21, N'Reservation Need Assignment Last Step (BM)', 21, 31, N'/Reservation/OpenIndex')
GO
INSERT [dbo].[NotificationGroup] ([ID], [GroupName], [Order], [ActionStep], [NotificationUrl]) VALUES (22, N'No Assignment Notification Last Step (BM)', 22, 32, N'/Reservation/OpenIndex')
GO
INSERT [dbo].[NotificationGroup] ([ID], [GroupName], [Order], [ActionStep], [NotificationUrl]) VALUES (23, N'No Form sumbitted Notification Last Step (BM)', 23, 33, N'/Reservation/OpenIndex')
GO
INSERT [dbo].[NotificationGroup] ([ID], [GroupName], [Order], [ActionStep], [NotificationUrl]) VALUES (24, N'Assigned To Me Last Step (BM)', 24, 35, N'/Reservation/OpenIndex')
GO
-----------------------------------------

------------------------add notification grouup 08/6/2020-----------
INSERT [dbo].[NotificationGroup] ([ID], [GroupName], [Order], [ActionStep], [NotificationUrl]) VALUES (25, N'NeedToChangeReservationStatus', 25, 36, N'/Reservation/ChangeStatus')
GO
INSERT [dbo].[StatusStep] ([StepId], [StepName], [ReseravationStatus], [StepOrder], [IsNotification], [IsLast]) VALUES (36, N'Change status', 4, 0, 1, NULL)
GO
-------------------------------------------------------------------

