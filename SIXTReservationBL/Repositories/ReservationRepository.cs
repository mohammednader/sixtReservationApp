using Microsoft.EntityFrameworkCore;
using SIXTReservationBL.CoreBL.IRepositories;
using SIXTReservationBL.Models.Domain;
using SIXTReservationBL.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace SIXTReservationBL.Repositories
{
    public class ReservationRepository : GenericRepository<Reservation>, IReservationRepository
    {
        public ReservationRepository(SixtReservationContext context) : base(context)
        {

        }

        public List<Reservation> GetReservationWithDetails(Expression<Func<Reservation, bool>> predicate)
        {
            try
            {
                var query = Context.Reservation.Include(f => f.Customer)
                                                         .Include(f => f.ReservationStatus)
                                                         .AsQueryable();

                if (predicate != null)
                {
                    query = query.Where(predicate);
                }

                return query.ToList();
            }
            catch (Exception e)
            {
                return null;
            }


        }
        public List<Reservation> GetReservationLog(Expression<Func<Reservation, bool>> predicate)
        {
            try
            {
                var query = Context.Reservation.Include(f => f.Customer)
                                                         .Include(f => f.ReservationStatus)
                                                         .AsQueryable();



                if (predicate != null)
                {
                    query = query.Where(predicate);
                }

                return query.ToList();
            }
            catch (Exception e)
            {
                return null;
            }


        }
        public void CopyObject(ref Reservation targetReservation, Reservation sourceReservation)
        {
            targetReservation.AgencyCountry = sourceReservation.AgencyCountry;
            targetReservation.AgencyParentName = sourceReservation.AgencyParentName;
            targetReservation.AgencySubsidiaryName = sourceReservation.AgencySubsidiaryName;
            targetReservation.BookingDate = sourceReservation.BookingDate;
            targetReservation.BusinessSegmentId = sourceReservation.BusinessSegmentId;
            targetReservation.BusinessSegmentText = sourceReservation.BusinessSegmentText;
            targetReservation.CancelledAfterBookingInDays = sourceReservation.CancelledAfterBookingInDays;
            targetReservation.CancelledBeforeBickUpInDays = sourceReservation.CancelledBeforeBickUpInDays;
            targetReservation.CancelledDate = sourceReservation.CancelledDate;
            targetReservation.Cdnumber = sourceReservation.Cdnumber;
            targetReservation.ConvertedToRental = sourceReservation.ConvertedToRental;
            targetReservation.CustomerCardIndicator = sourceReservation.CustomerCardIndicator;
            targetReservation.CustomerId = sourceReservation.CustomerId;
            targetReservation.DriverCountry = sourceReservation.DriverCountry;
            targetReservation.DriverName = sourceReservation.DriverName;
            targetReservation.DropOffBranchId = sourceReservation.DropOffBranchId;
            targetReservation.DropOffDate = sourceReservation.DropOffDate;
            targetReservation.DropOffHour = sourceReservation.DropOffHour;
            targetReservation.DropOffweekDay = sourceReservation.DropOffweekDay;
            targetReservation.LeadTimeInDays = sourceReservation.LeadTimeInDays;
            targetReservation.NoShowDate = sourceReservation.NoShowDate;
            targetReservation.NumberOfReservations = sourceReservation.NumberOfReservations;
            targetReservation.OneWayReservation = sourceReservation.OneWayReservation;
            targetReservation.OnRequest = sourceReservation.OnRequest;
            targetReservation.PickUpBranchId = sourceReservation.PickUpBranchId;
            targetReservation.PickUpDate = sourceReservation.PickUpDate;
            targetReservation.PickUpHour = sourceReservation.PickUpHour;
            targetReservation.PickUpweekDay = sourceReservation.PickUpweekDay;
            targetReservation.Prepaid = sourceReservation.Prepaid;
            targetReservation.RateSegmentCategory = sourceReservation.RateSegmentCategory;
            targetReservation.RateSegmentSubCategory = sourceReservation.RateSegmentSubCategory;
            targetReservation.RentalAgreementNumber = sourceReservation.RentalAgreementNumber;
            targetReservation.RentalDays = sourceReservation.RentalDays;
            targetReservation.RequestLevel = sourceReservation.RequestLevel;
            targetReservation.RequestLevelId = sourceReservation.RequestLevelId;
            targetReservation.ReservationAgent = sourceReservation.ReservationAgent;
            targetReservation.ReservationHour = sourceReservation.ReservationHour;
            targetReservation.ReservationNum = sourceReservation.ReservationNum;
            targetReservation.ReservationPointOfSale = sourceReservation.ReservationPointOfSale;
            targetReservation.ReservationSourceChannel1 = sourceReservation.ReservationSourceChannel1;
            targetReservation.ReservationSourceChannel2 = sourceReservation.ReservationSourceChannel2;
            targetReservation.ReservationSourceChannel3 = sourceReservation.ReservationSourceChannel3;
            targetReservation.ReservationStatusId = sourceReservation.ReservationStatusId;
            targetReservation.RevenueEur = sourceReservation.RevenueEur;
            targetReservation.RevenuePerDayEur = sourceReservation.RevenuePerDayEur;
            targetReservation.UploadId = sourceReservation.UploadId;
            targetReservation.VehicleAcriss = sourceReservation.VehicleAcriss;


        }

        public List<Reservation> GetReservationWithDetails(ReservationSC reservationSC)
        {

            try
            {
                var query = Context.Reservation.Include(f => f.Customer)
                                                         .Include(f => f.ReservationStatus)
                                                         .Include(f=>f)
                                                         .AsQueryable();

                if (reservationSC != null)
                {
                    if (reservationSC.ReservationNum.HasValue && reservationSC.ReservationNum.Value > 0)
                    {
                        query = query.Where(r => r.ReservationNum == reservationSC.ReservationNum);
                    }

                    if (reservationSC.ReservationStatusId.HasValue && reservationSC.ReservationStatusId.Value > 0)
                    {
                        query = query.Where(r => r.ReservationStatusId == reservationSC.ReservationStatusId);
                    }
                    if (reservationSC.BookingDateFrom.HasValue && reservationSC.BookingDateFrom.Value != DateTime.MinValue)
                    {
                        query = query.Where(r => r.BookingDate >= reservationSC.BookingDateFrom);
                    }
                    if (reservationSC.BookingDateTo.HasValue && reservationSC.BookingDateTo.Value != DateTime.MinValue)
                    {
                        query = query.Where(r => r.BookingDate <= reservationSC.BookingDateTo);
                    }
                    if (reservationSC.PickUpDateFrom.HasValue && reservationSC.PickUpDateFrom.Value != DateTime.MinValue)
                    {
                        query = query.Where(r => r.PickUpDate >= reservationSC.PickUpDateFrom);
                    }
                    if (reservationSC.PickUpDateTo.HasValue && reservationSC.PickUpDateTo.Value != DateTime.MinValue)
                    {
                        query = query.Where(r => r.PickUpDate <= reservationSC.PickUpDateTo);
                    }

                    if (reservationSC.CancelDateFrom.HasValue && reservationSC.CancelDateFrom.Value != DateTime.MinValue)
                    {
                        query = query.Where(r => r.CancelledDate >= reservationSC.CancelDateFrom);
                    }
                    if (reservationSC.CancelDateTo.HasValue && reservationSC.CancelDateTo.Value != DateTime.MinValue)
                    {
                        query = query.Where(r => r.CancelledDate <= reservationSC.CancelDateTo);
                    }

                    if (reservationSC.DropOffDate.HasValue && reservationSC.DropOffDate.Value != DateTime.MinValue)
                    {
                        query = query.Where(r => r.DropOffDate <= reservationSC.DropOffDate);
                    }


                    if (!string.IsNullOrWhiteSpace(reservationSC.CustomerName))
                    {
                        query = query.Where(r => r.Customer.Name.Contains(reservationSC.CustomerName));
                    }
                    if (!string.IsNullOrWhiteSpace(reservationSC.VehicleAcriss))
                    {
                        query = query.Where(r => r.VehicleAcriss == reservationSC.VehicleAcriss);
                    }
                    if (reservationSC.CancelledBeforePickUpInDays.HasValue && reservationSC.CancelledBeforePickUpInDays.Value > 0)
                    {
                        query = query.Where(r => r.CancelledBeforeBickUpInDays == reservationSC.CancelledBeforePickUpInDays);
                    }

                    if (reservationSC.PickUpBranchIds?.Length > 0 && !reservationSC.PickUpBranchIds.Contains(null))
                    {
                        query = query.Where(r => reservationSC.PickUpBranchIds.Contains(r.PickUpBranchId.GetValueOrDefault()));
                    }


                }

                return query.ToList();
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public Reservation GetReservationDetails(Expression<Func<Reservation, bool>> predicate)
        {
            try
            {
                var query = Context.Reservation.Include(f => f.Customer)
                                               .Include(f => f.ReservationStatus)
                                               .Include(f => f.DropOffBranch)
                                               .AsQueryable();

                if (predicate != null)
                {
                    query = query.Where(predicate);
                }

                return query.FirstOrDefault();
            }
            catch (Exception e)
            {
                return null;
            }


        }


    }
}
