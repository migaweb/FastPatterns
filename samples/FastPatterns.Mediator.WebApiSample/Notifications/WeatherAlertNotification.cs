using FastPatterns.Mediator.Core;

namespace FastPatterns.Mediator.WebApiSample.Notifications;

public class WeatherAlertNotification(string message) : INotification
{
  public string Message { get; } = message;
}
