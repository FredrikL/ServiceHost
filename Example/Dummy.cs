using System;
using System.Timers;
using ServiceHost;

namespace Example
{
    public abstract class Dummy : IInstance
    {
        Timer _timer;
        protected abstract int InstanceNumber { get; }
        
        public Dummy(ILogger logger)
        {
            Logger = logger;
            _timer = new Timer(1000) { AutoReset = true };
            _timer.Elapsed += (sender, eventArgs) => Logger.Info(string.Format("It is {0}, Instance: {1}", DateTime.Now, InstanceNumber));
        }

        public ILogger Logger { get; set; }

        public void Start()
        {
            _timer.Start();
        }

        public void Stop()
        {
            _timer.Stop();
        }
    }

    public class DummyOne : Dummy
    {
        public DummyOne(ILogger logger) : base(logger)
        {
        }

        protected override int InstanceNumber
        {
            get { return 1; }
        }
    }

    public class DummyTwo : Dummy
    {
        public DummyTwo(ILogger logger) : base(logger)
        {
        }

        protected override int InstanceNumber
        {
            get { return 2; }
        }
    }
}
