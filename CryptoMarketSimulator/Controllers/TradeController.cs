using CryptoMarketSimulator.Models;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
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
                tradeData.PriceBTC = (tradeData.Price / CoinValues.GetBtcValue().Price);
                tradeData.PriceBtcString = string.Format("{0:F8}", tradeData.PriceBTC).Replace(",",".") ;
                tradeData.ImageNumber = CoinValues.GetCurrencyName().Where(c => c.Value == currency).First().Key;
                tradeData.AllCoins = CoinValues.GetCurrencyName();
            

           
            
            using (var db = new SiteDbContext())
            {
                var user = db.Users.Find(User.Identity.GetUserId());
                tradeData.UserBalanceBTC = user.Balance;
                tradeData.UserBalanceUSD = user.Balance*CoinValues.GetBtcValue().Price;
                user.Wallet = user.Wallet;
                user.LimitOrders = user.LimitOrders.OrderByDescending(order => order.OrderDate).ToList();
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
            decimal bought = decimal.Parse(amount, CultureInfo.InvariantCulture);
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
        public ActionResult BuyLimitOrder(string currencyname, string buyamount , string buyatprice)
        {
            var userId = User.Identity.GetUserId();
            var db = new SiteDbContext();
            var user = db.Users.Find(userId);

            decimal BuyAmount = decimal.Parse(buyamount, CultureInfo.InvariantCulture);
            decimal BuyAtPrice = decimal.Parse(buyatprice, CultureInfo.InvariantCulture);

            if(BuyAmount == 0 || BuyAtPrice == 0 || BuyAtPrice < 0 || BuyAmount < 0)
            {
                return View("Index");
            }

            var TotalOrderBtcCost = BuyAmount * BuyAtPrice;

            var Coins = CoinValues.GetValues().ToList();
            if (Coins.Where(c => c.Name.Contains(currencyname)).Count() == 0)
            {
                return View("TradePage");

            }
            if (TotalOrderBtcCost > user.Balance)
            {
                return View("TradePage");
            }

            LimitOrder order = new LimitOrder()
            {
                User = user,
                Amount = BuyAmount,
                Currency = currencyname,
                AtPrice = BuyAtPrice,
                OrderType = "Buy",
                OrderDate = DateTime.Now
                
            };

            db.LimitOrders.Add(order);
            user.Balance -= TotalOrderBtcCost;

            db.SaveChanges();

            var Data = Newtonsoft.Json.JsonConvert.SerializeObject(new
            {
                UserBtc = user.Balance,
                UserUsd = user.Balance * CoinValues.GetBtcValue().Price
                 
            });

            return Json(Data, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpPost]
        public ActionResult SellLimitOrder(string currencyname, string sellamount, string sellatprice)
        {
            var userId = User.Identity.GetUserId();
            var db = new SiteDbContext();
            var user = db.Users.Find(userId);

            decimal SellAmount = decimal.Parse(sellamount, CultureInfo.InvariantCulture);
            decimal BuyAtPrice = decimal.Parse(sellatprice, CultureInfo.InvariantCulture);

            var TotalOrderBtcCost = SellAmount * BuyAtPrice;
            

            var Coins = CoinValues.GetValues().ToList();
            if (Coins.Where(c => c.Name.Contains(currencyname)).Count() == 0)
            {
                return View("TradePage");

            }
            if (user.Wallet[currencyname.Replace(" ",string.Empty)] < SellAmount)
            {
                return View("TradePage");
            }

            LimitOrder order = new LimitOrder()
            {
                User = user,
                Amount = SellAmount,
                Currency = currencyname,
                OrderType = "Sell",
                AtPrice = BuyAtPrice,
                OrderDate = DateTime.Now

            };

            db.LimitOrders.Add(order);
            user.Wallet[currencyname.Replace(" ", string.Empty)] -= SellAmount;

            db.SaveChanges();

            var Data = Newtonsoft.Json.JsonConvert.SerializeObject(new
            {
                UserCurrency = user.Wallet[currencyname.Replace(" ", string.Empty)]
        });

            return Json(Data, JsonRequestBehavior.AllowGet);
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
            decimal sold = decimal.Parse(amount, CultureInfo.InvariantCulture);
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
        public ActionResult DeleteOrder(string order)
        {
            var db = new SiteDbContext();
            var userId = User.Identity.GetUserId();
            var user = db.Users.Find(userId);


            var OrderToDelete = user.LimitOrders.Where(ord => ord.Order == order).First();
            if(OrderToDelete == null)
            {
                return View("Index","Home");
            }

            if(OrderToDelete.OrderType == "Buy")
            {
                var TotalOrderBtcCost = OrderToDelete.Amount * OrderToDelete.AtPrice;

                user.Balance += TotalOrderBtcCost;
            }
            if(OrderToDelete.OrderType == "Sell")
            {
                user.Wallet[OrderToDelete.Currency.Replace(" ", string.Empty)] += OrderToDelete.Amount;
            }


            db.LimitOrders.Remove(OrderToDelete);
            db.SaveChanges();

            var Data = Newtonsoft.Json.JsonConvert.SerializeObject(new
            {
                

            });
    

            return Json(Data, JsonRequestBehavior.AllowGet);
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

        [Authorize]
        [HttpPost]
        public ActionResult LimitOrderValidate(string currencyname)
        {
            var db = new SiteDbContext();
            var user = db.Users.Find(User.Identity.GetUserId());

            var UserBtcBalance = user.Balance;
            var UserCurrencyBalance = user.Wallet[currencyname.Replace(" ", string.Empty)];

            var Data = Newtonsoft.Json.JsonConvert.SerializeObject(new
            {
                UserBtcBalance = UserBtcBalance,
                UserCurrencyBalance = UserCurrencyBalance,
             

            });
            return Json(Data, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpPost]
        public ActionResult RefreshCurrency(string currencyname)
        {
            var coins = CoinValues.GetValues().ToList();
                Coin coin = coins
                .Where(v => v.Name.Contains(currencyname))
                .First();

                Coin Bitcoin = coins
                .Where(v => v.Name == "Bitcoin".Replace(" ", string.Empty))
                .First();

            decimal CurrencyUsdPrice = coin.Price;
            decimal BitcoinPrice = Bitcoin.Price;

            var db = new SiteDbContext();
            var user = db.Users.Find(User.Identity.GetUserId());

            var UserUsdBalance = user.Balance * BitcoinPrice;
            var UserCurrencyBalance = user.Wallet[currencyname.Replace(" ", string.Empty)];

            var Data = Newtonsoft.Json.JsonConvert.SerializeObject(new
            {
                CurrencyUsdPrice = CurrencyUsdPrice,
                BitcoinPrice = BitcoinPrice,
                UserBtcBalance = user.Balance,
                UserUsdBalance = UserUsdBalance,
                UserCurrencyBalance = UserCurrencyBalance,
                UserOrders = user.LimitOrders.ToList()

            }, new JsonSerializerSettings
            {
                PreserveReferencesHandling = PreserveReferencesHandling.Objects

            });
            return Json(Data, JsonRequestBehavior.AllowGet);
        }


        [Authorize]
        [HttpPost]
        public ActionResult GetUserOrders()
        {
       

            var db = new SiteDbContext();
            var user = db.Users.Find(User.Identity.GetUserId());

            var UserOrders = user.LimitOrders.ToList();
            foreach (var us in UserOrders)
            {
                us.User = null;
            }

            var Data = Newtonsoft.Json.JsonConvert.SerializeObject(new
            {              
                UserOrders = UserOrders

            }, new JsonSerializerSettings
            {
                PreserveReferencesHandling = PreserveReferencesHandling.Objects

            });
            return Json(Data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetCurrencyCharts(string currencyname)
        {


            var db = new SiteDbContext();
            var CurrencyData = db.CurrenciesCharts.Where(c => c.CurrencyName == currencyname)
                .ToList();


            var Data = Newtonsoft.Json.JsonConvert.SerializeObject(CurrencyData, new JsonSerializerSettings
            {
                PreserveReferencesHandling = PreserveReferencesHandling.Objects

            });
            return Json(Data, JsonRequestBehavior.AllowGet);
        }
    }


}