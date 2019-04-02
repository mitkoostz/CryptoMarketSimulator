using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoCurrencyStatistic
{
    public class CryptoStatistic
    {
        public int Id { get; set; }
        public string Currency { get; set; }
        public decimal Price { get; set; }
        public DateTime Date { get; set; }

    }
}
