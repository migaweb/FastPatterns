namespace FastPatterns.Observer;
/// <summary>
/// Represents immutable state shared by <see cref="StateManager{TState}"/>.
/// </summary>
public abstract record ViewState
{
  /// <summary>
  /// Gets the time when the state was last updated.
  /// </summary>
  public DateTime LastUpdated { get; init; } = DateTime.UtcNow;

  /// <summary>
  /// Indicates whether the state is currently loading.
  /// </summary>
  public bool IsLoading { get; init; }
}
