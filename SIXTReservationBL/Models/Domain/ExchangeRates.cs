using System;
using System.Collections.Generic;

namespace SIXTReservationBL.Models.Domain
{
    public partial class ExchangeRates
    {
        public int Id { get; set; }
        public string TargetCurrency { get; set; }
        public DateTime? ToDate { get; set; }
        public decimal? Rate { get; set; }
        public string BaseCurrency { get; set; }
        public DateTime? FromDate { get; set; }
    }
}
