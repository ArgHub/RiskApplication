using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;

namespace RiskApplication.WebApi.App_Start
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "ControllersApi",
                routeTemplate: "apimvc/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

           // config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
            config.Formatters.XmlFormatter.SupportedMediaTypes.Clear();
        }
    }
}