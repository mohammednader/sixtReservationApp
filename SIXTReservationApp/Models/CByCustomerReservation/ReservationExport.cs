using SIXTReservationBL.Models.Domain;
using SIXTReservationBL.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIXTReservationApp.Models.CByCustomerReservation
{
    public class ReservationExport
    {
        public long ReservationNum { get; set; }
        public string ReservationStatus { get; set; }
        public string BookingDate { get; set; }
        public string PickUpDate { get; set; }
        public string PickUpBranchName { get; set; }
        public string VehicleAcriss { get; set; }
        public string CancelledDate { get; set; }
        public int? RentalDays { get; set; }
        public string CancelledBeforePickUpInDays { get; set; }
        public string CancelledAfterBookingInDays { get; set; }

        public string DropOffDate { get; set; }

        public string NoShowDate { get; set; }
        public int? NumberOfReservation { get; }
        public int? ReservationHour { get; }
        public int? PickUpHour { get; }
        public string PickUpWeekDay { get; }
        public string CustomerCardIndicator { get; }
        public string AgencyParentName { get; private set; }
        public string AgencySubsidiaryName { get; }
        public string AgencyCountry { get; }
        public string BusinessSegmentText { get; }
       

        public string DriverCountry { get; }
        public int? LeadTimeInDays { get; }
        public int? DropOffHour { get; }
        public string DropOffWeekDay { get; }
        public string ReservationSourceChannel1 { get; }
        public string ReservationSourceChannel2 { get; }
        public string ReservationSourceChannel3 { get; }
        public string RateSegmentCategoryName { get; }
        public string RateSegmentName { get; }
        public int? RateSegmentCategory { get; }
        public string RateSegmentSubCategory { get; }
        public bool? OneWayReservation { get; }
        public bool? OnRequest { get; }
        public string RequestLevel { get; }

        public string ReservationAgent { get; }
        public long? RentalAgreementNumber { get; }
        public bool? ConvertedToRental { get; }
        public long? CDNumber { get; }
        public String CustomerName { get; set; }
        public string DriverName { get; set; }
        public DateTime? UploadCreationDate { get; set; }

        public string CreationDate { get; set; }
        public string AssignedToName { get; set; }
        public string NextStep { get; set; }
        public decimal? RevenueEUR { get; }
        public decimal? RevenuePerDayEUR { get; }
        public string AssignFromUser { get; }
        public string AssignToUser { get; }
        public string AssignCreationDate { get; }
        public string FormSubmitedBy { get; }
        public string FormSubmissionDate { get; }
        public string AnswerStatus { get; }
        public string FormSubmitReason { get; }
        public string FormSubmitComment { get; }

        public ReservationExport(VReservationListModel reservation)
        {
            if (reservation != null)
            {
                var ReservationData = reservation.ReservationList ;
                ReservationNum = reservation.ReservationNo;
                AssignedToName = reservation.IsAssigned ? reservation.AssignmentModel?.AssignToName : string.Empty;
                NextStep = reservation.NextStep;
                AssignFromUser = reservation.IsAssigned ? reservation.AssignmentModel?.AssignFromName : string.Empty;
                AssignToUser = reservation.IsAssigned ? reservation.AssignmentModel?.AssignToName : string.Empty;
                AssignCreationDate = reservation.IsAssigned ? reservation.AssignmentModel?.AssignmentDate : string.Empty;
                //
                FormSubmitedBy = reservation.IsFormSubmitted ? reservation.FormSumbitModel?.FormSubmitedBy : string.Empty;
                FormSubmissionDate = reservation.IsFormSubmitted ? reservation.FormSumbitModel?.FormSubmitionDate : string.Empty;
                AnswerStatus = reservation.IsFormSubmitted ? reservation.FormSumbitModel?.ReasonStatus : string.Empty;
                FormSubmitReason = reservation.IsFormSubmitted ? reservation.FormSumbitModel?.ReasonText : string.Empty;
                FormSubmitComment = reservation.IsFormSubmitted ? reservation.FormSumbitModel?.FormSubmitedComment : string.Empty;
                // 

                BookingDate = ReservationData?.BookingDate?.ToShortDateString();
                PickUpDate = ReservationData?.PickUpDate?.ToShortDateString();
                CancelledBeforePickUpInDays = ReservationData?.CancelledBeforeBickUpInDays?.ToString();
                CancelledAfterBookingInDays= ReservationData?.CancelledAfterBookingInDays?.ToString();
                DropOffDate = ReservationData?.DropOffDate?.ToShortDateString();
                CancelledDate = ReservationData?.CancelledDate?.ToShortDateString();
                ReservationStatus = ReservationData?.ReservationStatus;
                NoShowDate = ReservationData?.NoShowDate?.ToShortDateString();
                PickUpBranchName = ReservationData?.PickUpBranchName?.ToString();
                VehicleAcriss = ReservationData?.VehicleAcriss;
                CreationDate = ReservationData?.CreationDate?.ToShortDateString();

                RentalDays = ReservationData?.RentalDays;
                NumberOfReservation = ReservationData?.NumberOfReservations;
                ReservationHour = ReservationData?.ReservationHour;
                PickUpHour = ReservationData?.PickUpHour;
                PickUpWeekDay = ReservationData?.PickUpweekDay;
                RevenueEUR = ReservationData?.RevenueEur;
                RevenuePerDayEUR = ReservationData?.RevenuePerDayEur;
                LeadTimeInDays = ReservationData?.LeadTimeInDays;
                DropOffHour = ReservationData?.DropOffHour;
                DropOffWeekDay = ReservationData?.DropOffweekDay;
                ReservationSourceChannel1 = ReservationData?.ReservationSourceChannel1;
                ReservationSourceChannel2 = ReservationData?.ReservationSourceChannel2;
                ReservationSourceChannel3 = ReservationData?.ReservationSourceChannel3;
                OneWayReservation = ReservationData?.OneWayReservation;
                OnRequest = ReservationData?.OnRequest;
                RequestLevel = ReservationData?.RequestLevel;
                ReservationAgent = ReservationData?.ReservationAgent;
                RentalAgreementNumber = ReservationData?.RentalAgreementNumber;
                ConvertedToRental = ReservationData?.ConvertedToRental;
                CDNumber = ReservationData?.Cdnumber;
                CustomerName = ReservationData?.CustomerName?.ToString();
                DriverName = ReservationData?.DriverName?.ToString();
                DriverCountry = ReservationData?.DriverCountry?.ToString();
                CustomerCardIndicator = ReservationData?.CustomerCardIndicator;
                AgencyParentName = ReservationData?.AgencyParentName?.ToString();
                AgencySubsidiaryName = ReservationData?.AgencySubsidiaryName?.ToString();
                AgencyCountry = ReservationData?.AgencyCountry?.ToString();
                BusinessSegmentText = ReservationData?.BusinessSegmentText?.ToString();
                UploadCreationDate = ReservationData?.LogCreationDate;
                RateSegmentCategoryName = ReservationData?.RateSegmentCategoryName;
                RateSegmentName = ReservationData?.RateSegmentName;
            }

        }


    }
}
