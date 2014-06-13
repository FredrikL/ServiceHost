using System;
using System.Collections.Generic;

namespace ServiceHost
{
    public interface IConfigureInstanceFinder
    {
        IEnumerable<Type> Get();
    }
}