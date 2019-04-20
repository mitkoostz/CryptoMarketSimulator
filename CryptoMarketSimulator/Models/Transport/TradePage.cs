using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CryptoMarketSimulator.Models
{
    public class TradePage
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal PriceBTC { get; set; }
        public string PriceBtcString { get; set; }
        public decimal UserBalanceBTC { get; set; }
        public decimal UserBalanceUSD{ get; set; }
        public Dictionary<int,string> AllCoins { get; set; }
        public ApplicationUser User { get; set; }
        public virtual List<LimitOrder> UserOrders { get; set; }
        public decimal CurrencyBalance { get; set; }
        public int ImageNumber { get; set; }
    }
}