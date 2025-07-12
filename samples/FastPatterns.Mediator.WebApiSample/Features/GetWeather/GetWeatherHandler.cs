using FastPatterns.Mediator.Core;
using FastPatterns.Mediator.WebApiSample.Domain;
using FastPatterns.Mediator.WebApiSample.Notifications;

namespace FastPatterns.Mediator.WebApiSample.Features.GetWeather;

public class GetWeatherQuery : IQuery<WeatherForecast[]>
{
}

public class GetWeatherHandler(IMediator mediator) : IRequestHandler<GetWeatherQuery, WeatherForecast[]>
{
  private static readonly string[] _summaries =
  [
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
  ];

  public async Task<WeatherForecast[]> HandleAsync(GetWeatherQuery request, CancellationToken cancellationToken)
  {
    var forecast = Enumerable.Range(1, 5).Select(index =>
            new WeatherForecast
            (
                DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                Random.Shared.Next(-20, 55),
                _summaries[Random.Shared.Next(_summaries.Length)]
            )).ToArray();

    await mediator.PublishAsync(new WeatherAlertNotification("Storm incoming!"), cancellationToken);

    return forecast;
  }
}
