namespace KS.PizzaEmpire.WebAPI
{
    using KS.PizzaEmpire.WebAPI.Utility;
    using Microsoft.Owin.Security.OAuth;
    using System.Linq;
    using System.Web.Http;
    using System.Web.Http.Dispatcher;

    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // Register our custom controller selector to handle versions via the Accept Header
            config.Services
                .Replace(typeof(IHttpControllerSelector),
                    new APIVersionControllerSelector((config)));

            // default to JSON
            var appXmlType = config.Formatters.XmlFormatter.SupportedMediaTypes
                .FirstOrDefault(t => t.MediaType == "application/xml");
            config.Formatters.XmlFormatter.SupportedMediaTypes.Remove(appXmlType);
        }
    }
}