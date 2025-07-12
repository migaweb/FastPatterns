using FastPatterns.Core.Abstractions;
using FastPatterns.Extensions.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FastPatterns.Extensions.Infrastructure.Configuration;
/// <summary>
/// Provides extension methods for configuring services related to FastPatterns.
/// </summary>
public static class ExtensionsServiceCollectionExtensions
{
  /// <summary>
  /// Adds data protection services and configures security options for FastPatterns.
  /// </summary>
  /// <param name="services">The service collection to add the services to.</param>
  /// <param name="configuration">The application configuration.</param>
  /// <returns>The updated service collection.</returns>
  /// <exception cref="InvalidOperationException">Thrown if the required configuration section is missing.</exception>
  public static IServiceCollection AddFastPatternDataProtection(this IServiceCollection services, IConfiguration configuration)
  {
    services.AddDataProtection();

    var section = configuration.GetSection(SecurityOptions.Security);
    if (!section.Exists())
    {
      throw new InvalidOperationException($"Missing configuration section: {SecurityOptions.Security}");
    }

    services.Configure<SecurityOptions>(section);

    services.AddSingleton<IDataProtectionService, DataProtectionService>();

    return services;
  }
}
