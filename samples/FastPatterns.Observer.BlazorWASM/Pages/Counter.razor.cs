using Microsoft.AspNetCore.Components;
using FastPatterns.Observer.BlazorWASM.StateManagers;

namespace FastPatterns.Observer.BlazorWASM.Pages;

public partial class Counter : ComponentBase
{
  [Inject]
  private CounterStateManager CounterStateManager { get; set; } = default!;
  
  protected override async Task OnAfterRenderAsync(bool firstRender)
  {
    await CounterStateManager.EnsureLoadedAsync();
    await base.OnAfterRenderAsync(firstRender);
  }

  private void IncrementCount()
  {
    CounterStateManager.IncrementCount();
  }
}
