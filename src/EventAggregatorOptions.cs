// ReSharper disable once CheckNamespace

using Nefarius.Blazor.EventAggregator;
// ReSharper disable CheckNamespace

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
///     Options for <see cref="EventAggregator"/>.
/// </summary>
public class EventAggregatorOptions
{
    /// <summary>
    ///     If true, Event Aggregator tries to run ComponentBase.StateHasChanged for the target component after
    ///     it has handled the message.
    /// </summary>
    public bool AutoRefresh { get; set; }
}