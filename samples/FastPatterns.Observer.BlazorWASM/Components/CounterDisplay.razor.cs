using FastPatterns.Observer.BlazorWASM.StateManagers;
using Microsoft.AspNetCore.Components;

namespace FastPatterns.Observer.BlazorWASM.Components;

public partial class CounterDisplay : ComponentBase, IDisposable
{
  [Inject]
  private CounterStateManager CounterStateManager { get; set; } = default!;

  private int CurrentCount => CounterStateManager.State?.Count ?? 0;
  private DateTime LastUpdate => CounterStateManager.State?.LastUpdated ?? DateTime.MinValue;

  protected override void OnInitialized()
  {
    // Subscribe to state changes
    CounterStateManager.AddStateChangeListeners(OnStateChanged);
  }
  private void OnStateChanged()
  {
    // Update the display when the state changes
    StateHasChanged();
  }

  public void Dispose()
  {
    CounterStateManager.RemoveStateChangeListeners(OnStateChanged);
    GC.SuppressFinalize(this);
  }
}

