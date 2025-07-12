namespace FastPatterns.Observer;
public abstract record ViewState
{
  public DateTime LastUpdated { get; init; } = DateTime.UtcNow;
  public bool IsLoading { get; init; }
}
