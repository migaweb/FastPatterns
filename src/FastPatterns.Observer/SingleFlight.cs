namespace FastPatterns.Observer;

/// <summary>
/// Provides a single-flight "ensure once" mechanism that guarantees only one
/// producer task executes per key while concurrent callers await the same task.
/// </summary>
/// <typeparam name="TKey">The key used to identify in-flight operations.</typeparam>
/// <typeparam name="TResult">The result produced by the factory.</typeparam>
public sealed class SingleFlight<TKey, TResult>
  where TKey : notnull
{
  private readonly Dictionary<TKey, Task<TResult>> _inflight = [];
  private readonly Lock _lock = new();

  /// <summary>
  /// Executes the provided factory exactly once for the specified key.
  /// Concurrent callers with the same key await the same in-flight task
  /// rather than invoking the factory multiple times.
  /// </summary>
  /// <param name="key">The key that uniquely identifies the operation.</param>
  /// <param name="factory">The asynchronous function that produces the result.</param>
  /// <param name="ct">An optional cancellation token.</param>
  /// <returns>A task representing the single execution of the factory.</returns>
  public Task<TResult> RunAsync(TKey key, Func<CancellationToken, Task<TResult>> factory, CancellationToken ct = default)
  {
    lock (_lock)
    {
      if (_inflight.TryGetValue(key, out var existing))
        return existing;

      var t = RunCoreAsync(key, factory, ct);
      _inflight[key] = t;
      return t;
    }
  }

  private async Task<TResult> RunCoreAsync(TKey key, Func<CancellationToken, Task<TResult>> factory, CancellationToken ct)
  {
    try
    {
      return await factory(ct).ConfigureAwait(false);
    }
    finally
    {
      lock (_lock) _inflight.Remove(key);
    }
  }
}

