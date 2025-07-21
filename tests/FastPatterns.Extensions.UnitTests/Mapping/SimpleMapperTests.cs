using FastPatterns.Extensions.Mapping;

namespace FastPatterns.Extensions.UnitTests.Mapping;

[TestClass]
public sealed class SimpleMapperTests
{
    private class Source
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Age { get; set; }
    }

    private class Target
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public long Age { get; set; } // different type should not be mapped
        public int ReadOnlyProp { get; private set; }
    }

    [TestMethod]
    public void Map_NullSource_Returns_Default_Target()
    {
        var result = SimpleMapper<Source, Target>.Map((Source?)null);

        Assert.IsNotNull(result);
        Assert.AreEqual(0, result.Id);
        Assert.IsNull(result.Name);
        Assert.AreEqual(0L, result.Age);
    }

    [TestMethod]
    public void Map_Maps_Matching_Properties()
    {
        var src = new Source { Id = 3, Name = "Alice", Age = 30 };

        var result = SimpleMapper<Source, Target>.Map(src);

        Assert.AreEqual(src.Id, result.Id);
        Assert.AreEqual(src.Name, result.Name);
    }

    [TestMethod]
    public void Map_Ignores_NonMatching_And_ReadOnly_Properties()
    {
        var src = new Source { Id = 5, Name = "Bob", Age = 42 };

        var result = SimpleMapper<Source, Target>.Map(src);

        // Age type differs so should remain default
        Assert.AreEqual(0L, result.Age);
        // ReadOnlyProp cannot be set
        Assert.AreEqual(0, result.ReadOnlyProp);
    }

    [TestMethod]
    public void Map_Collection_Returns_All_Mapped_Items()
    {
        var src = new[]
        {
            new Source { Id = 1, Name = "A", Age = 10 },
            new Source { Id = 2, Name = "B", Age = 20 }
        };

        var result = SimpleMapper<Source, Target>.Map((IEnumerable<Source>)src);

        Assert.AreEqual(2, result.Count);
        Assert.AreEqual(src[0].Id, result[0].Id);
        Assert.AreEqual(src[0].Name, result[0].Name);
        Assert.AreEqual(0L, result[0].Age);
        Assert.AreEqual(src[1].Id, result[1].Id);
        Assert.AreEqual(src[1].Name, result[1].Name);
    }

    [TestMethod]
    public void Map_Null_Collection_Returns_Empty_List()
    {
        var result = SimpleMapper<Source, Target>.Map((IEnumerable<Source>)null!);

        Assert.IsNotNull(result);
        Assert.AreEqual(0, result.Count);
    }

    [TestMethod]
    public void MapLazy_Collection_Returns_Mapped_Sequence()
    {
        var src = new[]
        {
            new Source { Id = 3, Name = "C", Age = 10 },
            new Source { Id = 4, Name = "D", Age = 20 }
        };

        var result = SimpleMapper<Source, Target>.MapLazy((IEnumerable<Source>)src).ToList();

        Assert.AreEqual(2, result.Count);
        Assert.AreEqual(src[0].Id, result[0].Id);
        Assert.AreEqual(src[1].Id, result[1].Id);
    }

    [TestMethod]
    public void MapLazy_Null_Collection_Returns_Empty_Sequence()
    {
        var result = SimpleMapper<Source, Target>.MapLazy((IEnumerable<Source>)null!).ToList();

        Assert.IsNotNull(result);
        Assert.AreEqual(0, result.Count);
    }
}
