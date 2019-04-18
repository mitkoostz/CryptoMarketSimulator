using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CryptoMarketSimulator.Models.Transport
{
    public class Statistics
    {
        public Coin Currency { get; set; }
        public List<LimitOrder> Orders { get; set; }
        public List<Transaction> Transactions { get; set; }
        public decimal BitcoinPrice { get; set; }
        public decimal Volume { get; set; }
        public int TotalTransactions { get; set; }
        public int OpenOrders { get; set; }
    }
}