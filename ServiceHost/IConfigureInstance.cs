namespace ServiceHost
{
    public interface IConfigureInstance
    {
        ILogger Logger { get; }
        IInstance GetInstance();
    }
}