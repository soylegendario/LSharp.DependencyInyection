using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace LSharp.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInjectables(this IServiceCollection services)
        {
            var injectables = GetTypesWithInjectableAttribute();
            foreach (var (injectableType, injectableAttribute) in injectables)
            {
                var interfaces = injectableType.GetInterfaces();
                Inject(services, injectableAttribute, injectableType, interfaces);
            }
            return services;
        }

        private static void Inject(IServiceCollection services, 
            InjectableAttribute injectableAttribute, 
            Type injectableType, 
            IEnumerable<Type> interfaces)
        {
            switch (injectableAttribute.ServiceLifetime)
            {
                case ServiceLifetime.Singleton:
                    if (!interfaces.Any())
                    {
                        services.AddSingleton(injectableType);
                    } else
                    {
                        foreach (var serviceType in interfaces)
                        {
                            services.AddSingleton(serviceType, injectableType);
                        }
                    }
                    break;
                case ServiceLifetime.Scoped:
                    if (!interfaces.Any())
                    {
                        services.AddScoped(injectableType);
                    }
                    else
                    {
                        foreach (var serviceType in interfaces)
                        {
                            services.AddScoped(serviceType, injectableType);
                        }
                    }
                    break;
                case ServiceLifetime.Transient:
                    if (!interfaces.Any())
                    {
                        services.AddTransient(injectableType);
                    }
                    else
                    {
                        foreach (var serviceType in interfaces)
                        {
                            services.AddTransient(serviceType, injectableType);
                        }
                    }
                    break;
                default:
                    throw new Exception("Cannot configure injectable services");
            }
        }

        private static IEnumerable<Tuple<Type, InjectableAttribute>> GetTypesWithInjectableAttribute()
        {
            var injectableTypes =
                from a in AppDomain.CurrentDomain.GetAssemblies().AsParallel()
                from t in a.GetTypes()
                let attributes = t.GetCustomAttributes(typeof(InjectableAttribute), true)
                where attributes != null && attributes.Length > 0
                select new { Type = t, InjectableAttribute = attributes.Cast<InjectableAttribute>().First() };
            foreach (var o in injectableTypes)
            {
                yield return new Tuple<Type, InjectableAttribute>(o.Type, o.InjectableAttribute);
            }
        }

    }
}