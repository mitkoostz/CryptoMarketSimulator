using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CryptoMarketSimulator.Models.Transport
{
    public class TopCurrency
    {
        public string Currency { get; set; }
        public int Transactions { get; set; }
        public decimal DollarsSpend { get; set; }



    }
}