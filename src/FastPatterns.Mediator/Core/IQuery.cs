namespace FastPatterns.Mediator.Core;
/// <summary>
/// Represents a query that returns data of type <typeparamref name="TResponse"/>.
/// </summary>
/// <typeparam name="TResponse">The response type.</typeparam>
public interface IQuery<TResponse> : IRequest<TResponse> { }
