namespace FastPatterns.Observer;
public abstract class StateStoreBase<TState>
  where TState : ViewState, new()
{
  private readonly SingleFlight<string, TState> _sf = new();
  protected TState? _state;

  public TState? State => _state;
  public bool IsLoaded => _state is not null && !_state.IsLoading;

  protected abstract Task<TState?> LoadCoreAsync(CancellationToken ct);

  protected void SetState(TState newState)
  {
    // Only broadcast state change if the new state is different
    if (!EqualityComparer<TState>.Default.Equals(State, newState))
    {
      _state = newState;
      BroadcastStateChange();
    }
  }

  public Task EnsureLoadedAsync(CancellationToken ct = default)
    => _sf.RunAsync("__singleton__", async _ =>
    {
      if (IsLoaded) return _state!;

      // publish "loading" snapshot
      _state = new TState { IsLoading = true };
      BroadcastStateChange();

      var result = await LoadCoreAsync(ct).ConfigureAwait(false);
      _state = result ?? new TState { IsLoading = false };
      BroadcastStateChange();
      return _state!;
    }, ct);

  public async Task ReloadAsync(CancellationToken ct = default)
  {
    _state = new TState { IsLoading = true };
    BroadcastStateChange();

    var result = await LoadCoreAsync(ct).ConfigureAwait(false);
    _state = result ?? new TState { IsLoading = false };
    BroadcastStateChange();
  }

  public void Invalidate()
  {
    _state = null;
    BroadcastStateChange();
  }

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

