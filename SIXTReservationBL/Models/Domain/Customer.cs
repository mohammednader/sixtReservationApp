using System;
using System.Collections.Generic;

namespace SIXTReservationBL.Models.Domain
{
    public partial class Customer
    {
        public Customer()
        {
            Reservation = new HashSet<Reservation>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }
        public int? StepLogId { get; set; }
        public long? ReservationNo { get; set; }
        public string Comment { get; set; }

        public virtual ICollection<Reservation> Reservation { get; set; }
    }
}
