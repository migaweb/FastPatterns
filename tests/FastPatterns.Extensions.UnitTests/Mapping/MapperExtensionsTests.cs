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

    [TestMethod]
    public void MapWith_Collection_Maps_All_Items()
    {
        var src = new[]
        {
            new Source { Id = 1, Name = "A" },
            new Source { Id = 2, Name = "B" }
        };

        var result = MapperExtensions.MapWith<Source, Target>(src);

        Assert.AreEqual(2, result.Count);
        Assert.AreEqual(src[0].Id, result[0].Id);
        Assert.AreEqual(src[1].Name, result[1].Name);
    }

    [TestMethod]
    public void MapWith_Collection_Invokes_Custom_Mapping()
    {
        var src = new[] { new Source { Id = 3, Name = "C" } };

        var result = MapperExtensions.MapWith<Source, Target>(src, (s, d) => d.Extra = $"{s.Name}-{s.Id}");

        Assert.AreEqual("C-3", result[0].Extra);
    }

    [TestMethod]
    public void MapLazyWith_Collection_Returns_Mapped_Items_Lazily()
    {
        var src = new[]
        {
            new Source { Id = 4, Name = "D" },
            new Source { Id = 5, Name = "E" }
        };

        var result = MapperExtensions.MapLazyWith<Source, Target>(src, (s, d) => d.Extra = s.Name).ToList();

        Assert.AreEqual(2, result.Count);
        Assert.AreEqual("D", result[0].Extra);
        Assert.AreEqual("E", result[1].Extra);
    }
}
