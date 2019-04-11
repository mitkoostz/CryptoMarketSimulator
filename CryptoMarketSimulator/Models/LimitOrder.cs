using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CryptoMarketSimulator.Models
{
    public class LimitOrder
    {
        public LimitOrder()
        {
            Order = Guid.NewGuid().ToString();
        }

        public int Id { get; set; }
        
        public string Order { get; set; }

        public virtual ApplicationUser User { get; set; }
        public string OrderType { get; set; }
        public string Currency { get; set; }
        public decimal Amount { get; set; }
        public decimal AtPrice { get; set; }
        public DateTime OrderDate { get; set; }

    }
}