using System;
using ServiceHost;

namespace Example
{
    public class Program
    {
        public static void Main()
        {
            try
            {
                var host = new Host();

                host.Container.Register<DummyOne, IInstance>();
                host.Container.Register<DummyTwo, IInstance>();
                //host.Container.Register<NullLogger, ILogger>();
                
                host.Start();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            Console.ReadLine();
        }

        public class NullLogger : ILogger
        {
            public void Info(string msg)
            {
                
            }

            public void Debug(string msg)
            {
                
            }

            public void Error(string msg)
            {
                
            }
        }
    }
}