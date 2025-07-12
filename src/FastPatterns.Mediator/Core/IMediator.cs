namespace FastPatterns.Mediator.Core;
/// <summary>
/// Dispatches requests and notifications to their appropriate handlers.
/// </summary>
public interface IMediator
{
  /// <summary>
  /// Sends a request and returns a response.
  /// </summary>
  /// <param name="request">The request to send.</param>
  /// <param name="cancellationToken">Token used to cancel the operation.</param>
  /// <typeparam name="TResponse">Type of the response returned by the handler.</typeparam>
  Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default);

  /// <summary>
  /// Sends a request that does not produce a response.
  /// </summary>
  /// <param name="request">The request to send.</param>
  /// <param name="cancellationToken">Token used to cancel the operation.</param>
  Task SendAsync(IRequest request, CancellationToken cancellationToken = default);

  /// <summary>
  /// Publishes a notification to all registered handlers.
  /// </summary>
  /// <param name="notification">The notification instance.</param>
  /// <param name="cancellationToken">Token used to cancel the operation.</param>
  Task PublishAsync<TNotification>(TNotification notification, CancellationToken cancellationToken = default)
      where TNotification : INotification;
}
