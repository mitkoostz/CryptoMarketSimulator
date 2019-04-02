using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CryptoMarketSimulator.Models.Transport
{
    public class HomeAuthenticated
    {
        public ApplicationUser UserData { get; set; }
        public Coin Bitcoin { get; set; }
        public decimal TotalBalance { get; set; }
    }
}