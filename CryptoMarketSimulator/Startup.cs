using BombaySapphireCds.App_Start;
using CryptoMarketSimulator.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using System.Data.Entity;
using System.Linq;

[assembly: OwinStartupAttribute(typeof(CryptoMarketSimulator.Startup))]
namespace CryptoMarketSimulator
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            Database.SetInitializer(
                new MigrateDatabaseToLatestVersion<SiteDbContext,
                CryptoMarketSimulator.Migrations.Configuration>());

           

            //Uncomment when publishing

             OrderWorkerConfig.Start();
             ChartsWorkerConfig.Start();

            ConfigureAuth(app);
        }
    }
}
