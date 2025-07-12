namespace FastPatterns.Observer;

/// <summary>
/// Provides basic functionality for managing listeners and broadcasting state
/// changes.
/// </summary>
public class Observer
{
  /// <summary>
  /// The registered listeners that are invoked when the state changes.
  /// </summary>
  protected Action listeners = default!;

  /// <summary>
  /// Registers a listener to be notified when the state changes.
  /// </summary>
  /// <param name="listener">The callback to register.</param>
  public void AddStateChangeListeners(Action listener)
  {
    if (listener is not null)
      listeners += listener;
  }

  /// <summary>
  /// Unregisters a previously added listener.
  /// </summary>
  /// <param name="listener">The callback to remove.</param>
  public void RemoveStateChangeListeners(Action listener)
  {
    if (listener is not null)
#pragma warning disable CS8601 // Possible null reference assignment.
      listeners -= listener;
#pragma warning restore CS8601 // Possible null reference assignment.
  }

  /// <summary>
  /// Invokes all registered listeners.
  /// </summary>
  public void BroadcastStateChange()
  {
    listeners?.Invoke();
  }
}
