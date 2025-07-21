using FastPatterns.Mediator.Core;
using FastPatterns.Mediator.Core.Validation;

namespace FastPatterns.Validation;

public record CreateUserCommand : ICommand<bool>
{
  public string? Name { get; set; }
  public string? Username { get; set; }
  public string? Password { get; set; }
  public string? PasswordMatch { get; set; }
}

public class CreateUserCommandValidator : IValidator<CreateUserCommand>
{
  public Task<IEnumerable<ValidationError>> ValidateAsync(CreateUserCommand request, CancellationToken cancellationToken)
  {
    List<ValidationError> errors = [];

    if (string.IsNullOrWhiteSpace(request.Name))
    {
      errors.Add(new ValidationError(nameof(request.Name), "Name is required."));
    }
    if (string.IsNullOrWhiteSpace(request.Username))
    {
      errors.Add(new ValidationError(nameof(request.Username), "Username is required."));
    }
    if (string.IsNullOrWhiteSpace(request.Password))
    {
      errors.Add(new ValidationError(nameof(request.Password), "Password is required."));
    }
    if (request.Password != request.PasswordMatch)
    {
      errors.Add(new ValidationError(nameof(request.PasswordMatch), "Passwords must match."));
    }

    return Task.FromResult<IEnumerable<ValidationError>>(errors);
  }
}

public class CreateUserHandler : IRequestHandler<CreateUserCommand, bool>
{
  public Task<bool> HandleAsync(CreateUserCommand request, CancellationToken cancellationToken)
  {
    // Here you would typically save the user to a database
    return Task.FromResult(true);
  }
}

