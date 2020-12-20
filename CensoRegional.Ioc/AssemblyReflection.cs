using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace curso.Ioc
{
    public static class AssemblyReflection
    {
        public static IEnumerable<Type> GetApplicationClasses()
        {
            return Assembly.Load("CensoRegional.Application").GetTypes().Where(
                type => type.IsClass
                && !type.IsAbstract
                && type.GetCustomAttribute<CompilerGeneratedAttribute>() == null);
        }

        public static IEnumerable<Type> GetInterfaces()
        {
            return Assembly.Load("CensoRegional.Domain").GetTypes().Where(
                type => type.IsInterface
                && type.Namespace != null
             );
        }

        public static IEnumerable<Type> GetInfra()
        {
            return Assembly.Load("CensoRegional.Infrastructure").GetTypes().Where(
                type => type.IsClass
                && !type.IsAbstract
                && type.Namespace != null
                && (type.Namespace.StartsWith("CensoRegional.Infrastructure.Database.Repository") 
                    || type.Namespace.StartsWith("CensoRegional.Infrastructure.Messaging"))
                && (!type.Namespace.Contains("Installer"))
                && type.GetCustomAttribute<CompilerGeneratedAttribute>() == null);
        }

        public static IEnumerable<Assembly> GetCurrentAssemblies()
        {
            return new Assembly[]
            {
                Assembly.Load("CensoRegional.Application"),
                Assembly.Load("CensoRegional.Domain"),
                Assembly.Load("CensoRegional.Infrastructure"),
                Assembly.Load("CensoRegional.Ioc")
            };
        }

        public static Type FindType(Type @interface, IEnumerable<Type> types)
        {
            foreach (var type in types)
            {
                if (type.GetInterfaces().Contains(@interface))
                {
                    return type;
                }
            }

            return null;
        }

        public static Type FindInterface(Type type, IEnumerable<Type> interfaces)
        {
            foreach (var @interface in interfaces)
            {
                if (type.GetInterfaces().Contains(@interface))
                {
                    return @interface;
                }
            }

            return null;
        }
    }
}
