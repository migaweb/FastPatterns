
using FastPatterns.Core.Diagnostics;
using FastPatterns.Extensions.Mapping;
using FastPatterns.Mapper.ConsoleApp.Extensions;
using FastPatterns.Mapper.ConsoleApp.Models;

bool printProperties = true;

List<Source> sourceItems = [];
sourceItems.AddRange(Enumerable.Range(1, 10).Select(i => new Source()));

using var _ = new ScopedStopwatch(elapsed =>
         Console.WriteLine($"Elapsed: {elapsed.TotalMilliseconds} ms"));

var destCollection = sourceItems.MapWith<Source, Dest>((src, dest) =>
{
  dest.NameReversed = src.Name.ReverseString();
  dest.Nested = SimpleMapper<NestedSource, NestedDest>.Map(src.Nested);
  dest.Car = RecordMapper.MapToRecord<Car, CarDto>(src.Car);
});

if (printProperties)
{
  foreach (var item in destCollection)
  {
    Console.WriteLine(item);
  }
}

Console.WriteLine("Mapping completed successfully.");




