using CryptoMarketSimulator.Models;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BombaySapphireCds.Jobs
{
    public class OrderWorker : IDisposable
    {
        private CancellationTokenSource m_cancel;
        private Task m_task;
        private TimeSpan m_interval;
        private bool m_running;

        public OrderWorker(TimeSpan interval)
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
                try
                {
                    var db = new SiteDbContext();
                    if (db.LimitOrders.Count() == 0)
                    {
                        return;
                    }
                    var Coins = CoinValues.GetValues();
                    var Orders = db.LimitOrders.OrderByDescending(order => order.OrderDate).ToList();

                    foreach (var order in Orders)
                    {
                        Coin coin = Coins.Where(c => c.Name.Contains(order.Currency)).First();
                        Coin Bitcoin = Coins.Where(c => c.Name.Contains("Bitcoin")).First();

                        decimal CoinBtcPrice = decimal.Round((coin.Price / Bitcoin.Price), 8);

                        if (CoinBtcPrice == decimal.Round(order.AtPrice, 8))
                        {
                            //Do ORDER
                            var User = db.Users.Find(order.User.Id);

                            if (order.OrderType == "Buy")
                            {
                                User.Wallet[order.Currency.Replace(" ", string.Empty)] += order.Amount;
                                db.LimitOrders.Remove(order);
                                Orders.Remove(order);

                                Transaction transaction = new Transaction()
                                {
                                    Type = "Bought",
                                    Currency = coin.Name,
                                    Amount = order.Amount,
                                    CurrencyPrice = coin.Price,
                                    TotalUSD = order.Amount * coin.Price,
                                    Date = DateTime.Now,
                                    User = User

                                };
                                db.Transactions.Add(transaction);
                                db.SaveChanges();
                            };

                            if (order.OrderType == "Sell")
                            {
                                User.Balance += (CoinBtcPrice * order.Amount);
                                db.LimitOrders.Remove(order);
                                Orders.Remove(order);

                                Transaction transaction = new Transaction()
                                {
                                    Type = "Sold",
                                    Currency = coin.Name,
                                    Amount = order.Amount,
                                    CurrencyPrice = coin.Price,
                                    TotalUSD = order.Amount * coin.Price,
                                    Date = DateTime.Now,
                                    User = User

                                };
                                db.Transactions.Add(transaction);
                                db.SaveChanges();
                            };


                        }


                    }



                }

                catch (Exception e)
                {

                }

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