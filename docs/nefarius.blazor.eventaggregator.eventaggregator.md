# EventAggregator

Namespace: Nefarius.Blazor.EventAggregator

```csharp
public class EventAggregator : IEventAggregator
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [EventAggregator](./nefarius.blazor.eventaggregator.eventaggregator.md)<br>
Implements [IEventAggregator](./nefarius.blazor.eventaggregator.ieventaggregator.md)

## Constructors

### <a id="constructors-.ctor"/>**EventAggregator(IOptions&lt;EventAggregatorOptions&gt;)**

```csharp
public EventAggregator(IOptions<EventAggregatorOptions> options)
```

#### Parameters

`options` IOptions&lt;EventAggregatorOptions&gt;<br>

## Methods

### <a id="methods-publishasync"/>**PublishAsync(Object)**

```csharp
public Task PublishAsync(object message)
```

#### Parameters

`message` [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)<br>

#### Returns

[Task](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task)

### <a id="methods-subscribe"/>**Subscribe(Object)**

```csharp
public void Subscribe(object subscriber)
```

#### Parameters

`subscriber` [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)<br>

### <a id="methods-unsubscribe"/>**Unsubscribe(Object)**

```csharp
public void Unsubscribe(object subscriber)
```

#### Parameters

`subscriber` [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)<br>
