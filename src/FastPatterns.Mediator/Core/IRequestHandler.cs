namespace FastPatterns.Mediator.Core;
public interface IRequestHandler<in TRequest, TResponse>
  where TRequest : IRequest<TResponse>
{
  public Task<TResponse> HandleAsync(TRequest request, CancellationToken cancellationToken);
}
