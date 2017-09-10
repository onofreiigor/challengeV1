using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Data.SqlClient;
using System.Web.Routing;
using DotaChallengeV1.Models;

namespace DotaChallengeV1
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static SqlConnection SqlConn;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //Data Base Connection
            SqlConn = ChallengeDataBase.ConnectDataBase();
        }
    }
}
