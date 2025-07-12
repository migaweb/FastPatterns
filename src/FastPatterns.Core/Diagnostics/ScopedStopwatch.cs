using System.Diagnostics;

namespace FastPatterns.Core.Diagnostics;
/// <summary>
/// A struct that measures the elapsed time of a code block and invokes a callback with the elapsed time when disposed.
/// </summary>
public readonly struct ScopedStopwatch(Action<TimeSpan> onDispose) : IDisposable
{
  private readonly Stopwatch _stopwatch = Stopwatch.StartNew();
  private readonly Action<TimeSpan> _onDispose = onDispose;

  /// <summary>
  /// Stops the stopwatch and invokes the callback with the elapsed time.
  /// </summary>
  public void Dispose()
  {
    _stopwatch.Stop();
    _onDispose(_stopwatch.Elapsed);
  }
}
