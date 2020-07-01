using SIXTReservationBL.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIXTReservationApp.Models
{
    public class VReservationListVM
    {
        public int? Id { get; set; }
        public long ReservationNum { get; set; }
        public int? NumberOfReservations { get; set; }
        public string BookingDate { get; set; }
        public int? ReservationHour { get; set; }
        public string PickUpDate { get; set; }
        public int? PickUpHour { get; set; }
        public string PickUpweekDay { get; set; }
        public decimal? RevenueEur { get; set; }
        public decimal? RevenuePerDayEur { get; set; }
        public int? RentalDays { get; set; }
        public int? LeadTimeInDays { get; set; }
        public int? CancelledAfterBookingInDays { get; set; }
        public int? CancelledBeforeBickUpInDays { get; set; }
        public int? PickUpBranchId { get; set; }
        public string DropOffDate { get; set; }
        public int? DropOffHour { get; set; }
        public string DropOffweekDay { get; set; }
        public int? DropOffBranchId { get; set; }
        public string VehicleAcriss { get; set; }
        public string ReservationSourceChannel1 { get; set; }
        public string ReservationSourceChannel2 { get; set; }
        public string ReservationSourceChannel3 { get; set; }
        public int? BusinessSegmentId { get; set; }
        public int? RateSegmentCategory { get; set; }
        public string RateSegmentSubCategory { get; set; }
        public string ReservationPointOfSale { get; set; }
        public bool? OneWayReservation { get; set; }
        public bool? OnRequest { get; set; }
        public string RequestLevel { get; set; }
        public int? RequestLevelId { get; set; }
        public string ReservationAgent { get; set; }
        public string BranchName { get; set; }
        public int? ReservationStatusId { get; set; }
        public string CancelledDate { get; set; }
        public long? RentalAgreementNumber { get; set; }
        public bool? Prepaid { get; set; }
        public bool? ConvertedToRental { get; set; }
        public string NoShowDate { get; set; }
        public long? Cdnumber { get; set; }
        public int? CustomerId { get; set; }
        public string DriverName { get; set; }
        public string DriverCountry { get; set; }
        public string CustomerCardIndicator { get; set; }
        public string AgencyCountry { get; set; }
        public string AgencySubsidiaryName { get; set; }
        public string AgencyParentName { get; set; }
        public int? UploadId { get; set; }
        public string BusinessSegmentText { get; set; }
        public string CreationDate { get; set; }
        public int? LogId { get; set; }
        public string PickUpBranchName { get; set; }
        public string CancelledBeforePickUpInDays { get; }
        public string ReservationStatus { get; set; }
        public int? StepId { get; set; }
        public bool? IsDone { get; set; }
        public int? LogReservationStatus { get; set; }
        public string LogCreationDate { get; set; }
        public int? ReservationId { get; set; }
        public int? LogStatusId { get; set; }
        public string CompleteDate { get; set; }
        public int? AssignmentId { get; set; }
        public int? AssignFromUser { get; set; }
        public int? AssignToUser { get; set; }
        public string AssignCreationDate { get; set; }
        public long? AssignReservationNo { get; set; }
        public int? AssignLogId { get; set; }
        public int? DiffDateAssignSubmit { get; set; }
        public string StepName { get; set; }
        public string AssignedToName { get; set; }
        public int? AssignedToJobtitle { get; set; }
        public string CustomerName { get; set; }
        public string NextStep { get; set; }

        public string RateSegmentCategoryName { get; set; }
        public string RateSegmentName { get; set; }

        public VReservationListVM(VReservationListModel reservation)
        {
            if (reservation != null)
            {
                var ReservationData = reservation.ReservationList ;
                Id = ReservationData?.Id;
                NumberOfReservations = ReservationData?.NumberOfReservations;
                ReservationNum = reservation.ReservationNo;
                AssignedToName = reservation.IsAssigned ? reservation.AssignmentModel?.AssignToName : string.Empty;
                NextStep = reservation.NextStep;
                ReservationHour = ReservationData?.ReservationHour??0;
                ReservationPointOfSale = ReservationData?.ReservationPointOfSale;
                PickUpweekDay = ReservationData?.PickUpweekDay.ToString();
                PickUpHour = ReservationData?.PickUpHour;
                RevenueEur = ReservationData?.RevenueEur;
                RevenuePerDayEur = ReservationData?.RevenuePerDayEur;
                LeadTimeInDays = ReservationData?.LeadTimeInDays;
                CancelledAfterBookingInDays = ReservationData?.CancelledAfterBookingInDays;
                CancelledBeforeBickUpInDays = ReservationData?.CancelledBeforeBickUpInDays;
                DropOffHour = ReservationData?.DropOffHour;
                DropOffweekDay = ReservationData?.DropOffweekDay;
                ReservationSourceChannel1 = ReservationData?.ReservationSourceChannel1.ToString();
                ReservationSourceChannel2 = ReservationData?.ReservationSourceChannel2.ToString();
                ReservationSourceChannel3 = ReservationData?.ReservationSourceChannel3.ToString();
                BusinessSegmentText = ReservationData?.BusinessSegmentText.ToString();
                RateSegmentSubCategory = ReservationData?.RateSegmentSubCategory??string.Empty;
                OneWayReservation = ReservationData?.OneWayReservation;
                OnRequest = ReservationData?.OnRequest;
                RequestLevel = ReservationData?.RequestLevel.ToString();
                ReservationAgent = ReservationData?.ReservationAgent??string.Empty;
                CancelledDate = ReservationData?.CancelledDate?.ToLongDateString();
                RentalAgreementNumber = ReservationData?.RentalAgreementNumber;
                Prepaid = ReservationData?.Prepaid;
                ConvertedToRental = ReservationData?.ConvertedToRental;
                NoShowDate = ReservationData?.NoShowDate?.ToShortDateString();
                Cdnumber = ReservationData?.Cdnumber;
                 DriverName = ReservationData?.DriverName??string.Empty;
                DriverCountry = ReservationData?.DriverCountry.ToString();
                CustomerCardIndicator = ReservationData?.CustomerCardIndicator.ToString();
                AgencyCountry = ReservationData?.AgencyCountry ?? string.Empty;
                AgencySubsidiaryName = ReservationData?.AgencySubsidiaryName ?? string.Empty;
                AgencyParentName = ReservationData?.AgencyParentName?? string.Empty;

                PickUpBranchName = ReservationData?.PickUpBranchName;
                ReservationStatus = ReservationData?.ReservationStatus;
                AssignCreationDate = ReservationData?.AssignCreationDate?.ToLongDateString();
                StepName = ReservationData?.StepName;
                RateSegmentCategoryName = ReservationData?.RateSegmentCategoryName;
                RateSegmentName = ReservationData?.RateSegmentName;


                ////// date from grouping 
                BookingDate = ReservationData?.BookingDate?.ToShortDateString();
                PickUpDate = ReservationData?.PickUpDate?.ToShortDateString();
                PickUpBranchName = ReservationData?.PickUpBranchName;
                CancelledBeforePickUpInDays = ReservationData?.CancelledBeforeBickUpInDays?.ToString();
                DropOffDate = ReservationData?.DropOffDate?.ToShortDateString();

                CustomerName = ReservationData?.CustomerName?.ToString();
                RentalDays = ReservationData?.RentalDays;

                DriverName = ReservationData?.DriverName;
                VehicleAcriss = ReservationData?.VehicleAcriss;
                CreationDate = ReservationData?.CreationDate?.ToShortDateString();
            }
        }
    }
}
