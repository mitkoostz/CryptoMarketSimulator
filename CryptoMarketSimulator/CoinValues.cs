using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;


    public class CoinValues
    {

        public static Dictionary<int,string> GetCurrencyName()
    {
        var result = new Dictionary<int, string>()
        {
            [2] = "Ethereum",
            [3] = "XRP",
            [4] = "EOS",
            [5] = "Litecoin",
            [6] = "Bitcoin Cash",
            [7] = "Binance Coin",
            [8] = "Stellar",
            [9] = "Tether",
            [10] = "Cardano",
            [11] = "TRON",
            [12] = "Bitcoin SV",
            [13] = "Dash",
            [14] = "Monero",
            [15] = "IOTA",
            [16] = "Maker",
            [17] = "Tezos",
            [18] = "Ontology",
            [19] = "NEO",
            [20] = "Ethereum Classic",
            [21] = "NEM",
            [22] = "Zcash",
            [23] = "Huobi Token",
            [24] = "VeChain",
            [25] = "Waves",
            [26] = "Qtum",
            [27] = "OmiseGO",
            [28] = "Dogecoin",
            [29] = "USD Coin",
            [30] = "Bitcoin Gold",
            [31] = "Maximine Coin",
            [32] = "Lisk",
            [33] = "TrueUSD",
            [34] = "BitTorrent",
            [35] = "Ravencoin",
            [36] = "IOST",
            [37] = "Decred",
            [38] = "Chainlink",
            [39] = "Zilliqa",
            [40] = "Augur",
            [41] = "ICON",
            [42] = "BitShares",
            [43] = "Holo",
            [44] = "Verge",
            [45] = "DigiByte",
            [46] = "Nano",
            [47] = "Steem",
            [48] = "Bytecoin",
            [49] = "Bitcoin Diamond",
            [50] = "Aeternity",

        };
        return result;



    }
        public static List<Coin> GetValues()
            {
            WebClient client = new WebClient();
            string coinmarket = client.DownloadString("https://coinmarketcap.com/");

            var doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(coinmarket);



            var findpriceDEMO = doc.DocumentNode
                .Descendants("a")
                .Where(d =>
                   d.Attributes.Contains("class")
                   &&
                   d.Attributes["class"].Value.Contains("price")

                );

    


            var findprice = findpriceDEMO.ToList();

            var findname = doc.DocumentNode
            .Descendants("a")
            .Where(d =>
               d.Attributes.Contains("class")
               &&
               d.Attributes["class"].Value.Contains("currency-name-container")

            ).ToList();

        //var findImage = doc.DocumentNode
        //       .Descendants("img")
        //       .Where(d =>
        //       d.Attributes.Contains("src")
        //      && d.Attributes.Contains("class")
        //      &&
        //       d.Attributes["class"].Value.Contains("logo-sprite")).ToList();


        var coins = new List<Coin>();

            for (int i = 0; i < 65; i++)
            {

                var crypto = new Coin();
                crypto.Name = findname[i].InnerText;
                crypto.Price = decimal.Parse(findprice[i].InnerText.Replace("$", ""), CultureInfo.InvariantCulture);

            //if (!findImage[i].Attributes["src"].Value.Contains(@"https://s2"))
            //{
            //    crypto.CoinImage = findImage[i].Attributes["data-src"].Value;
            //}
            //else
            //{
            //    crypto.CoinImage = findImage[i].Attributes["src"].Value;

            //}
            coins.Add(crypto);

            }

            return coins;

        }

    public static List<Coin> GetStatistic()
    {
        WebClient client = new WebClient();
        string coinmarket = client.DownloadString("https://coinmarketcap.com/");

        var doc = new HtmlAgilityPack.HtmlDocument();
        doc.LoadHtml(coinmarket);



        var findpriceDEMO = doc.DocumentNode
            .Descendants("a")
            .Where(d =>
               d.Attributes.Contains("class")
               &&
               d.Attributes["class"].Value.Contains("price")

            );


        var findprice = findpriceDEMO.Take(65).ToList();

        var findname = doc.DocumentNode
        .Descendants("a")
        .Where(d =>
           d.Attributes.Contains("class")
           &&
           d.Attributes["class"].Value.Contains("currency-name-container")

        ).Take(65).ToList();

        var findchange24 = doc.DocumentNode
     .Descendants("td")
     .Where(d =>
        d.Attributes.Contains("class")
        &&
        d.Attributes["class"].Value.Contains("no-wrap percent-change")

     ).Take(65).ToList();


        var coins = new List<Coin>();

        for (int i = 0; i < 65; i++)
        {

            var crypto = new Coin();
            crypto.Name = findname[i].InnerText;
            var pricereplaced = findprice[i].InnerText.Replace("$", "").Replace(",", "");
            decimal price = decimal.Parse(pricereplaced, CultureInfo.InvariantCulture);
            crypto.Price = price;
            crypto.Change24 = findchange24[i].InnerText;
            coins.Add(crypto);

        }

        return coins;

    }

    public static Coin GetBtcValue()
    {
        WebClient client = new WebClient();
        string coinmarket = client.DownloadString("https://coinmarketcap.com/");

        var doc = new HtmlAgilityPack.HtmlDocument();
        doc.LoadHtml(coinmarket);



        var findpriceDEMO = doc.DocumentNode
            .Descendants("a")
            .Where(d =>
               d.Attributes.Contains("class")
               &&
               d.Attributes["class"].Value.Contains("price")

            );


        var findprice = findpriceDEMO.ToList();

        var findname = doc.DocumentNode
        .Descendants("a")
        .Where(d =>
           d.Attributes.Contains("class")
           &&
           d.Attributes["class"].Value.Contains("currency-name-container")

        ).ToList();

        var coins = new List<Coin>();

        for (int i = 0; i < 2; i++)
        {

            var crypto = new Coin();
            crypto.Name = findname[i].InnerText;
            crypto.Price = decimal.Parse(findprice[i].InnerText.Replace("$", ""), CultureInfo.InvariantCulture);
            coins.Add(crypto);

        }

        return coins.First();

    }

}

