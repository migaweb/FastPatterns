namespace FastPatterns.Observer.BlazorWASM.StateManagers;

public record CounterState(int Count) : ViewState;

public class CounterStateManager : StateManager<CounterState>
{
  public void IncrementCount()
  {
    // Increment the count and update the state
    var newCount = (State?.Count ?? 0) + 1;
    SetState(new CounterState(newCount));
  }
}
