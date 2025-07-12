namespace FastPatterns.Observer;
public abstract class StateManager<TState> 
  : Observer where TState : ViewState
{
  public TState? State { get; protected set; }

  protected void SetState(TState newState)
  {
    // Only broadcast state change if the new state is different
    if (!EqualityComparer<TState>.Default.Equals(State, newState))
    {
      State = newState;
      BroadcastStateChange();
    }
  }
}
