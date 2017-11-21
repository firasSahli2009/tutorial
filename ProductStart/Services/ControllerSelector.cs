using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;

namespace ProductStart.Services
{
    public class ControllerSelector : DefaultHttpControllerSelector
    {
        private HttpConfiguration _config;
        public ControllerSelector(HttpConfiguration configuration) : base(configuration)
        {
            _config = configuration;
        }

        public override HttpControllerDescriptor SelectController(HttpRequestMessage request)
        {
            var controllers = GetControllerMapping();

            var routeData = request.GetRouteData();

            var controllerName = (string)routeData.Values["controller"];

            HttpControllerDescriptor descriptor;

            if (string.IsNullOrWhiteSpace(controllerName))
            {
                return base.SelectController(request);
            }
            else if (controllers.TryGetValue(controllerName, out descriptor))
            {
                //var version = GetVersionFromQueryString(request);
                //var version = GetVersionFromHeader(request);
                //var version = GetVersionFromAcceptHeaderVersion(request);
                var version = GetVersionFromMediaType(request);

                var newName = string.Concat(controllerName, "V", version);

                HttpControllerDescriptor versionedDescriptor;

                if (controllers.TryGetValue(newName, out versionedDescriptor))
                {
                    return versionedDescriptor;
                }

                return descriptor;
            }

            return null;
        }

        private string GetVersionFromMediaType(HttpRequestMessage request)
        {
            var accept = request.Headers.Accept;
            var ex = new Regex(@"application\/json\+vnd\.([a-z]+)\.v([0-9]+)", RegexOptions.IgnoreCase);

            foreach (var mime in accept)
            {
                var match = ex.Match(mime.MediaType);
                if (match != null)
                {
                    return match.Groups[2].Value;
                }
            }

            return "1";
        }
    }
}