using System;
using Microsoft.Extensions.DependencyInjection;

namespace LSharp.DependencyInjection
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class InjectableAttribute : Attribute
    {
        /// <summary>
        ///     Get the lifetime to inject the class
        /// </summary>
        public ServiceLifetime ServiceLifetime { get; }

        /// <summary>
        ///     Define a injectable class with Transient lifetime
        /// </summary>
        public InjectableAttribute() : this(ServiceLifetime.Transient)
        {
        }

        /// <summary>
        ///     Define a injectable class
        /// </summary>
        /// <param name="serviceLifetime">Lifetime of the class</param>
        public InjectableAttribute(ServiceLifetime serviceLifetime)
        {
            ServiceLifetime = serviceLifetime;
        }
    }
}