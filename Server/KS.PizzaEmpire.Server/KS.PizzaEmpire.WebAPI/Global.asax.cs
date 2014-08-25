namespace KS.PizzaEmpire.WebAPI
{
    using Business.Automapper;
    using Common.GameLogic;
    using GameLogic.ExperienceLevelLogic;
    using GameLogic.ItemLogic;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;

    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AutoMapperConfiguration.Configure();

            Task.WaitAll(ItemManager.Instance.Initialize());
            Task.WaitAll(ExperienceLevelManager.Instance.Initialize());
            GamePlayerLogic.Instance.BuildableItems = 
                ItemManager.Instance.BuildableItems;
            GamePlayerLogic.Instance.ExperienceLevels = 
                ExperienceLevelManager.Instance.ExperienceLevels;

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
