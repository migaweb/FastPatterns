namespace FastPatterns.Mediator.Core;
public readonly struct Unit : IEquatable<Unit>
{
  public static readonly Unit Value = new Unit();

  public override string ToString() => "()";

  public override int GetHashCode() => 0;

  public override bool Equals(object? obj) => obj is Unit;

  public bool Equals(Unit other) => true;

  public static bool operator ==(Unit left, Unit right) => true;

  public static bool operator !=(Unit left, Unit right) => false;
}
