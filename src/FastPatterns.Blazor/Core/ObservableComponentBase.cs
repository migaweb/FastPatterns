using Microsoft.AspNetCore.Components;

namespace FastPatterns.Blazor.Core;
/// <summary>
/// Base component that wires up a state manager and automatically re-renders
/// when the state changes.
/// </summary>
/// <typeparam name="TState">Type of the state manager used by the component.</typeparam>
public abstract class ObservableComponentBase<TState> : ComponentBase, IDisposable
  where TState : Observer.Observer, new()
{
  /// <summary>
  /// Gets or sets the injected state manager instance.
  /// </summary>
  [Inject] protected TState State { get; set; } = default!;

  /// <summary>
  /// Registers the component with the state manager when initialized.
  /// </summary>
  protected override void OnInitialized()
  {
    base.OnInitialized();
    State?.AddStateChangeListeners(OnStateChanged);
  }

  private void OnStateChanged()
  {
    InvokeAsync(StateHasChanged);
  }

  /// <summary>
  /// Removes the listener when the component is disposed.
  /// </summary>
  public void Dispose()
  {
    State.RemoveStateChangeListeners(OnStateChanged);
    GC.SuppressFinalize(this);
  }
}
