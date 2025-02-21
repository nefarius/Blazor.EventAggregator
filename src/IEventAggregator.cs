﻿using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Nefarius.Blazor.EventAggregator;

/// <summary>
///     Event aggregator service instance.
/// </summary>
[SuppressMessage("ReSharper", "UnusedMember.Global")]
public interface IEventAggregator
{
    /// <summary>
    ///     Subscribes an instance to all events declared through implementations of <see cref="IHandle{TMessage}" />
    /// </summary>
    /// <param name="subscriber">The instance to subscribe for event publication.</param>
    void Subscribe(object subscriber);

    /// <summary>
    ///     Unsubscribes the instance from all events.
    /// </summary>
    /// <param name="subscriber">The instance to unsubscribe.</param>
    void Unsubscribe(object subscriber);

    /// <summary>
    ///     Publishes a message.
    /// </summary>
    /// <param name="message">The message instance.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task PublishAsync(object message);
}