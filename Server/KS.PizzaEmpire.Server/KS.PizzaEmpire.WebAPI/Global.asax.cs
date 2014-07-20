﻿using GameLogic.ExperienceLevelLogic;
using GameLogic.ItemLogic;
using KS.PizzaEmpire.Services;
using KS.PizzaEmpire.Services.Caching;
using KS.PizzaEmpire.Services.Serialization;
using KS.PizzaEmpire.Services.Storage;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace KS.PizzaEmpire.WebAPI
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Task.WaitAll(ItemManager.Instance.Initialize());
            Task.WaitAll(ExperienceLevelManager.Instance.Initialize());

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
