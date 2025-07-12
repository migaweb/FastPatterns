using FastPatterns.Mediator.Core;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
namespace FastPatterns.Mediator.Infrastructure;
/// <summary>
/// Default implementation of <see cref="IMediator"/> resolving handlers from an
/// <see cref="IServiceProvider"/>.
/// </summary>
/// <param name="serviceProvider">The service provider used to resolve handlers.</param>
public class Mediator(IServiceProvider serviceProvider) : IMediator
{
  private readonly IServiceProvider _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));

  /// <inheritdoc />
  public async Task PublishAsync<TNotification>(TNotification notification, CancellationToken cancellationToken = default) where TNotification : INotification
  {
    var handlers = _serviceProvider.GetServices<INotificationHandler<TNotification>>();
    var tasks = handlers.Select(h => h.HandleAsync(notification, cancellationToken));
    await Task.WhenAll(tasks);
  }
  /// <inheritdoc />
  public Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
  {
    var requestType = request.GetType();
    var method = typeof(Mediator)
        .GetMethod(nameof(SendInternal), BindingFlags.NonPublic | BindingFlags.Instance)!
        .MakeGenericMethod(requestType, typeof(TResponse));
    return (Task<TResponse>)method.Invoke(this, [request, cancellationToken])!;
  }
  /// <inheritdoc />
  public async Task SendAsync(IRequest request, CancellationToken cancellationToken = default)
  {
    var typed = (IRequest<Unit>)request;
    await SendAsync<Unit>(typed, cancellationToken);
  }
  #region Private methods
  private Task<TResponse> SendInternal<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken)
    where TRequest : IRequest<TResponse>
  {
    return InvokePipeline<TRequest, TResponse>(request, cancellationToken);
  }
  private Task<TResponse> InvokePipeline<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken)
            where TRequest : IRequest<TResponse>
  {
    var behaviors = _serviceProvider.GetServices<IPipelineBehavior<TRequest, TResponse>>().ToList();
    RequestHandlerDelegate<TResponse> handlerDelegate = async () =>
    {
      var handler = _serviceProvider.GetRequiredService<IRequestHandler<TRequest, TResponse>>();
      return await handler.HandleAsync(request, cancellationToken);
    };
    foreach (var behavior in behaviors)
    {
      var next = handlerDelegate;
      handlerDelegate = () => behavior.HandleAsync(request, next, cancellationToken);
    }
    return handlerDelegate();
  }
  #endregion
}
