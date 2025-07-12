namespace FastPatterns.Mediator.Core;
/// <summary>
/// Handles notifications of type <typeparamref name="TNotification"/>.
/// </summary>
public interface INotificationHandler<in TNotification>
    where TNotification : INotification
{
  /// <summary>
  /// Processes the notification.
  /// </summary>
  /// <param name="notification">The notification instance.</param>
  /// <param name="cancellationToken">Token used to cancel the operation.</param>
  Task HandleAsync(TNotification notification, CancellationToken cancellationToken);
}
