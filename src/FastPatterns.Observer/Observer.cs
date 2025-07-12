namespace FastPatterns.Observer;

public class Observer
{
  protected Action listeners = default!;

  public void AddStateChangeListeners(Action listener)
  {
    if (listener is not null)
      listeners += listener;
  }

  public void RemoveStateChangeListeners(Action listener)
  {
    if (listener is not null)
#pragma warning disable CS8601 // Possible null reference assignment.
      listeners -= listener;
#pragma warning restore CS8601 // Possible null reference assignment.
  }

  public void BroadcastStateChange()
  {
    listeners?.Invoke();
  }
}
