# EventAggregatorOptions

Namespace: Microsoft.Extensions.DependencyInjection

Options for [EventAggregator](./nefarius.blazor.eventaggregator.eventaggregator.md).

```csharp
public class EventAggregatorOptions
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [EventAggregatorOptions](./microsoft.extensions.dependencyinjection.eventaggregatoroptions.md)

## Properties

### <a id="properties-autorefresh"/>**AutoRefresh**

If true, Event Aggregator tries to run ComponentBase.StateHasChanged for the target component after
 it has handled the message.

```csharp
public bool AutoRefresh { get; set; }
```

#### Property Value

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

## Constructors

### <a id="constructors-.ctor"/>**EventAggregatorOptions()**

```csharp
public EventAggregatorOptions()
```
