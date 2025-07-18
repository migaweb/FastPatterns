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
        var result = SimpleMapper<Source, Target>.Map(null);

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
}
