namespace FastPatterns.Mediator.Core.Validation;
/// <summary>  
/// Represents an exception that occurs during validation.  
/// </summary>  
public class ValidationException : Exception
{
  /// <summary>  
  /// Gets the collection of validation errors associated with this exception.  
  /// </summary>  
  public IReadOnlyCollection<ValidationError> Errors { get; }

  /// <summary>  
  /// Initializes a new instance of the <see cref="ValidationException"/> class with a specified error message.  
  /// </summary>  
  /// <param name="message">The message that describes the error.</param>  
  public ValidationException(string message) : base(message)
  {
    Errors = [];
  }

  /// <summary>  
  /// Initializes a new instance of the <see cref="ValidationException"/> class with a collection of validation errors.  
  /// </summary>  
  /// <param name="errors">The collection of validation errors.</param>  
  public ValidationException(IEnumerable<ValidationError> errors)
      : base("One or more validation errors occurred.")
  {
    Errors = [.. errors];
  }
}
