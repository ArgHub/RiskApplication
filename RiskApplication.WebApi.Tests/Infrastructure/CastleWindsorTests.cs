using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Controllers;
using Castle.Core;
using Castle.Core.Internal;
using Castle.MicroKernel;
using Castle.Windsor;
using NUnit.Framework;
using RiskApplication.WebApi.Controllers;
using RiskApplication.WebApi.Infrastructure;

namespace RiskApplication.WebApi.Tests.Infrastructure
{
    [TestFixture]
    public class CastleWindsorTests
    {
        /*
         * These tests check that all controllers are configured consistently
         * and according to convention. They also check that they have been 
         * registered correctly in DependencyConventions.cs.
         * 
         * The purpose of these tests is to catch new controllers that have
         * not been configured correctly. Please do not modify these tests
         * unless you are certain that you need to.
         */

        [SetUp]
        public void SetUp()
        {
            windsorContainer = new WindsorContainer().Install(new DependencyConventions());
        }

        private IWindsorContainer windsorContainer;

        private static IEnumerable<IHandler> GetHandlersFor(Type type, IWindsorContainer container)
        {
            return container.Kernel.GetAssignableHandlers(type);
        }

        private static Type[] GetImplementationTypesFor(Type type, IWindsorContainer container)
        {
            return GetHandlersFor(type, container)
                .Select(h => h.ComponentModel.Implementation)
                .OrderBy(t => t.Name)
                .ToArray();
        }

        private static Type[] GetPublicClassesFromApplicationAssembly(Predicate<Type> where)
        {
            return typeof(HomeController).Assembly.GetExportedTypes()
                .Where(t => t.IsClass)
                .Where(t => t.IsAbstract == false)
                .Where(where.Invoke)
                .OrderBy(t => t.Name)
                .ToArray();
        }

        [Test]
        public void AllAndOnlyApiControllersHaveControllersSuffix()
        {
            Type[] allControllers =
                GetPublicClassesFromApplicationAssembly(c => c.Is<IHttpController>() && c.Name.EndsWith("Controller"));
            Type[] registeredControllers = GetImplementationTypesFor(typeof(IHttpController), windsorContainer);
            Assert.AreEqual(allControllers, registeredControllers);
        }

        [Test]
        public void AllAndOnlyApiControllersLiveInControllersNamespace()
        {
            Type[] allControllers =
                GetPublicClassesFromApplicationAssembly(
                    c => c.Is<IHttpController>() && c.Namespace != null && c.Namespace.Contains("Controllers"));
            Type[] registeredControllers = GetImplementationTypesFor(typeof(IHttpController), windsorContainer);
            Assert.AreEqual(allControllers, registeredControllers);
        }

        [Test]
        public void AllControllersAreRegistered()
        {
            Type[] allControllers = GetPublicClassesFromApplicationAssembly(c => c.Is<IHttpController>());
            Type[] registeredControllers = GetImplementationTypesFor(typeof(IHttpController), windsorContainer);
            Assert.AreEqual(allControllers, registeredControllers);
        }

        [Test]
        public void AllControllersAreTransient()
        {
            IHandler[] nonTransientControllers = GetHandlersFor(typeof(IHttpController), windsorContainer)
                .Where(controller => controller.ComponentModel.LifestyleType != LifestyleType.Transient)
                .ToArray();

            Assert.IsEmpty(nonTransientControllers);
        }

        [Test]
        public void AllControllersExposeThemselvesAsService()
        {
            IHandler[] controllersWithWrongName = GetHandlersFor(typeof(IHttpController), windsorContainer)
                .Where(
                    controller =>
                        controller.ComponentModel.Services.Single() != controller.ComponentModel.Implementation)
                .ToArray();

            Assert.IsEmpty(controllersWithWrongName);
        }
    }
}
