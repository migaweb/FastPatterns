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

  /// <summary>
  /// Maps a collection of <typeparamref name="TSource"/> objects to a list of <typeparamref name="TDest"/> objects,
  /// using a simple mapping strategy and an optional custom mapping action.
  /// The entire list is mapped eagerly and returned as a fully populated <see cref="List{TDest}"/>.
  /// </summary>
  /// <typeparam name="TSource">The source type to map from.</typeparam>
  /// <typeparam name="TDest">The destination type to map to. Must have a parameterless constructor.</typeparam>
  /// <param name="source">The source collection to map.</param>
  /// <param name="customMapping">
  /// An optional action that allows additional custom mapping logic between the source and destination instances.
  /// </param>
  /// <returns>A list of mapped <typeparamref name="TDest"/> objects.</returns>
  public static List<TDest> MapWith<TSource, TDest>(this IEnumerable<TSource> source, Action<TSource, TDest>? customMapping = null)
      where TDest : new()
  {

    var destCollection = new List<TDest>();

    foreach (var item in source)
    {
      var dest = SimpleMapper<TSource, TDest>.Map(item);

      if (customMapping is not null)
      {
        customMapping(item, dest);
      }

      destCollection.Add(dest);
    }
    
    return destCollection;
  }

  /// <summary>
  /// Lazily maps a collection of <typeparamref name="TSource"/> objects to a sequence of <typeparamref name="TDest"/> objects,
  /// using a simple mapping strategy and an optional custom mapping action.
  /// Items are mapped and returned one at a time using deferred execution via <c>yield return</c>.
  /// </summary>
  /// <typeparam name="TSource">The source type to map from.</typeparam>
  /// <typeparam name="TDest">The destination type to map to. Must have a parameterless constructor.</typeparam>
  /// <param name="source">The source collection to map.</param>
  /// <param name="customMapping">
  /// An optional action that allows additional custom mapping logic between the source and destination instances.
  /// </param>
  /// <returns>An <see cref="IEnumerable{TDest}"/> sequence of mapped items, evaluated lazily during enumeration.</returns>
  public static IEnumerable<TDest> MapLazyWith<TSource, TDest>(this IEnumerable<TSource> source, Action<TSource, TDest>? customMapping = null)
      where TDest : new()
  {
    foreach (var item in source)
    {
      var dest = SimpleMapper<TSource, TDest>.Map(item);

      if (customMapping is not null)
      {
        customMapping(item, dest);
      }

      yield return dest;
    }
  }
}
