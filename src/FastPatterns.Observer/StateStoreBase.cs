namespace FastPatterns.Observer;

/// <summary>
/// Base class for stores that manage view state and ensure the initial state
/// is loaded only once using the single-flight pattern.
/// </summary>
/// <typeparam name="TState">The type of view state maintained by the store.</typeparam>
public abstract class StateStoreBase<TState>
  where TState : ViewState, new()
{
  private readonly SingleFlight<string, TState> _sf = new();
  protected TState? _state;

  /// <summary>
  /// Gets the current state value.
  /// </summary>
  public TState? State => _state;

  /// <summary>
  /// Gets a value indicating whether the state has been loaded and is not
  /// currently in a loading state.
  /// </summary>
  public bool IsLoaded => _state is not null && !_state.IsLoading;

  /// <summary>
  /// Loads the state from the underlying source. Implemented by derived classes.
  /// </summary>
  /// <param name="ct">A cancellation token to observe.</param>
  /// <returns>The loaded state or <c>null</c> if loading failed.</returns>
  protected abstract Task<TState?> LoadCoreAsync(CancellationToken ct);

  /// <summary>
  /// Sets the current state and broadcasts the change if the value is different
  /// from the existing state.
  /// </summary>
  /// <param name="newState">The new state value.</param>
  protected void SetState(TState newState)
  {
    // Only broadcast state change if the new state is different
    if (!EqualityComparer<TState>.Default.Equals(State, newState))
    {
      _state = newState;
      BroadcastStateChange();
    }
  }

  /// <summary>
  /// Ensures the state has been loaded, using the single-flight mechanism to
  /// perform the load operation only once.
  /// </summary>
  /// <param name="ct">An optional cancellation token.</param>
  /// <returns>A task that completes when the state is loaded.</returns>
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

  /// <summary>
  /// Reloads the state regardless of its current value.
  /// </summary>
  /// <param name="ct">An optional cancellation token.</param>
  /// <returns>A task representing the reload operation.</returns>
  public async Task ReloadAsync(CancellationToken ct = default)
  {
    _state = new TState { IsLoading = true };
    BroadcastStateChange();

    var result = await LoadCoreAsync(ct).ConfigureAwait(false);
    _state = result ?? new TState { IsLoading = false };
    BroadcastStateChange();
  }

  /// <summary>
  /// Clears the current state and notifies listeners of the change.
  /// </summary>
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

