using System;
using Microsoft.Extensions.DependencyInjection;

namespace LSharp.DependencyInjection
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class InjectableAttribute : Attribute
    {
        public ServiceLifetime ServiceLifetime { get; }

        public InjectableAttribute(ServiceLifetime serviceLifetime)
        {
            ServiceLifetime = serviceLifetime;
        }
    }
}