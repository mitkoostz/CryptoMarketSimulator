    using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace CryptoMarketSimulator.Models
{
    public class Wallet
    {

        public int Id { get; set; }
        public string ImagePath { get; set; }


        public decimal Ethereum { get; set; }
        public decimal XRP { get; set; }
        public decimal EOS { get; set; }
        public decimal Litecoin { get; set; }
        public decimal BitcoinCash { get; set; }
        public decimal BinanceCoin { get; set; }
        public decimal Stellar { get; set; }
        public decimal Tether { get; set; }
        public decimal Cardano { get; set; }
        public decimal TRON { get; set; }
        public decimal BitcoinSV { get; set; }
        public decimal Dash { get; set; }
        public decimal Monero { get; set; }
        public decimal IOTA { get; set; }
        public decimal Maker { get; set; }
        public decimal Tezos { get; set; }
        public decimal Ontology { get; set; }
        public decimal NEO { get; set; }
        public decimal EthereumClassic { get; set; }
        public decimal NEM { get; set; }
        public decimal Zcash { get; set; }
        public decimal HuobiToken { get; set; }
        public decimal VeChain { get; set; }
        public decimal Waves { get; set; }
        public decimal Qtum { get; set; }
        public decimal OmiseGO { get; set; }
        public decimal Dogecoin { get; set; }
        public decimal USDCoin { get; set; }
        public decimal BitcoinGold { get; set; }
        public decimal MaximineCoin { get; set; }
        public decimal Lisk { get; set; }
        public decimal TrueUSD { get; set; }
        public decimal BitTorrent { get; set; }
        public decimal IOST { get; set; }
        public decimal Decred { get; set; }
        public decimal Ravencoin { get; set; }
        public decimal Chainlink { get; set; }
        public decimal Zilliqa { get; set; }
        public decimal Augur { get; set; }
        public decimal ICON { get; set; }
        public decimal BitShares { get; set; }
        public decimal Holo { get; set; }
        public decimal KuCoinShares { get; set; }
        public decimal DigiByte { get; set; }
        public decimal Nano { get; set; }
        public decimal Steem { get; set; }
        public decimal Bytecoin { get; set; }
        public decimal BitcoinDiamond { get; set; }
        public decimal Aeternity { get; set; }


        public decimal this[string propertyName]
        {
            get
            {
                // probably faster without reflection:
                // like:  return Properties.Settings.Default.PropertyValues[propertyName] 
                // instead of the following
                Type myType = typeof(Wallet);
                PropertyInfo myPropInfo = myType.GetProperty(propertyName);
                return (decimal)myPropInfo.GetValue(this, null);
            }
            set
            {
                Type myType = typeof(Wallet);
                PropertyInfo myPropInfo = myType.GetProperty(propertyName);
                myPropInfo.SetValue(this, value, null);

            }

        }

    }
}