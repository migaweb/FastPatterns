using FastPatterns.Mediator.Core;
using MediatorClass = FastPatterns.Mediator.Infrastructure.Mediator;
using Microsoft.Extensions.DependencyInjection;

namespace FastPatterns.Mediator.UnitTests.Infrastructure;

[TestClass]
public class MediatorTests
{
    private class TestRequest(string value) : IRequest<string>
    {
        public string Value { get; } = value;
    }

    private class TestHandler(List<string> log) : IRequestHandler<TestRequest, string>
    {
        public Task<string> HandleAsync(TestRequest request, CancellationToken cancellationToken)
        {
            log.Add("handler");
            return Task.FromResult(request.Value);
        }
    }

    private class TestNotification(string message) : INotification
    {
        public string Message { get; } = message;
    }

    private class RecordingNotificationHandler : INotificationHandler<TestNotification>
    {
        public bool Handled { get; private set; }
        public Task HandleAsync(TestNotification notification, CancellationToken cancellationToken)
        {
            Handled = true;
            return Task.CompletedTask;
        }
    }

    private class Pipeline1(List<string> log) : IPipelineBehavior<TestRequest, string>
    {
        public async Task<string> HandleAsync(TestRequest request, RequestHandlerDelegate<string> next, CancellationToken cancellationToken)
        {
            log.Add("before1");
            var result = await next();
            log.Add("after1");
            return result + "1";
        }
    }

    private class Pipeline2(List<string> log) : IPipelineBehavior<TestRequest, string>
    {
        public async Task<string> HandleAsync(TestRequest request, RequestHandlerDelegate<string> next, CancellationToken cancellationToken)
        {
            log.Add("before2");
            var result = await next();
            log.Add("after2");
            return result + "2";
        }
    }

    [TestMethod]
    public async Task SendAsync_Returns_Handler_Response()
    {
        var services = new ServiceCollection();
        var log = new List<string>();
        services.AddSingleton(log);
        services.AddTransient<IRequestHandler<TestRequest, string>, TestHandler>();
        var provider = services.BuildServiceProvider();
        var mediator = new MediatorClass(provider);

        var result = await mediator.SendAsync(new TestRequest("test"));

        Assert.AreEqual("test", result);
        CollectionAssert.AreEqual(new[] { "handler" }, log);
    }

    [TestMethod]
    public async Task PublishAsync_Invokes_All_Handlers()
    {
        var services = new ServiceCollection();
        var handler1 = new RecordingNotificationHandler();
        var handler2 = new RecordingNotificationHandler();
        services.AddSingleton<INotificationHandler<TestNotification>>(handler1);
        services.AddSingleton<INotificationHandler<TestNotification>>(handler2);
        var mediator = new MediatorClass(services.BuildServiceProvider());

        await mediator.PublishAsync(new TestNotification("hello"));

        Assert.IsTrue(handler1.Handled);
        Assert.IsTrue(handler2.Handled);
    }

    [TestMethod]
    public async Task SendAsync_Executes_Pipeline_In_Registration_Order()
    {
        var services = new ServiceCollection();
        var log = new List<string>();
        services.AddSingleton(log);
        services.AddTransient<IRequestHandler<TestRequest, string>, TestHandler>();
        services.AddTransient<IPipelineBehavior<TestRequest, string>, Pipeline1>();
        services.AddTransient<IPipelineBehavior<TestRequest, string>, Pipeline2>();
        var mediator = new MediatorClass(services.BuildServiceProvider());

        var result = await mediator.SendAsync(new TestRequest("ping"));

        Assert.AreEqual("ping12", result);
        CollectionAssert.AreEqual(
            new[] { "before2", "before1", "handler", "after1", "after2" },
            log);
    }
}
