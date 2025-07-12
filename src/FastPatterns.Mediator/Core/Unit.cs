namespace FastPatterns.Mediator.Core;
/// <summary>
/// Represents the absence of a value. Equivalent to <c>void</c> for generic
/// type parameters.
/// </summary>
public readonly struct Unit : IEquatable<Unit>
{
  /// <summary>
  /// Singleton instance of <see cref="Unit"/>.
  /// </summary>
  public static readonly Unit Value = new Unit();

  /// <inheritdoc />
  public override string ToString() => "()";

  /// <inheritdoc />
  public override int GetHashCode() => 0;

  /// <inheritdoc />
  public override bool Equals(object? obj) => obj is Unit;

  /// <inheritdoc />
  public bool Equals(Unit other) => true;

  /// <summary>
  /// Checks equality between two <see cref="Unit"/> values.
  /// </summary>
  public static bool operator ==(Unit left, Unit right) => true;

  /// <summary>
  /// Checks inequality between two <see cref="Unit"/> values.
  /// </summary>
  public static bool operator !=(Unit left, Unit right) => false;
}
