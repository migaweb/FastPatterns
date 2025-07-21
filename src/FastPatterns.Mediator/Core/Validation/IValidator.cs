namespace FastPatterns.Mediator.Core.Validation;
/// <summary>  
/// Defines a validator for a specific request type.  
/// </summary>  
/// <typeparam name="TRequest">The type of the request to validate.</typeparam>  
public interface IValidator<in TRequest>
{
  /// <summary>  
  /// Validates the specified request asynchronously.  
  /// </summary>  
  /// <param name="request">The request to validate.</param>  
  /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>  
  /// <returns>A task that represents the asynchronous validation operation.  
  /// The task result contains a collection of validation errors.</returns>  
  Task<IEnumerable<ValidationError>> ValidateAsync(TRequest request, CancellationToken cancellationToken);
}
