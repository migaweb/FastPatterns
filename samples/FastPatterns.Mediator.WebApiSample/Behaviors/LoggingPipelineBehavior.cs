using FastPatterns.Mediator.Core;
using System.Diagnostics;

namespace FastPatterns.Mediator.WebApiSample.Behaviors;

public class LoggingPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
  public async Task<TResponse> HandleAsync(
      TRequest request,
      RequestHandlerDelegate<TResponse> next,
      CancellationToken cancellationToken)
  {
    var requestName = typeof(TRequest).Name;
    Console.WriteLine($"[Pipeline] Handling {requestName}");

    var stopwatch = Stopwatch.StartNew();
    try
    {
      var response = await next();
      stopwatch.Stop();
      Console.WriteLine($"[Pipeline] Handled {requestName} in {stopwatch.ElapsedMilliseconds}ms");
      return response;
    }
    catch (Exception ex)
    {
      stopwatch.Stop();
      Console.WriteLine($"[Pipeline] Error in {requestName}: {ex.Message} ({stopwatch.ElapsedMilliseconds}ms)");
      throw;
    }
  }
}

