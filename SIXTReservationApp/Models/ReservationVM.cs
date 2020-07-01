using SIXTReservationBL.Models.Domain;
using SIXTReservationBL.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIXTReservationApp.Models
{
    public class ReservationVM
    {
        public int? Id { get; set; }
        public long ReservationNum { get; set; }
        public string ReservationStatus { get; set; }
        public string BookingDate { get; set; }
        public string PickUpDate { get; set; }
        public string PickUpBranchName { get; set; }
        public string VehicleAcriss { get; set; }
        public string CancelledDate { get; set; }
        public string CancelledBeforePickUpInDays { get; set; }
        public string CancelledAfterBookingInDays { get; set; }
        public string CustomerName { get; set; }
        public string DropOffDate { get; set; }
        public int? ReservationStatusId { get; set; }
        public string NoShowDate { get; set; }
        public string DriverName { get; set; }
        public int? CustomerId { get; set; }
        public int? UploadId { get; set; }
        public string CreationDate { get; set; }
        public string AssignedToName { get; set; }
        public string NextStep { get; set; }
        public bool NeedToAssign { get; set; }
        public bool NeedToAssignTCCAgent { get; }
        public bool NeedToAssignToBranchAgent { get; }
        public bool? IsCompleted { get; }


        public int? RentalDays { get; set; }

        public ReservationVM(Reservation reservation)
        {
            if (reservation != null)
            {
                Id = reservation.Id;
                ReservationNum = reservation.ReservationNum;
                BookingDate = reservation.BookingDate?.ToShortDateString();
                PickUpDate = reservation.PickUpDate?.ToShortDateString();
                PickUpBranchName = reservation.PickUpBranch?.Name;
                CancelledBeforePickUpInDays = reservation.CancelledBeforeBickUpInDays?.ToString();
                CancelledAfterBookingInDays = reservation?.CancelledAfterBookingInDays?.ToString();
                DropOffDate = reservation.DropOffDate?.ToShortDateString();
                CancelledDate = reservation.CancelledDate?.ToShortDateString();
                ReservationStatusId = reservation.ReservationStatusId;
                ReservationStatus = reservation.ReservationStatus.Status;
                NoShowDate = reservation.NoShowDate?.ToShortDateString();
                CustomerName = reservation.Customer?.Name;
                DriverName = reservation.DriverName;
                VehicleAcriss = reservation.VehicleAcriss;
                CustomerId = reservation.CustomerId;
                UploadId = reservation.UploadId;
                CreationDate = reservation.CreationDate?.ToShortDateString();
                RentalDays = reservation.RentalDays;

            }

        }
        public ReservationVM(VReservationListModel reservation)
        {
            if (reservation != null)
            {
                var ReservationData = reservation.ReservationList ;
                Id = ReservationData?.Id;
                ReservationNum = reservation.ReservationNo;
                AssignedToName = reservation.IsAssigned ? reservation.AssignmentModel?.AssignToName : string.Empty;
                NextStep = reservation.NextStep?? string.Empty;
                NeedToAssign = reservation.IsNeedToAssign;
                NeedToAssignTCCAgent = reservation.IsNeedToAssignToCCAgent;
                NeedToAssignToBranchAgent = reservation.IsNeedToAssignToBM;
                ////// date from grouping 
                BookingDate = ReservationData?.BookingDate?.ToShortDateString();
                PickUpDate = ReservationData?.PickUpDate?.ToShortDateString();
                PickUpBranchName = ReservationData?.PickUpBranchName ?? string.Empty;
                CancelledBeforePickUpInDays = ReservationData?.CancelledBeforeBickUpInDays?.ToString();
                DropOffDate = ReservationData?.DropOffDate?.ToShortDateString();
                CancelledDate = ReservationData?.CancelledDate?.ToShortDateString();
                ReservationStatusId = ReservationData?.ReservationStatusId;
                ReservationStatus = ReservationData?.ReservationStatus;
                NoShowDate = ReservationData?.NoShowDate?.ToShortDateString();
                CustomerName = ReservationData?.CustomerName?.ToString();
                CancelledAfterBookingInDays = ReservationData?.CancelledAfterBookingInDays.ToString();
                RentalDays = ReservationData?.RentalDays;

                // DriverName = ReservationData?.DriverName;
                VehicleAcriss = ReservationData?.VehicleAcriss;
                //  CustomerId = ReservationData?.CustomerId;
                // UploadId = ReservationData?.UploadId;
                CreationDate = ReservationData?.CreationDate?.ToShortDateString();
                IsCompleted = ReservationData?.IsCompleted;

            }

        }

    }
}
