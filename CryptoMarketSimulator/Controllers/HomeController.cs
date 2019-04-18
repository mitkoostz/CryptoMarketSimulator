using CryptoMarketSimulator.Models;
using CryptoMarketSimulator.Models.Transport;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;

namespace CryptoMarketSimulator.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
        

            if (!Request.IsAuthenticated)
            {
                HomeAuthenticated model = new HomeAuthenticated();

                var btc = CoinValues.GetStatistic().Where(c => c.Name.Contains("Bitcoin")).First();
                model.Bitcoin = btc;

                return View(model);
            }
            else
            {
                SiteDbContext db = new SiteDbContext();
                var user = db.Users.Find(User.Identity.GetUserId());
                user.Transactions = user.Transactions;
                user.Wallet = user.Wallet;


                HomeAuthenticated model = new HomeAuthenticated();
                model.UserData = db.Users.Find(User.Identity.GetUserId());
                model.Bitcoin = CoinValues.GetStatistic().Where(c => c.Name.Contains("Bitcoin")).First();

                var TotalBalanceUSD = ProfileController.CalculateTotalBalance(user);

                model.TotalBalance = TotalBalanceUSD;

                return View(model);
            }
           
        }


        public ActionResult Analytics()
        {
            var getstatistic = CoinValues.GetStatistic();
            return View(getstatistic);
        }


        public ActionResult Statistics(string currency)
        {
            var Currencies = CoinValues.GetStatistic().Where(cur => cur.Name == currency);

            var BitcoinPrice = CoinValues.GetValues().Where(c => c.Name == "Bitcoin").First().Price;
            if (currency == null || Currencies.Count() == 0 || Currencies.Count() > 1)
            {
                return View("Index");
            }

            Coin Currency = Currencies.First();
            var db = new SiteDbContext();
            List<LimitOrder> Orders = db.LimitOrders.Where(c => c.Currency == Currency.Name).ToList();
            List<Transaction> Transactions = db.Transactions.Where(c => c.Currency == Currency.Name).ToList();

           
            Statistics statistics = new Statistics();
            statistics.Volume = Transactions.Sum(tr => tr.TotalUSD);
            statistics.TotalTransactions = Transactions.Count;
            statistics.Currency = Currency;
            statistics.OpenOrders = Orders.Count;
            statistics.Orders = Orders;
            statistics.Transactions = Transactions.OrderByDescending(tr => tr.Date).ToList();
            statistics.BitcoinPrice = BitcoinPrice;
          

            return View(statistics);
        }

        [HttpPost]
        public ActionResult GetStatistics(string currencyname)
        {

        
            var db = new SiteDbContext();
            
            var OpenOrders = db.LimitOrders.Where(cur => cur.Currency == currencyname).ToList();
           
            var SellOrders = OpenOrders.Where(or => or.OrderType == "Sell").ToList();
            var BuyOrders = OpenOrders.Where(or => or.OrderType == "Buy").ToList();

        

            if (OpenOrders.Count <= 0)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }


            var Data = Newtonsoft.Json.JsonConvert.SerializeObject(new
            {
                SellOrders = SellOrders.Count,
                BuyOrders = BuyOrders.Count
            }
            , new JsonSerializerSettings
            {
                PreserveReferencesHandling = PreserveReferencesHandling.Objects

            });
            return Json(Data, JsonRequestBehavior.AllowGet);
        }

        //[WebMethod]
        //public JsonResult CurrentPrices()
        //{

        //    using (var db = new SiteDbContext())
        //    {
        //        var result = db.CryptoStatistics.Where(s => s.Currency == "TRON").Take(4).OrderByDescending(s => s.Date).ToList();
        //        return Json(result, JsonRequestBehavior.AllowGet);

        //    }
        //}

    }
}