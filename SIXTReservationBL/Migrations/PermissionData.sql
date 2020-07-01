----------------------Edite & Adding new  permission  updated 12/5----------------------
delete from permission

GO
INSERT [dbo].[Permission] ([ID], [Name], [DisplayName], [Description], [ParentID], [Order], [PermissionType], [PermissionURL], [StyleClass], [IsDeleted]) VALUES (1, N'administration', N'Administration', NULL, NULL, 1, 1, N'/menu/administration', N'sidebar-item-icon fas fa-user-shield', NULL)
GO
INSERT [dbo].[Permission] ([ID], [Name], [DisplayName], [Description], [ParentID], [Order], [PermissionType], [PermissionURL], [StyleClass], [IsDeleted]) VALUES (2, N'userManagement-index', N'User Management', NULL, 1, 1, 1, N'/usermanagement/index', NULL, NULL)
GO
INSERT [dbo].[Permission] ([ID], [Name], [DisplayName], [Description], [ParentID], [Order], [PermissionType], [PermissionURL], [StyleClass], [IsDeleted]) VALUES (3, N'role-index', N'Role Mangement', NULL, 1, 2, 1, N'/rolemanagement/roleindex', NULL, NULL)
GO
INSERT [dbo].[Permission] ([ID], [Name], [DisplayName], [Description], [ParentID], [Order], [PermissionType], [PermissionURL], [StyleClass], [IsDeleted]) VALUES (4, N'branch-index', N'Branch Management', NULL, 1, 3, 1, N'/Branch/BranchIndex', NULL, NULL)
GO
INSERT [dbo].[Permission] ([ID], [Name], [DisplayName], [Description], [ParentID], [Order], [PermissionType], [PermissionURL], [StyleClass], [IsDeleted]) VALUES (5, N'reason-index', N'Reason Management', NULL, 1, 4, 1, N'/ReasonManagement/ReasonIndex', NULL, NULL)
GO
INSERT [dbo].[Permission] ([ID], [Name], [DisplayName], [Description], [ParentID], [Order], [PermissionType], [PermissionURL], [StyleClass], [IsDeleted]) VALUES (6, N'question-index', N'Question Mangement', NULL, 1, 5, 1, N'/QuestionManagement/QuestionIndex', NULL, NULL)
GO
INSERT [dbo].[Permission] ([ID], [Name], [DisplayName], [Description], [ParentID], [Order], [PermissionType], [PermissionURL], [StyleClass], [IsDeleted]) VALUES (7, N'notification-index', N'Notification Mangement', NULL, 1, 6, 1, N'/NotificationManagement/NotificationIndex', NULL, NULL)
GO
INSERT [dbo].[Permission] ([ID], [Name], [DisplayName], [Description], [ParentID], [Order], [PermissionType], [PermissionURL], [StyleClass], [IsDeleted]) VALUES (8, N'actionSetting-index', N'Action Setting Mangement', NULL, 1, 7, 1, N'/actionsettingmanagement/actionindex', NULL, NULL)
GO
INSERT [dbo].[Permission] ([ID], [Name], [DisplayName], [Description], [ParentID], [Order], [PermissionType], [PermissionURL], [StyleClass], [IsDeleted]) VALUES (9, N'uploads', N'Uploads', NULL, NULL, 2, 1, N'/menu/uploads', N'sidebar-item-icon fas fa-upload', NULL)
GO
INSERT [dbo].[Permission] ([ID], [Name], [DisplayName], [Description], [ParentID], [Order], [PermissionType], [PermissionURL], [StyleClass], [IsDeleted]) VALUES (10, N'newUpload', N'New Upload', NULL, 9, 1, 1, N'/Uploads/Index', NULL, NULL)
GO
INSERT [dbo].[Permission] ([ID], [Name], [DisplayName], [Description], [ParentID], [Order], [PermissionType], [PermissionURL], [StyleClass], [IsDeleted]) VALUES (11, N'uploadLog', N'Upload Log', NULL, 9, 2, 1, N'/Uploads/UploadsIndex', NULL, NULL)
GO
INSERT [dbo].[Permission] ([ID], [Name], [DisplayName], [Description], [ParentID], [Order], [PermissionType], [PermissionURL], [StyleClass], [IsDeleted]) VALUES (12, N'reservationStatus', N'Reservation Status', NULL, NULL, 3, 1, N'/menu/ReservationStatus', N'sidebar-item-icon fas fa-clipboard-list-check', NULL)
GO
INSERT [dbo].[Permission] ([ID], [Name], [DisplayName], [Description], [ParentID], [Order], [PermissionType], [PermissionURL], [StyleClass], [IsDeleted]) VALUES (13, N'cancellationByCustomer', N'Cancellation By Customer', NULL, 12, 1, 1, N'/Reservation/CByCustomerIndex', NULL, NULL)
GO
INSERT [dbo].[Permission] ([ID], [Name], [DisplayName], [Description], [ParentID], [Order], [PermissionType], [PermissionURL], [StyleClass], [IsDeleted]) VALUES (14, N'usermanagement-_userlist', N'Users List', NULL, 2, 1, 2, N'/usermanagement/_userlist', NULL, NULL)
GO
INSERT [dbo].[Permission] ([ID], [Name], [DisplayName], [Description], [ParentID], [Order], [PermissionType], [PermissionURL], [StyleClass], [IsDeleted]) VALUES (15, N'usermanagement-createUser', N'Add User', NULL, 2, 2, 2, N'/usermanagement/createuser', NULL, NULL)
GO
INSERT [dbo].[Permission] ([ID], [Name], [DisplayName], [Description], [ParentID], [Order], [PermissionType], [PermissionURL], [StyleClass], [IsDeleted]) VALUES (16, N'usermanagement-updateuser', N'Update User', NULL, 2, 3, 2, N'/usermanagement/updateuser', NULL, NULL)
GO
INSERT [dbo].[Permission] ([ID], [Name], [DisplayName], [Description], [ParentID], [Order], [PermissionType], [PermissionURL], [StyleClass], [IsDeleted]) VALUES (17, N'usermanagement-deactivateuser', N'Deactivate User', NULL, 2, 4, 2, N'/usermanamgent/deactivateuser', NULL, NULL)
GO
INSERT [dbo].[Permission] ([ID], [Name], [DisplayName], [Description], [ParentID], [Order], [PermissionType], [PermissionURL], [StyleClass], [IsDeleted]) VALUES (18, N'usermanagement-activateuser', N'Activate User', NULL, 2, 5, 2, N'/usermanagement/activateuser', NULL, NULL)
GO
INSERT [dbo].[Permission] ([ID], [Name], [DisplayName], [Description], [ParentID], [Order], [PermissionType], [PermissionURL], [StyleClass], [IsDeleted]) VALUES (19, N'usermanagement-resetpassord', N'Reset Password', NULL, 2, 6, 2, N'/usermanagement/resetpassword', NULL, NULL)
GO
INSERT [dbo].[Permission] ([ID], [Name], [DisplayName], [Description], [ParentID], [Order], [PermissionType], [PermissionURL], [StyleClass], [IsDeleted]) VALUES (20, N'rolemanagement-_rolelist', N'Roles List', NULL, 3, 1, 2, N'/rolemanagement/_rolelist', NULL, NULL)
GO
INSERT [dbo].[Permission] ([ID], [Name], [DisplayName], [Description], [ParentID], [Order], [PermissionType], [PermissionURL], [StyleClass], [IsDeleted]) VALUES (21, N'rolemanagement-createrole', N'Add Role', NULL, 3, 2, 2, N'/rolemanagement/createrole', NULL, NULL)
GO
INSERT [dbo].[Permission] ([ID], [Name], [DisplayName], [Description], [ParentID], [Order], [PermissionType], [PermissionURL], [StyleClass], [IsDeleted]) VALUES (22, N'rolemanagement-updaterole', N'Update Role', NULL, 3, 3, 2, N'/rolemanagement/updaterole', NULL, NULL)
GO
INSERT [dbo].[Permission] ([ID], [Name], [DisplayName], [Description], [ParentID], [Order], [PermissionType], [PermissionURL], [StyleClass], [IsDeleted]) VALUES (23, N'rolemanagement-deleterole', N'Delete Role', NULL, 3, 4, 2, N'/rolemanagement/deleterole', NULL, NULL)
GO
INSERT [dbo].[Permission] ([ID], [Name], [DisplayName], [Description], [ParentID], [Order], [PermissionType], [PermissionURL], [StyleClass], [IsDeleted]) VALUES (24, N'reasonmanagement-_reasonlist', N'Reasons List', NULL, 5, 1, 2, N'/reasonmanagement/_reasonlist', NULL, NULL)
GO
INSERT [dbo].[Permission] ([ID], [Name], [DisplayName], [Description], [ParentID], [Order], [PermissionType], [PermissionURL], [StyleClass], [IsDeleted]) VALUES (25, N'reasonmanagement-createreason', N'Add Reason', NULL, 5, 2, 2, N'/reasonmanagement/createreason', NULL, NULL)
GO
INSERT [dbo].[Permission] ([ID], [Name], [DisplayName], [Description], [ParentID], [Order], [PermissionType], [PermissionURL], [StyleClass], [IsDeleted]) VALUES (26, N'reasonmanagement-updatereason', N'Update Reason', NULL, 5, 3, 2, N'/reasonmanagement/updatereason', NULL, NULL)
GO
INSERT [dbo].[Permission] ([ID], [Name], [DisplayName], [Description], [ParentID], [Order], [PermissionType], [PermissionURL], [StyleClass], [IsDeleted]) VALUES (27, N'reasonmanagement-activatereason', N'Activate Reason', NULL, 5, 4, 2, N'/reasonmanagement/activatereason', NULL, NULL)
GO
INSERT [dbo].[Permission] ([ID], [Name], [DisplayName], [Description], [ParentID], [Order], [PermissionType], [PermissionURL], [StyleClass], [IsDeleted]) VALUES (28, N'reasonmanagement-deactivatereason', N'Deactivate Reason', NULL, 5, 4, 2, N'/reasonmanagement/deactivatereason', NULL, NULL)
GO
INSERT [dbo].[Permission] ([ID], [Name], [DisplayName], [Description], [ParentID], [Order], [PermissionType], [PermissionURL], [StyleClass], [IsDeleted]) VALUES (29, N'reasonmanagement-deletereason', N'Delete Reason', NULL, 5, 4, 2, N'/reasonmanagement/deletereason', NULL, NULL)
GO
INSERT [dbo].[Permission] ([ID], [Name], [DisplayName], [Description], [ParentID], [Order], [PermissionType], [PermissionURL], [StyleClass], [IsDeleted]) VALUES (30, N'branch-_branchlist', N'Branch List', NULL, 4, 1, 2, N'/branch/_branchlist', NULL, NULL)
GO
INSERT [dbo].[Permission] ([ID], [Name], [DisplayName], [Description], [ParentID], [Order], [PermissionType], [PermissionURL], [StyleClass], [IsDeleted]) VALUES (31, N'branch-createbranch', N'Add Branch', NULL, 4, 2, 2, N'/branch/createbranch', NULL, NULL)
GO
INSERT [dbo].[Permission] ([ID], [Name], [DisplayName], [Description], [ParentID], [Order], [PermissionType], [PermissionURL], [StyleClass], [IsDeleted]) VALUES (32, N'branch-updatebranch', N'Update Branch', NULL, 4, 3, 2, N'/branch/updatebranch', NULL, NULL)
GO
INSERT [dbo].[Permission] ([ID], [Name], [DisplayName], [Description], [ParentID], [Order], [PermissionType], [PermissionURL], [StyleClass], [IsDeleted]) VALUES (33, N'branch-activatebranch', N'Activate Branch', NULL, 4, 4, 2, N'/branch/activatebranch', NULL, NULL)
GO
INSERT [dbo].[Permission] ([ID], [Name], [DisplayName], [Description], [ParentID], [Order], [PermissionType], [PermissionURL], [StyleClass], [IsDeleted]) VALUES (34, N'branch-deactivatebranch', N'Deactivate Branch', NULL, 4, 5, 2, N'/branch/deactivatebranch', NULL, NULL)
GO
INSERT [dbo].[Permission] ([ID], [Name], [DisplayName], [Description], [ParentID], [Order], [PermissionType], [PermissionURL], [StyleClass], [IsDeleted]) VALUES (35, N'notificationmanagement-_notificationlist', N'Notifications List', NULL, 7, 1, 2, N'/notificationmanagement/_notificationlist', NULL, NULL)
GO
INSERT [dbo].[Permission] ([ID], [Name], [DisplayName], [Description], [ParentID], [Order], [PermissionType], [PermissionURL], [StyleClass], [IsDeleted]) VALUES (36, N'notificationmanagement-createnotification', N'Add Notification', NULL, 7, 2, 2, N'/notificationmanagement/createnotification', NULL, NULL)
GO
INSERT [dbo].[Permission] ([ID], [Name], [DisplayName], [Description], [ParentID], [Order], [PermissionType], [PermissionURL], [StyleClass], [IsDeleted]) VALUES (37, N'notificationmanagement-updatenotification', N'Update Notification', NULL, 7, 3, 2, N'/notificationmanagement/updatenotification', NULL, NULL)
GO
INSERT [dbo].[Permission] ([ID], [Name], [DisplayName], [Description], [ParentID], [Order], [PermissionType], [PermissionURL], [StyleClass], [IsDeleted]) VALUES (38, N'notificationmanagement-deletenotification', N'Delete Notification', NULL, 7, 4, 2, N'/notificationmanagement/deletenotification', NULL, NULL)
GO
INSERT [dbo].[Permission] ([ID], [Name], [DisplayName], [Description], [ParentID], [Order], [PermissionType], [PermissionURL], [StyleClass], [IsDeleted]) VALUES (39, N'notificationmanagement-notificationlist', N'All NotificationList', NULL, NULL, 4, 2, N'/notificationmanagement/notificationlist', NULL, NULL)
GO
INSERT [dbo].[Permission] ([ID], [Name], [DisplayName], [Description], [ParentID], [Order], [PermissionType], [PermissionURL], [StyleClass], [IsDeleted]) VALUES (40, N'questionmanagement-_questionlist', N'Question List', NULL, 6, 1, 2, N'/questionmanagement/_questionlist', NULL, NULL)
GO
INSERT [dbo].[Permission] ([ID], [Name], [DisplayName], [Description], [ParentID], [Order], [PermissionType], [PermissionURL], [StyleClass], [IsDeleted]) VALUES (41, N'questionmanagement-createquestion', N'Add Question', NULL, 6, 2, 2, N'/questionmanagement/createquestion', NULL, NULL)
GO
INSERT [dbo].[Permission] ([ID], [Name], [DisplayName], [Description], [ParentID], [Order], [PermissionType], [PermissionURL], [StyleClass], [IsDeleted]) VALUES (42, N'questionmanagement-updatequestion', N'Update Question', NULL, 6, 3, 2, N'/questionmanagement/updatequestion', NULL, NULL)
GO
INSERT [dbo].[Permission] ([ID], [Name], [DisplayName], [Description], [ParentID], [Order], [PermissionType], [PermissionURL], [StyleClass], [IsDeleted]) VALUES (43, N'questionmanagement-deletequestion', N'Delete Question', NULL, 6, 4, 2, N'/questionmanagement/deletequestion', NULL, NULL)
GO
INSERT [dbo].[Permission] ([ID], [Name], [DisplayName], [Description], [ParentID], [Order], [PermissionType], [PermissionURL], [StyleClass], [IsDeleted]) VALUES (44, N'actionsettingmanagement-_actionsettinglist', N'Action Setting List', NULL, 8, 1, 2, N'/actionsettingmanagement/_actionsettinglist', NULL, NULL)
GO
INSERT [dbo].[Permission] ([ID], [Name], [DisplayName], [Description], [ParentID], [Order], [PermissionType], [PermissionURL], [StyleClass], [IsDeleted]) VALUES (45, N'actionsettingmanagement-createactionsetting', N'Add Action Setting', NULL, 8, 2, 2, N'/actionsettingmanagement/createactionsetting', NULL, NULL)
GO
INSERT [dbo].[Permission] ([ID], [Name], [DisplayName], [Description], [ParentID], [Order], [PermissionType], [PermissionURL], [StyleClass], [IsDeleted]) VALUES (46, N'actionsettingmanagement-updateactionsetting', N'Update Action Setting', NULL, 8, 3, 2, N'/actionsettingmanagement/updateactionsetting', NULL, NULL)
GO
INSERT [dbo].[Permission] ([ID], [Name], [DisplayName], [Description], [ParentID], [Order], [PermissionType], [PermissionURL], [StyleClass], [IsDeleted]) VALUES (47, N'actionsettingmanagement-deleteactionsetting', N'Delete Action Setting', NULL, 8, 4, 2, N'/actionsettingmanagement/deleteactionsetting', NULL, NULL)
GO
INSERT [dbo].[Permission] ([ID], [Name], [DisplayName], [Description], [ParentID], [Order], [PermissionType], [PermissionURL], [StyleClass], [IsDeleted]) VALUES (48, N'reservation-_cancelledbycustomerlist', N'Cancel By Customer List', NULL, 13, 1, 2, N'/reservation/_cancelledbycustomerlist', NULL, NULL)
GO
INSERT [dbo].[Permission] ([ID], [Name], [DisplayName], [Description], [ParentID], [Order], [PermissionType], [PermissionURL], [StyleClass], [IsDeleted]) VALUES (49, N'reservation-cbycustomerdetails', N'Cancel By Customer details', NULL, 48, 1, 2, N'/reservation/cbycustomerdetails', NULL, NULL)
GO
INSERT [dbo].[Permission] ([ID], [Name], [DisplayName], [Description], [ParentID], [Order], [PermissionType], [PermissionURL], [StyleClass], [IsDeleted]) VALUES (50, N'reservation-assignagent_multiple', N'Cancel By Customer Assign Agent', NULL, 48, 2, 2, N'/reservation/assignagent_multiple', NULL, NULL)
GO
INSERT [dbo].[Permission] ([ID], [Name], [DisplayName], [Description], [ParentID], [Order], [PermissionType], [PermissionURL], [StyleClass], [IsDeleted]) VALUES (51, N'reservation-formsubmitted', N'Cancel By Customer Form Submit', NULL, 48, 3, 2, N'/reservation/formsubmitted', NULL, NULL)
GO
INSERT [dbo].[Permission] ([ID], [Name], [DisplayName], [Description], [ParentID], [Order], [PermissionType], [PermissionURL], [StyleClass], [IsDeleted]) VALUES (52, N'reservation-ExportReservation', N'Cancel By Customer Export', NULL, 48, 4, 2, N'/reservation/exportreservation', NULL, NULL)
GO
INSERT [dbo].[Permission] ([ID], [Name], [DisplayName], [Description], [ParentID], [Order], [PermissionType], [PermissionURL], [StyleClass], [IsDeleted]) VALUES (53, N'reservation-CByCustomerSteps', N'Cancel By Customer Steps Index View ', NULL, 13, 6, 2, N'/reservation/CByCustomerSteps', NULL, NULL)
GO
INSERT [dbo].[Permission] ([ID], [Name], [DisplayName], [Description], [ParentID], [Order], [PermissionType], [PermissionURL], [StyleClass], [IsDeleted]) VALUES (54, N'reservation-_CByCustomerNotifyContactCenter', N'Cancel By Customer Notification (Step 1) View ', NULL, 53, 1, 2, N'/reservation/_CByCustomerNotifyContactCenter', NULL, NULL)
GO
INSERT [dbo].[Permission] ([ID], [Name], [DisplayName], [Description], [ParentID], [Order], [PermissionType], [PermissionURL], [StyleClass], [IsDeleted]) VALUES (55, N'reservation-_CByCustomerAssignContactCenter', N'Cancel By Customer Assignment (Step 2) View', NULL, 53, 2, 2, N'/reservation/_CByCustomerAssignContactCenter', NULL, NULL)
GO
INSERT [dbo].[Permission] ([ID], [Name], [DisplayName], [Description], [ParentID], [Order], [PermissionType], [PermissionURL], [StyleClass], [IsDeleted]) VALUES (56, N'reservation-_CByCustomerFormSubmit', N'Cancel By Customer Form Submitted (Step 3) View', NULL, 53, 3, 2, N'/reservation/_CByCustomerFormSubmit', NULL, NULL)
GO
INSERT [dbo].[Permission] ([ID], [Name], [DisplayName], [Description], [ParentID], [Order], [PermissionType], [PermissionURL], [StyleClass], [IsDeleted]) VALUES (57, N'CancellationBySixt', N'Cancellation By Sixt', NULL, 12, 2, 1, N'/Reservation/CBySixtIndex', NULL, NULL)
GO
INSERT [dbo].[Permission] ([ID], [Name], [DisplayName], [Description], [ParentID], [Order], [PermissionType], [PermissionURL], [StyleClass], [IsDeleted]) VALUES (58, N'reservation-_cancelledbysixtlist', N'Cancel By Sixt List', NULL, 57, 1, 2, N'/Reservation/_cancelBySixtList', NULL, NULL)
GO
INSERT [dbo].[Permission] ([ID], [Name], [DisplayName], [Description], [ParentID], [Order], [PermissionType], [PermissionURL], [StyleClass], [IsDeleted]) VALUES (59, N'reservation-cbysixtdetails', N'Cancel By Sixt Details', NULL, 58, 1, 2, N'/Reservation/cbysixtDetails', NULL, NULL)
GO
INSERT [dbo].[Permission] ([ID], [Name], [DisplayName], [Description], [ParentID], [Order], [PermissionType], [PermissionURL], [StyleClass], [IsDeleted]) VALUES (60, N'reservation-CBySixtAssign_Multiple', N'Cancel By Sixt Assign Agent', NULL, 58, 2, 2, N'/Reservation/CBySixtAssign_Multiple', NULL, NULL)
GO
INSERT [dbo].[Permission] ([ID], [Name], [DisplayName], [Description], [ParentID], [Order], [PermissionType], [PermissionURL], [StyleClass], [IsDeleted]) VALUES (61, N'reservation-CBySixtFormSubmitted', N'Cancel By Customer Form Submit', NULL, 58, 3, 2, N'/Reservation/CBySixtFormSubmitted', NULL, NULL)
GO
INSERT [dbo].[Permission] ([ID], [Name], [DisplayName], [Description], [ParentID], [Order], [PermissionType], [PermissionURL], [StyleClass], [IsDeleted]) VALUES (62, N'reservation-CBySixtExportReservation', N'Cancel By Sixt Export', NULL, 58, 4, 2, N'/Reservation/CBySixtExportReservation', NULL, NULL)
GO
INSERT [dbo].[Permission] ([ID], [Name], [DisplayName], [Description], [ParentID], [Order], [PermissionType], [PermissionURL], [StyleClass], [IsDeleted]) VALUES (63, N'reservation-CBySixtSteps', N'Cancel By Sixt Steps Index View', NULL, 57, 6, 2, N'/Reservation/CBySixtSteps', NULL, NULL)
GO
INSERT [dbo].[Permission] ([ID], [Name], [DisplayName], [Description], [ParentID], [Order], [PermissionType], [PermissionURL], [StyleClass], [IsDeleted]) VALUES (64, N'reservation-_CBySixtNotify', N'Cancel By Sixt Notification (Step 1) View', NULL, 63, 1, 2, N'/Reservation/_CBySixtNotify', NULL, NULL)
GO
INSERT [dbo].[Permission] ([ID], [Name], [DisplayName], [Description], [ParentID], [Order], [PermissionType], [PermissionURL], [StyleClass], [IsDeleted]) VALUES (65, N'reservation-_CBySixtAssignment', N'Cancel By Sixt Assignment (Step 2) View', NULL, 63, 2, 2, N'/Reservation/_CBySixtAssignment', NULL, NULL)
GO
INSERT [dbo].[Permission] ([ID], [Name], [DisplayName], [Description], [ParentID], [Order], [PermissionType], [PermissionURL], [StyleClass], [IsDeleted]) VALUES (66, N'reservation-_CBySixtFormSubmit', N'Cancel By Sixt Form Submitted (Step 3) View', NULL, 63, 3, 2, N'/Reservation/_CBySixtFormSubmit', NULL, NULL)
GO
INSERT [dbo].[Permission] ([ID], [Name], [DisplayName], [Description], [ParentID], [Order], [PermissionType], [PermissionURL], [StyleClass], [IsDeleted]) VALUES (67, N'uploads-_UploadsList', N'Upload List', NULL, 11, 1, 2, N'/uploads/_UploadsList', NULL, NULL)
GO
----------------------------------------------------------

----- No Show Permission--------------------
GO
INSERT [dbo].[Permission] ([ID], [Name], [DisplayName], [Description], [ParentID], [Order], [PermissionType], [PermissionURL], [StyleClass], [IsDeleted]) VALUES (68, N'NoShow', N'No Show', NULL, 12, 3, 1, N'/Reservation/NoShowIndex', NULL, NULL)
GO
INSERT [dbo].[Permission] ([ID], [Name], [DisplayName], [Description], [ParentID], [Order], [PermissionType], [PermissionURL], [StyleClass], [IsDeleted]) VALUES (69, N'reservation-_noshowlist', N'No Show List', NULL, 68, 1, 2, N'/Reservation/_NoShowList', NULL, NULL)
GO
INSERT [dbo].[Permission] ([ID], [Name], [DisplayName], [Description], [ParentID], [Order], [PermissionType], [PermissionURL], [StyleClass], [IsDeleted]) VALUES (70, N'reservation-noshowdetails', N'No Show Details', NULL, 69, 1, 2, N'/Reservation/noshowdetails', NULL, NULL)
GO
INSERT [dbo].[Permission] ([ID], [Name], [DisplayName], [Description], [ParentID], [Order], [PermissionType], [PermissionURL], [StyleClass], [IsDeleted]) VALUES (71, N'reservation-noshowassign_multiple', N'No Show Assign Agent', NULL, 69, 2, 2, N'/Reservation/noshowAssign_Multiple', NULL, NULL)
GO
INSERT [dbo].[Permission] ([ID], [Name], [DisplayName], [Description], [ParentID], [Order], [PermissionType], [PermissionURL], [StyleClass], [IsDeleted]) VALUES (72, N'reservation-noshowformsubmitted', N'No Show Form Submit', NULL, 69, 3, 2, N'/Reservation/noshowFormSubmitted', NULL, NULL)
GO
INSERT [dbo].[Permission] ([ID], [Name], [DisplayName], [Description], [ParentID], [Order], [PermissionType], [PermissionURL], [StyleClass], [IsDeleted]) VALUES (73, N'reservation-noshowexportreservation', N'No Show Export', NULL, 69, 4, 2, N'/Reservation/noshowExportReservation', NULL, NULL)
GO
INSERT [dbo].[Permission] ([ID], [Name], [DisplayName], [Description], [ParentID], [Order], [PermissionType], [PermissionURL], [StyleClass], [IsDeleted]) VALUES (74, N'reservation-noshowsteps', N'No Show Steps Index View', NULL, 68, 2, 2, N'/Reservation/noshowSteps', NULL, NULL)
GO
INSERT [dbo].[Permission] ([ID], [Name], [DisplayName], [Description], [ParentID], [Order], [PermissionType], [PermissionURL], [StyleClass], [IsDeleted]) VALUES (75, N'reservation-_noshownotify', N'No Show Notification (Step 1) View', NULL, 74, 1, 2, N'/Resercation/_noshowNotify', NULL, NULL)
GO
INSERT [dbo].[Permission] ([ID], [Name], [DisplayName], [Description], [ParentID], [Order], [PermissionType], [PermissionURL], [StyleClass], [IsDeleted]) VALUES (76, N'reservation-_noshowAssignment', N'No Show Assignment (Step 2) View', NULL, 74, 2, 2, N'/Reservation/_noshowAssignment', NULL, NULL)
GO
INSERT [dbo].[Permission] ([ID], [Name], [DisplayName], [Description], [ParentID], [Order], [PermissionType], [PermissionURL], [StyleClass], [IsDeleted]) VALUES (77, N'reservation-_noshowFormsubmit', N'No Show Form Submitted (Step 3) View', NULL, 74, 3, 2, N'/Reservation/_noshowFormSubmit', NULL, NULL)
-------------End-------------------------------------
update permission set DisplayName='Cancel By Sixt Form Submitted' where ID=61
update permission set [Name] ='reservation-noshowassignagent_multiple',PermissionURL='/Reservation/NoShowAssignAgent_Multiple' where ID=71
----------------------------------------------------

----------------------adding Permissions for open 09/06/2020-----------
Delete from permission where Id>77
INSERT [dbo].[Permission] ([ID], [Name], [DisplayName], [Description], [ParentID], [Order], [PermissionType], [PermissionURL], [StyleClass], [IsDeleted]) VALUES (78, N'Open', N'Open', NULL, 12, 4, 1, N'/Reservation/OpenIndex', NULL, NULL)
GO
INSERT [dbo].[Permission] ([ID], [Name], [DisplayName], [Description], [ParentID], [Order], [PermissionType], [PermissionURL], [StyleClass], [IsDeleted]) VALUES (79, N'reservation-_openList', N'Open List', NULL, 78, 1, 2, N'/Reservation/_OpenList', NULL, NULL)
GO
INSERT [dbo].[Permission] ([ID], [Name], [DisplayName], [Description], [ParentID], [Order], [PermissionType], [PermissionURL], [StyleClass], [IsDeleted]) VALUES (80, N'reservation-OpenDetails', N'Open Details', NULL, 79, 1, 2, N'/Reservation/OpenDetails', NULL, NULL)
GO
INSERT [dbo].[Permission] ([ID], [Name], [DisplayName], [Description], [ParentID], [Order], [PermissionType], [PermissionURL], [StyleClass], [IsDeleted]) VALUES (81, N'reservation-OpenAssignAnyUser_Multiple', N'Open Assign (Any user)', NULL, 79, 2, 2, N'/Reservation/OpenAssignAnyUser_Multiple', NULL, NULL)
GO
INSERT [dbo].[Permission] ([ID], [Name], [DisplayName], [Description], [ParentID], [Order], [PermissionType], [PermissionURL], [StyleClass], [IsDeleted]) VALUES (81, N'reservation-OpenAssignAgent_Multiple', N'Open Assign (BA)', NULL, 105, 3, 2, N'/Reservation/OpenAssignAgent_Multiple', NULL, NULL)

INSERT [dbo].[Permission] ([ID], [Name], [DisplayName], [Description], [ParentID], [Order], [PermissionType], [PermissionURL], [StyleClass], [IsDeleted]) VALUES (82, N'reservation-OpenFormSubmitted_Deny', N'Open Form Submit Deny (BA)', NULL, 79, 4, 2, N'/Reservation/OpenFormSubmitted_Deny', NULL, NULL)
GO
INSERT [dbo].[Permission] ([ID], [Name], [DisplayName], [Description], [ParentID], [Order], [PermissionType], [PermissionURL], [StyleClass], [IsDeleted]) VALUES (83, N'reservation-OpenFormSubmitted_Confirm', N'Open Form submit confirm (BA)', NULL, 79, 5, 2, N'/Reservation/OpenFormSubmitted_Confirm', NULL, NULL)
GO
INSERT [dbo].[Permission] ([ID], [Name], [DisplayName], [Description], [ParentID], [Order], [PermissionType], [PermissionURL], [StyleClass], [IsDeleted]) VALUES (84, N'reservation-OpenAssignContactCenter_Multiple', N'Open Assign (CCM)', NULL, 79, 6, 2, N'/Reservation/OpenAssignContactCenter_Multiple', NULL, NULL)
GO
INSERT [dbo].[Permission] ([ID], [Name], [DisplayName], [Description], [ParentID], [Order], [PermissionType], [PermissionURL], [StyleClass], [IsDeleted]) VALUES (85, N'reservation-OpenFormSubmitted_NoAnswer', N'Open Form Submit No Answer (CCM)', NULL, 79, 7, 2, N'/Reservation/OpenFormSubmitted_NoAnswer', NULL, NULL)
GO
INSERT [dbo].[Permission] ([ID], [Name], [DisplayName], [Description], [ParentID], [Order], [PermissionType], [PermissionURL], [StyleClass], [IsDeleted]) VALUES (86, N'reservation-OpenFormSubmitted_Cancel', N'Open Form Submit Cancel (CCM)', NULL, 79, 8, 2, N'/Reservation/OpenFormSubmitted_Cancel', NULL, NULL)
GO
INSERT [dbo].[Permission] ([ID], [Name], [DisplayName], [Description], [ParentID], [Order], [PermissionType], [PermissionURL], [StyleClass], [IsDeleted]) VALUES (87, N'reservation-OpenFormSubmitted_ConfirmedConfirm', N'Open Form Submit Confirm (CCM)', NULL, 79, 9, 2, N'/Reservation/OpenFormSubmitted_ConfirmedConfirm', NULL, NULL)
GO
INSERT [dbo].[Permission] ([ID], [Name], [DisplayName], [Description], [ParentID], [Order], [PermissionType], [PermissionURL], [StyleClass], [IsDeleted]) VALUES (88, N'reservation-OpenFormSubmitted_CustomerForm', N'Open Fill Form Submit ', NULL, 79, 10, 2, N'/Reservation/OpenFormSubmitted_CustomerForm', NULL, NULL)
GO
INSERT [dbo].[Permission] ([ID], [Name], [DisplayName], [Description], [ParentID], [Order], [PermissionType], [PermissionURL], [StyleClass], [IsDeleted]) VALUES (89, N'reservation-OpenAssignBranchAgent_Multiple', N'Open Assign (BM)', NULL, 79, 11, 2, N'/Reservation/OpenAssignBranchAgent_Multiple', NULL, NULL)
GO
INSERT [dbo].[Permission] ([ID], [Name], [DisplayName], [Description], [ParentID], [Order], [PermissionType], [PermissionURL], [StyleClass], [IsDeleted]) VALUES (90, N'reservation-OpenBranchAgentFormSubmitted', N'Open Form Submit Cancel (BM)', NULL, 79, 12, 2, N'/Reservation/OpenBranchAgentFormSubmitted', NULL, NULL)
GO
INSERT [dbo].[Permission] ([ID], [Name], [DisplayName], [Description], [ParentID], [Order], [PermissionType], [PermissionURL], [StyleClass], [IsDeleted]) VALUES (91, N'reservation-OpenBranchAgentFormSubmitted_Confirm', N'Open Form Submit Confirm (BM)', NULL, 79, 13, 2, N'/Reservation/OpenBranchAgentFormSubmitted_Confirm', NULL, NULL)
GO
INSERT [dbo].[Permission] ([ID], [Name], [DisplayName], [Description], [ParentID], [Order], [PermissionType], [PermissionURL], [StyleClass], [IsDeleted]) VALUES (92, N'reservation-OpenExportReservation', N'Open Export', NULL, 79, 14, 2, N'/Reservation/OpenExportReservation', NULL, NULL)
GO
INSERT [dbo].[Permission] ([ID], [Name], [DisplayName], [Description], [ParentID], [Order], [PermissionType], [PermissionURL], [StyleClass], [IsDeleted]) VALUES (93, N'reservation-OpenSteps', N'Open Steps Index', NULL, 78, 2, 2, N'/Reservation/OpenSteps', NULL, NULL)
GO
INSERT [dbo].[Permission] ([ID], [Name], [DisplayName], [Description], [ParentID], [Order], [PermissionType], [PermissionURL], [StyleClass], [IsDeleted]) VALUES (94, N'reservation-_OpenNotify', N'Open Notification (Step 1) View', NULL, 93, 1, 2, N'/Reservation/_OpenNotify', NULL, NULL)
GO
INSERT [dbo].[Permission] ([ID], [Name], [DisplayName], [Description], [ParentID], [Order], [PermissionType], [PermissionURL], [StyleClass], [IsDeleted]) VALUES (95, N'reservation-_OpenAssignment', N'Open Assignment (BA) (Step2) View ', NULL, 93, 2, 2, N'/Reservation/_OpenAssignment', NULL, NULL)
GO
INSERT [dbo].[Permission] ([ID], [Name], [DisplayName], [Description], [ParentID], [Order], [PermissionType], [PermissionURL], [StyleClass], [IsDeleted]) VALUES (96, N'reservation-_OpenFormSubmit', N'Open Form Submit (BA) (Step3) View ', NULL, 93, 3, 2, N'/Reservation/_OpenFormSubmit', NULL, NULL)
GO
INSERT [dbo].[Permission] ([ID], [Name], [DisplayName], [Description], [ParentID], [Order], [PermissionType], [PermissionURL], [StyleClass], [IsDeleted]) VALUES (97, N'reservation-_OpenCAgentManagementAssignment', N'Open Assignmet (CCM) (Step 4) View ', NULL, 93, 4, 2, N'/Reservation/_OpenCAgentManagementAssignment', NULL, NULL)
GO
INSERT [dbo].[Permission] ([ID], [Name], [DisplayName], [Description], [ParentID], [Order], [PermissionType], [PermissionURL], [StyleClass], [IsDeleted]) VALUES (98, N'reservation-_OpenContactCenterFormSubmit', N'Open Form Submit (CCM) (Step 5) View', NULL, 93, 5, 2, N'/Reservation/_OpenContactCenterFormSubmit', NULL, NULL)
GO
INSERT [dbo].[Permission] ([ID], [Name], [DisplayName], [Description], [ParentID], [Order], [PermissionType], [PermissionURL], [StyleClass], [IsDeleted]) VALUES (99, N'reservation-_OpenFillCustomerForm', N'Open Fill Customer Form (Step 6)', NULL, 93, 6, 2, N'/Reservation/_OpenFillCustomerForm', NULL, NULL)
GO
INSERT [dbo].[Permission] ([ID], [Name], [DisplayName], [Description], [ParentID], [Order], [PermissionType], [PermissionURL], [StyleClass], [IsDeleted]) VALUES (100, N'reservation-_OpenConfirmedSendEmailDone', N'Open Send Mail Done (Step7)', NULL, 93, 7, 2, N'/Reservation/_OpenConfirmedSendEmailDone', NULL, NULL)
GO
INSERT [dbo].[Permission] ([ID], [Name], [DisplayName], [Description], [ParentID], [Order], [PermissionType], [PermissionURL], [StyleClass], [IsDeleted]) VALUES (101, N'reservation-_OpenBranchAssignment', N'Open Assignment (BM) (Step 8)', NULL, 93, 8, 2, N'/Reservation/_OpenBranchAssignment', NULL, NULL)
GO
INSERT [dbo].[Permission] ([ID], [Name], [DisplayName], [Description], [ParentID], [Order], [PermissionType], [PermissionURL], [StyleClass], [IsDeleted]) VALUES (102, N'reservation-_OpenBranchAgentFormSubmit', N'Open Form Submit (BM) (Step 9)', NULL, 93, 9, 2, N'/Reservation/_OpenBranchAgentFormSubmit', NULL, NULL)
GO
INSERT [dbo].[Permission] ([ID], [Name], [DisplayName], [Description], [ParentID], [Order], [PermissionType], [PermissionURL], [StyleClass], [IsDeleted]) VALUES (103, N'reservation-ChangeStatus', N'Change Status', NULL, 12, 5, 1, N'/Reservation/ChangeStatus', NULL, NULL)
GO
INSERT [dbo].[Permission] ([ID], [Name], [DisplayName], [Description], [ParentID], [Order], [PermissionType], [PermissionURL], [StyleClass], [IsDeleted]) VALUES (104, N'reservation-_ChangeStatusList', N'Change Status List View', NULL, 103, 1, 2, N'/Reservation/_ChangeStatusList', NULL, NULL)
GO
INSERT [dbo].[Permission] ([ID], [Name], [DisplayName], [Description], [ParentID], [Order], [PermissionType], [PermissionURL], [StyleClass], [IsDeleted]) VALUES (105, N'reservation-ChangeReservationStatus', N'Change Status Submit', NULL, 104, 1, 2, N'/Reservation/ChangeReservationStatus', NULL, NULL)
GO
----------------------------End-----------------------------------------------
update permission set Name='uploads-UploadsIndex' where ID=11
GO
INSERT [dbo].[Permission] ([ID], [Name], [DisplayName], [Description], [ParentID], [Order], [PermissionType], [PermissionURL], [StyleClass], [IsDeleted]) VALUES (106, N'uploads-_UploadsList', N'Upload List', NULL, 11, 1, 2, N'/uploads/uploadsList', NULL, NULL)
GO

  update permission set [Name]='reservation-OpenBranchAgentFormSubmitted_Cancelled' , PermissionURL='/Reservation/OpenBranchAgentFormSubmitted_Cancelled' where ID=90
  update permission set [Order]=15 where ID=92
  INSERT [dbo].[Permission] ([ID], [Name], [DisplayName], [Description], [ParentID], [Order], [PermissionType], [PermissionURL], [StyleClass], [IsDeleted]) VALUES (108, N'reservation-OpenBranchAgentFormSubmitted_NoResponse', N'Open Form Submit No Response (BM)', NULL, 79, 14, 2, N'/Reservation/OpenBranchAgentFormSubmitted_NoResponse', NULL, NULL)

---------------------------------------------
--------------------------------------- Add permission for email setting -------------------
GO
INSERT [dbo].[Permission] ([ID], [Name], [DisplayName], [Description], [ParentID], [Order], [PermissionType], [PermissionURL], [StyleClass], [IsDeleted]) VALUES (109, N'EmailSetting-EmailSettingIndex', N'Email Setting Index', NULL, 1, 8, 1, N'/EmailSetting/EmailSettingIndex', NULL, NULL)
GO
INSERT [dbo].[Permission] ([ID], [Name], [DisplayName], [Description], [ParentID], [Order], [PermissionType], [PermissionURL], [StyleClass], [IsDeleted]) VALUES (110, N'EmailSetting-_EmailSettingList', N'Email Setting List', NULL, 109, 1, 2, N'/EmailSetting/_EmailSettingList', NULL, NULL)
GO
INSERT [dbo].[Permission] ([ID], [Name], [DisplayName], [Description], [ParentID], [Order], [PermissionType], [PermissionURL], [StyleClass], [IsDeleted]) VALUES (111, N'EmailSetting-CreateEmailSetting', N'Add Email Setting', NULL, 109, 2, 2, N'/EmailSetting/CreateEmailSetting', NULL, NULL)
GO
INSERT [dbo].[Permission] ([ID], [Name], [DisplayName], [Description], [ParentID], [Order], [PermissionType], [PermissionURL], [StyleClass], [IsDeleted]) VALUES (112, N'EmailSetting-UpdateEmailSetting', N'Update Email Setting', NULL, 109, 3, 2, N'/EmailSetting/UpdateEmailSetting', NULL, NULL)
GO
---------------------------------------------------------------------------------