using System.Web.Http.Controllers;
using Castle.Windsor;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using RiskApplication.Repository.Abstract;
using RiskApplication.Repository.Concrete;

namespace RiskApplication.WebApi.Infrastructure
{
    public class DependencyConventions : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes.FromThisAssembly()
                .BasedOn<IHttpController>()
                .LifestyleTransient());

            container.Register(Component.For<IFileManager>().ImplementedBy<FileManager>());
            container.Register(Component.For(typeof(IDataLoader<>)).ImplementedBy(typeof(DataLoader<>)).LifestylePerWebRequest());
            container.Register(Component.For<ISettledBetRepository>().ImplementedBy<SettledBetRepository>());
            container.Register(Component.For<IUnsettledBetRepository>().ImplementedBy<UnsettledBetRepository>());
            container.Register(Component.For<IDataPathFinder>().ImplementedBy<DataPathFinder>());
        }
    }
}