using CryptoMarketSimulator.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace CryptoMarketSimulator.Controllers
{
    public class TradeController : Controller
    {
        [Authorize]
        public ActionResult Index(string currency)
        {
            if(currency == null)
            {
            //    var Coins = CoinValues.GetValues().Skip(1).Take(49).ToList();

            //    for (int i = 2; i < 51; i++)
            //    {
            //        var image = Coins[i-2].CoinImage;
            //       // var number = i + 1;

            //        using (WebClient client = new WebClient())
            //        {
            //            client.DownloadFile(new Uri(image), Server.MapPath(Path.Combine("~/Resources/CoinImages/", +i + ".png")));


            //        }
            //    }



                return View();
            }

           

            var CurrencyName = CoinValues.GetCurrencyName().Where(c => c.Value == currency).First().Value;
            var tradeData = new TradePage();

                tradeData.Name = CurrencyName;
                tradeData.Price = CoinValues.GetValues().Where(c => c.Name == CurrencyName).First().Price;
                tradeData.ImageNumber = CoinValues.GetCurrencyName().Where(c => c.Value == currency).First().Key;
                tradeData.AllCoins = CoinValues.GetCurrencyName();
            

           
            
            using (var db = new SiteDbContext())
            {
                var user = db.Users.Find(User.Identity.GetUserId());
                tradeData.UserBalanceBTC = user.Balance;
                tradeData.UserBalanceUSD = user.Balance*CoinValues.GetBtcValue().Price;
                user.Wallet = user.Wallet;
                tradeData.User = user;
                tradeData.CurrencyBalance = user.Wallet[currency.Replace(" ",string.Empty)];
            }

            return View("TradePage",tradeData);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Buy(string amount, string name)
        {
            decimal d;
            if (!decimal.TryParse(amount,out d))
            {
                return View("Index");
            }

            var userId = User.Identity.GetUserId();
            decimal bought = decimal.Parse(amount);
            decimal currencyPrice = CoinValues.GetValues().Where(v => v.Name == name).First().Price;
            decimal bitcoin = CoinValues.GetBtcValue().Price;

            decimal boughtPrice = bought * currencyPrice;
            decimal btcPrice = boughtPrice / bitcoin;


            using (var db = new SiteDbContext())
            {
                var user = db.Users.Find(userId);
                var userBTC = user.Balance;
                if (userBTC < btcPrice || btcPrice < 0)
                {
                    return View("Index");
                }

                user.Wallet[name.Replace(" ", string.Empty)] += bought;
                user.Balance -= btcPrice;

                //add Transaction
                var transaction = new Transaction()
                {
                    Amount = bought,
                    Currency = name,
                    Date = DateTime.Now,
                    User = user,
                    Type = "Bought",
                    CurrencyPrice = currencyPrice,
                    TotalUSD = bought * currencyPrice


                };
                db.Transactions.Add(transaction);

                db.SaveChanges();




                //Request.UrlReferrer.PathAndQuery

                //string Data = "{\"name\":\"Joe\"}";
                var Data = Newtonsoft.Json.JsonConvert.SerializeObject(new
                {
                    CurrencyCurrentPrice = currencyPrice,
                    CurrencyBalance = user.Wallet[name.Replace(" ", string.Empty)],
                    UserBtcBalance = user.Balance,
                    UserUsdBalance = user.Balance * CoinValues.GetBtcValue().Price

                });

                return Json(Data, JsonRequestBehavior.AllowGet);

             }
        }

        [Authorize]
        [HttpPost]
        public ActionResult Sell(string amount, string name)
        {
            decimal d;
            if (!decimal.TryParse(amount, out d))
            {
                return View("Index");
            }

            var userId = User.Identity.GetUserId();
            decimal sold = decimal.Parse(amount);
            decimal currencyPrice = CoinValues.GetValues().Where(v => v.Name == name).First().Price;
            decimal bitcoin = CoinValues.GetBtcValue().Price;

            decimal boughtPrice = sold * currencyPrice;
            decimal btcPrice = boughtPrice / bitcoin;


            using (var db = new SiteDbContext())
            {
                var user = db.Users.Find(userId);
                var userBTC = user.Balance;
                if (sold > user.Wallet[name.Replace(" ", string.Empty)] || sold < 0)
                {
                    return View("Index");
                }

                user.Wallet[name.Replace(" ", string.Empty)] -= sold;
                user.Balance += btcPrice;

                //add Transaction
                var transaction = new Transaction()
                {
                    Amount = sold,
                    Currency = name,
                    Date = DateTime.Now,
                    User = user,
                    Type = "Sold",
                    CurrencyPrice = currencyPrice,
                    TotalUSD = sold * currencyPrice

                };
                db.Transactions.Add(transaction);

                db.SaveChanges();

                var Data = Newtonsoft.Json.JsonConvert.SerializeObject(new
                {
                    CurrencyCurrentPrice = currencyPrice,
                    CurrencyBalance = user.Wallet[name.Replace(" ", string.Empty)],
                    UserBtcBalance = user.Balance,
                    UserUsdBalance = user.Balance * CoinValues.GetBtcValue().Price

                });
                ViewBag.Result = "Successfully";

                return Json(Data, JsonRequestBehavior.AllowGet);

            }

        }


        [Authorize]
        [HttpPost]
        public ActionResult Validate(string currencyname)
        {
            var db = new SiteDbContext();
            var user = db.Users.Find(User.Identity.GetUserId());

            var UserBtcBalance = user.Balance;
            var UserCurrencyBalance = user.Wallet[currencyname.Replace(" ", string.Empty)];
            var CurrencyPrice = CoinValues.GetValues().Where(v => v.Name.Contains(currencyname)).First();
            var BitcoinPrice = CoinValues.GetBtcValue().Price;




            var Data = Newtonsoft.Json.JsonConvert.SerializeObject(new
            {
                UserBtcBalance = UserBtcBalance,
                UserCurrencyBalance = UserCurrencyBalance,
                CurrencyPrice = CurrencyPrice.Price,
                BitcoinPrice = BitcoinPrice

            });
            return Json(Data, JsonRequestBehavior.AllowGet);
        }

        }


}