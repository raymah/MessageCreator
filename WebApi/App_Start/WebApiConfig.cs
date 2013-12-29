using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using WebApi.Hal;

namespace WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            GlobalConfiguration.Configuration.Formatters.Add(new JsonHalMediaTypeFormatter());
            GlobalConfiguration.Configuration.Formatters.Add(new XmlHalMediaTypeFormatter());

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
