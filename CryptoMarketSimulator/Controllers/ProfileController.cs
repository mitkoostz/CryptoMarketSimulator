using CryptoMarketSimulator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.IO;
using System.Text.RegularExpressions;
using System.Web.Services;
using CryptoMarketSimulator.Models.Transport;

namespace CryptoMarketSimulator.Controllers
{
    public class ProfileController : Controller
    {
        public static decimal  CalculateTotalBalance(ApplicationUser User)
        {
            List<Coin> Coins = CoinValues.GetValues();

            Dictionary<int,string> CurrencyNames = CoinValues.GetCurrencyName();    
            decimal TotalBalanceUSD = 0.00m;

            var Bitcoin = Coins.Where(c => c.Name.Contains("Bitcoin")).First().Price;
             TotalBalanceUSD += Bitcoin * User.Balance;

            var Ethereum = Coins.Where(c => c.Name.Contains(CurrencyNames[2])).First().Price;
            TotalBalanceUSD += Ethereum * User.Wallet.Ethereum;
            var XRP = Coins.Where(c => c.Name.Contains(CurrencyNames[3])).First().Price;
            TotalBalanceUSD += XRP * User.Wallet.XRP;
            var EOS = Coins.Where(c => c.Name.Contains(CurrencyNames[4])).First().Price;
            TotalBalanceUSD += EOS * User.Wallet.EOS;
            var Litecoin = Coins.Where(c => c.Name.Contains(CurrencyNames[5])).First().Price;
            TotalBalanceUSD += Litecoin * User.Wallet.Litecoin;
            var BitcoinCash = Coins.Where(c => c.Name.Contains(CurrencyNames[6])).First().Price;
            TotalBalanceUSD += BitcoinCash * User.Wallet.BitcoinCash;
            var BinanceCoin = Coins.Where(c => c.Name.Contains(CurrencyNames[7])).First().Price;
            TotalBalanceUSD += BinanceCoin * User.Wallet.BinanceCoin;
            var Stellar = Coins.Where(c => c.Name.Contains(CurrencyNames[8])).First().Price;
            TotalBalanceUSD += Stellar * User.Wallet.Stellar;
            var Tether = Coins.Where(c => c.Name.Contains(CurrencyNames[9])).First().Price;
            TotalBalanceUSD += Tether * User.Wallet.Tether;
            var Cardano = Coins.Where(c => c.Name.Contains(CurrencyNames[10])).First().Price;
            TotalBalanceUSD += Cardano * User.Wallet.Cardano;
            var TRON = Coins.Where(c => c.Name.Contains(CurrencyNames[11])).First().Price;
            TotalBalanceUSD += TRON * User.Wallet.TRON;
            var BitcoinSV = Coins.Where(c => c.Name.Contains(CurrencyNames[12])).First().Price;
            TotalBalanceUSD += BitcoinSV * User.Wallet.BitcoinSV;
            var Dash = Coins.Where(c => c.Name.Contains(CurrencyNames[13])).First().Price;
            TotalBalanceUSD += Dash * User.Wallet.Dash;
            var Monero = Coins.Where(c => c.Name.Contains(CurrencyNames[14])).First().Price;
            TotalBalanceUSD += Monero * User.Wallet.Monero;
            var IOTA = Coins.Where(c => c.Name.Contains(CurrencyNames[15])).First().Price;
            TotalBalanceUSD += IOTA * User.Wallet.IOTA;
            var Maker = Coins.Where(c => c.Name.Contains(CurrencyNames[16])).First().Price;
            TotalBalanceUSD += Maker * User.Wallet.Maker;
            var Tezos = Coins.Where(c => c.Name.Contains(CurrencyNames[17])).First().Price;
            TotalBalanceUSD += Tezos * User.Wallet.Tezos;
            var Ontology = Coins.Where(c => c.Name.Contains(CurrencyNames[18])).First().Price;
            TotalBalanceUSD += Ontology * User.Wallet.Ontology;
            var NEO = Coins.Where(c => c.Name.Contains(CurrencyNames[19])).First().Price;
            TotalBalanceUSD += NEO * User.Wallet.NEO;
            var EthereumClassic = Coins.Where(c => c.Name.Contains(CurrencyNames[20])).First().Price;
            TotalBalanceUSD += EthereumClassic * User.Wallet.EthereumClassic;
            var NEM = Coins.Where(c => c.Name.Contains(CurrencyNames[21])).First().Price;
            TotalBalanceUSD += NEM * User.Wallet.NEM;
            var Zcash = Coins.Where(c => c.Name.Contains(CurrencyNames[22])).First().Price;
            TotalBalanceUSD += Zcash * User.Wallet.Zcash;
            var HuobiToken = Coins.Where(c => c.Name.Contains(CurrencyNames[23])).First().Price;
            TotalBalanceUSD += HuobiToken * User.Wallet.HuobiToken;
            var VeChain = Coins.Where(c => c.Name.Contains(CurrencyNames[24])).First().Price;
            TotalBalanceUSD += VeChain * User.Wallet.VeChain;
            var Waves = Coins.Where(c => c.Name.Contains(CurrencyNames[25])).First().Price;
            TotalBalanceUSD += Waves * User.Wallet.Waves;
            var Qtum = Coins.Where(c => c.Name.Contains(CurrencyNames[26])).First().Price;
            TotalBalanceUSD += Qtum * User.Wallet.Qtum;
            var OmiseGO = Coins.Where(c => c.Name.Contains(CurrencyNames[27])).First().Price;
            TotalBalanceUSD += OmiseGO * User.Wallet.OmiseGO;
            var Dogecoin = Coins.Where(c => c.Name.Contains(CurrencyNames[28])).First().Price;
            TotalBalanceUSD += Dogecoin * User.Wallet.Dogecoin;
            var USDCoin = Coins.Where(c => c.Name.Contains(CurrencyNames[29])).First().Price;
            TotalBalanceUSD += USDCoin * User.Wallet.USDCoin;
            var BitcoinGold = Coins.Where(c => c.Name.Contains(CurrencyNames[30])).First().Price;
            TotalBalanceUSD += BitcoinGold * User.Wallet.BitcoinGold;
            var MaximineCoin = Coins.Where(c => c.Name.Contains(CurrencyNames[31])).First().Price;
            TotalBalanceUSD += MaximineCoin * User.Wallet.MaximineCoin;
            var Lisk = Coins.Where(c => c.Name.Contains(CurrencyNames[32])).First().Price;
            TotalBalanceUSD += Lisk * User.Wallet.Lisk;
            var TrueUSD = Coins.Where(c => c.Name.Contains(CurrencyNames[33])).First().Price;
            TotalBalanceUSD += TrueUSD * User.Wallet.TrueUSD;
            var BitTorrent = Coins.Where(c => c.Name.Contains(CurrencyNames[34])).First().Price;
            TotalBalanceUSD += BitTorrent * User.Wallet.BitTorrent;
            var Ravencoin = Coins.Where(c => c.Name.Contains(CurrencyNames[35])).First().Price;
            TotalBalanceUSD += Ravencoin * User.Wallet.Ravencoin;
            var IOST = Coins.Where(c => c.Name.Contains(CurrencyNames[36])).First().Price;
            TotalBalanceUSD += IOST * User.Wallet.IOST;
            var Decred = Coins.Where(c => c.Name.Contains(CurrencyNames[37])).First().Price;
            TotalBalanceUSD += Decred * User.Wallet.Decred;
            var Chainlink = Coins.Where(c => c.Name.Contains(CurrencyNames[38])).First().Price;
            TotalBalanceUSD += Chainlink * User.Wallet.Chainlink;
            var Zilliqa = Coins.Where(c => c.Name.Contains(CurrencyNames[39])).First().Price;
            TotalBalanceUSD += Zilliqa * User.Wallet.Zilliqa;
            var Augur = Coins.Where(c => c.Name.Contains(CurrencyNames[40])).First().Price;
            TotalBalanceUSD += Augur * User.Wallet.Augur;
            var ICON = Coins.Where(c => c.Name.Contains(CurrencyNames[41])).First().Price;
            TotalBalanceUSD += ICON * User.Wallet.ICON;
            var BitShares = Coins.Where(c => c.Name.Contains(CurrencyNames[42])).First().Price;
            TotalBalanceUSD += BitShares * User.Wallet.BitShares;
            var Holo = Coins.Where(c => c.Name.Contains(CurrencyNames[43])).First().Price;
            TotalBalanceUSD += Holo * User.Wallet.Holo;
            var KuCoinShares = Coins.Where(c => c.Name.Contains(CurrencyNames[44])).First().Price;
            TotalBalanceUSD += KuCoinShares * User.Wallet.KuCoinShares;
            var DigiByte = Coins.Where(c => c.Name.Contains(CurrencyNames[45])).First().Price;
            TotalBalanceUSD += DigiByte * User.Wallet.DigiByte;
            var Nano = Coins.Where(c => c.Name.Contains(CurrencyNames[46])).First().Price;
            TotalBalanceUSD += Nano * User.Wallet.Nano;
            var Steem = Coins.Where(c => c.Name.Contains(CurrencyNames[47])).First().Price;
            TotalBalanceUSD += Steem * User.Wallet.Steem;
            var Bytecoin = Coins.Where(c => c.Name.Contains(CurrencyNames[48])).First().Price;
            TotalBalanceUSD += Bytecoin * User.Wallet.Bytecoin;
            var BitcoinDiamond = Coins.Where(c => c.Name.Contains(CurrencyNames[49])).First().Price;
            TotalBalanceUSD += BitcoinDiamond * User.Wallet.BitcoinDiamond;
            var Aeternity = Coins.Where(c => c.Name.Contains(CurrencyNames[50])).First().Price;
            TotalBalanceUSD += Aeternity * User.Wallet.Aeternity;

            return TotalBalanceUSD;





        }
        [Authorize]
        public ActionResult Index()
        {

            using (SiteDbContext db = new SiteDbContext())
            {
                var user = db.Users.Find(User.Identity.GetUserId());
                user.Wallet = user.Wallet;
                user.Transactions = user.Transactions.OrderByDescending(t => t.Date).ToList();

            

                ProfileViewModel ViewModel = new ProfileViewModel();

                ViewModel.User = user;
                ViewModel.TotalBalanceUSD = CalculateTotalBalance(user);
                

                return View(ViewModel);
            }


        }

        [Authorize]
        [HttpPost]
        public ActionResult UploadPic()
        {
            HttpPostedFileBase file = Request.Files["myFile"];

            int byteCount = file.ContentLength;
            if (file.ContentLength == 0)
            {
                return Redirect(Request.UrlReferrer.PathAndQuery);
            }

            if (!CryptoMarketSimulator.HttpPostedFileBaseExtensions.IsImage(file))
            {
                return Redirect(Request.UrlReferrer.PathAndQuery);

            }

            //check file was submitted
            if (file != null && file.ContentLength > 0)
            {
                using (var db = new SiteDbContext())
                {
                    var currentuser = db.Users.Find(User.Identity.GetUserId());
                    string fname = Path.GetFileName(file.FileName);
                    string extension = Path.GetExtension(file.FileName);

                    file.SaveAs(Server.MapPath(Path.Combine("~/Resources/UsersImages/", currentuser.Id + extension)));
                    currentuser.ProfilePicture = "/Resources/UsersImages/" + currentuser.Id + extension;
                    db.SaveChanges();

                    return Redirect(Request.UrlReferrer.PathAndQuery);

                }
            }
            return Redirect(Request.UrlReferrer.PathAndQuery);

        }


        [WebMethod]
        public JsonResult UserTop5()
        {
            List<TopCurrency> top5 = new List<TopCurrency>();

            using (var db = new SiteDbContext())
            {
                var cuerrentuser = db.Users.Find(User.Identity.GetUserId());
                 var res = cuerrentuser.Transactions.GroupBy(t => t.Currency).OrderByDescending(g => g.Count()).Take(5).ToList();
                // IEnumerable<TopCurrency> smths = res.SelectMany(group => group);

                foreach (var group in res)
                {
                    var topcurrency = new TopCurrency();

                    foreach (var transaction in group)
                    {
                        topcurrency.Currency = transaction.Currency;
                        topcurrency.Transactions = group.Count();
                    }
                    topcurrency.DollarsSpend = group.Sum(sum => sum.TotalUSD);
                    top5.Add(topcurrency);
                }

            }

            return Json(top5, JsonRequestBehavior.AllowGet);
        }


    }




}
