using System;
using System.Timers;
using ServiceHost;

namespace Example
{
    public class Dummy : IInstance
    {
        Timer _timer;
        private int _instanceNumber;
        public Dummy(int instanceNumber)
        {
            _instanceNumber = instanceNumber;
            _timer = new Timer(1000) { AutoReset = true };
            _timer.Elapsed += (sender, eventArgs) => Logger.Info(string.Format("It is {0}, Instance: {1}", DateTime.Now, _instanceNumber));
        }

        public ILogger Logger { get; set; }

        public void Start()
        {

            _timer.Start();
        }
        public void Stop() { _timer.Stop(); }
    }

    public class ConfigureDummyOne : IConfigureInstance
    {
        private Dummy _dummy;

        public ConfigureDummyOne()
        {
            _dummy = new Dummy(1);
        }

        public ILogger Logger { get; set; }
        public IInstance GetInstance()
        {
            return _dummy; 
        }
    }

    public class ConfigureDummyTwo : IConfigureInstance
    {
        private Dummy _dummy;

        public ConfigureDummyTwo()
        {
            _dummy = new Dummy(2);
        }

        public ILogger Logger { get; set; }
        public IInstance GetInstance()
        {
            return _dummy;
        }
    }
}
