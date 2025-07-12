namespace FastPatterns.Mediator.Core;

/// <summary>
/// Represents a request that returns a response of type <typeparamref name="TResponse"/>.
/// </summary>
/// <typeparam name="TResponse">The type of the response.</typeparam>
public interface IRequest<TResponse> { }

/// <summary>
/// Marker interface for requests that do not return a value.
/// </summary>
public interface IRequest : IRequest<Unit> { }


