using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Nefarius.Blazor.EventAggregator;

/// <summary>
///     Describes a class which can handle a particular type of message.
/// </summary>
/// <typeparam name="TMessage">The type of message to handle.</typeparam>
[SuppressMessage("ReSharper", "UnusedMember.Global")]
public interface IHandle<in TMessage>
{
    /// <summary>
    ///     Handles the message.
    /// </summary>
    /// <param name="message">The message.</param>
    /// <returns>A task that represents the operation.</returns>
    Task HandleAsync(TMessage message);
}