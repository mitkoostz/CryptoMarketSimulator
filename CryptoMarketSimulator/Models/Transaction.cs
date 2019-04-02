using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CryptoMarketSimulator.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public ApplicationUser User { get; set; }
        public string Type { get; set; }
        public string Currency { get; set; }
        public decimal Amount { get; set; }
        public decimal CurrencyPrice { get; set; }
        public decimal TotalUSD { get; set; }
        public DateTime Date { get; set; }

    }
}