using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace LSharp.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        ///     Configure the dependency injection for injectables classes 
        /// </summary>
        /// <param name="services">Service collection</param>
        /// <returns></returns>
        public static IServiceCollection AddInjectables(this IServiceCollection services)
        {
            var injectables = GetTypesWithInjectableAttribute();
            foreach (var (injectableType, injectableAttribute) in injectables)
            {
                var interfaces = injectableType.GetInterfaces().FirstOrDefault() ?? injectableType;
                services.Add(new ServiceDescriptor(interfaces, injectableType, injectableAttribute.ServiceLifetime));
            }
            return services;
        }

        /// <summary>
        ///     Get the all classes decorated with <see cref="InjectableAttribute"/> in all assemblies
        /// </summary>
        private static IEnumerable<Tuple<Type, InjectableAttribute>> GetTypesWithInjectableAttribute()
        {
            var injectableTypes =
                from a in AppDomain.CurrentDomain.GetAssemblies().AsParallel()
                from t in a.GetTypes()
                let attributes = t.GetCustomAttributes(typeof(InjectableAttribute), true)
                where attributes.Length > 0
                select new { Type = t, InjectableAttribute = attributes.Cast<InjectableAttribute>().First() };
            foreach (var o in injectableTypes)
            {
                yield return new Tuple<Type, InjectableAttribute>(o.Type, o.InjectableAttribute);
            }
        }

    }
}