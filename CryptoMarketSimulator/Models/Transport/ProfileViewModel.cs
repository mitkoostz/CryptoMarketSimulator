using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CryptoMarketSimulator.Models.Transport
{
    public class ProfileViewModel
    {
        public ApplicationUser User { get; set; }
        public decimal TotalBalanceUSD { get; set; }
    }
}