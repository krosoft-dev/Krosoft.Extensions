﻿using Microsoft.Extensions.DependencyInjection;

namespace Krosoft.Extensions.Testing.Extensions;

public static class ServiceCollectionExtensions
{
    public static void RemoveService(this IServiceCollection services, Func<ServiceDescriptor, bool> filter)
    {
        var descriptor = services.SingleOrDefault(filter);
        if (descriptor != null)
        {
            services.Remove(descriptor);
        }
    }

    public static void RemoveServices<T>(this IServiceCollection services)
    {
        services.RemoveServices(d => d.ServiceType == typeof(T));
    }

    public static void RemoveServices(this IServiceCollection services,
                                      Func<ServiceDescriptor, bool> filter)
    {
        var servicesDescriptor = services.Where(filter).ToList();
        if (servicesDescriptor.Any())
        {
            foreach (var serviceDescriptor in servicesDescriptor)
            {
                services.Remove(serviceDescriptor);
            }
        }
    }

    public static void RemoveTransient<TService>(this IServiceCollection services)
    {
        var serviceDescriptors = services.Where(x => x.ServiceType == typeof(TService) && x.Lifetime == ServiceLifetime.Transient).ToList();
        foreach (var serviceDescriptor in serviceDescriptors)
        {
            services.Remove(serviceDescriptor);
        }
    }

    /// <summary>
    /// Removes all registered <see cref="ServiceLifetime.Transient" /> registrations of <see cref="TService" /> and adds a new registration which uses the <see cref="Func{IServiceProvider, TService}" />.
    /// </summary>
    /// <typeparam name="TService">The type of service interface which needs to be placed.</typeparam>
    /// <param name="services"></param>
    /// <param name="implementationFactory">The implementation factory for the specified type.</param>
    public static void SwapTransient<TService>(this IServiceCollection services, Func<IServiceProvider, TService> implementationFactory)
    {
        services.RemoveTransient<TService>();
        services.AddTransient(typeof(TService), sp => implementationFactory(sp) ?? throw new InvalidOperationException());
    }

    public static void SwapTransient<TService, TImplementation>(this IServiceCollection services)
        where TImplementation : class, TService
        where TService : class
    {
        services.RemoveTransient<TService>();
        services.AddSingleton<TService, TImplementation>();
    }
}