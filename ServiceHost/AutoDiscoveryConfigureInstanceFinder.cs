using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using Timer = System.Timers.Timer;


namespace ServiceHost
{
    public class AutoDiscoveryConfigureInstanceFinder : IConfigureInstanceFinder
    {
        public IEnumerable<Type> Get()
        {
            var assemblies = GetAssembliesToScan();

            return
                from assembly in assemblies
                from type in assembly.GetTypes()
                where !type.IsInterface && !type.IsAbstract
                where type.GetInterfaces().Contains(typeof(IConfigureInstance))
                select type;
        }

        private IEnumerable<Assembly> GetAssembliesToScan()
        {
            return AppDomain.CurrentDomain.GetAssemblies();
        }
    }
}
