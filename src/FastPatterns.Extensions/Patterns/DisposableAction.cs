namespace FastPatterns.Extensions.Patterns;
/// <summary>
/// Represents an action that will be executed when the object is disposed.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="DisposableAction"/> struct.
/// </remarks>
/// <param name="disposeAction">The action to execute when the object is disposed.</param>
/// <exception cref="ArgumentNullException">Thrown when <paramref name="disposeAction"/> is null.</exception>
public struct DisposableAction(Action disposeAction) : IDisposable
{
  private readonly Action? _disposeAction = disposeAction ?? throw new ArgumentNullException(nameof(disposeAction));
  private int _disposed = 0;

  /// <summary>
  /// Executes the dispose action if it has not already been executed.
  /// </summary>
  public void Dispose()
  {
    if (Interlocked.Exchange(ref _disposed, 1) == 0)
    {
      _disposeAction?.Invoke();
    }
  }
}
