using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Reflection;

namespace HCI.Core.Helper.IoC
{
    /// <summary>
    /// Automatically register all services by following the name pattern by assembly.
    /// </summary>
    public static class ServicesRegister
    {

        /// <summary>
        /// Add services as scope
        /// </summary>
        /// <param name="container">service container</param>
        /// <param name="assembly">service assembly</param>
        /// <param name="patternSuffix">end of the service nomenclature</param>
        /// <param name="patternPrefix">starts of the service nomenclature (default "I")</param>
        public static void AddServicesScope(IServiceCollection container, Assembly assembly, string patternSuffix, string patternPrefix = "I")
            => AddServices(container, assembly, patternSuffix, patternPrefix, InjectionTypeValue.Scope);

        /// <summary>
        /// Add services as singleton
        /// </summary>
        /// <param name="container">service container</param>
        /// <param name="assembly">service assembly</param>
        /// <param name="patternSuffix">end of the service nomenclature</param>
        /// <param name="patternPrefix">starts of the service nomenclature (default "I")</param>
        public static void AddServicesSingleton(IServiceCollection container, Assembly assembly, string patternSuffix, string patternPrefix = "I")
            => AddServices(container, assembly, patternSuffix, patternPrefix, InjectionTypeValue.Singleton);

        /// <summary>
        /// Add services as transient
        /// </summary>
        /// <param name="container">service container</param>
        /// <param name="assembly">service assembly</param>
        /// <param name="patternSuffix">end of the service nomenclature</param>
        /// <param name="patternPrefix">starts of the service nomenclature (default "I")</param>
        public static void AddServicesTransient(IServiceCollection container, Assembly assembly, string patternSuffix, string patternPrefix = null)
            => AddServices(container, assembly, patternSuffix, patternPrefix, InjectionTypeValue.Transient);

        private static void AddServices(IServiceCollection container, Assembly assembly, string patternSuffix, string patternPrefix, InjectionTypeValue injectionTypeValue)
        {
            patternPrefix = string.IsNullOrWhiteSpace(patternPrefix) ? "I" : patternPrefix;
            var registrations =
              from type in assembly.GetExportedTypes()
              where type.Name.EndsWith(patternSuffix)
              where !type.IsInterface
              where type.GetInterfaces().Any(x => x.Name.Equals(patternPrefix + type.Name))
              select new { Service = type.GetInterfaces().FirstOrDefault(i => i.Name.Equals(patternPrefix + type.Name)), Implementation = type };

            foreach (var reg in registrations)
                switch (injectionTypeValue)
                {
                    case InjectionTypeValue.Scope:
                        container.AddScoped(reg.Service, reg.Implementation);
                        break;
                    case InjectionTypeValue.Singleton:
                        container.AddSingleton(reg.Service, reg.Implementation);
                        break;
                    case InjectionTypeValue.Transient:
                        container.AddTransient(reg.Service, reg.Implementation);
                        break;
                }
        }

        private enum InjectionTypeValue
        {
            Scope,
            Singleton,
            Transient,
        }
    }
}
