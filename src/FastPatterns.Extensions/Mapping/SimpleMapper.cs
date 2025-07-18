namespace FastPatterns.Extensions.Mapping;
/// <summary>
/// Provides functionality to map properties from a source object of type <typeparamref name="TSource"/> 
/// to a target object of type <typeparamref name="TTarget"/>.
/// </summary>
/// <typeparam name="TSource">The type of the source object.</typeparam>
/// <typeparam name="TTarget">The type of the target object. Must have a parameterless constructor.</typeparam>
public static class SimpleMapper<TSource, TTarget> where TTarget : new()
{
  /// <summary>
  /// A compiled action that maps properties from the source object to the target object.
  /// </summary>
  private static readonly Action<TSource, TTarget> _mapAction = CreateMapAction();

  /// <summary>
  /// Creates a mapping action that assigns values from matching properties in the source object 
  /// to the target object.
  /// </summary>
  /// <returns>An action that performs the mapping.</returns>
  private static Action<TSource, TTarget> CreateMapAction()
  {
    var sourceProps = typeof(TSource).GetProperties();
    var targetProps = typeof(TTarget).GetProperties();
    var assignOps = new List<Action<TSource, TTarget>>();

    foreach (var sProp in sourceProps)
    {
      var tProp = targetProps.FirstOrDefault(p => p.Name == sProp.Name && p.PropertyType == sProp.PropertyType && p.CanWrite);
      if (tProp != null)
      {
        assignOps.Add((src, tgt) =>
        {
          if (src != null)
            tProp.SetValue(tgt, sProp.GetValue(src));
          // else: do nothing, or optionally set default value
        });
      }
    }

    return (src, tgt) => { foreach (var op in assignOps) op(src, tgt); };
  }

  /// <summary>
  /// Maps properties from the source object to a new instance of the target object.
  /// </summary>
  /// <param name="source">The source object to map from.</param>
  /// <returns>A new instance of the target object with mapped properties.</returns>
  public static TTarget Map(TSource? source)
  {
    if (source == null)
      return new TTarget(); // Return a new instance with default values if source is null
    var target = new TTarget();
    _mapAction(source, target);
    return target;
  }
}
