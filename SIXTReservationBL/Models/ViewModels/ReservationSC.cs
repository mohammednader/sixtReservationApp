using System;
using System.Collections.Generic;
using System.Text;

namespace SIXTReservationBL.Models.ViewModels
{
    public class ReservationSC
    {
        public int? ReservationId { get; set; }
        public long? ReservationNum { get; set; }
        public int? ReservationStatusId { get; set; }
        public DateTime? BookingDateFrom { get; set; }
        public DateTime? BookingDateTo { get; set; }

        public DateTime? PickUpDateFrom { get; set; }
        public DateTime? PickUpDateTo { get; set; }
        public DateTime? CancelDateFrom { get; set; }
        public DateTime? CancelDateTo { get; set; }
        public DateTime? DropOffDate { get; set; }
        public int?[] PickUpBranchIds { get; set; }

        public string VehicleAcriss { get; set; }
        public DateTime? CancelledDate { get; set; }
        public int? CancelledBeforePickUpInDays { get; set; }
        public int? CancelledAfterBookingDays { get; set; }
        public string CustomerName { get; set; }

        public int? ActionStepId { get; set; }
        public bool  AssignedToMe { get; set; }
        public int AssignedToId { get; set; }
        public int? RentalDays { get; set; }
    }
}
