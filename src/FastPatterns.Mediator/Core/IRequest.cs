namespace FastPatterns.Mediator.Core;

public interface IRequest<TResponse> { }

public interface IRequest : IRequest<Unit> { }


