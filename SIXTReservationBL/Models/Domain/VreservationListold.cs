using System;
using System.Collections.Generic;

namespace SIXTReservationBL.Models.Domain
{
    public partial class VreservationListold
    {
        public int Id { get; set; }
        public long ReservationNum { get; set; }
        public int? NumberOfReservations { get; set; }
        public DateTime? BookingDate { get; set; }
        public int? ReservationHour { get; set; }
        public DateTime? PickUpDate { get; set; }
        public int? PickUpHour { get; set; }
        public string PickUpweekDay { get; set; }
        public decimal? RevenueEur { get; set; }
        public decimal? RevenuePerDayEur { get; set; }
        public int? RentalDays { get; set; }
        public int? LeadTimeInDays { get; set; }
        public int? CancelledAfterBookingInDays { get; set; }
        public int? CancelledBeforeBickUpInDays { get; set; }
        public int? PickUpBranchId { get; set; }
        public DateTime? DropOffDate { get; set; }
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
        public int? ReservationStatusId { get; set; }
        public DateTime? CancelledDate { get; set; }
        public long? RentalAgreementNumber { get; set; }
        public bool? Prepaid { get; set; }
        public bool? ConvertedToRental { get; set; }
        public DateTime? NoShowDate { get; set; }
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
        public DateTime? CreationDate { get; set; }
        public int LogId { get; set; }
        public string PickUpBranchName { get; set; }
        public string ReservationStatus { get; set; }
        public int? StepId { get; set; }
        public bool? IsDone { get; set; }
        public int? LogReservationStatus { get; set; }
        public DateTime? LogCreationDate { get; set; }
        public int? ReservationId { get; set; }
        public int? LogStatusId { get; set; }
        public DateTime? CompleteDate { get; set; }
        public int? AssignmentId { get; set; }
        public int? AssignFromUser { get; set; }
        public int? AssignToUser { get; set; }
        public DateTime? AssignCreationDate { get; set; }
        public long? AssignReservationNo { get; set; }
        public int? AssignLogId { get; set; }
        public int? DiffDateAssignSubmit { get; set; }
        public string StepName { get; set; }
        public string AssignedToName { get; set; }
        public string AssignedFromName { get; set; }
        public int? AssignedToJobtitle { get; set; }
        public string FormSubmitCreatedBy { get; set; }
        public DateTime? FormSubmitCreatedDate { get; set; }
        public bool? FormSubmitReasonStatus { get; set; }
        public string FormSubmitReason { get; set; }
        public string FormSubmitComment { get; set; }
        public string CustomerName { get; set; }
        public string RateSegmentCategoryName { get; set; }
        public string RateSegmentName { get; set; }
    }
}
