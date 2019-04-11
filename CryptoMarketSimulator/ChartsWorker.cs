using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using CryptoMarketSimulator.Models;
using System.Threading;
using System.Threading.Tasks;

namespace BombaySapphireCds.Jobs
{
    public class ChartsWorker : IDisposable
    {
        private CancellationTokenSource m_cancel;
        private Task m_task;
        private TimeSpan m_interval;
        private bool m_running;

        public ChartsWorker(TimeSpan interval)
        {
            m_interval = interval;
            m_running = true;
            m_cancel = new CancellationTokenSource();
            m_task = Task.Run(() => TaskLoop(), m_cancel.Token);
        }

        private void TaskLoop()
        {
            while (m_running)
            {
                var Coins = CoinValues.GetValues();
                var Bitcoin = Coins.Where(name => name.Name.Contains("Bitcoin")).First();
                var db = new SiteDbContext();
                foreach (var coin in Coins)
                {
                    CurrenciesChart chart = new CurrenciesChart()
                    {
                        CurrencyName = coin.Name,
                        Date = DateTime.Now,
                        CurrencyUsdValue = coin.Price,
                        CurrencyBtcValue = coin.Price / Bitcoin.Price

                    };
                    db.CurrenciesCharts.Add(chart);
                }
                db.SaveChanges();

                Thread.Sleep(m_interval);
            }
        }

        public void Dispose()
        {
            m_running = false;

            if (m_cancel != null)
            {
                try
                {
                    m_cancel.Cancel();
                    m_cancel.Dispose();
                }
                catch
                {
                }
                finally
                {
                    m_cancel = null;
                }
            }
        }
    }
}