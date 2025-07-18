
using FastPatterns.Core.Diagnostics;
using FastPatterns.Extensions.Mapping;
using FastPatterns.Mapper.ConsoleApp.Extensions;
using FastPatterns.Mapper.ConsoleApp.Models;

bool printProperties = true;

foreach (var i in Enumerable.Range(1, 10))
{
  using var _ = new ScopedStopwatch(elapsed =>
         Console.WriteLine($"Elapsed: {elapsed.TotalMilliseconds} ms"));

  var source = new Source();

  var dest = source.MapWith<Source, Dest>((src, dest) =>
  {
    dest.NameReversed = src.Name.ReverseString();
    dest.Nested = SimpleMapper<NestedSource, NestedDest>.Map(src.Nested);
    dest.Car = RecordMapper.MapToRecord<Car, CarDto>(source.Car);
  });

  if (printProperties)
  {
    Console.WriteLine($"Dest: {dest}");
  }

  Console.WriteLine("Mapping completed successfully.");
}


