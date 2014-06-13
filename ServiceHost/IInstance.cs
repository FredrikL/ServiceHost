namespace ServiceHost
{
    public interface IInstance
    {
        ILogger Logger { get; set; }
        void Start();
        void Stop();
    }
}