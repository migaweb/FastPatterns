namespace FastPatterns.Observer;
public sealed class SingleFlight<TKey, TResult>
  where TKey : notnull
{
  private readonly Dictionary<TKey, Task<TResult>> _inflight = new();
  private readonly object _lock = new();

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

