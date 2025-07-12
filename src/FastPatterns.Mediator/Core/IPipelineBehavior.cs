namespace FastPatterns.Mediator.Core;
/// <summary>
/// Defines a step in the request processing pipeline.
/// </summary>
public interface IPipelineBehavior<in TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
  /// <summary>
  /// Handles a request and optionally invokes the next step in the pipeline.
  /// </summary>
  /// <param name="request">The incoming request.</param>
  /// <param name="next">Delegate representing the next element in the pipeline.</param>
  /// <param name="cancellationToken">Token used to cancel the operation.</param>
  /// <returns>The response from the next step or handler.</returns>
  Task<TResponse> HandleAsync(
      TRequest request,
      RequestHandlerDelegate<TResponse> next,
      CancellationToken cancellationToken);
}

/// <summary>
/// Delegate representing the next request handler in the pipeline.
/// </summary>
public delegate Task<TResponse> RequestHandlerDelegate<TResponse>();
