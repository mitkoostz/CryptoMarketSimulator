using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Linq.Expressions;
using System.Data.Entity.ModelConfiguration.Configuration;
using System;
using System.Reflection;
using System.Linq;
using CryptoCurrencyStatistic;

namespace CryptoMarketSimulator.Models
{


    public class SiteDbContext : IdentityDbContext<ApplicationUser>
    {
        public SiteDbContext()
            : base("SmarterAsp", throwIfV1Schema: false)
        {
        }

        public static SiteDbContext Create()
        {
            return new SiteDbContext();
        }

        //TABLES
        public virtual DbSet<Wallet> Wallets { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }
        public virtual DbSet<LimitOrder> LimitOrders { get; set; }
        public virtual DbSet<CurrenciesChart> CurrenciesCharts { get; set; }




        protected override void OnModelCreating(System.Data.Entity.DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationUser>().Property(x => x.Balance).HasPrecision(18, 8);
            modelBuilder.Properties<decimal>().Configure(x => x.HasPrecision(18, 8));

            base.OnModelCreating(modelBuilder);
        }

    }

}
