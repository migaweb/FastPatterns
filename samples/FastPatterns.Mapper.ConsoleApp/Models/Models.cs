namespace FastPatterns.Mapper.ConsoleApp.Models;
public record Car(string Make) { }
public record CarDto(string Make) { }

public class Source
{
  public int Id { get; set; } = 999;
  public decimal Price { get; set; } = 123.45m;
  public string Name { get; set; } = "Test Name";
  public DateTime BestBefore { get; set; } = DateTime.Now;
  public IEnumerable<string> Hobbies { get; set; } = ["Running", ".NET", "Gardening"];
  public NestedSource? Nested { get; set; } = new() { Occupation = "Programmer" };
  public Car Car { get; set; } = new("Toyota");
}

public class Dest
{
  public int Id { get; set; }
  public decimal Price { get; set; }
  public string Name { get; set; } = string.Empty;
  public DateTime BestBefore { get; set; }
  public IEnumerable<string> Hobbies { get; set; } = [];
  public NestedDest? Nested { get; set; }
  public CarDto? Car { get; set; }
  public string NameReversed { get; set; } = string.Empty;

  public override string ToString()
  {
    return $"Id: {Id}, Price: {Price}, Name: {Name}, BestBefore: {BestBefore}, " +
           $"Hobbies: [{string.Join(", ", Hobbies)}], NameReversed: {NameReversed}, " +
           $"Nested Occupation: {Nested?.Occupation}, Nested Car: {Car?.Make}";
  }
}

public class NestedSource()
{
  public string? Occupation { get; set; }
}

public class NestedDest
{
  public string? Occupation { get; set; }
}
