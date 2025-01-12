# IEventAggregator

Namespace: Nefarius.Blazor.EventAggregator

Event aggregator service instance.

```csharp
public interface IEventAggregator
```

## Methods

### <a id="methods-publishasync"/>**PublishAsync(Object)**

Publishes a message.

```csharp
Task PublishAsync(object message)
```

#### Parameters

`message` [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)<br>
The message instance.

#### Returns

A task that represents the asynchronous operation.

### <a id="methods-subscribe"/>**Subscribe(Object)**

Subscribes an instance to all events declared through implementations of [IHandle&lt;TMessage&gt;](./nefarius.blazor.eventaggregator.ihandle-1.md)

```csharp
void Subscribe(object subscriber)
```

#### Parameters

`subscriber` [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)<br>
The instance to subscribe for event publication.

### <a id="methods-unsubscribe"/>**Unsubscribe(Object)**

Unsubscribes the instance from all events.

```csharp
void Unsubscribe(object subscriber)
```

#### Parameters

`subscriber` [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)<br>
The instance to unsubscribe.
