using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Autofac;
using Autofac.Core;

namespace ServiceHost
{
    public class Host
    {
        public IoC Container { get; set; }
        
        public Host(IoC container = null)
        {
            Container = container ?? DefaultRegistrations.Get();
        }

        public void Start()
        {
            var instances = Container.ResolveAll<IInstance>();

            Start(instances.ToArray());
        }

        public void Start(params IInstance[] instances)
        {
            foreach (var instance in instances)
            {
                ThreadPool.QueueUserWorkItem(state =>
                {
                    try
                    {
                        instance.Start();
                    }
                    catch (Exception e)
                    {
                        var logger = instance.Logger ?? Container.Resolve<ILogger>();
                        logger.Error(e.ToString());
                        instance.Stop();
                    }
                });
            }

            new ManualResetEvent(false).WaitOne();
        }

        private static class DefaultRegistrations
        {
            public static IoC Get()
            {
                var ioc = new AutofacContainer();
                ioc.Register<ConsoleLogger, ILogger>();
                return ioc;
            }
        }
    }

    public interface IoC
    {
        void Register<T, K>();
        T Resolve<T>();
        IEnumerable<T> ResolveAll<T>();
    }

    public class AutofacContainer : IoC
    {
        private readonly ContainerBuilder _builder;

        private IContainer _container;
        private IContainer Container
        {
            get
            {
                if (_container == null)
                {
                    _container = _builder.Build();
                }
                return _container;
            }
        }

        public AutofacContainer()
        {
            _builder = new ContainerBuilder();
        }

        public void Register<T, K>()
        {
            _builder.RegisterType<T>().As<K>();
        }

        public T Resolve<T>()
        {
            return Container.Resolve<T>();
        }

        public IEnumerable<T> ResolveAll<T>()
        {
            return Container.Resolve<IEnumerable<T>>();
        }
    }
}