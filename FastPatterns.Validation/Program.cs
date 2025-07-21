// See https://aka.ms/new-console-template for more information

using FastPatterns.Mediator.Core;
using FastPatterns.Mediator.Core.Validation;
using FastPatterns.Mediator.Infrastructure.Configuration;
using FastPatterns.Validation;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
services.AddMediator().AddValidationBehavior();

var serviceProvider = services.BuildServiceProvider();

var mediator = serviceProvider.GetRequiredService<IMediator>();

try
{
  var result = await mediator.SendAsync(new CreateUserCommand()
  {
    Name = "John Doe",
    Password = "johnDoe1",
    PasswordMatch = "jonDoe1",
    Username = "doe87"
  });

  Console.WriteLine($"User creation result: {result}");

}
catch (ValidationException ex)
{
  Console.WriteLine("Validation errors occurred:");
  foreach (var error in ex.Errors)
  {
    Console.WriteLine($"Property: {error.PropertyName}, Error: {error.ErrorMessage}");
  }
  return;
}