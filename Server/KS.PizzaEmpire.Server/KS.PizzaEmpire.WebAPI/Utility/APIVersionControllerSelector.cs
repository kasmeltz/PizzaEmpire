using System;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;

namespace KS.PizzaEmpire.WebAPI.Utility
{
    /// <summary>
    /// Represents an item that selects the appropriate controller based on the API version
    /// in the Accept Header of the request.
    /// </summary>
    public class APIVersionControllerSelector : DefaultHttpControllerSelector
    {        
        /// <summary>
        /// Creates a new instance of an APIVersionControllerSelector with the
        /// provided HttpConfiguration
        /// </summary>
        /// <param name="config"></param>
        public APIVersionControllerSelector(HttpConfiguration config)
            : base(config)
        {
            _config = config;
        }

        private HttpConfiguration _config;

        private string GetVersionFromAcceptHeaderVersion(HttpRequestMessage request)
        {
            var acceptHeader = request.Headers.Accept;

            foreach (var mime in acceptHeader)
            {
                if (mime.MediaType == "application/json")
                {
                    var version = mime.Parameters
                        .Where(v => v.Name.Equals("version", StringComparison.OrdinalIgnoreCase)).FirstOrDefault();

                    if (version != null)
                    {
                        return version.Value;
                    }
                    return "1";
                }
            }
            return "1";
        }

        /// <summary>
        /// Selects the appropriate controller based on the request URI and the
        /// version number in the Accept Header
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public override HttpControllerDescriptor SelectController(HttpRequestMessage request)
        {
            var controllers = GetControllerMapping();  
            var routeData = request.GetRouteData(); 
            var controllerName = routeData.Values["controller"].ToString(); 
            HttpControllerDescriptor controllerDescriptor;
            var version = GetVersionFromAcceptHeaderVersion(request);
            var versionedControllerName = string.Concat(controllerName, "V", version);

            if (controllers.TryGetValue(versionedControllerName, out controllerDescriptor))
            {
                return controllerDescriptor;
            }
 
            return null; 
        }
    }
}