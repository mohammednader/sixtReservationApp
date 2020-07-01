using System;
using System.Collections.Generic;

namespace SIXTReservationBL.Models.Domain
{
    public partial class ReservationHistory
    {
        public int Id { get; set; }
        public long ReservationId { get; set; }
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
        public DateTime? CreatedDate { get; set; }
        public bool? IsCurrent { get; set; }
        public int? CreatedByUserId { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
    }
}
