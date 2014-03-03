using SimpleCqrs;
using SimpleCqrs.Commanding;
using SimpleCqrs.Eventing;
using SimpleCqrs.EventStore.SqlServer;
using SimpleCqrs.EventStore.SqlServer.Serializers;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DaveScriven.CodeSample.Site.Cqrs
{
    /// <summary>
    /// The site's CQRS runtime.
    /// </summary>
    public class CqrsRuntime : SimpleCqrs.SimpleCqrsRuntime<SimpleInjectorServiceLocator>
    {
        private readonly Container container;

        /// <summary>
        /// Initializes a new instance of the <see cref="CqrsRuntime"/> class.
        /// </summary>
        /// <param name="container">The container.</param>
        public CqrsRuntime(Container container)
        {
            this.container = container;
        }

        /// <summary>
        /// Gets the service locator.
        /// </summary>
        protected override SimpleInjectorServiceLocator GetServiceLocator()
        {
            return new SimpleInjectorServiceLocator(this.container);
        }

        /// <summary>
        /// Gets the command bus.
        /// </summary>
        /// <param name="serviceLocator">The service locator.</param>
        protected override ICommandBus GetCommandBus(IServiceLocator serviceLocator)
        {
            return new LocalCommandBus(new AssemblyTypeCatalog(this.GetAssembliesToScan(serviceLocator)), serviceLocator);
        }

        /// <summary>
        /// Gets the event bus.
        /// </summary>
        /// <param name="serviceLocator">The service locator.</param>
        /// <returns></returns>
        protected override IEventBus GetEventBus(IServiceLocator serviceLocator)
        {
            var typeCatalog = new AssemblyTypeCatalog(this.GetAssembliesToScan(serviceLocator));
            var domainEventHandlerFactory = new DomainEventHandlerFactory(serviceLocator);
            var domainEventTypes = typeCatalog.GetGenericInterfaceImplementations(typeof(IHandleDomainEvents<>));

            return new LocalEventBus(domainEventTypes, domainEventHandlerFactory);
        }

        /// <summary>
        /// Gets the event store.
        /// </summary>
        /// <param name="serviceLocator">The service locator.</param>
        /// <returns></returns>
        protected override IEventStore GetEventStore(IServiceLocator serviceLocator)
        {
            return new SqlServerEventStore(new SqlServerConfiguration("Server=(local)\\sqlexpress;Database=DaveScriven.CodeSample.EventStore;Trusted_Connection=True;"), new JsonDomainEventSerializer());
        }
    }

    /// <summary>
    /// A Simple Injector service locator implementation for Simple CQRS.
    /// </summary>
    public class SimpleInjectorServiceLocator : IServiceLocator
    {
        private readonly Container container;

        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleInjectorServiceLocator"/> class.
        /// </summary>
        /// <param name="container">The container.</param>
        public SimpleInjectorServiceLocator(Container container)
        {
            this.container = container;
        }

        public TService Inject<TService>(TService instance) where TService : class
        {
            this.container.InjectProperties(instance);

            return instance;
        }

        public void Register<TInterface>(Func<TInterface> factoryMethod) where TInterface : class
        {
            this.container.Register<TInterface>(factoryMethod);
        }

        public void Register<TInterface>(TInterface instance) where TInterface : class
        {
            this.container.RegisterSingle<TInterface>(instance);
        }

        public void Register(Type serviceType, Type implType)
        {
            this.container.Register(serviceType, implType);
        }

        public void Register(string key, Type type)
        {
            throw new NotSupportedException();
        }

        public void Register<TInterface, TImplementation>(string key) where TImplementation : class, TInterface
        {
            throw new NotSupportedException();
        }

        public void Register<TInterface, TImplementation>() where TImplementation : class, TInterface
        {
            this.container.Register(typeof(TInterface), typeof(TImplementation));
        }

        public void Register<TInterface>(Type implType) where TInterface : class
        {
            this.container.Register(typeof(TInterface), implType);
        }

        public void Release(object instance)
        {
        }

        public void Reset()
        {
        }

        public object Resolve(Type type)
        {
            return this.container.GetInstance(type);
        }

        public T Resolve<T>(string key) where T : class
        {
            throw new NotSupportedException();
        }

        public T Resolve<T>() where T : class
        {
            return this.container.GetInstance<T>();
        }

        public IList<T> ResolveServices<T>() where T : class
        {
            return this.container.GetAllInstances<T>().ToList();
        }

        public void TearDown<TService>(TService instance) where TService : class
        {
        }

        public void Dispose()
        {
        }
    }
}