using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Nefarius.Blazor.EventAggregator;

/// <inheritdoc />
[SuppressMessage("ReSharper", "UnusedMember.Local")]
[SuppressMessage("ReSharper", "ClassWithVirtualMembersNeverInherited.Global")]
public class EventAggregator : IEventAggregator
{
    private readonly List<Handler> _handlers = new();
    private readonly EventAggregatorOptions _options;

    public EventAggregator(IOptions<EventAggregatorOptions> options)
    {
        _options = options.Value;
    }

    /// <inheritdoc />
    public virtual void Subscribe(object subscriber)
    {
        ArgumentNullException.ThrowIfNull(subscriber);

        lock (_handlers)
        {
            if (_handlers.Any(x => x.Matches(subscriber)))
            {
                return;
            }

            _handlers.Add(new Handler(subscriber));
        }
    }

    /// <inheritdoc />
    public virtual void Unsubscribe(object subscriber)
    {
        ArgumentNullException.ThrowIfNull(subscriber);

        lock (_handlers)
        {
            Handler handlersFound = _handlers.FirstOrDefault(x => x.Matches(subscriber));

            if (handlersFound != null)
            {
                _handlers.Remove(handlersFound);
            }
        }
    }

    public virtual async Task PublishAsync(object message)
    {
        ArgumentNullException.ThrowIfNull(message);

        Handler[] handlersToNotify;

        lock (_handlers)
        {
            handlersToNotify = _handlers.ToArray();
        }

        Type messageType = message.GetType();

        IEnumerable<Task> tasks = handlersToNotify.Select(h => h.Handle(messageType, message));

        await Task.WhenAll(tasks);

        if (_options.AutoRefresh)
        {
            foreach (Handler handler in handlersToNotify.Where(x => !x.IsDead))
            {
                if (handler.Reference.Target is not ComponentBase component)
                {
                    continue;
                }

                MethodInfo invoker = component.GetType().GetMethods(BindingFlags.Instance | BindingFlags.NonPublic)
                    .FirstOrDefault(x =>
                        string.Equals(x.Name, "InvokeAsync") &&
                        x.GetParameters().FirstOrDefault()?.ParameterType == typeof(Action));

                if (invoker == null)
                {
                    continue;
                }

                MethodInfo stateHasChangedMethod = component.GetType()
                    .GetMethod("StateHasChanged", BindingFlags.Instance | BindingFlags.NonPublic);

                if (stateHasChangedMethod == null)
                {
                    continue;
                }

                object[] args = [new Action(() => stateHasChangedMethod.Invoke(component, null))];
                Task tOut = (Task)invoker.Invoke(component, args);

                await tOut;
            }
        }

        List<Handler> deadHandlers = handlersToNotify.Where(h => h.IsDead).ToList();

        if (deadHandlers.Count != 0)
        {
            lock (_handlers)
            {
                foreach (Handler item in deadHandlers)
                {
                    _handlers.Remove(item);
                }
            }
        }
    }

    private class Handler
    {
        private readonly Dictionary<Type, MethodInfo> _supportedHandlers = new();

        public Handler(object handler)
        {
            Reference = new WeakReference(handler);

            IEnumerable<Type> interfaces = handler.GetType().GetTypeInfo().ImplementedInterfaces
                .Where(x => x.GetTypeInfo().IsGenericType && x.GetGenericTypeDefinition() == typeof(IHandle<>));

            foreach (Type handleInterface in interfaces)
            {
                Type type = handleInterface.GetTypeInfo().GenericTypeArguments[0];
                MethodInfo method = handleInterface.GetRuntimeMethod("HandleAsync", [type]);

                if (method != null)
                {
                    _supportedHandlers[type] = method;
                }
            }
        }

        public bool IsDead => Reference.Target == null;

        public WeakReference Reference { get; }

        public bool Matches(object instance)
        {
            return Reference.Target == instance;
        }

        public Task Handle(Type messageType, object message)
        {
            object target = Reference.Target;

            if (target == null)
            {
                return Task.FromResult(false);
            }

            List<Task> tasks = _supportedHandlers
                .Where(handler => handler.Key.GetTypeInfo().IsAssignableFrom(messageType.GetTypeInfo()))
                .Select(pair => pair.Value.Invoke(target, [message]))
                .Select(result => (Task)result)
                .ToList();

            return Task.WhenAll(tasks);
        }

        public bool Handles(Type messageType)
        {
            return _supportedHandlers.Any(
                pair => pair.Key.GetTypeInfo().IsAssignableFrom(messageType.GetTypeInfo()));
        }
    }
}