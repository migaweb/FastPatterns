using FastPatterns.Core.Diagnostics;
using Microsoft.Extensions.Logging;

namespace FastPatterns.Extensions.Logging;
/// <summary>
/// Provides extension methods for logging operations.
/// </summary>
public static class LoggerExtensions
{
  /// <summary>
  /// Creates a timed scope for logging the duration of an operation.
  /// </summary>
  /// <param name="logger">The logger instance to use for logging.</param>
  /// <param name="operationName">The name of the operation being timed.</param>
  /// <param name="level">The log level to use for the log entry. Defaults to Information.</param>
  /// <returns>An <see cref="IDisposable"/> that logs the elapsed time when disposed.</returns>
  public static IDisposable TimedScope(this ILogger logger, string operationName, LogLevel level = LogLevel.Information)
  {
    return new ScopedStopwatch(elapsed =>
        logger.Log(level, "{Operation} completed in {ElapsedMilliseconds} ms", operationName, elapsed.TotalMilliseconds));
  }
}
