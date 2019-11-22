using System;
using System.Configuration;

namespace RepositoryTest
{
    internal class RepositoryFactory
    {
        internal static object Instance(string instance)
        {
            string targetAssembly = ConfigurationManager.AppSettings["targetAssembly"];
            return Activator.CreateInstance(targetAssembly, targetAssembly + "." + instance).Unwrap();
        }
    }
}
