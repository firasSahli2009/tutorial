using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Mvc;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using ProductStart.Providers;
using ProductStart.Repository;
using ProductStart.Resolver;
using Unity;
using Unity.Lifetime;

namespace ProductStart
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var container = new UnityContainer();
            container.RegisterType<IProductRepository, ProductRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IProductProvider, ProductProvider>(new HierarchicalLifetimeManager());

            container.RegisterType<IClientRepository, ClientRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IClientProvider, ClientProvider>(new HierarchicalLifetimeManager());

            config.DependencyResolver = new UnityResolver(container);

            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes
            config.MapHttpAttributeRoutes();

            //config.Routes.MapHttpRoute(
            //    name: "Product",
            //    routeTemplate: "api/products/{productid}",
            //    defaults: new { productid = RouteParameter.Optional },
            //    constraints: new {id="/d+"}
            //);

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().FirstOrDefault();
            if (jsonFormatter != null)
                jsonFormatter.SerializerSettings.ContractResolver= new CamelCasePropertyNamesContractResolver();
        }
    }
}
