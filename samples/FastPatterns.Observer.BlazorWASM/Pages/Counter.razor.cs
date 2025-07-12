using Microsoft.AspNetCore.Components;
using FastPatterns.Observer.BlazorWASM.StateManagers;

namespace FastPatterns.Observer.BlazorWASM.Pages;

public partial class Counter : ComponentBase
{
  [Inject]
  private CounterStateManager CounterStateManager { get; set; } = default!;
  protected override void OnInitialized()
  {
    currentCount = CounterStateManager.State?.Count ?? 0;
  }

  private int currentCount = 0;

  private void IncrementCount()
  {
    CounterStateManager.IncrementCount();
  }
}
