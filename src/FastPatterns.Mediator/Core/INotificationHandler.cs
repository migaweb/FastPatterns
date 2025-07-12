namespace FastPatterns.Mediator.Core;
public interface INotificationHandler<in TNotification>
    where TNotification : INotification
{
  Task HandleAsync(TNotification notification, CancellationToken cancellationToken);
}
