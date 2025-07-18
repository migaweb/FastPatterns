namespace FastPatterns.Extensions.Mapping;
/// <summary>  
/// Provides functionality to map a source object to a target record type.  
/// </summary>  
public static class RecordMapper
{
  /// <summary>  
  /// Maps the properties of a source object to the constructor parameters of a target record type.  
  /// Allows optional custom overrides for constructor arguments.  
  /// </summary>  
  /// <typeparam name="TSource">The type of the source object.</typeparam>  
  /// <typeparam name="TTarget">The type of the target record.</typeparam>  
  /// <param name="source">The source object to map from.</param>  
  /// <param name="customOverrides">An optional function to provide custom overrides for constructor arguments.</param>  
  /// <returns>An instance of the target record type.</returns>  
  public static TTarget MapToRecord<TSource, TTarget>(TSource source, Func<TSource, Dictionary<string, object>>? customOverrides = null)
  {
    var ctor = typeof(TTarget).GetConstructors().First();
    var ctorParams = ctor.GetParameters();
    var srcProps = typeof(TSource).GetProperties();

    // Build dictionary of parameter name -> value  
    var values = new Dictionary<string, object?>();
    foreach (var param in ctorParams)
    {
      var srcProp = srcProps.FirstOrDefault(p => string.Equals(p.Name, param.Name, StringComparison.OrdinalIgnoreCase));
      if (srcProp != null)
        values[param.Name!] = srcProp.GetValue(source);
    }
    // Allow user to override any constructor arg  
    if (customOverrides != null)
    {
      foreach (var kvp in customOverrides(source))
        values[kvp.Key] = kvp.Value;
    }

    var args = ctorParams.Select(p => values.TryGetValue(p.Name!, out var v) ? v : GetDefault(p.ParameterType)).ToArray();
    return (TTarget)ctor.Invoke(args);
  }

  /// <summary>  
  /// Gets the default value for a given type.  
  /// </summary>  
  /// <param name="t">The type to get the default value for.</param>  
  /// <returns>The default value for the specified type.</returns>  
  private static object? GetDefault(Type t) => t.IsValueType ? Activator.CreateInstance(t) : null;
}
