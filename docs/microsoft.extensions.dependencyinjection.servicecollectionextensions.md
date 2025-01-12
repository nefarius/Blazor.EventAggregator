# ServiceCollectionExtensions

Namespace: Microsoft.Extensions.DependencyInjection

```csharp
public static class ServiceCollectionExtensions
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ServiceCollectionExtensions](./microsoft.extensions.dependencyinjection.servicecollectionextensions.md)

## Methods

### <a id="methods-addeventaggregator"/>**AddEventAggregator(IServiceCollection, Action&lt;EventAggregatorOptions&gt;)**

Adds IEventAggregator as a singleton

```csharp
public static IServiceCollection AddEventAggregator(IServiceCollection services, Action<EventAggregatorOptions> configure)
```

#### Parameters

`services` IServiceCollection<br>
Service collection

`configure` [Action&lt;EventAggregatorOptions&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.action-1)<br>
Optional configuration.

#### Returns

Service collection
