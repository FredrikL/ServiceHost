using System;
using System.Linq;
using System.Threading;

namespace ServiceHost
{
    public class Host
    {
        private readonly IConfigureInstanceFinder _finder;
        private readonly IConfigureInstanceResolver _resolver;

        public Host(IConfigureInstanceFinder finder = null, IConfigureInstanceResolver resolver = null)
        {
            _finder = finder ?? new AutoDiscoveryConfigureInstanceFinder();
            _resolver = resolver ?? new ActivatorConfigureInstanceResolver();
        }

        public void Start()
        {
            var instanceConfigurationConcreteTypes = _finder.Get();
            // TODO: Check that all types in instanceConfigurationConcreteTypes really is IConfigureInstance

            var instanceConfigurations = instanceConfigurationConcreteTypes.Select(t => _resolver.Get(t)).ToArray();

            var instances =
                from config in instanceConfigurations
                select GetConfiguredInstance(config);

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
                        var logger = instance.Logger ?? DefaultLogger;
                        logger.Error(e.ToString());
                        instance.Stop();
                    }
                });
            }

            new ManualResetEvent(false).WaitOne();
        }

        private IInstance GetConfiguredInstance(IConfigureInstance configuration)
        {
            var instance = configuration.GetInstance();
            instance.Logger = configuration.Logger ?? DefaultLogger;
            return instance;
        }

        private ILogger _defaultLogger;
        private ILogger DefaultLogger
        {
            get { return _defaultLogger ?? (_defaultLogger = new ConsoleLogger()); }
        }
    }
}