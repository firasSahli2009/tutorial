using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http.Routing;

namespace ProductStart.Services
{
    public class VersionConstraint : IHttpRouteConstraint
    {
        private const int DefaultVersion = 1;
        private int _allowedVersion;

        public VersionConstraint(int allowedVersion)
        {
            _allowedVersion = allowedVersion;
        }

        public bool Match(HttpRequestMessage request, IHttpRoute route, string parameterName, IDictionary<string, object> values, HttpRouteDirection routeDirection)
        {
            if (routeDirection == HttpRouteDirection.UriResolution)
            {
                int version = GetVersionFromHeader(request, parameterName) ?? DefaultVersion;

                return (version == _allowedVersion);
            }

            return true;
        }

        private int? GetVersionFromHeader(HttpRequestMessage request, string parameterName)
        {
            var acceptHeader = request.Headers.Accept;
            foreach (var mime in acceptHeader)
            {
                if (mime.MediaType == "application/json")
                {
                    NameValueHeaderValue version = mime.Parameters
                        .FirstOrDefault(v => v.Name.Equals(parameterName, StringComparison.OrdinalIgnoreCase));
                    int parsedVersion;
                    return int.TryParse(version.Value, out parsedVersion)
                        ? parsedVersion
                        : (int?)null;
                }
            }

            return null;
        }
    }
}