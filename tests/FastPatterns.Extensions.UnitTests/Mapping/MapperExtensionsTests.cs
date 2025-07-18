using FastPatterns.Extensions.Mapping;

namespace FastPatterns.Extensions.UnitTests.Mapping;

[TestClass]
public sealed class MapperExtensionsTests
{
    private class Source
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }

    private class Target
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Extra { get; set; }
    }

    [TestMethod]
    public void MapWith_Maps_Matching_Properties()
    {
        var src = new Source { Id = 7, Name = "Joe" };

        var result = MapperExtensions.MapWith<Source, Target>(src);

        Assert.AreEqual(src.Id, result.Id);
        Assert.AreEqual(src.Name, result.Name);
        Assert.IsNull(result.Extra);
    }

    [TestMethod]
    public void MapWith_Invokes_Custom_Mapping()
    {
        var src = new Source { Id = 2, Name = "Bob" };

        var result = MapperExtensions.MapWith<Source, Target>(src, (s, d) => d.Extra = $"{s.Name}-{s.Id}");

        Assert.AreEqual("Bob-2", result.Extra);
    }

    [TestMethod]
    public void MapWith_Null_Source_Still_Invokes_Custom_Mapping()
    {
        Source? src = null;
        bool called = false;

        var result = MapperExtensions.MapWith<Source?, Target>(src, (s, d) =>
        {
            called = true;
            d.Extra = "set";
        });

        Assert.IsTrue(called);
        Assert.AreEqual("set", result.Extra);
    }
}
