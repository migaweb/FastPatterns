using FastPatterns.Mediator.Core;
using FastPatterns.Mediator.Core.Validation;
using FastPatterns.Mediator.PipelineBehavior;
using Microsoft.Extensions.DependencyInjection;

namespace FastPatterns.Mediator.Infrastructure.Configuration;
/// <summary>
/// Extension methods for registering mediator services with
/// <see cref="IServiceCollection"/>.
/// </summary>
public static class MediatorServiceCollectionExtensions
{
  /// <summary>
  /// Adds validation behavior to the mediator pipeline.
  /// </summary>
  /// <param name="services">The service collection to configure.</param>
  /// <returns>The updated service collection.</returns>
  public static IServiceCollection AddValidationBehavior(this IServiceCollection services)
  {
    services.AddPipeline(typeof(ValidationBehavior<,>));

    var validatorInterface = typeof(IValidator<>);

    services.Scan(scan => scan
        .FromApplicationDependencies()
        .AddClasses(c => c.AssignableTo(validatorInterface))
        .AsImplementedInterfaces()
        .WithTransientLifetime());

    return services;
  }

  /// <summary>
  /// Registers a pipeline behavior as an open generic type.
  /// </summary>
  /// <param name="services">The service collection to configure.</param>
  /// <param name="behaviorType">The open generic behavior type.</param>
  /// <returns>The updated service collection.</returns>
  /// <exception cref="ArgumentException">
  /// Thrown when <paramref name="behaviorType"/> is not an open generic type.
  /// </exception>
  public static IServiceCollection AddPipeline(this IServiceCollection services, Type behaviorType)
  {
    if (!behaviorType.IsGenericTypeDefinition)
      throw new ArgumentException("Must be open generic type like typeof(LoggingBehavior<,>)");

    services.AddTransient(typeof(IPipelineBehavior<,>), behaviorType);
    return services;
  }

  /// <summary>
  /// Adds the core mediator services and discovers handlers from the application
  /// dependencies.
  /// </summary>
  /// <param name="services">The service collection to configure.</param>
  /// <returns>The updated service collection.</returns>
  public static IServiceCollection AddMediator(this IServiceCollection services)
  {
    services.AddTransient<IMediator, Mediator>();
   
    services.AddRequestHandlers();

    services.AddNotificationHandlers();

    return services;
  }

  /// <summary>
  /// Registers all request handlers found in application dependencies.
  /// </summary>
  private static IServiceCollection AddRequestHandlers(this IServiceCollection services)
  {
    services.Scan(scan => scan
        .FromApplicationDependencies()
        .AddClasses(c => c.AssignableTo(typeof(IRequestHandler<,>)))
        .AsImplementedInterfaces()
        .WithTransientLifetime());
    return services;
  }

  /// <summary>
  /// Registers all notification handlers found in application dependencies.
  /// </summary>
  /// <param name="services">The service collection to configure.</param>
  /// <returns>The updated service collection.</returns>
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
