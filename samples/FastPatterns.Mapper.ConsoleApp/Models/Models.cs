namespace FastPatterns.Mapper.ConsoleApp.Models;
internal record Car(string Make) { }
internal record CarDto(string Make) { }

internal record Source
{
  internal int Id { get; set; } = 999;
  internal decimal Price { get; set; } = 123.45m;
  internal string Name { get; set; } = "Test Name";
  internal DateTime BestBefore { get; set; } = DateTime.Now;
  internal IEnumerable<string> Hobbies { get; set; } = ["Running", ".NET", "Gardening"];
  internal NestedSource? Nested { get; set; } = new() { Occupation = "Programmer" };
  internal Car Car { get; set; } = new("Toyota");
}

internal record Dest
{
  internal int Id { get; set; }
  internal decimal Price { get; set; }
  internal string Name { get; set; } = string.Empty;
  internal DateTime BestBefore { get; set; }
  internal IEnumerable<string> Hobbies { get; set; } = [];
  internal NestedDest? Nested { get; set; }
  internal CarDto? Car { get; set; }
  internal string NameReversed { get; set; } = string.Empty;

  public override string ToString()
  {
    return $"Id: {Id}, Price: {Price}, Name: {Name}, BestBefore: {BestBefore}, " +
           $"Hobbies: [{string.Join(", ", Hobbies)}], NameReversed: {NameReversed}, " +
           $"Nested Occupation: {Nested?.Occupation}, Nested Car: {Car?.Make}";
  }
}

internal record NestedSource()
{
  internal string? Occupation { get; set; }
}

internal record NestedDest
{
  internal string? Occupation { get; set; }
}
