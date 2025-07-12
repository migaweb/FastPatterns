namespace FastPatterns.Mediator.Core;
/// <summary>
/// Handles a specific type of request and returns a response.
/// </summary>
public interface IRequestHandler<in TRequest, TResponse>
  where TRequest : IRequest<TResponse>
{
  /// <summary>
  /// Handles the request.
  /// </summary>
  /// <param name="request">The request instance.</param>
  /// <param name="cancellationToken">Token used to cancel the operation.</param>
  /// <returns>The response produced by the handler.</returns>
  public Task<TResponse> HandleAsync(TRequest request, CancellationToken cancellationToken);
}
