using System;

namespace ServiceHost
{
    public class ActivatorConfigureInstanceResolver : IConfigureInstanceResolver
    {
        public IConfigureInstance Get(Type type)
        {
            return Activator.CreateInstance(type) as IConfigureInstance;
        }
    }
}