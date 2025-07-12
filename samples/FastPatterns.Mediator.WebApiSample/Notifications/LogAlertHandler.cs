using FastPatterns.Mediator.Core;

namespace FastPatterns.Mediator.WebApiSample.Notifications;

public class LogAlertHandler : INotificationHandler<WeatherAlertNotification>
{
  public Task HandleAsync(WeatherAlertNotification notification, CancellationToken cancellationToken)
  {
    Console.WriteLine($"[Logger] Alert: {notification.Message}");
    return Task.CompletedTask;
  }
}
