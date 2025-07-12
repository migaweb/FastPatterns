namespace FastPatterns.Core.Entities;
/// <summary>
/// Represents the base entity with common properties for all entities.
/// </summary>
public abstract class BaseEntity
{
  /// <summary>
  /// Gets or sets the unique identifier for the entity.
  /// </summary>
  public int Id { get; set; }

  /// <summary>
  /// Gets or sets the date and time when the entity was created.
  /// </summary>
  public DateTime Created { get; set; }

  /// <summary>
  /// Gets or sets the date and time when the entity was last changed.
  /// </summary>
  public DateTime Changed { get; set; }

  /// <summary>
  /// Gets or sets the username of the user who created the entity.
  /// </summary>
  public string CreatedBy { get; set; } = string.Empty;

  /// <summary>
  /// Gets or sets the username of the user who last changed the entity.
  /// </summary>
  public string ChangedBy { get; set; } = string.Empty;
}
