using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Data.SqlClient;
using System.Web.Routing;
using DotaChallengeV1.Models;
using SteamWebAPI2.Interfaces;

namespace DotaChallengeV1
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static SqlConnection SqlConn;
        public static DOTA2Match MatchInterface;
        public static DOTA2Econ EconInterface;
        public static SteamUser SteamUser;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //Data Base Connection
            SqlConn = ChallengeDataBase.ConnectDataBase();
            //Steam API Connection
            string SteamWebApiKey = "4CE963A0198750BC26CF9355706F2686";
            MatchInterface = new DOTA2Match(SteamWebApiKey);
            EconInterface = new DOTA2Econ(SteamWebApiKey);
            SteamUser = new SteamUser(SteamWebApiKey);
        }
    }
}
