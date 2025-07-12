# FastPatterns

A collection of lightweight design pattern implementations for .NET applications.

## Mediator
The `FastPatterns.Mediator` project provides a simple mediator with request/response and notification support.

- `IMediator` exposes `SendAsync` for requests and `PublishAsync` for notifications.
- The mediator resolves handlers and pipeline behaviors from dependency injection, invoking them in order.
- Request handlers implement `IRequestHandler<TRequest, TResponse>` while notifications use `INotificationHandler<TNotification>`.
- `IPipelineBehavior` allows cross-cutting code to run before and after a request handler.
- Extension methods register the mediator and all handlers with `IServiceCollection`.
- See `samples/FastPatterns.Mediator.WebApiSample` for a minimal API example.

## Observer
`FastPatterns.Observer` implements a basic observable state system.

- `Observer` manages subscriptions using `AddStateChangeListeners` and `BroadcastStateChange`.
- `StateManager<TState>` extends `Observer` and exposes a `SetState` method that only broadcasts when the state changes.
- `ViewState` records a timestamp and loading flag and can be extended with custom state data.
- The Blazor WebAssembly sample under `samples/FastPatterns.Observer.BlazorWASM` shows how to manage component state with these classes.

## ObservableComponentBase
`ObservableComponentBase<TState>` in `FastPatterns.Blazor` integrates the observer pattern with Blazor components. It:

- Injects a state manager of type `TState`.
- Subscribes to state change notifications in `OnInitialized` and triggers `StateHasChanged` when notified.
- Unsubscribes when disposed to prevent memory leaks.

These libraries provide small building blocks that can be composed to add mediator and observer patterns to .NET applications.
