namespace FastPatterns.Observer;
/// <summary>
/// Base class for managing and observing the state of a <see cref="ViewState" />.
/// </summary>
/// <typeparam name="TState">Type of the state managed by this instance.</typeparam>
public abstract class StateManager<TState>
  : Observer where TState : ViewState
{
  /// <summary>
  /// Gets the current state instance.
  /// </summary>
  public TState? State { get; protected set; }

  /// <summary>
  /// Updates the state and broadcasts the change when it differs from the
  /// previous value.
  /// </summary>
  /// <param name="newState">The new state value.</param>
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
