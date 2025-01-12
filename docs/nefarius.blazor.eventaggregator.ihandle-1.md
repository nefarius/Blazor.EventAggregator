# IHandle&lt;TMessage&gt;

Namespace: Nefarius.Blazor.EventAggregator

Describes a class which can handle a particular type of message.

```csharp
public interface IHandle<TMessage>
```

#### Type Parameters

`TMessage`<br>
The type of message to handle.

## Methods

### <a id="methods-handleasync"/>**HandleAsync(TMessage)**

Handles the message.

```csharp
Task HandleAsync(TMessage message)
```

#### Parameters

`message` TMessage<br>
The message.

#### Returns

A task that represents the operation.
