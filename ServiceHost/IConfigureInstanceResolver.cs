using System;

namespace ServiceHost
{
    public interface IConfigureInstanceResolver
    {
        IConfigureInstance Get(Type t);
    }
}