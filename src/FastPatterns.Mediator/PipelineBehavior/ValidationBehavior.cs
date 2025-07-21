using FastPatterns.Mediator.Core;
using FastPatterns.Mediator.Core.Validation;

namespace FastPatterns.Mediator.PipelineBehavior;
/// <summary>  
/// Represents a pipeline behavior that performs validation on the incoming request.  
/// </summary>  
/// <typeparam name="TRequest">The type of the request.</typeparam>  
/// <typeparam name="TResponse">The type of the response.</typeparam>  
public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
  : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
  private readonly IEnumerable<IValidator<TRequest>> _validators = validators;

  /// <summary>  
  /// Handles the request by validating it and passing it to the next delegate in the pipeline.  
  /// </summary>  
  /// <param name="request">The request to be processed.</param>  
  /// <param name="next">The next delegate in the pipeline.</param>  
  /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>  
  /// <returns>The response from the next delegate in the pipeline.</returns>  
  /// <exception cref="ValidationException">Thrown when validation errors are found.</exception>  
  public async Task<TResponse> HandleAsync(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
  {
    var errors = new List<ValidationError>();

    foreach (var validator in _validators)
    {
      var result = await validator.ValidateAsync(request, cancellationToken);
      errors.AddRange(result);
    }

    if (errors.Count > 0)
      throw new ValidationException(errors);

    return await next();
  }
}
