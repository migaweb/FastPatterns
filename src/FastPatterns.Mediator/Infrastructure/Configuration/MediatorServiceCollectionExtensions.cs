using FastPatterns.Mediator.Core;
using Microsoft.Extensions.DependencyInjection;

namespace FastPatterns.Mediator.Infrastructure.Configuration;
public static class MediatorServiceCollectionExtensions
{
  public static IServiceCollection AddPipeline(this IServiceCollection services, Type behaviorType)
  {
    if (!behaviorType.IsGenericTypeDefinition)
      throw new ArgumentException("Must be open generic type like typeof(LoggingBehavior<,>)");

    services.AddTransient(typeof(IPipelineBehavior<,>), behaviorType);
    return services;
  }

  public static IServiceCollection AddMediator(this IServiceCollection services)
  {
    services.AddTransient<IMediator, Mediator>();
   
    services.AddRequestHandlers();

    services.AddNotificationHandlers();

    return services;
  }

  private static IServiceCollection AddRequestHandlers(this IServiceCollection services)
  {
    services.Scan(scan => scan
        .FromApplicationDependencies()
        .AddClasses(c => c.AssignableTo(typeof(IRequestHandler<,>)))
        .AsImplementedInterfaces()
        .WithTransientLifetime());
    return services;
  }

  public static IServiceCollection AddNotificationHandlers(this IServiceCollection services)
  {
    services.Scan(scan => scan
        .FromApplicationDependencies()
        .AddClasses(c => c.AssignableTo(typeof(INotificationHandler<>)))
        .AsImplementedInterfaces()
        .WithTransientLifetime());
    return services;
  }
}
