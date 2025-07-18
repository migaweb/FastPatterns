using FastPatterns.Extensions.Mapping;

namespace FastPatterns.Extensions.UnitTests.Mapping;

[TestClass]
public sealed class RecordMapperTests
{
    private class Source
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }

    private record Target(int Id, string? Name);

    [TestMethod]
    public void MapToRecord_Copies_Matching_Properties()
    {
        var src = new Source { Id = 1, Name = "Jane" };

        var result = RecordMapper.MapToRecord<Source, Target>(src);

        Assert.AreEqual(src.Id, result.Id);
        Assert.AreEqual(src.Name, result.Name);
    }

    private record TargetWithDefault(int Id, string? Name, int Age);

    [TestMethod]
    public void MapToRecord_Missing_Source_Property_Uses_Default()
    {
        var src = new Source { Id = 5, Name = "Foo" };

        var result = RecordMapper.MapToRecord<Source, TargetWithDefault>(src);

        Assert.AreEqual(src.Id, result.Id);
        Assert.AreEqual(src.Name, result.Name);
        Assert.AreEqual(0, result.Age);
    }

    private record TargetOverride(int Id, string? Name);

    [TestMethod]
    public void MapToRecord_Custom_Overrides_Take_Priority()
    {
        var src = new Source { Id = 7, Name = "Bar" };

        var result = RecordMapper.MapToRecord<Source, TargetOverride>(src, s => new()
        {
            ["Id"] = 42
        });

        Assert.AreEqual(42, result.Id);
        Assert.AreEqual(src.Name, result.Name);
    }

    private class SourceLower
    {
        public int id { get; set; }
    }

    private record TargetCase(int Id);

    [TestMethod]
    public void MapToRecord_Is_Case_Insensitive_For_Source_Property_Names()
    {
        var src = new SourceLower { id = 9 };

        var result = RecordMapper.MapToRecord<SourceLower, TargetCase>(src);

        Assert.AreEqual(9, result.Id);
    }
}
