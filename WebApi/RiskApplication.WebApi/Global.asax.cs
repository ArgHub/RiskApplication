using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Http.ExceptionHandling;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Castle.Windsor;
using RiskApplication.WebApi.App_Start;
using RiskApplication.WebApi.Infrastructure;

namespace RiskApplication.WebApi
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private readonly IWindsorContainer container;

        /// <summary>
        ///     Default constructor
        /// </summary>
        protected MvcApplication()
        {
            container = new WindsorContainer().Install(new DependencyConventions());
        }
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
           // RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            GlobalConfiguration.Configuration.Services.Replace(typeof(IHttpControllerActivator),
                    new WindsorCompositionRoot(container));
        }

        /// <summary>
        ///     Method to clean up the DI container
        /// </summary>
        public override void Dispose()
        {
            container.Dispose();
            base.Dispose();
        }
    }
}
