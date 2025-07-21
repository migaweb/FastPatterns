namespace FastPatterns.Mediator.Core.Validation;

/// <summary>
/// Represents a validation error for a specific property of a request.
/// <param name="PropertyName">The name of the property.</param> 
/// <param name="ErrorMessage">Error message.</param> 
/// </summary>
public record ValidationError(string PropertyName, string ErrorMessage);
