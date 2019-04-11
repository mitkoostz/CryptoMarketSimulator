using CryptoMarketSimulator.Models;
using CryptoMarketSimulator.Models.Transport;
using Microsoft.AspNet.Identity;
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