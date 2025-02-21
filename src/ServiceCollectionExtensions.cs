﻿using System;

using Nefarius.Blazor.EventAggregator;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
///     <see cref="IServiceCollection" /> extensions.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    ///     Adds <see cref="IEventAggregator" /> as a singleton
    /// </summary>
    /// <param name="services">Service collection</param>
    /// <param name="configure">Optional configuration.</param>
    /// <returns>Service collection</returns>
    public static IServiceCollection AddEventAggregator(this IServiceCollection services,
        Action<EventAggregatorOptions> configure = null)
    {
        services.AddSingleton<IEventAggregator, EventAggregator>();

        if (configure != null)
        {
            services.Configure(configure);
        }

        return services;
    }
}