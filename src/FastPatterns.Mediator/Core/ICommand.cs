namespace FastPatterns.Mediator.Core;
/// <summary>
/// Represents a command that results in a response of type <typeparamref name="TResponse"/>.
/// </summary>
/// <typeparam name="TResponse">The type returned by the command handler.</typeparam>
public interface ICommand<TResponse> : IRequest<TResponse> { }
