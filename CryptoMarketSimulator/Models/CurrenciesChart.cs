using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CryptoMarketSimulator.Models
{
    public class CurrenciesChart
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string CurrencyName { get; set; }
        public decimal CurrencyUsdValue { get; set; }
        public decimal CurrencyBtcValue { get; set; }

    }
}