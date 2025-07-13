# FastPatterns

A collection of lightweight design pattern implementations for .NET applications.

## Core
`FastPatterns.Core` provides foundational utilities and building blocks for .NET applications. It includes:

- **ScopedStopwatch**: A struct for measuring the elapsed time of code blocks, invoking a callback with the elapsed time when disposed. Useful for lightweight performance diagnostics and timing operations.

```csharp

using (new ScopedStopwatch(elapsed => 
         Console.WriteLine($"Elapsed: {elapsed.TotalMilliseconds} ms")))
{
    Task.Delay(1000).Wait(); // simulate work
}

```

## Extensions
`FastPatterns.Extensions` offers reusable extension methods and utility types to enhance .NET development, focusing on logging, diagnostics, and resource management:

- **LoggerExtensions**: Extension methods for `ILogger` to simplify logging timed operations. The `TimedScope` method creates a disposable scope that logs the duration of an operation when disposed.
- **DisposableAction**: A struct that executes a provided action when disposed. Useful for managing temporary event subscriptions, resource cleanup, or scoped logic where an action should run at the end of a block.

### DisposableAction Example
```csharp
void HookTemporaryEvent(Button btn)
{
    btn.Click += OnClick;

    using var _ = new DisposableAction(() => btn.Click -= OnClick);
    // Scoped logic...
}
```
### Logger Example
```csharp
public class OrderService
{
    private readonly ILogger<OrderService> _logger;

    public OrderService(ILogger<OrderService> logger)
    {
        _logger = logger;
    }

    public async Task ProcessOrderAsync()
    {
        using (_logger.TimedScope("ProcessOrder"))
        {
            await Task.Delay(500); // simulate work
        }
    }
}
```
### Encrypting EF Core Columns using DataProtectionService

You can use the `IDataProtectionService` to encrypt specific EF Core properties like this:
var encryptionConverter = new ValueConverter<string, string>(
    v => dataProtectionService.Encrypt(v),
    v => dataProtectionService.Decrypt(v)
);

modelBuilder.Entity<Position>()
    .Property(e => e.Name)
    .HasConversion(encryptionConverter);#### 🔧 Setup in Program.cs
builder.Services.AddFastPatternDataProtection(builder.Configuration);"FastPatterns:Extensions:Security": {
  "EncryptionKey": "your-key-here"
}
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
