using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Routing;

namespace ProductStart.Services
{
    public class VersionedRouteAttribute : RouteFactoryAttribute
    {
       
        public VersionedRouteAttribute(string template, int allowedVersion) : base(template)
        {
            _allowedVersion = allowedVersion;
        }

        public override IDictionary<string, object> Constraints
        {
            get
            {
                return new HttpRouteValueDictionary
            {
                {"version", new VersionConstraint(_allowedVersion)}
            };
            }
        }

        private int _allowedVersion;
    }
}