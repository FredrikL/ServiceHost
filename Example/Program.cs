using System;
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

                var host = new Host();

                host.Container.Register<DummyOne, IInstance>();
                host.Container.Register<DummyTwo, IInstance>();
                host.Container.Register<Log4NetLogger, ILogger>();
                
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
    }
}