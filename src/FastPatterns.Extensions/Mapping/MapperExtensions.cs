namespace FastPatterns.Extensions.Mapping;
/// <summary>
/// Provides extension methods for mapping objects with custom mapping logic.
/// </summary>
public static class MapperExtensions
{
  /// <summary>
  /// Maps the source object to a destination object of type <typeparamref name="TDest"/> using the specified custom mapping logic.
  /// </summary>
  /// <typeparam name="TSource">The type of the source object.</typeparam>
  /// <typeparam name="TDest">The type of the destination object.</typeparam>
  /// <param name="source">The source object to map.</param>
  /// <param name="customMapping">The custom mapping logic to apply.</param>
  /// <returns>A new instance of <typeparamref name="TDest"/> with the mapped values.</returns>
  public static TDest MapWith<TSource, TDest>(this TSource source, Action<TSource, TDest>? customMapping = null)
      where TDest : new()
  {
    var dest = SimpleMapper<TSource, TDest>.Map(source);

    if (customMapping is not null)
    {
      customMapping(source, dest);
    }
    
    return dest;
  }
}
