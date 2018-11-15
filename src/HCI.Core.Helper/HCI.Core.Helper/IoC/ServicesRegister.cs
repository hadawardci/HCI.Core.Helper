using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Reflection;

namespace HCI.Core.Helper.IoC
{
    /// <summary>
    /// Automatically register services by following the name pattern.
    /// </summary>
    public static class ServicesRegister
    {
        /// <summary>
        /// Add services as scope
        /// </summary>
        /// <param name="container">service container</param>
        /// <param name="assembly">service assembly</param>
        /// <param name="patternSuffix">end of the service nomenclature</param>
        /// <param name="patternPrefix">starts of the service nomenclature</param>
        public static void AddServicesScope(IServiceCollection container, Assembly assembly, string patternSuffix, string patternPrefix = "I")
        {
            var registrations =
              from type in assembly.GetExportedTypes()
              where type.Name.EndsWith(patternSuffix)
              where type.Name.StartsWith(patternPrefix)
              where !type.IsInterface
              where type.GetInterfaces().Any()
              select new { Implementation = type, Service = type.GetInterfaces().FirstOrDefault() };

            foreach (var reg in registrations)
                container.AddScoped(reg.Service, reg.Implementation);
        }

        /// <summary>
        /// Add services as singleton
        /// </summary>
        /// <param name="container">service container</param>
        /// <param name="assembly">service assembly</param>
        /// <param name="patternSuffix">end of the service nomenclature</param>
        /// <param name="patternPrefix">starts of the service nomenclature</param>
        public static void AddServicesSingleton(IServiceCollection container, Assembly assembly, string patternSuffix, string patternPrefix = "I")
        {
            var registrations =
              from type in assembly.GetExportedTypes()
              where type.Name.EndsWith(patternSuffix)
              where type.Name.StartsWith(patternPrefix)
              where !type.IsInterface
              where type.GetInterfaces().Any()
              select new { Implementation = type, Service = type.GetInterfaces().FirstOrDefault() };

            foreach (var reg in registrations)
                container.AddSingleton(reg.Service, reg.Implementation);
        }

        /// <summary>
        /// Add services as transient
        /// </summary>
        /// <param name="container">service container</param>
        /// <param name="assembly">service assembly</param>
        /// <param name="patternSuffix">end of the service nomenclature</param>
        /// <param name="patternPrefix">starts of the service nomenclature</param>
        public static void AddServicesTransient(IServiceCollection container, Assembly assembly, string patternSuffix, string patternPrefix = "I")
        {
            var registrations =
              from type in assembly.GetExportedTypes()
              where type.Name.EndsWith(patternSuffix)
              where type.Name.StartsWith(patternPrefix)
              where !type.IsInterface
              where type.GetInterfaces().Any()
              select new { Implementation = type, Service = type.GetInterfaces().FirstOrDefault() };

            foreach (var reg in registrations)
                container.AddTransient(reg.Service, reg.Implementation);
        }
    }
}
