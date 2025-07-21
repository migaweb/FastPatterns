using FastPatterns.Mediator.Core;
using FastPatterns.Mediator.Core.Validation;
using FastPatterns.Mediator.PipelineBehavior;

namespace FastPatterns.Mediator.UnitTests.PipelineBehavior;

[TestClass]
public class ValidationBehaviorTests
{
    private record TestRequest(string Value) : IRequest<string>;

    private class FakeValidator(IEnumerable<ValidationError> errors) : IValidator<TestRequest>
    {
        private readonly IEnumerable<ValidationError> _errors = errors;
        public Task<IEnumerable<ValidationError>> ValidateAsync(TestRequest request, CancellationToken cancellationToken)
            => Task.FromResult(_errors);
    }

    [TestMethod]
    public async Task HandleAsync_NoValidators_Calls_Next()
    {
        var behavior = new ValidationBehavior<TestRequest, string>([]);
        bool called = false;
        RequestHandlerDelegate<string> next = () => { called = true; return Task.FromResult("ok"); };

        var result = await behavior.HandleAsync(new TestRequest("x"), next, CancellationToken.None);

        Assert.IsTrue(called);
        Assert.AreEqual("ok", result);
    }

    [TestMethod]
    public async Task HandleAsync_Validators_NoErrors_Returns_Result()
    {
        var validators = new[]
        {
            new FakeValidator([]),
            new FakeValidator([])
        };
        var behavior = new ValidationBehavior<TestRequest, string>(validators);
        bool called = false;
        RequestHandlerDelegate<string> next = () => { called = true; return Task.FromResult("valid"); };

        var result = await behavior.HandleAsync(new TestRequest("y"), next, CancellationToken.None);

        Assert.IsTrue(called);
        Assert.AreEqual("valid", result);
    }

    [TestMethod]
    public async Task HandleAsync_Validators_WithErrors_Throws_Exception()
    {
        var error1 = new ValidationError("Prop1", "msg1");
        var error2 = new ValidationError("Prop2", "msg2");
        var validators = new[]
        {
            new FakeValidator([error1]),
            new FakeValidator([error2])
        };
        var behavior = new ValidationBehavior<TestRequest, string>(validators);
        bool called = false;
        RequestHandlerDelegate<string> next = () => { called = true; return Task.FromResult("fail"); };

        var ex = await Assert.ThrowsExceptionAsync<ValidationException>(async () =>
            await behavior.HandleAsync(new TestRequest("z"), next, CancellationToken.None));

        Assert.IsFalse(called);
        CollectionAssert.AreEqual(new[] { error1, error2 }, ex.Errors.ToArray());
    }
}
