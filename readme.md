# Blazor.EventAggregator

Blazor.EventAggregator is a lightweight Event Aggregator for Blazor.

Event aggregator is used for indirect component to component communication. In event aggregator pattern you have message/event publishers and subscribers. In the case of Blazor, component can publish its events and other component(s) can react to those events.

[![NuGet](https://img.shields.io/nuget/v/EventAggregator.Blazor.svg)](https://www.nuget.org/packages/EventAggregator.Blazor/)

## Note

Blazor.EventAggregator is completely based on the work done in [Caliburn.Micro](https://caliburnmicro.com/). The source code was copied from it and then altered to work with Blazor.

Also note that the library has only been tested with the server-side version of Blazor. There's no known issues with the WebAssembly-version of Blazor.

## Getting Started

First register EventAggregator in app’s ConfigureServices:

```cs
public void ConfigureServices(IServiceCollection services)
{
    services.AddEventAggregator();
}
```

The rest depends on if you’re using components with code-behinds (inheritance) or without inheritance. The following guidance is for code-behind scenarios.

### Creating the publisher

To create an event publishing component, first inject IEventAggregator:

```cs
[Inject]
private IEventAggregator _eventAggregator { get; set; }
```

Then publish the message when something interesting happens:

```cs
await _eventAggregator.PublishAsync(new CounterIncreasedMessage());
```

Here’s a full example of a publisher:

```cs
public class CounterComponent : ComponentBase
{
    [Inject]
    private IEventAggregator _eventAggregator { get; set; }

    public int currentCount = 0;

    public async Task IncrementCountAsync()
    {
        currentCount++;
        await _eventAggregator.PublishAsync(new CounterIncreasedMessage());
    }
}

public class CounterIncreasedMessage
{
}
```

### Creating the subscriber

To create an event subscriber, also start by injecting IEventAggregator:

```cs
[Inject]
private IEventAggregator _eventAggregator { get; set; }
```

Then make sure to add and implement the `IHandle<TMessageType>` interface for all the event’s your component is interested in:

```cs
public class CounterListenerComponent : ComponentBase, IHandle<CounterIncreasedMessage>
...
public Task HandleAsync(CounterIncreasedMessage message)
{
    currentCount += 1;
    return Task.CompletedTask;
}
```

Here’s full example of a subscriber:

```cs
public class CounterListenerComponent : ComponentBase, IHandle<CounterIncreasedMessage>
{
    [Inject]
    private IEventAggregator _eventAggregator { get; set; }

    public int currentCount = 0;

    protected override void OnInit()
    {
        _eventAggregator.Subscribe(this);
    }

    public Task HandleAsync(CounterIncreasedMessage message)
    {
        currentCount += 1;
        return Task.CompletedTask;
    }
}
```

## Note about auto refresh

The library can try to automatically call subscriber component's StateHasChanged after it has handled the event. By default this functionality is disabled. You can enable it through options:

```cs
services.AddEventAggregator(options => options.AutoRefresh = true);
```

Auto refresh is based on reflection and it assumes that the subscriber inherits from ComponentBase.

## Samples

The project site contains a full working sample of the code-behind model in the samples-folder.

## Requirements

The library has been developed and tested using the following tools:

* .NET 7.0
* Visual Studio 2022

## Acknowledgements

Work is based on the code available in [Caliburn.Micro](https://caliburnmicro.com/).
