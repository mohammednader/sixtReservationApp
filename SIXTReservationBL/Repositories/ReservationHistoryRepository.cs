using SIXTReservationBL.CoreBL.IRepositories;
using SIXTReservationBL.Models.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIXTReservationBL.Repositories
{
    public class ReservationHistoryRepository : GenericRepository<ReservationHistory>, IReservationHistoryRepository
    {
        public ReservationHistoryRepository(SixtReservationContext context) : base(context)
        {

        }

        public ReservationHistory GetHistoryObject(Reservation reservation, int loggedUserId)
        {
            DateTime now = DateTime.Now;
            ReservationHistory history = new ReservationHistory()
            {
                AgencyCountry = reservation.AgencyCountry,
                AgencyParentName = reservation.AgencyParentName,
                AgencySubsidiaryName = reservation.AgencySubsidiaryName,
                BookingDate = reservation.BookingDate,
                BusinessSegmentId = reservation.BusinessSegmentId,
                BusinessSegmentText = reservation.BusinessSegmentText,
                CancelledAfterBookingInDays = reservation.CancelledAfterBookingInDays,
                CancelledBeforeBickUpInDays = reservation.CancelledBeforeBickUpInDays,
                CancelledDate = reservation.CancelledDate,
                Cdnumber = reservation.Cdnumber,
                ConvertedToRental = reservation.ConvertedToRental,
                CustomerCardIndicator = reservation.CustomerCardIndicator,
                CustomerId = reservation.CustomerId,
                DriverCountry = reservation.DriverCountry,
                DriverName = reservation.DriverName,
                DropOffBranchId = reservation.DropOffBranchId,
                DropOffDate = reservation.DropOffDate,
                DropOffHour = reservation.DropOffHour,
                DropOffweekDay = reservation.DropOffweekDay,

                LeadTimeInDays = reservation.LeadTimeInDays,
                NoShowDate = reservation.NoShowDate,
                NumberOfReservations = reservation.NumberOfReservations,
                OneWayReservation = reservation.OneWayReservation,
                OnRequest = reservation.OnRequest,
                PickUpBranchId = reservation.PickUpBranchId,
                PickUpDate = reservation.PickUpDate,
                PickUpHour = reservation.PickUpHour,
                PickUpweekDay = reservation.PickUpweekDay,
                Prepaid = reservation.Prepaid,
                RateSegmentCategory = reservation.RateSegmentCategory,
                RateSegmentSubCategory = reservation.RateSegmentSubCategory,
                RentalAgreementNumber = reservation.RentalAgreementNumber,
                RentalDays = reservation.RentalDays,
                RequestLevel = reservation.RequestLevel,
                RequestLevelId = reservation.RequestLevelId,
                ReservationAgent = reservation.ReservationAgent,
                ReservationHour = reservation.ReservationHour,
                ReservationId = reservation.Id,
                ReservationNum = reservation.ReservationNum,
                ReservationPointOfSale = reservation.ReservationPointOfSale,
                ReservationSourceChannel1 = reservation.ReservationSourceChannel1,
                ReservationSourceChannel2 = reservation.ReservationSourceChannel2,
                ReservationSourceChannel3 = reservation.ReservationSourceChannel3,
                ReservationStatusId = reservation.ReservationStatusId,
                RevenueEur = reservation.RevenueEur,
                RevenuePerDayEur = reservation.RevenuePerDayEur,
                UploadId = reservation.UploadId,
                VehicleAcriss = reservation.VehicleAcriss,
                //
                CreatedByUserId = loggedUserId,
                CreatedDate = now,
                DateFrom = reservation.CreationDate,
                //DateTo = now,
                //IsCurrent = true,

            };
            return history;

        }
    }
}
