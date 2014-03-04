using DaveScriven.CodeSample.Site.Cqrs;
using DaveScriven.CodeSample.Data;
using SimpleInjector;
using SimpleInjector.Integration.Web.Mvc;
using SimpleInjector.Integration.WebApi;
using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;

[assembly: WebActivator.PostApplicationStartMethod(typeof(DaveScriven.CodeSample.Site.Bootstrapper), "Initialize")]
namespace DaveScriven.CodeSample.Site
{
    /// <summary>
    /// Initiates IoC and the CQRS runtime.
    /// </summary>
    public static class Bootstrapper
    {
        private static readonly Container container = new Container();

        /// <summary>
        /// Gets the container.
        /// </summary>
        internal static Container Container { get { return container; } }

        /// <summary>
        /// Initializes the bootstrapper.
        /// </summary>
        public static void Initialize()
        {
            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());
            container.RegisterMvcAttributeFilterProvider();
            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);

            RegisterServices(container);

            var runtime = new CqrsRuntime(container);
            container.RegisterSingle(runtime);

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);

            runtime.Start();
        }

        private static void RegisterServices(Container container)
        {
            container.RegisterPerWebRequest<IFishLogReadModel, FishLogReadModel>();
        }
    }
}