using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using VideoLib.Controllers;
using VideoLib.Models;

namespace VideoLib
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //using (var db = new ApplicationDbContext())
            //{
            //    if (!db.Users.Where(x => x.Email == "marcin_dziekan@wp.pl").Any())
            //    {
            //        var regModel = new RegisterViewModel
            //        {
            //            Email = "marcin_dziekan@wp.pl",
            //            Password = "Maximus,.1",
            //            ConfirmPassword = "Maximus,.1"
            //        };
            //        var registered = new AccountController().Register(regModel);
            //    }
            //}
        }
    }
}
