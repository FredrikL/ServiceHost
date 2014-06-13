using System;
using System.Collections.Generic;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using log4net;
using ServiceHost;

namespace Example
{
    public class Program
    {
        public static void Main()
        {
            try
            {
                log4net.Config.XmlConfigurator.Configure(); // Logger initialized later with LoggingFacility

                var container = new WindsorContainer();

                container.Register(Component.For<IInstance>().ImplementedBy<DummyOne>());
                container.Register(Component.For<IInstance>().ImplementedBy<DummyTwo>());
                container.Register(Component.For<ILogger>().ImplementedBy<Log4NetLogger>());

                var ioc = new WindsorIoC(container);

                var host = new Host(ioc);
                host.Start();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            Console.ReadLine();
        }

        public class Log4NetLogger : ILogger
        {
            private readonly ILog _logger = LogManager.GetLogger(typeof (Log4NetLogger));

            public void Info(string msg)
            {
                _logger.Info(msg);   
            }

            public void Debug(string msg)
            {
                _logger.Debug(msg);
            }

            public void Error(string msg)
            {
                _logger.Error(msg);
            }
        }

        public class WindsorIoC : IoC
        {
            private IWindsorContainer _realContainer;

            public WindsorIoC(IWindsorContainer realContainer)
            {
                _realContainer = realContainer;
            }

            public void Register<T, K>()
            {
                _realContainer.Register(Component.For(typeof(T)).ImplementedBy(typeof(K)));
            }

            public T Resolve<T>()
            {
                return _realContainer.Resolve<T>();
            }

            public IEnumerable<T> ResolveAll<T>()
            {
                return _realContainer.ResolveAll<T>();
            }
        }
    }
}