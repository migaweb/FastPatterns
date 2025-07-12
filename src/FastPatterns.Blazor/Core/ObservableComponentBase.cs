using Microsoft.AspNetCore.Components;

namespace FastPatterns.Blazor.Core;
public abstract class ObservableComponentBase<TState> : ComponentBase, IDisposable
  where TState : Observer.Observer, new()
{
  [Inject] protected TState State { get; set; } = default!;

  protected override void OnInitialized()
  {
    base.OnInitialized();
    State?.AddStateChangeListeners(OnStateChanged);
  }

  private void OnStateChanged()
  {
    InvokeAsync(StateHasChanged);
  }

  public void Dispose()
  {
    State.RemoveStateChangeListeners(OnStateChanged);
    GC.SuppressFinalize(this);
  }
}
