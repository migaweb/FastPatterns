
namespace FastPatterns.Observer.BlazorWASM.StateManagers;

public record CounterState() : ViewState
{
  public int Count { get; set; }
}

public class CounterStateManager : StateStoreBase<CounterState>
{
  public void IncrementCount()
  {
    // Increment the count and update the state
    var newCount = (State?.Count ?? 0) + 1;
    SetState(new CounterState() { Count = newCount });
  }

  protected override async Task<CounterState?> LoadCoreAsync(CancellationToken ct)
  {
    await Task.Delay(2000, ct); // Simulate a delay for loading
    return new CounterState() { Count = 0 };
  }
}
